. ".\common.ps1"

# Rebuild solution
Set-Location $rootFolder
& dotnet restore

# Create all packages
foreach($project in $projects) {
    
    $projectFolder = Join-Path $rootFolder $project

    # Create nuget pack
    Set-Location $projectFolder
    Remove-Item -Recurse (Join-Path $projectFolder "bin/Release")
    & dotnet build -c Release
    & dotnet pack -c Release

    if (-Not $?) {
        Write-Host ("Packaging failed for the project: " + $projectFolder)
        exit $LASTEXITCODE
    }
    
    # Copy nuget package
    $projectName = $project.Substring($project.LastIndexOf("/") + 1)
    $projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $projectName + ".*.nupkg")
    Move-Item $projectPackPath $packFolder

}

# Go back to the pack folder
Set-Location $packFolder