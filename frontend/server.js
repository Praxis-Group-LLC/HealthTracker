import { createServer } from "node:http";
import { createReadStream, statSync } from "node:fs";
import { readFile } from "node:fs/promises";
import path from "node:path";
import { fileURLToPath } from "node:url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);
const distDir = path.join(__dirname, "dist");
const indexPath = path.join(distDir, "index.html");
const port = Number.parseInt(process.env.PORT ?? "8000", 10);

const backendBaseUrl =
  process.env.BACKEND_HTTP ??
  process.env.services__backend__http__0 ??
  process.env.BACKEND_HTTPS ??
  process.env.services__backend__https__0;

const contentTypes = new Map([
  [".css", "text/css; charset=utf-8"],
  [".html", "text/html; charset=utf-8"],
  [".ico", "image/x-icon"],
  [".js", "text/javascript; charset=utf-8"],
  [".json", "application/json; charset=utf-8"],
  [".png", "image/png"],
  [".svg", "image/svg+xml"],
  [".txt", "text/plain; charset=utf-8"],
  [".woff", "font/woff"],
  [".woff2", "font/woff2"],
]);

function sendText(response, statusCode, body) {
  response.writeHead(statusCode, {
    "Content-Type": "text/plain; charset=utf-8",
    "Content-Length": Buffer.byteLength(body),
  });
  response.end(body);
}

async function readRequestBody(request) {
  const chunks = [];

  for await (const chunk of request) {
    chunks.push(typeof chunk === "string" ? Buffer.from(chunk) : chunk);
  }

  return chunks.length === 0 ? undefined : Buffer.concat(chunks);
}

async function proxyApiRequest(request, response) {
  if (!backendBaseUrl) {
    sendText(response, 500, "Backend endpoint is not configured.");
    return;
  }

  const upstreamUrl = new URL(request.url, backendBaseUrl);
  const requestBody = await readRequestBody(request);
  const headers = new Headers();

  for (const [name, value] of Object.entries(request.headers)) {
    if (!value || name.toLowerCase() === "host") {
      continue;
    }

    headers.set(name, Array.isArray(value) ? value.join(",") : value);
  }

  headers.set("x-forwarded-host", request.headers.host ?? "");

  const forwardedProto = request.headers["x-forwarded-proto"];
  headers.set(
    "x-forwarded-proto",
    Array.isArray(forwardedProto) ? forwardedProto.join(",") : (forwardedProto ?? "https")
  );

  const upstreamResponse = await fetch(upstreamUrl, {
    method: request.method,
    headers,
    body: requestBody,
  });

  const responseBody = Buffer.from(await upstreamResponse.arrayBuffer());
  const responseHeaders = {};

  upstreamResponse.headers.forEach((value, name) => {
    if (name.toLowerCase() === "transfer-encoding") {
      return;
    }

    responseHeaders[name] = value;
  });

  response.writeHead(upstreamResponse.status, responseHeaders);
  response.end(responseBody);
}

function resolveStaticPath(urlPath) {
  const decodedPath = decodeURIComponent(urlPath.split("?")[0]);
  const sanitizedPath = decodedPath.replace(/^\/+/, "");
  const candidatePath = path.resolve(distDir, sanitizedPath);

  if (!candidatePath.startsWith(distDir)) {
    return null;
  }

  try {
    if (statSync(candidatePath).isFile()) {
      return candidatePath;
    }
  } catch {
    return null;
  }

  if (candidatePath === indexPath) {
    return candidatePath;
  }

  return null;
}

async function serveSpa(response) {
  const html = await readFile(indexPath);
  response.writeHead(200, { "Content-Type": "text/html; charset=utf-8" });
  response.end(html);
}

const server = createServer(async (request, response) => {
  try {
    if (!request.url) {
      sendText(response, 400, "Missing request URL.");
      return;
    }

    if (request.url === "/health") {
      sendText(response, 200, "ok");
      return;
    }

    if (request.url.startsWith("/api/")) {
      await proxyApiRequest(request, response);
      return;
    }

    const staticPath = resolveStaticPath(request.url);
    if (staticPath) {
      const extension = path.extname(staticPath);
      response.writeHead(200, {
        "Content-Type": contentTypes.get(extension) ?? "application/octet-stream",
      });
      createReadStream(staticPath).pipe(response);
      return;
    }

    await serveSpa(response);
  } catch (error) {
    const message = error instanceof Error ? error.message : "Unknown server error.";
    sendText(response, 500, message);
  }
});

server.listen(port, "0.0.0.0", () => {
  console.log(`Frontend server listening on port ${port}`);
});
