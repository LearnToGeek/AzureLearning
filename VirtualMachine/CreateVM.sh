#!/bin/bash


# Global Variable
rgName=web-public-RG
region=canadacentral
vnet=web-public-vnet
vmimage=rhel

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
    --address-prefix "10.1.0.0/16" \
    --subnet-name "web-subnet" \
    --subnet-prefix "10.1.1.0/24"

#5 - Create public IP address
az network public-ip create \
    --resource-group $rgName \
    --name "web-public-ip"

#6 - Create network security group
az network nsg create \
    --resource-group $rgName \
    --name "web-public-nsg"


#7 - Create NIC and associate it with public IP web-public-ip
az network nic create \
    --resource-group $rgName \
    --name "web-public-nic" \
    --vnet-name $vnet \
    --subnet "web-subnet" \
    --network-security-group "web-public-nsg" \
    --public-ip-address "web-public-ip"

# az vm create --help | more

#8 - Create virtual machine
az vm create \
    --resource-group $rgName \
    --location $region \
    --name "web-app-vm" \
    --nics "web-public-nic" \
    --image $vmimage \
    --admin-username "demoadmin" \
    --authentication-type "ssh" \
    --generate-ssh-keys

#9 - open port 22 
az vm open-port \
    --resource-group "web-public-RG" \
    --name "web-app-vm" \
    --port "22"

#10 - Grab the public IP of the virtual machine
az vm list-ip-addresses --name "web-app-vm" --output table
