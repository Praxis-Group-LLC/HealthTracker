// src/features/today/SleepCard.tsx
import React from "react";
import { MoonIcon } from "@heroicons/react/24/outline";

export const SleepCard: React.FC = () => {
    return (
        <div className="flex flex-col gap-2">
            <label className="text-xs font-medium text-zinc-600 dark:text-zinc-300">Sleep (hours)</label>
            <div className="flex items-center gap-2 rounded-md border border-zinc-200 bg-zinc-50 px-2 py-1.5 text-sm text-zinc-600 dark:border-zinc-700 dark:bg-zinc-900 dark:text-zinc-300">
                <MoonIcon className="h-4 w-4 text-lavender-600 dark:text-lavender-400" />
                <span className="flex-1 text-zinc-500 dark:text-zinc-400">
                    tap to set (you wire this up)
                </span>
            </div>
        </div>
    );
};
