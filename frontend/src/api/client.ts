// src/api/client.ts
import createClient, { type Middleware } from "openapi-fetch";
import type { paths } from "./schema";
import { deviceTokenStore } from "./deviceTokenStore";

export const apiClient = createClient<paths>({});

const deviceAuthMiddleware: Middleware = {
  async onRequest({ request }) {
    const token = deviceTokenStore.get();
    if (token) {
      request.headers.set("X-Device-Token", token);
    }
    return request;
  },
};

apiClient.use(deviceAuthMiddleware);

export const { GET, POST, PUT, DELETE } = apiClient;
