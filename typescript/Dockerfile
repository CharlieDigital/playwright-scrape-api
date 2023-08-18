FROM mcr.microsoft.com/playwright:v1.34.0-jammy

# Create app directory in the image
WORKDIR /usr/src/app

# Copy over assets
COPY package.json ./
COPY yarn.lock ./

# Install dependencies.
RUN yarn install --immutable --immutable-cache --check-cache

# Copy source
COPY . .

# Build the TypeScript
RUN npx tsc

# Start the server.
EXPOSE 8080
CMD ["node", "dist/index.js"]