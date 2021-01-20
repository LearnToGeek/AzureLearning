
$rg = 'geekycopyloopRG'

New-AzResourceGroup `
    -Name $rg `
    -Location 'Canada Central' `
    -Tag @{'Owner'='Ravi';"Project"='Learning'}


$timestamp=get-date -f MM-dd-yyyy_HH_mm_ss
$deploymentName="deployment_"+ $timestamp
$templateFile = '.\09-copy-loop-demo.json'
$parameterFile = '.\09-copy-loop-demo.parameters.json'

New-AzResourceGroupDeployment `
    -Name $deploymentName `
    -ResourceGroupName $rg `
    -TemplateFile $templateFile `
    -TemplateParameterFile $parameterFile