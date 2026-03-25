// src/features/today/JournalCard.tsx
import React from "react";
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
    <div className="rounded-xl border border-slate-800 bg-slate-900/60 p-4 md:col-span-2">
      <div className="flex items-center justify-between">
        <div>
          <h2 className="text-sm font-semibold text-slate-100">
            Today&apos;s journal
          </h2>
          <p className="text-xs text-slate-400">
            This stays on your device’s anonymous account. No email, no profile.
          </p>
        </div>
        <button
          onClick={saveEntry}
          className="rounded-md bg-emerald-500 px-3 py-1.5 text-xs font-medium text-slate-950 hover:bg-emerald-400"
        >
          Save entry
        </button>
      </div>

      <div className="mt-3">
        <textarea
          className="min-h-[180px] w-full resize-y rounded-lg border border-slate-800 bg-slate-950/60 px-3 py-2 text-sm text-slate-100 placeholder:text-slate-500 focus:border-emerald-400 focus:outline-none focus:ring-1 focus:ring-emerald-500"
          placeholder="Write about your day, what triggered you, what helped, and anything you want to remember for your provider..."
          value={text}
          onChange={(e) => setText(e.target.value)}
        />
      </div>
    </div>
  );
};
