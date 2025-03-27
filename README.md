# desafio_logica_rotas
desafio logica rotas

## módulos ====================================
-frontend
-backend
-datastore

# frontend ---
web site em angular
gerado com: ng version = 19
comando: ng new "frontend" --no-standalone
(css, sem otimizacao server-side)
porta recomendada: 4998
aida não contém https, authn, authz

# backend ---
web api rest em .net core 9, com c#
gerado com: dotnet
comando: dotnet new web api "Rotas.Api"
porta recomendada: 4999
aida não contém https, authn, authz

# datastore ---
Arquivo "deslocamento.csv"
a razão é simplificar a execução e testes, focando no escopo: "construir um frontend Angular com páginas crud, e uma funcionalidade para solicitar a melhor rota entre 2 pontos;um backend web api com .net core, provendo os endpoints para o backend;datastore: o mais simples possível, para focar nos componentes e no algoritmo"
seu conteúdo é json gerado e controlado pelo c# em runtime, a partir de Serialize de objetos.


## pré-requisitos ====================================

Build-debug:
-node
-angular cli
-dotnet

Container:
-docker ou podman

# Build node + angular ---

-node versao >= 22

-ng versao >= 19
ng version ==> snapshot do ambiente atual:
angular\node version ---
Angular CLI: 19.2.4
Node: 22.14.0
Package Manager: npm 11.2.0
OS: win32 x64

Angular:
...

Package                      Version
@angular-devkit/architect    0.1902.4 (cli-only)
@angular-devkit/core         19.2.4 (cli-only)
@angular-devkit/schematics   19.2.4 (cli-only)
@schematics/angular          19.2.4 (cli-only)

# Build dotnet ---
sdk version: 9


# Container ---

docker cli >= 28
docker desktop >= 4.39

ou 

podman cli >= 5


## build ====================================

local\debug:
web api:
	executar em modo "Development"
	assegurar que o launchSettings.json tenha a variável ASPNETCORE_ENVIRONMENT=Development
	ou assegurar que o sistema tenha a variável de ambiente ASPNETCORE_ENVIRONMENT=Development
	dotnet run "Rotas.Api\Rotas.Api.csproj"
	deverá estar em listen em http://localhost:4999/
	swagger: http://localhost:4999/index/swagger
	
angular:
	ng serve --configuration=Development
	deverá estar em listen em http://localhost:4998/


## containers ====================================
recomendado: usar docker compose para subir os 2 serviços juntos
na raiz do repositório tem o compose.yaml

**acessar o frontend e fazer o build do Angular antes!!!**
cd \src\frontend
ng build --configuration=Development

somente após o build ter gerado dentro da pasta "\dist", exeute o compose.
docker compose -f compose.Development.yaml up --build

deveremos ter os módulos disponíveis conforme abaixo:
	web api: disponível em http://localhost:4999/
	frontend: disponível em http://localhost:4998/

