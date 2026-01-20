# Azure Exploration - ASP.NET Core Web API

A containerized ASP.NET Core 8.0 Web API application with a clean architecture pattern (Controllers, Managers, Repositories) and ready for deployment to Azure Container Apps.

## Project Structure

```
azure_exploration/
├── Controllers/         # API controllers
├── Managers/           # Business logic layer
├── Repositories/       # Data access layer
├── Models/             # Data models
├── Dockerfile          # Container definition
└── .github/
    └── workflows/      # GitHub Actions CI/CD
```

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (optional, for local development without Docker)

## Running Locally with Docker Desktop

### Step 1: Build the Docker Image

From the project root directory, build the Docker image:

```bash
docker build -t azure_exploration:latest .
```

### Step 2: Run the Container

Run the container and map port 8080 to your local machine:

```bash
docker run -d -p 8080:8080 --name azure-exploration-api azure_exploration:latest
```

**Alternative with environment variables:**

```bash
docker run -d -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development --name azure-exploration-api azure_exploration:latest
```

### Step 3: Access the Application

- **Swagger UI**: http://localhost:8080/swagger
- **API Base URL**: http://localhost:8080/api
- **Health Check**: http://localhost:8080/health

### Step 4: Test the API

**Get all items:**
```bash
curl http://localhost:8080/api/items
```

**Create an item:**
```bash
curl -X POST http://localhost:8080/api/items \
  -H "Content-Type: application/json" \
  -d '{"name":"Test Item","description":"Test Description"}'
```

**Get item by ID:**
```bash
curl http://localhost:8080/api/items/1
```

## Container Management

### View Running Containers

```bash
docker ps
```

### View Container Logs

```bash
docker logs azure-exploration-api
```

### Stop the Container

```bash
docker stop azure-exploration-api
```

### Remove the Container

```bash
docker rm azure-exploration-api
```

### Restart the Container

```bash
docker restart azure-exploration-api
```

## Running Without Docker (Local Development)

If you have .NET SDK installed:

```bash
dotnet restore
dotnet run
```

The application will be available at:
- Swagger UI: https://localhost:5001/swagger
- API Base: https://localhost:5001/api

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/items` | Get all items |
| GET | `/api/items/{id}` | Get item by ID |
| GET | `/api/items/search?name={name}` | Search items by name |
| POST | `/api/items` | Create a new item |
| PUT | `/api/items/{id}` | Update an item |
| DELETE | `/api/items/{id}` | Delete an item |
| GET | `/health` | Health check endpoint |

## Architecture

- **Controllers**: Handle HTTP requests and responses
- **Managers**: Business logic and validation
- **Repositories**: Data access layer (currently using in-memory storage)
- **Models**: Data transfer objects

All layers use interfaces for dependency injection and testability.

## Deployment

This application is configured for deployment to:
- **Azure Container Apps** via GitHub Actions (see `.github/workflows/azure-container-apps.yml`)
- **Azure Kubernetes Service** (AKS) - can be deployed using Kubernetes manifests
- Any container orchestration platform

## Configuration

### Environment Variables

- `ASPNETCORE_ENVIRONMENT`: Set to `Development` for Swagger UI and debug features

### Secrets Required for GitHub Actions

- `AZURE_CREDENTIALS`: Service principal credentials (JSON)
- `ACR_NAME`: Azure Container Registry name
- `ACR_LOGIN_SERVER`: ACR login server (e.g., `registry.azurecr.io`)
- `AZURE_CONTAINER_APP_NAME`: Container App name
- `AZURE_RESOURCE_GROUP`: Resource group name

## License

This project is for exploration and learning purposes.
