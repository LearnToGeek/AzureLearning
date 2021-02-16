# Create new repository
az acr create --resource-group psdemo-rg --name geek123 --sku Basic

# List all repository
az acr list
az acr list -o table

# Get login server for a repository
az acr show --name geek123 --resource-group psdemo-rg --query loginServer

# Login into repository (Use AzureAD for cli mode login)
az acr login --name geek123

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

# List all images in the respository
az acr repository list --name geek123 --output table

# List all tags for particular images in the repository
az acr repository show-tags --name geek123 --repository initialdemo -o table
