$prefix = "cmk"

$location = "canadacentral"
$id= Get-Random -Minimum 1000 -Maximum 9999
$rg = "$prefix-rg-$id"
$kvName = "$prefix-keyvault-$id"

New-AzResourceGroup -Name $rg -Location $location


$kvParameters = @{
    Name = $kvName
    ResourceGroupName = $rg
    Location = $location
    Sku = "Standard"
}

$keyVault = New-AzKeyVault @kvParameters

$objectId = $(Get-AzADUser -Mail "<SignIn Username>").Id

$accessParams = @{
    VaultName = $kvName
    ResourceGroupName = $rg
    PermissionsToKeys = @("get")
    PermissionsToSecrets = @("get","list")
    ObjectId = $objectId
}

Set-AzKeyVaultAccessPolicy @accessParams


