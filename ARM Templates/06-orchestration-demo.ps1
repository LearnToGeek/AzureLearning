$rg = 'orchestration-demo'

New-AzResourceGroup -Name $rg -Location canadacentral -Force

New-AzResourceGroupDeployment `
 -Name 'geekydeplyment' `
 -ResourceGroupName $rg `
 -TemplateFile '06-orchestration-demo.json' `
 -accountName 'geekystorage06012021'
