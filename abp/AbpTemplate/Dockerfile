FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# set up node
ENV NODE_VERSION 16.13.0
ENV NODE_DOWNLOAD_URL https://nodejs.org/dist/v$NODE_VERSION/node-v$NODE_VERSION-linux-x64.tar.gz
ENV NODE_DOWNLOAD_SHA 589b7e7eb22f8358797a2c14a0bd865459d0b44458b8f05d2721294dacc7f734

RUN curl -SL "$NODE_DOWNLOAD_URL" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

RUN apt update && apt -y install gnupg

ENV YARN_VERSION 1.22.15

RUN set -ex \
  && wget -qO- https://dl.yarnpkg.com/debian/pubkey.gpg | gpg --import \
  && curl -fSLO --compressed "https://yarnpkg.com/downloads/$YARN_VERSION/yarn-v$YARN_VERSION.tar.gz" \
  && curl -fSLO --compressed "https://yarnpkg.com/downloads/$YARN_VERSION/yarn-v$YARN_VERSION.tar.gz.asc" \
  && gpg --batch --verify yarn-v$YARN_VERSION.tar.gz.asc yarn-v$YARN_VERSION.tar.gz \
  && mkdir -p /opt/yarn \
  && tar -xzf yarn-v$YARN_VERSION.tar.gz -C /opt/yarn --strip-components=1 \
  && ln -s /opt/yarn/bin/yarn /usr/local/bin/yarn \
  && ln -s /opt/yarn/bin/yarn /usr/local/bin/yarnpkg \
  && rm yarn-v$YARN_VERSION.tar.gz.asc yarn-v$YARN_VERSION.tar.gz

COPY ["abp/AbpTemplate/AbpTemplate.csproj", "abp/AbpTemplate/"]
RUN dotnet restore "abp/AbpTemplate/AbpTemplate.csproj"
COPY . .
WORKDIR "/src/abp/AbpTemplate"
RUN dotnet tool install -g Volo.Abp.Cli
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN abp install-libs
RUN dotnet build "AbpTemplate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AbpTemplate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AbpTemplate.dll"]
