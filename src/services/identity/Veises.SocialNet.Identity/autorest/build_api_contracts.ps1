$scriptLocation = $PSScriptRoot;

$swaggerFileUrl = "http://localhost:55162/swagger/v1/swagger.json";

$swaggerFilePath = Join-Path $scriptLocation "swagger.json";

$contractsPath = Join-Path $scriptLocation "../../Veises.SocialNet.Identity.Contracts/Client/";

$constractNamespace = "Veises.SocialNet.Identity.Contracts.Client";

Write-Host "Generation started";

if (Test-Path $swaggerFilePath)
{
	Remove-Item $swaggerFilePath -ErrorAction Stop;

	Write-Host "Swagger JSON file removed";
}

Write-Host "Downloading Swagger JSON file...";

iwr $swaggerFileUrl -o $swaggerFilePath -UseDefaultCredential -ErrorAction Stop;

Write-Host "Swagger JSON file saved";

if (Test-Path $contractsPath)
{
	Remove-Item $contractsPath -Recurse -ErrorAction Stop;

	Write-Host "Contracts folder removed";
}

Write-Host "Starting API C# client generation...";

autorest --input-file=$swaggerFilePath --csharp --output-folder=$contractsPath --namespace=$constractNamespace;

Write-Host "Generation complete";