docker container ls
docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' posgressql-db-1

dotnet test --filter FullyQualifiedName!~IntegrationTests /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./coverage.cobertura.xml
dotnet reportgenerator -reports:".\tests\TestResult\{guid}\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html

docker build . -f .\src\Dotnet.Sales.WebApi\Dockerfile -t dotnet-sales-webapi/dev:latest
docker build . -f Dockerfile -t dotnet-sales-web/dev:latest
docker compose up -d


