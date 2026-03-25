// src/pages/HistoryPage.tsx
import React from "react";

export const HistoryPage: React.FC = () => {
  return (
    <div className="rounded-xl border border-slate-800 bg-slate-900/60 p-4">
      <h2 className="text-sm font-semibold text-slate-100">History</h2>
      <p className="mt-1 text-xs text-slate-400">
        This is where you&apos;ll show past entries, filters, charts, etc.
      </p>
    </div>
  );
};
