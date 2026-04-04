// src/features/today/QuickCopingCard.tsx
import React from "react";
import { ArrowPathIcon, EyeIcon } from "@heroicons/react/24/outline";

export const QuickCopingCard: React.FC = () => {
  return (
    <div className="rounded-xl border border-zinc-200 bg-white/80 p-6 shadow-sm backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/70">
      <h2 className="text-sm font-semibold text-zinc-900 dark:text-zinc-50">Quick coping</h2>
      <p className="mt-1 text-xs text-zinc-500 dark:text-zinc-400">
        Need a reset before journaling?
      </p>
      <div className="mt-3 space-y-2 text-xs">
        <button className="flex w-full items-center justify-between rounded-lg border border-zinc-200 bg-zinc-50 px-3 py-2 text-left hover:border-seafoam-400 hover:bg-seafoam-50 dark:border-zinc-700 dark:bg-zinc-900/60 dark:hover:border-seafoam-500 dark:hover:bg-zinc-900/40">
          <span className="flex flex-col">
            <span className="font-medium text-zinc-900 dark:text-zinc-50">Box breathing</span>
            <span className="text-[0.7rem] text-zinc-500 dark:text-zinc-400">
              4–4–4–4 guided breaths
            </span>
          </span>
          <ArrowPathIcon className="h-4 w-4 text-seafoam-600 dark:text-seafoam-400" />
        </button>
        <button className="flex w-full items-center justify-between rounded-lg border border-zinc-200 bg-zinc-50 px-3 py-2 text-left hover:border-seafoam-400 hover:bg-seafoam-50 dark:border-zinc-700 dark:bg-zinc-900/60 dark:hover:border-seafoam-500 dark:hover:bg-zinc-900/40">
          <span className="flex flex-col">
            <span className="font-medium text-zinc-900 dark:text-zinc-50">
              5–4–3–2–1 grounding
            </span>
            <span className="text-[0.7rem] text-zinc-500 dark:text-zinc-400">
              Senses-based reset
            </span>
          </span>
          <EyeIcon className="h-4 w-4 text-seafoam-600 dark:text-seafoam-400" />
        </button>
      </div>
    </div>
  );
};
