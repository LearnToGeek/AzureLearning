$rg = "psdemo-rg"
$location = "canadacentral"
$regname = "geekyregpshell"

#New-AzResourceGroup -Name $rg -Location $location

$regparam = @{
    ResourceGroupName = $rg
    Name = $regname
    Sku = "Basic"
}

$registry = New-AzContainerRegistry @regparam

# With inline parameter
# $registry = New-AzContainerRegistry -ResourceGroupName $rg `
#                                       -Name $regname `
#                                       -Sku "Basic" `
#                                       -Location $location `
#                                       -EnableAdminUser

# Get AdminUser credential for the registry
$creds = Get-AzContainerRegistryCredential -Registry $registry

# Login into registry with AdminUser
$creds.Password | docker login $repository.LoginServer -u $creds.Username --password-stdin

# Pull demo startup image from microsoft repository
docker pull mcr.microsoft.com/hello-world

# Get all local docker images
docker images

# Tag local image with custom tag before pushing to azure container repository
# https://docs.docker.com/engine/reference/commandline/tag/
docker tag fce289e99eb9 geek123.azurecr.io/initialdemo:v1.0

# Push to remote container repository in the azure
docker push geek123.azurecr.io/initialdemo:v1.0

# We can remove local taged image if we don't need (It doesn't remove from azure CR)
docker rmi geek123.azurecr.io/initialdemo:v1.0