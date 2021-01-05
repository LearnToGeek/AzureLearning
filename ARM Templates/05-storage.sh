
# Resource Group Name
group1='Ravi-Playground'

az deployment group create -g $group1 \
--template-file 05-storage.json \
--parameters @05-storage.parameter-dev.json

az deployment group create -g $group1 \
--template-file 05-storage.json \
--parameters @05-storage.parameter-prod.json