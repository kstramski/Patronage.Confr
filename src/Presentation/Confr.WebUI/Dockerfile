FROM microsoft/dotnet:2.2-aspnetcore-runtime AS Base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build

# Restore dotnet before build to allow for caching
WORKDIR /
COPY src/Core/Confr.Application/Confr.Application.csproj /src/src/Core/Confr.Application/
COPY src/Common/Confr.Common/Confr.Common.csproj /src/src/Common/Confr.Common/
COPY src/Core/Confr.Domain/Confr.Domain.csproj /src/src/Core/Confr.Domain/
COPY src/Infrastructure/Confr.Infrastructure/Confr.Infrastructure.csproj /src/src/Infrastructure/Confr.Infrastructure/
COPY src/Infrastructure/Confr.Persistence/Confr.Persistence.csproj /src/src/Infrastructure/Confr.Persistence/
COPY src/Presentation/Confr.WebUI/Confr.WebUI.csproj /src/src/Presentation/Confr.WebUI/

RUN dotnet restore /src/src/Presentation/Confr.WebUI/Confr.WebUI.csproj

# Copy source files and build
COPY . /src

RUN dotnet build /src/src/Presentation/Confr.WebUI/Confr.WebUI.csproj --no-restore -c Release
RUN dotnet publish /src/src/Presentation/Confr.WebUI/Confr.WebUI.csproj --no-restore -c Release -o /app

# Copy compiled app to runtime container
FROM base AS final
COPY --from=build /app .
CMD ["dotnet", "Confr.WebUI.dll"]