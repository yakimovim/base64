var target = Argument("Target", "Build");
var configuration = Argument("Configuration", "Release");

var resultDirectory = "./Result/";

Task("Restore")
	.Does(() => {
	NuGetRestore("Base64.sln");
});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() => {
	DotNetBuild("Base64.sln", settings => {
		settings.SetConfiguration(configuration);
	});
});

Task("CleanPackaged")
	.Does(() => {
	DeleteFiles(resultDirectory + "*.*");
});

Task("Package")
	.IsDependentOn("Build")
	.IsDependentOn("CleanPackaged")
	.Does(() => {
		if (!DirectoryExists(resultDirectory))
		{
			CreateDirectory(resultDirectory);
		}
		CopyFile($"./DecodeBase64/bin/{configuration}/debase64.exe", resultDirectory + "debase64.exe");
		CopyFile($"./EncodeBase64/bin/{configuration}/enbase64.exe", resultDirectory + "enbase64.exe");
});

RunTarget(target);