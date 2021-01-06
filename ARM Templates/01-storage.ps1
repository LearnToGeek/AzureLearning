$rg = 'arm-introduction-01'

New-AzResourceGroup -Name $rg -Location canadacentral -Force

New-AzResourceGroupDeployment `
     -Name 'new-storage' `
     -ResourceGroupName $rg `
     -TemplateFile '01-storage.json' `
     -storageName 'geekystorage2'


     New-AzResourceGroupDeployment -Name 'geekydeplyment' -ResourceGroupName 'Ravi-Playground' -TemplateFile '06-orchestration-demo.json' -accountName 'geekystorage06012021'

