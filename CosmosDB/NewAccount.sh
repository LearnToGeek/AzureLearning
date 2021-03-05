# Create Resource Group
az group create --name cosmosdemo --location canadacentral

# Create CosmosDB Account
az cosmosdb create --resource-group "cosmosdemo" --name "learn-to-geek"

# Create CosmosDB Database with particular api
az cosmosdb sql database create -g "cosmosdemo" --account-name "learn-to-geek" -n "retail-store"

# Create CosmosDB container within database which is same as table in relational database
az cosmosdb sql container create -g "cosmosdemo" --account-name "learn-to-geek" --database-name "retail-store" -n "products"
