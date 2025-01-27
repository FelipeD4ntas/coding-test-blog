# Projeto Blog Simples

Este projeto é uma aplicação de blog simples composta por um backend em .NET e um frontend em Vue.js. Ambos os serviços podem ser executados utilizando contêineres Docker para facilitar a configuração e execução.

---

## Pré-requisitos

Antes de iniciar, certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/install/)

---

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

```
/
├── coding-test-blog/         # Diretório do backend (.NET)
├── dotkon-teste-frontend/    # Diretório do frontend (Vue.js)
├── docker-compose.yml        # Arquivo de orquestração Docker
```

---

## Como Rodar o Projeto

### Passo 1: Clonar o Repositório

Faça o clone do repositório para a sua máquina local:

```bash
git clone <url-do-repositorio>
cd <diretorio-do-projeto>
```

### Passo 2: Configurar as Variáveis de Ambiente

Certifique-se de que as variáveis de ambiente necessárias estão configuradas. Você pode utilizar o arquivo `.env` para definir configurações adicionais, se necessário.

### Passo 3: Construir e Rodar os Contêineres

No diretório raiz do projeto, execute o comando abaixo para construir e iniciar os serviços:

```bash
docker-compose up --build
```

Este comando irá:

1. Construir a imagem Docker para o backend.
2. Construir a imagem Docker para o frontend.
3. Iniciar os contêineres e expor as portas configuradas no `docker-compose.yml`.

### Passo 4: Acessar a Aplicação

- **Frontend**: Acesse o frontend pelo navegador em:

  ```
  http://localhost:8080
  ```

- **Backend**: Você pode testar o backend, incluindo os endpoints da API, em:

  ```
  http://localhost:7164
  ```

---

## Estrutura dos Arquivos Docker

### Backend (`coding-test-blog/Dockerfile`)

O backend é um projeto .NET e está configurado com o seguinte `Dockerfile`:

```dockerfile
# Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 7164
ENV ASPNETCORE_URLS=http://+:7164
ENTRYPOINT ["dotnet", "TesteDotkon.WebApi.dll"]
```

### Frontend (`dotkon-teste-frontend/Dockerfile`)

O frontend é uma aplicação Vue.js e está configurado com o seguinte `Dockerfile`:

```dockerfile
FROM node:18 AS build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
```

### Docker Compose (`docker-compose.yml`)

O arquivo `docker-compose.yml` orquestra os dois serviços:

```yaml
version: "3.8"

services:
  backend:
    build:
      context: ./coding-test-blog
      dockerfile: Dockerfile
    ports:
      - "7164:7164"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:7164

  frontend:
    build:
      context: ./dotkon-teste-frontend
      dockerfile: Dockerfile
    ports:
      - "8080:80"
    depends_on:
      - backend
```

---

## Testando a Aplicação

1. Acesse o frontend e realize o login ou cadastro.
2. Adicione novos posts pelo frontend. As postagens aparecerão automaticamente devido à integração WebSocket configurada no backend.

---

## Parando os Contêineres

Para parar os contêineres, utilize o comando:

```bash
docker-compose down
```

Este comando encerrará todos os serviços e removerá os contêineres associados.

---

## Resolução de Problemas

### 1. **Erro de Conexão com o WebSocket**

- Certifique-se de que o WebSocket está configurado corretamente no backend.
- Verifique se a porta `7164` está exposta e não está sendo bloqueada por firewall.

### 2. **Mudanças não Refletidas no Frontend/Backend**

- Após realizar alterações no código, reconstrua os contêineres com:

  ```bash
  docker-compose up --build
  ```

### 3. **Porta em Uso**

- Caso alguma porta esteja em uso, altere as portas no arquivo `docker-compose.yml` e reinicie os serviços.

