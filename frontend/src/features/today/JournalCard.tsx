// src/features/today/JournalCard.tsx
import React from "react";
import { PencilSquareIcon } from "@heroicons/react/24/outline";
import type {
  JournalEntryDto,
  CreateNewJournalEntryRequest,
} from "../../api/types";
import { apiClient } from "../../api/client";

type JournalCardProps = {
  journalEntry: JournalEntryDto | null;
};

export const JournalCard: React.FC<JournalCardProps> = ({ journalEntry }) => {
  const [text, setText] = React.useState(journalEntry?.text ?? "");

  const saveEntry = async () => {
    const { error } = await apiClient.POST("/api/JournalEntries", {
      body: { text } as CreateNewJournalEntryRequest,
    });

    if (error) {
      console.error("Failed to save journal entry:", error);
    }
  };

  React.useEffect(() => {
    setText(journalEntry?.text ?? "");
  }, [journalEntry]);

  return (
    <div className="rounded-xl border border-zinc-200 bg-white/80 p-6 md:col-span-2 shadow-sm backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/70">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-2">
          <PencilSquareIcon className="h-5 w-5 text-lavender-600 dark:text-lavender-400" />
          <div>
            <h2 className="text-sm font-semibold text-zinc-900 dark:text-zinc-50">
              Today&apos;s journal
            </h2>
            <p className="text-xs text-zinc-500 dark:text-zinc-400">
              This stays on your device's anonymous account. No email, no profile.
            </p>
          </div>
        </div>
        <button
          onClick={saveEntry}
          className="flex items-center gap-2 rounded-md bg-seafoam-600 px-3 py-1.5 text-xs font-medium text-white hover:bg-seafoam-700 dark:bg-seafoam-700 dark:hover:bg-seafoam-600"
        >
          Save entry
        </button>
      </div>

      <div className="mt-3">
        <textarea
          className="min-h-[180px] w-full resize-y rounded-lg border border-zinc-200 bg-zinc-50 px-3 py-2 text-sm text-zinc-900 placeholder:text-zinc-400 focus:border-seafoam-400 focus:outline-none focus:ring-1 focus:ring-seafoam-500 dark:border-zinc-700 dark:bg-zinc-900/60 dark:text-zinc-100 dark:placeholder:text-zinc-500 dark:focus:border-seafoam-500 dark:focus:ring-seafoam-500"
          placeholder="Write about your day, what triggered you, what helped, and anything you want to remember for your provider..."
          value={text}
          onChange={(e) => setText(e.target.value)}
        />
      </div>
    </div>
  );
};
