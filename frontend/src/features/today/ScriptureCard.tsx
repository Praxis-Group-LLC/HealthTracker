// src/features/today/ScriptureCard.tsx
import React from "react";

export const ScriptureCard: React.FC = () => {
  return (
    <div className="rounded-xl border border-slate-800 bg-slate-900/60 p-4 text-xs">
      <h2 className="text-sm font-semibold text-slate-100">Scripture corner</h2>
      <p className="mt-1 text-slate-400">
        When scripture mode is enabled, this area can surface passages tied to
        endurance, peace, and hope.
      </p>
    </div>
  );
};
