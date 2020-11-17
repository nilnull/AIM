#!/bin/sh

echo "Restoring..."
dotnet restore "..\AIM.sln"


echo "Building..."
dotnet build "..\AIM.sln" -c Release

