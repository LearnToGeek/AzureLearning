# Global variables
$prefix = "cmk"
$location = "canadacentral"
$id = Get-Random -Minimum 1000 -Maximum 9999
$rg = "$prefix-rg-$id"
$kvName = "$prefix-kv-$id"

# Create Resource Group
New-AzResourceGroup -Name $rg -Location $location

# Create KeyVault
$kvParams = @{
    VaultName = $kvName
    ResourceGroupName = $rg
    location = $location
    Sku = "Standard"
}
$keyVault = New-AzKeyVault @kvParams

# Create Storage Account
$stroageaccountParams = @{
    ResourceGroupName = $rg
    Name = "$($prefix)logs$id"
    Type="Standard_LRS"
    Location = $location
}
$sa = New-AzStorageAccount @stroageaccountParams


# Create workspace for KeyVault Analytics
$la = New-AzOperationalInsightsWorkspace -ResourceGroupName $rg -Name "$($prefix)kvlog$($id)" -Location $location -Sku "Standard"

