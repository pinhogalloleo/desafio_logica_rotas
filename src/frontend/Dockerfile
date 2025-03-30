
# Define the Angular environment (default: Development)
# possible values for environment: Default, Development, Test
ARG ANGULAR_ENVIRONMENT="Test"

# Stage 1: Build the Angular app
FROM node:18 AS build
WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the application
COPY . ./

# Build the Angular app for the specified environment
ARG ANGULAR_ENVIRONMENT="Test"
RUN npm run build --configuration=${ANGULAR_ENVIRONMENT}

# Stage 2: Serve the app with Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy the Angular app's build output to the Nginx HTML directory
COPY --from=build /app/dist/rotas/browser /usr/share/nginx/html

# Copy the prerendered-routes.json file
COPY --from=build /app/dist/rotas/prerendered-routes.json /usr/share/nginx/html/prerendered-routes.json

# Dynamically copy the correct Nginx configuration file
ARG ANGULAR_ENVIRONMENT
COPY nginx.${ANGULAR_ENVIRONMENT}.conf /etc/nginx/nginx.conf

# MANDATORY to expose this, once it is default port for Nginx
EXPOSE 80

# Expose port 4998 for the Angular app, symbolically
EXPOSE 4998

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
