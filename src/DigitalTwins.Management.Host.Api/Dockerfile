#Setup build image
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env

# Build arg to hold ADO PAT for restoring private feed packages
ARG PAT

#Copy files to container file system
COPY ./ ./build/

#Set workdir to /build
WORKDIR /build

# Install the nuget credential provider
RUN apt-get update && apt-get install -y locales && sed -i -e 's/# en_US.UTF-8 UTF-8/en_US.UTF-8 UTF-8/' /etc/locale.gen
RUN dpkg-reconfigure --frontend=noninteractive locales && update-locale LANG=en_US.UTF-8
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash

# set the credential provider variable and run restore.
RUN sed -i "s|</configuration>|<packageSourceCredentials><dfds-devex-shared><add key=\"Username\" value=\"PAT\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /></dfds-devex-shared></packageSourceCredentials></configuration>|" nuget.config

#Set workdir to current project folder
WORKDIR /build/src/DigitalTwins.Management.Host.Api

#Restore csproj packages.
RUN dotnet restore

#Compile source code using standard Release profile
RUN dotnet publish -c Release -o /build/out DigitalTwins.Management.Host.Api.csproj

#Generate cert for https
#RUN dotnet dev-certs https --export-path /build/out/DigitalTwins.Management.Host.Api.pfx --password $PAT
#RUN dotnet dev-certs https --trust

#Setup final container images.
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

EXPOSE 5000
#EXPOSE 5001

ENV ASPNETCORE_URLS="http://localhost:5000"

#Copy binaries from publish container to final container
COPY --from=build-env /build/out .

#Run dotnet executable
ENTRYPOINT ["dotnet", "DigitalTwins.Management.Host.Api.dll"]