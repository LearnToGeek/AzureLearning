$rg = 'webapp-demo'

New-AzResourceGroup -Name $rg -Location 'Canada Central'

New-AzResourceGroupDeployment -Name 'webapp-deployment' -ResourceGroupName $rg `
-TemplateFile '.\07-webapp-demo.json' `
-planName 'geekyFarm' `
-webAppName 'geekywebsite'