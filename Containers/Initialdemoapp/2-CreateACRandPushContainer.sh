

#Step 0 - Create Resource Group for our demo if it doesn't exist
az group create --name psdemo-rg --location centralus


#Step 1 - Create an Azure Container Registry
#SKUs include Basic, Standard and Premium (speed, replication, adv security features)
#https://docs.microsoft.com/en-us/azure/container-registry/container-registry-skus#sku-features-and-limits

az acr create --resource-group psdemo-rg --name "geekydemoacr" --sku Standard


#Step 2 - Log into ACR to push containers...this will use our current azure cli login context
az acr login --name "geekydemoacr"


#Step 3 - Get the loginServer which is used in the image tag
az acr show --name "geekydemoacr" --query loginServer --output tsv


#Step 4 - Tag the container image using the login server name. This doesn't push it to ACR, that's the next step.
#[loginUrl]/[repository:][tag]
docker tag webappimage:v1 geekydemoacr.azurecr.io/webappimage:v1
docker image ls geekydemoacr.azurecr.io/webappimage:v1
docker image ls


#Step 5 - Push image to Azure Container Registry
docker push geekydemoacr.azurecr.io/webappimage:v1


#Step 6 - Get a listing of the repositories and images/tags in our Azure Container Registry
az acr repository list --name "geekydemoacr" --output table
az acr repository show-tags --name "geekydemoacr" --repository webappimage --output table


####
#We don't have to build locally then push, we can build in ACR with Tasks.
####

#Step 1 - use ACR build to build our image in azure and then push that into ACR
az acr build --image "webappimage:v1-acr-task" --registry "geekydemoacr" .


#Both images are in there now, the one we built locally and the one build with ACR tasks
az acr repository show-tags --name "geekydemoacr" --repository webappimage --output table
