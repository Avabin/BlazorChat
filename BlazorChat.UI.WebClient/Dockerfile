FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
EXPOSE 80
COPY /release/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
