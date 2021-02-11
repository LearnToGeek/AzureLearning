#Prefix for resources
$prefix = "cmk"

#Basic variables
$location = "canadacentral"
$id = Get-Random -Minimum 1000 -Maximum 9999
$ResourceGroupName = "$prefix-windows-vm-$id"
$VMName = "$prefix-win-vm-$id"


$WinVMParameters = @{
    adminUsername = "winadmin"
    adminPassword = 'Geek@217@Mac'
    dnsName = "$prefix$id"
    vmName = $VMName
}

$WinVMDeploymentParameters = @{
    Name = "WindowsVMDeploy"
    ResourceGroupName = $ResourceGroupName
    TemplateFile = ".\KeyVault\WindowsVM.json"
    TemplateParameterObject = $WinVMParameters
    Mode = "Incremental"
}

#Create the resource group for the VM
$vmRG = New-AzResourceGroup -Name $ResourceGroupName -Location $Location

#Deploy the #Windows VM template
New-AzResourceGroupDeployment @WinVMDeploymentParameters

#Now provision a Key Vault if you don't already have one

#Create a new Key Vault
$keyVaultParameters = @{
    Name = "$prefix-key-vault-$id"
    ResourceGroupName = $vmRG.ResourceGroupName
    Location = $location
    EnabledForDiskEncryption = $true
    EnabledForDeployment = $true
    Sku = "Standard"
}
$keyVault = New-AzKeyVault @keyVaultParameters

#Create a Key Encrypting Key
$kekParameters = @{
    VaultName = $keyVault.VaultName
    Name = "$prefix-kek-$id"
    Destination = "Software"

}

$kek = Add-AzKeyVaultKey @kekParameters

#Set the disk encryption settings for the Windows VM
$DiskEncryptionParameters = @{
    ResourceGroupName = $vmRG.ResourceGroupName
    VMname = $VMName
    DiskEncryptionKeyVaultUrl = $keyVault.VaultUri
    DiskEncryptionKeyVaultId = $keyVault.ResourceId
    KeyEncryptionKeyUrl = $kek.Key.Kid
    KeyEncryptionKeyVaultId = $keyVault.ResourceId
    VolumeType = "All"
}

Set-AzVMDiskEncryptionExtension @DiskEncryptionParameters


#Check the encryption settings once the command completes
Get-AzVmDiskEncryptionStatus -ResourceGroupName $vmRG.ResourceGroupName -VMName $VMName