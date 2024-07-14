$l = 'westus3'
$rg = 'af4dev'
$sa = 'af4devcoursestorage'
$fa = 'af4devcourse'

az group create --name $rg --location $l

az storage account create --name $sa --resource-group $rg

az functionapp create --consumption-plan-location $l --name $fa --os-type Linux --resource-group $rg --functions-version 4 --runtime dotnet-isolated --runtime-version 8 --storage-account $sa
