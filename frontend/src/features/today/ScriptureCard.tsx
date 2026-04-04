// src/features/today/ScriptureCard.tsx
import React from "react";
import { BookOpenIcon } from "@heroicons/react/24/outline";

export const ScriptureCard: React.FC = () => {
  return (
    <div className="rounded-xl border border-zinc-200 bg-white/80 p-6 shadow-sm backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/70 text-xs">
      <div className="flex items-center gap-2">
        <BookOpenIcon className="h-5 w-5 text-lavender-600 dark:text-lavender-400" />
        <h2 className="text-sm font-semibold text-zinc-900 dark:text-zinc-50">Scripture corner</h2>
      </div>
      <p className="mt-3 text-zinc-600 dark:text-zinc-400">
        When scripture mode is enabled, this area can surface passages tied to
        endurance, peace, and hope.
      </p>
    </div>
  );
};
