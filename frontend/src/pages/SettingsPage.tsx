// src/pages/SettingsPage.tsx
import React from "react";

export const SettingsPage: React.FC = () => {
  return (
    <div className="rounded-xl border border-slate-800 bg-slate-900/60 p-4 space-y-4">
      <h2 className="text-sm font-semibold text-slate-100">Settings</h2>
      <p className="text-xs text-slate-400">
        Time zone, reminder time, scripture mode, export, etc.
      </p>
      {/* You’ll wire forms here */}
    </div>
  );
};
