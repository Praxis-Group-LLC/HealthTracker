// src/api/deviceTokenStore.ts
let _deviceToken: string | null = null;

export const deviceTokenStore = {
  get(): string | null {
    return _deviceToken;
  },
  set(token: string | null) {
    _deviceToken = token;
  },
};
