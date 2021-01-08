$rg = '08-multiple-resource-demo'

New-AzResourceGroup `
    -Name $rg `
    -Location "Canada Central" `
    -Tag @{Owner="Ravi";Project="Learning"}

$timestamp=get-date -f MM-dd-yyyy_HH_mm_ss
$deploymentName="deployment_"+"$timestamp"
$templateFile = ".\08-multiple-resource-demo.json"
$parameterFile = ".\08-multiple-resource-demo.parameters.json"

#With WhatIF option to validate template
New-AzResourceGroupDeployment `
    -Name $deploymentName `
    -ResourceGroupName $rg `
    -WhatIf `
    -TemplateFile $templateFile `
    -TemplateParameterFile $parameterFile

#Deploy template
New-AzResourceGroupDeployment `
    -Name $deploymentName `
    -ResourceGroupName $rg `
    -TemplateFile $templateFile `
    -TemplateParameterFile $parameterFile

