FROM node:carbon-alpine AS builder
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.17-alpine
WORKDIR /app
COPY --from=builder /app/build /app
COPY nginx.conf /etc/nginx/conf.d/default.conf

