// src/features/today/SleepCard.tsx
import React from "react";

export const SleepCard: React.FC = () => {
    return (
        <div className="flex flex-col gap-2">
            <label className="text-xs text-slate-400">Sleep (hours)</label>
            <div className="flex items-center gap-2 rounded-md border border-slate-800 bg-slate-900 px-2 py-1.5 text-sm text-slate-200">
                <span className="text-slate-500">⏱</span>
                <span className="flex-1 text-slate-400">
                    tap to set (you wire this up)
                </span>
            </div>
        </div>
    );
};
