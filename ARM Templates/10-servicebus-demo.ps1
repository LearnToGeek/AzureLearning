$rg = 'GeekyARMRG'

New-AzResourceGroup `
    -Name $rg `
    -Location 'Canada Central' `
    -Tag @{'Owner'='Ravi';'Project'="Learning"}


$timestamp = get-date -f MM-dd-yyyy_HH_mm_ss
$deploymentName = "deployment_"+ $timestamp
$templateFile = '.\10-servicebus-demo.json'
$parameterFile = '.\10-servicebus-demo.parameters.json'

New-AzResourceGroupDeployment `
    -Name $deploymentName `
    -ResourceGroupName $rg `
    -TemplateFile $templateFile `
    -TemplateParameterFile $parameterFile