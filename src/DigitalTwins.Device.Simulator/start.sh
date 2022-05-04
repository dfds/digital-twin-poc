#!/bin/bash
az iot hub device-identity create --device-id $AZ_IOT_DEVICE_ID --hub-name $AZ_IOT_HUB_NAME 2>nul

methodResponsePayload=$(cat <<EOF
{
  "result": "Direct method successful."
}
EOF
)

az iot device simulate -d $AZ_IOT_DEVICE_ID -n $AZ_IOT_HUB_NAME --method-response-code 201 --method-response-payload $methodResponsePayload