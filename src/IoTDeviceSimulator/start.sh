az iot hub device-identity create --device-id $AZ_IOT_DEVICE_ID --hub-name $AZ_IOT_HUB 2>nul

az iot device simulate -d $AZ_IOT_DEVICE_ID -n $AZ_IOT_HUB