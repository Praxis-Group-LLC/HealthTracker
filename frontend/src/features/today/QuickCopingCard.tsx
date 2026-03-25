// src/features/today/QuickCopingCard.tsx
import React from "react";

export const QuickCopingCard: React.FC = () => {
  return (
    <div className="rounded-xl border border-slate-800 bg-slate-900/60 p-4">
      <h2 className="text-sm font-semibold text-slate-100">Quick coping</h2>
      <p className="mt-1 text-xs text-slate-400">
        Need a reset before journaling?
      </p>
      <div className="mt-3 space-y-2 text-xs">
        <button className="flex w-full items-center justify-between rounded-lg border border-slate-800 bg-slate-950/60 px-3 py-2 text-left hover:border-emerald-400">
          <span className="flex flex-col">
            <span className="font-medium text-slate-100">Box breathing</span>
            <span className="text-[0.7rem] text-slate-400">
              4–4–4–4 guided breaths
            </span>
          </span>
          <span>💨</span>
        </button>
        <button className="flex w-full items-center justify-between rounded-lg border border-slate-800 bg-slate-950/60 px-3 py-2 text-left hover:border-emerald-400">
          <span className="flex flex-col">
            <span className="font-medium text-slate-100">
              5–4–3–2–1 grounding
            </span>
            <span className="text-[0.7rem] text-slate-400">
              Senses-based reset
            </span>
          </span>
          <span>🌱</span>
        </button>
      </div>
    </div>
  );
};
