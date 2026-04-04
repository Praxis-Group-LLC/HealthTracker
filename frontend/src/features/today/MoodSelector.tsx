// features/today/MoodSelector.tsx
import React from "react";

export const MoodSelector: React.FC = () => {
  return (
    <div className="flex flex-col gap-2">
      <label className="text-xs font-medium text-zinc-600 dark:text-zinc-300">
        Mood (1–10)
      </label>
      <div className="flex gap-1">
        {Array.from({ length: 10 }).map((_, i) => (
          <button
            key={i}
            type="button"
            className="flex-1 rounded-lg border border-zinc-200 bg-zinc-50 py-1 text-xs text-zinc-600 shadow-sm hover:border-lavender-300 hover:bg-lavender-50 hover:text-lavender-700 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-lavender-400 dark:border-zinc-700 dark:bg-zinc-900 dark:text-zinc-300 dark:hover:border-lavender-500 dark:hover:bg-zinc-900/80 dark:hover:text-lavender-200"
          >
            {i + 1}
          </button>
        ))}
      </div>
    </div>
  );
};
