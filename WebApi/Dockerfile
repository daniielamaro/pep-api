#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

RUN echo 'Configurando dependecias' \
  && apt-get update \
  && apt-get upgrade -y \
  && apt-get install -y \
    curl \
    locales \
    jq \
    iproute2 \
    vim \
    libbz2-dev \
    zlib1g-dev \
    procps \
  && sed -i -e 's/# en_US.UTF-8 UTF-8/pt_BR.UTF-8 UTF-8/' /etc/locale.gen \
  && locale-gen pt_BR.UTF-8

ENV TZ="America/Sao_Paulo" \
  LANG="pt_BR.UTF-8" \
  LANGUAGE="pt_BR:pt:en" \
  LC_CTYPE="pt_BR.UTF-8" \
  LC_NUMERIC="pt_BR.UTF-8" \
  LC_TIME="pt_BR.UTF-8" \
  LC_COLLATE="pt_BR.UTF-8" \
  LC_MONETARY="pt_BR.UTF-8" \
  LC_MESSAGES="pt_BR.UTF-8" \
  LC_PAPER="pt_BR.UTF-8" \
  LC_NAME="pt_BR.UTF-8" \
  LC_ADDRESS="pt_BR.UTF-8" \
  LC_TELEPHONE="pt_BR.UTF-8" \
  LC_MEASUREMENT="pt_BR.UTF-8" \
  LC_IDENTIFICATION="pt_BR.UTF-8"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Aplicacao/Aplicacao.csproj", "Aplicacao/"]
COPY ["Infraestrutura/Infraestrutura.csproj", "Infraestrutura/"]
COPY ["Dominio/Dominio.csproj", "Dominio/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]