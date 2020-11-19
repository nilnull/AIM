#!/bin/sh

echo "Packing..."
dotnet pack ".\AegisImplictMail\AegisImplicitMail.csproj" -c Release


echo "Pushing..."
dotnet nuget push ".\AegisImplictMail\bin\Release\AIM.*.nupkg"  -k $NUGET_API -s https://api.nuget.org/v3/index.json


