// src/auth/DeviceAuthContext.tsx
import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";
import { deviceTokenStore } from "../api/deviceTokenStore";
import { apiClient } from "../api/client";

type DeviceAuthContextValue = {
  deviceToken: string | null;
  isLoading: boolean;
  error: unknown;
};

const DeviceAuthContext = createContext<DeviceAuthContextValue | undefined>(
  undefined,
);

const DEVICE_TOKEN_STORAGE_KEY = "neuronest_device_token";

export const DeviceAuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [deviceToken, setDeviceToken] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<unknown>(null);

  const applyToken = useCallback((token: string | null) => {
    setDeviceToken(token);
    deviceTokenStore.set(token);
    if (token) {
      window.localStorage.setItem(DEVICE_TOKEN_STORAGE_KEY, token);
    } else {
      window.localStorage.removeItem(DEVICE_TOKEN_STORAGE_KEY);
    }
  }, []);

  useEffect(() => {
    let cancelled = false;

    async function init() {
      setIsLoading(true);
      setError(null);

      try {
        // 1. localStorage
        const stored = window.localStorage.getItem(DEVICE_TOKEN_STORAGE_KEY);
        if (stored) {
          if (!cancelled) applyToken(stored);
          return;
        }

        // 2. Register device
        const { data, error } = await apiClient.POST(
          "/api/auth/register-device",
          {},
        );

        if (error) throw error;

        const token = (data as any)?.deviceToken ?? (data as any)?.token;
        if (!token || typeof token !== "string") {
          throw new Error("Device registration response missing token");
        }

        if (!cancelled) applyToken(token);
      } catch (err) {
        console.error("Failed to register device", err);
        if (!cancelled) setError(err);
      } finally {
        if (!cancelled) setIsLoading(false);
      }
    }

    void init();

    return () => {
      cancelled = true;
    };
  }, [applyToken]);

  const value: DeviceAuthContextValue = { deviceToken, isLoading, error };

  return (
    <DeviceAuthContext.Provider value={value}>
      {children}
    </DeviceAuthContext.Provider>
  );
};

export function useDeviceAuth(): DeviceAuthContextValue {
  const ctx = useContext(DeviceAuthContext);
  if (!ctx) {
    throw new Error("useDeviceAuth must be used within DeviceAuthProvider");
  }
  return ctx;
}
