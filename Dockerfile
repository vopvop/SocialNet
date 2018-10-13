FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 443/tcp

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/*.xml ./
COPY src/common/ common/
COPY src/services/ services/
WORKDIR /src/services/identity/Veises.SocialNet.Identity/
RUN dotnet restore Veises.SocialNet.Identity.csproj
RUN dotnet build Veises.SocialNet.Identity.csproj -c Release -o /app

FROM build as publish
RUN dotnet publish Veises.SocialNet.Identity.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Veises.SocialNet.Identity.dll"]