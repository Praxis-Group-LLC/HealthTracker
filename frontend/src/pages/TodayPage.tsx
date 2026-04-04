// TodayPage.tsx
import React from "react";
import { MoodSelector } from "../features/today/MoodSelector";
import { SleepCard } from "../features/today/SleepCard";
import { JournalCard } from "../features/today/JournalCard";
import { QuickCopingCard } from "../features/today/QuickCopingCard";
import { ScriptureCard } from "../features/today/ScriptureCard";
import { useTodayJournal } from "../features/today/useTodayJournal";
import { useDeviceAuth } from "../auth/DeviceAuthContext";

export const TodayPage: React.FC = () => {
  const todayIso = new Date().toISOString().slice(0, 10);
  const { isLoading: authLoading, error: authError } = useDeviceAuth();
  const { entries, loading, error } = useTodayJournal(todayIso);

  if (authLoading) {
    return (
      <div className="text-xs text-zinc-500 dark:text-zinc-400">
        Preparing device session…
      </div>
    );
  }

  if (authError) {
    return (
      <div className="text-xs text-red-500">
        Could not register this device. Please try reloading.
      </div>
    );
  }

  if (loading) {
    return (
      <div className="text-xs text-zinc-500 dark:text-zinc-400">
        Loading journal entry…
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-xs text-red-500">
        Failed to load today&apos;s journal entry. Please try reloading.
      </div>
    );
  }

  return (
    <div className="space-y-6 md:space-y-8">
      {/* Top row: mood + quick stats */}
      <div className="grid gap-6 md:grid-cols-3">
        <div className="rounded-2xl border border-zinc-200 bg-white/80 p-6 shadow-sm backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/70 md:col-span-2 space-y-4">
          <div className="flex items-center justify-between gap-2">
            <div>
              <h2 className="text-sm font-semibold text-zinc-900 dark:text-zinc-50">
                How are you feeling?
              </h2>
              <p className="text-xs text-zinc-500 dark:text-zinc-400">
                Log a quick snapshot of today before writing your journal.
              </p>
            </div>
            <span className="inline-flex items-center rounded-full bg-lavender-50 px-3 py-1 text-[0.7rem] font-medium text-lavender-700 dark:bg-lavender-950/70 dark:text-lavender-200">
              Device-bound · Private
            </span>
          </div>

          <div className="grid gap-6 md:grid-cols-3">
            <MoodSelector />
            <SleepCard />
          </div>
        </div>

        <div className="rounded-2xl border border-zinc-200 bg-white/80 p-6 shadow-sm backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/70 flex flex-col justify-between">
          <div>
            <h2 className="text-sm font-semibold text-zinc-900 dark:text-zinc-50">
              Reminder status
            </h2>
            <p className="mt-1 text-xs text-zinc-500 dark:text-zinc-400">
              You’ll be nudged once per day at your chosen time.
            </p>
          </div>
          <div className="mt-3 space-y-2 text-xs">
            <div className="flex items-center justify-between">
              <span className="text-zinc-500 dark:text-zinc-400">
                Daily reminder
              </span>
              <span className="inline-flex items-center gap-2 rounded-full bg-lavender-50 px-3 py-1 text-[0.7rem] font-medium text-lavender-700 dark:bg-lavender-950/70 dark:text-lavender-200">
                <span className="h-2 w-2 rounded-full bg-lavender-500 dark:bg-lavender-400" />
                Enabled
              </span>
            </div>
            <div className="flex items-center justify-between text-zinc-500 dark:text-zinc-400">
              <span>Next at</span>
              <span className="font-medium text-zinc-900 dark:text-zinc-50">
                8:00 PM
              </span>
            </div>
          </div>
        </div>
      </div>

      {/* Journal + coping / scripture */}
      <div className="grid gap-6 md:grid-cols-3">
        <JournalCard
          journalEntry={
            entries == null || entries.length == 0 ? null : entries[0]
          }
        />
        <div className="space-y-6">
          <QuickCopingCard />
          <ScriptureCard />
        </div>
      </div>
    </div>
  );
};
