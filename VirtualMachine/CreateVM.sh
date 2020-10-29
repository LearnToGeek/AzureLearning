#!/bin/bash


# Global Variable
rgName=cmd-new-RG
region=canadacentral
vnet=my-vnet-demo

#1 - Create Resource Group
az group create \
    --name $rgName \
    --location $region

#2 - Show list of RGs
#az group list -o table

#3 - Delete RG
# az group delete --name "cmd-new-RG"

#4  - Create virtual network and subnet
az network vnet create \
    --resource-group=$rgName \
    --name $vnet \
    --address-prefix 
