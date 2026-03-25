// src/features/today/useTodayJournal.ts
import { useEffect, useState } from "react";
import { apiClient } from "../../api/client";
import { useDeviceAuth } from "../../auth/DeviceAuthContext";
import type { JournalEntryDto } from "../../api/types";

export function useTodayJournal(dateIso: string) {
  const { deviceToken, isLoading: authLoading, error: authError } = useDeviceAuth();
  const [entries, setEntries] = useState<JournalEntryDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<unknown>(null);

  useEffect(() => {
    // Don't fetch until auth is ready and we have a token
    if (authLoading || !deviceToken || authError) {
      return;
    }

    let cancelled = false;

    async function load() {
      setLoading(true);
      setError(null);

      const { data, error } = await apiClient.GET("/api/JournalEntries", {
        params: { query: { from: dateIso, to: dateIso } },
      });

      if (cancelled) return;

      if (error) {
        console.error(error);
        setError(error);
        setEntries([]);
      } else {
        setEntries(data ?? []);
      }

      setLoading(false);
    }

    void load();

    return () => {
      cancelled = true;
    };
  }, [authLoading, authError, deviceToken, dateIso]);

  return { entries, loading: loading || authLoading, error: error ?? authError };
}
