# Stage 1: Use a slim and secure base image for build steps.
FROM node:22-bookworm-slim AS base
# Declare the build argument for NEXT_PUBLIC_API_URL
ARG NEXT_PUBLIC_API_URL

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
ENV NEXT_PUBLIC_API_URL=$NEXT_PUBLIC_API_URL
RUN npm run build


# Stage 4: Run application
# Use a distroless and production secure image for production
FROM gcr.io/distroless/nodejs22-debian12
# Set current working directory
WORKDIR /app
# Next.js collects completely anonymous telemetry data about general usage.
# Learn more here: https://nextjs.org/telemetry
# Uncomment the following line in case you want to disable telemetry during the build.
ENV NEXT_TELEMETRY_DISABLED 1
COPY --from=builder /app/public ./public
# Automatically leverage output traces to reduce image size
# https://nextjs.org/docs/advanced-features/output-file-tracing
COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static
USER nextjs
# Expose the port the app runs on
EXPOSE 3000
ENV NODE_ENV production
ENV PORT 3000
# server.js is created by next build from the standalone output
# https://nextjs.org/docs/pages/api-reference/next-config-js/output
CMD ["node", "server.js"]
