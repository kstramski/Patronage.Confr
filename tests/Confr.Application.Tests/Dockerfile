FROM microsoft/dotnet:2.2-sdk
WORKDIR /

# Restore dotnet before build to allow for caching
COPY src/Core/Confr.Application/Confr.Application.csproj /src/src/Core/Confr.Application/
COPY src/Common/Confr.Common/Confr.Common.csproj /src/src/Common/Confr.Common/
COPY src/Core/Confr.Domain/Confr.Domain.csproj /src/src/Core/Confr.Domain/
COPY src/Infrastructure/Confr.Persistence/Confr.Persistence.csproj /src/src/Infrastructure/Confr.Persistence/
COPY tests/Confr.Application.Tests/Confr.Application.Tests.csproj /src/tests/Confr.Application.Tests/

RUN dotnet restore /src/tests/Confr.Application.Tests/Confr.Application.Tests.csproj

# Copy source files and build
COPY . /src

RUN dotnet build /src/tests/Confr.Application.Tests/Confr.Application.Tests.csproj --no-restore -c Release -o /app

WORKDIR /src/tests/Confr.Application.Tests
ENTRYPOINT ["dotnet", "test"]   