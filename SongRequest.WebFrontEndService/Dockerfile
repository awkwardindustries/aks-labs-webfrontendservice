FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy only the project file and restore as a distict layer of image
COPY *.csproj ./
RUN dotnet restore

# Copy all other files and build as a distinct layer of image
COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SongRequest.WebFrontEndService.dll"]