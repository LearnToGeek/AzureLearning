$username = "geeky123"
$password = ConvertTo-SecureString 'Geek@123456' -AsPlainText -Force
$winCredential = New-Object System.Management.Automation.PSCredential ($username,$password)
$rg = "vm-demo"
$location = "canadacentral"

$newrg = New-AzResourceGroup -Name $rg -location $location


$newvm = New-AzVM -ResourceGroupName $rg `
                  -Name "win-ps-demo" `
                  -Image "win2019datacenter" `
                  -Credential $winCredential `
                  -OpenPorts 3389


$ipAddress = Get-AzPublicIpAddress -ResourceGroupName $rg `
                                   -Name "win-ps-demo" `
                                   | Select-Object IpAddress

$ipAddress
