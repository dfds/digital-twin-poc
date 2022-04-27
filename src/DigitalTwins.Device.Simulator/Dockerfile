FROM mcr.microsoft.com/azure-cli

ARG AZ_SERVICEPRINCIPAL_TENANT=73a99466-ad05-4221-9f90-e7142aa2f6c1
ARG AZ_SERVICEPRINCIPAL_ID=f5c3f7e2-a642-45b8-903e-35ecdaec671b
ARG AZ_SERVICEPRINCIPAL_PASSWORD

ENV AZ_IOT_DEVICE_ID=
ENV AZ_IOT_HUB_NAME=

RUN az extension add --name azure-iot
RUN az login --service-principal -u ${AZ_SERVICEPRINCIPAL_ID} -p ${AZ_SERVICEPRINCIPAL_PASSWORD} --tenant ${AZ_SERVICEPRINCIPAL_TENANT}

COPY ./src/DigitalTwins.Device.Simulator/start.sh ./

CMD ./start.sh