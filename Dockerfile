FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
WORKDIR .
COPY . .
RUN dotnet publish -c Release -o /publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf