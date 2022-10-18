<# Build from a PowerShell command line with a command like the following (make sure the folder "c:\nuget\local" exists):

./build.ps1 -v "2.1.1" -s "beta"

#>

<#
SYNOPSIS
    Builds Formulate packages.
#>
param (
    [Parameter(Mandatory)]
    [string]
    [Alias("v")]  $version, #version to build

    [Parameter()]
    [string]
    [Alias("s")]
    $suffix, #optional suffix to append to version (for pre-releases)

    [Parameter()]
    [string]
    [Alias("e")]
    $env = 'release', #build environment to use when packing

    [Parameter()]
    [string]
    [Alias("l")]
    $localNuGetPath = 'C:\nuget\local' #local nuget feed location
)

if ($version.IndexOf('-') -ne -1) {
    Write-Host "Version shouldn't contain a - (remember version and suffix are seperate)"
    exit
}

$fullVersion = $version;

if (![string]::IsNullOrWhiteSpace($suffix)) {
   $fullVersion = -join($version, '-', $suffix)
}

$majorFolder = $version.Substring(0, $version.LastIndexOf('.'))

$outFolder = "..\dist\$majorFolder\$version\$fullVersion"
if (![string]::IsNullOrWhiteSpace($suffix)) {
    $suffixFolder = $suffix;
    if ($suffix.IndexOf('.') -ne -1) {
        $suffixFolder = $suffix.substring(0, $suffix.indexOf('.'))
    }
    $outFolder = "..\dist\$majorFolder\$version\$version-$suffixFolder\$fullVersion"
}

"----------------------------------"
Write-Host "Version     :" $fullVersion
Write-Host "Config      :" $env
Write-Host "Folder      :" $outFolder
Write-Host "Local NuGet :" $localNuGetPath
"----------------------------------";

dotnet restore ..\src

##### Build support packages for entry point package
dotnet pack ..\src\Formulate.Core\Formulate.Core.csproj --no-restore -c $env -o $outFolder /p:ContinuousIntegrationBuild=true,version=$fullVersion
dotnet pack ..\src\Formulate.BackOffice\Formulate.BackOffice.csproj --no-restore -c $env -o $outFolder /p:ContinuousIntegrationBuild=true,version=$fullversion
dotnet pack ..\src\Formulate.Web\Formulate.Web.csproj --no-restore -c $env -o $outFolder /p:ContinuousIntegrationBuild=true,version=$fullversion
dotnet pack ..\src\Formulate.BackOffice.StaticAssets\Formulate.BackOffice.StaticAssets.csproj --no-restore -c $env -o $outFolder /p:ContinuousIntegrationBuild=true,version=$fullVersion 

##### Build extensions
dotnet pack ..\src\Formulate.Extensions.PlainJavaScriptTemplate\Formulate.Extensions.PlainJavaScriptTemplate.csproj --no-restore -c $env -o $outFolder /p:ContinuousIntegrationBuild=true,version=$fullversion
dotnet pack ..\src\Formulate.Extensions.StoreData\Formulate.Extensions.StoreData.csproj --no-restore -c $env -o $outFolder /p:ContinuousIntegrationBuild=true,version=$fullversion

##### Build entry point package
dotnet pack ..\src\Formulate\Formulate.csproj --no-restore -c $env -o $outFolder /p:ContinuousIntegrationBuild=true,version=$fullVersion

##### Copying to Local Deploy
XCOPY "$outFolder\*.nupkg" $localNuGetPath /Q /Y 

Write-Host "Formulate Packaged : $fullVersion"
