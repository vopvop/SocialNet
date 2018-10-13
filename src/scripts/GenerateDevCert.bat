@echo off

set CertPath=%USERPROFILE%\.aspnet\https\aspnetapp.pfx

dotnet dev-certs https -ep "%CertPath%" -p %1
dotnet dev-certs https --trust

@echo Certificate path: %CertPath%