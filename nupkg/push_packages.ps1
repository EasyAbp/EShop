. ".\common.ps1"

$keywords = ".Application.Contracts", ".Domain.Shared", ".HttpApi.Client", ".Application", ".Domain", ".EntityFrameworkCore", ".HttpApi", ".MongoDB", ".Web"

# Publish all packages
foreach($project in $projects) {
    $projectName = $project.Substring($project.LastIndexOf("/") + 1)
	$moduleName = $projectName
	foreach($keyword in $keywords) {
		$moduleName = $moduleName.Replace($keyword, "")
	}
	$moduleFolder = Join-Path (Join-Path $rootFolder "modules") $moduleName
	Write-Host $moduleFolder
	# Get the version
	[xml]$commonPropsXml = Get-Content (Join-Path $moduleFolder "common.props")
	$version = $commonPropsXml.Project.PropertyGroup.Version
    & dotnet nuget push ($projectName + "." + $version + ".nupkg") -s https://api.nuget.org/v3/index.json
}

# Go back to the pack folder
Set-Location $packFolder
