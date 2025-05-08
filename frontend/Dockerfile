# Stage 1: Use a slim and secure base image for build steps.
FROM node:18-alpine AS base
# Declare the build argument for NEXT_PUBLIC_API_URL

# Stage 2: Install dependencies only when needed
FROM base AS deps
# Set current working directory
WORKDIR /app
# Copy files that contains dependencies
COPY package.json package-lock.json ./
# Install only production dependencies
RUN npm install

# Stage 3: Build application
FROM base AS builder
# Set current working directory
WORKDIR /app
# Copy files from deps stage
COPY --from=deps /app/node_modules ./node_modules
ENV NEXT_TELEMETRY_DISABLED 1
COPY . .
# Set the build-time environment variable (use the build ARG to pass the value)
ARG NEXT_PUBLIC_API_URL
ENV NEXT_PUBLIC_API_URL=$NEXT_PUBLIC_API_URL
ARG NEXTAUTH_URL
ENV NEXTAUTH_URL=$NEXTAUTH_URL
# Add the SKIP_KEYVAULT_FETCH build argument for build-only usage
ARG SKIP_KEYVAULT_FETCH
ENV SKIP_KEYVAULT_FETCH=$SKIP_KEYVAULT_FETCH

ARG AZURE_CLIENT_ID
ENV AZURE_CLIENT_ID=$AZURE_CLIENT_ID
ARG AZURE_TENANT_ID
ENV AZURE_TENANT_ID=$AZURE_TENANT_ID
ARG AZURE_SUBSCRIPTION_ID
ENV AZURE_SUBSCRIPTION_ID=$AZURE_SUBSCRIPTION_ID

RUN npm run build


# Stage 4: Run application
# Use a distroless and production secure image for production
FROM base AS runner
WORKDIR /app
ENV NODE_ENV production

RUN addgroup -g 1001 -S nodejs
RUN adduser -S nextjs -u 1001

COPY --from=builder /app/public ./public
# Automatically leverage output traces to reduce image size
# https://nextjs.org/docs/advanced-features/output-file-tracing
COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static
# Next.js collects completely anonymous telemetry data about general usage.
ENV NEXT_TELEMETRY_DISABLED 1
# Expose the port the app runs on
ENV PORT 3000
EXPOSE 3000

# server.js is created by next build from the standalone output
# https://nextjs.org/docs/pages/api-reference/next-config-js/output
CMD HOSTNAME="0.0.0.0" node server.js
