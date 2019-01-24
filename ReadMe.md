# Patronage Confr

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Launch the back end by running:
     ```
	 dotnet run
	 ```
  5. Launch [http://localhost:52468/api](http://localhost:52468/api) in your browser to view the API

### Docker Setup
Follow these steps to run project using Docker containers:

  1. Download and install [Docker Desktop](https://www.docker.com/products/docker-desktop)
  2. Swith to Linux containers if you using Windows containers
  3. Disable active services, which using 80 and 443 port
  3. At the root directory run command:
     ```
     docker-compose up
     ```
  4. (Optional) After making changes in the project run commands:
	 ```
     docker-compose build
     docker-compose up
     ```
  5. Launch [http://localhost/api](http://localhost/api) in your browser to view the API

### Docker Project Tests
Follow these steps to run project tests using Docker containers:

  1. At the root directory run command:
     ```
     docker-compose -f docker-compose.tests.yml up
     ```
  2. (Optional) After making changes in the project tests run commands:
	 ```
     docker-compose -f docker-compose.tests.yml build
     docker-compose -f docker-compose.tests.yml up
     ```
  
## Technologies
* .NET Core 2.2
* ASP.NET Core 2.2
* Entity Framework Core 2.2
