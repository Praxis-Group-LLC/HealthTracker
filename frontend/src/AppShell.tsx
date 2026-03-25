// AppShell.tsx
import React from "react";
import { NavLink } from "react-router-dom";
import { useTheme } from "./theme/ThemeContext"; // adjust path as needed

type AppShellProps = {
  children: React.ReactNode;
};

export function AppShell({ children }: AppShellProps) {
  const { theme, toggleTheme } = useTheme();
  const isDark = theme === "dark";

  return (
    <div className="flex min-h-screen bg-zinc-50 text-zinc-900 dark:bg-zinc-950 dark:text-zinc-100">
      {/* Sidebar (desktop) */}
      <aside className="hidden md:flex md:w-64 flex-col border-r border-zinc-200 bg-white/80 backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/70">
        {/* ... logo + nav as before ... */}
        <div className="flex items-center gap-3 px-6 py-4 border-b border-zinc-200 dark:border-zinc-800">
          <div className="h-9 w-9 rounded-2xl bg-gradient-to-br from-violet-500 to-violet-300 shadow-md dark:from-violet-500 dark:to-violet-400" />
          <div>
            <div className="text-sm font-semibold tracking-tight">
              NeuroNest
            </div>
            <div className="text-xs text-zinc-500 dark:text-zinc-400">
              Gentle health tracking
            </div>
          </div>
        </div>

        <nav className="flex-1 px-3 py-4 space-y-1 text-sm">
          <NavItem icon="📝" label="Today" to="/today" />
          <NavItem icon="📊" label="History" to="/history" />
          <NavItem icon="💨" label="Coping tools" to="/coping" />
          <NavItem icon="⚙️" label="Settings" to="/settings" />
        </nav>

        <div className="px-4 py-4 border-t border-zinc-200 text-xs text-zinc-500 dark:border-zinc-800 dark:text-zinc-400">
          {/* ... device info ... */}
        </div>
      </aside>

      {/* Main column */}
      <div className="flex flex-1 flex-col">
        {/* Top bar */}
        <header className="flex items-center justify-between gap-4 border-b border-zinc-200 bg-white/80 px-4 py-3 backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/80 md:px-6">
          <div className="flex flex-col">
            <span className="text-[0.7rem] uppercase tracking-[0.25em] text-violet-500 dark:text-violet-300">
              Today
            </span>
            <h1 className="text-lg font-semibold tracking-tight md:text-xl">
              Daily check-in
            </h1>
          </div>

          <div className="flex items-center gap-3">
            <button className="hidden text-xs rounded-full border border-zinc-200 bg-zinc-100/70 px-3 py-1 text-zinc-700 shadow-sm hover:bg-violet-50 hover:border-violet-300 dark:border-zinc-700 dark:bg-zinc-900/80 dark:text-zinc-200 dark:hover:border-violet-500 md:inline-flex">
              Scripture mode
              <span className="ml-2 h-2 w-2 rounded-full bg-violet-500 dark:bg-violet-400" />
            </button>

            {/* Theme toggle */}
            <button
              type="button"
              onClick={toggleTheme}
              className="flex items-center gap-1 rounded-full border border-zinc-200 bg-zinc-100/70 px-3 py-1 text-xs text-zinc-700 shadow-sm hover:border-violet-300 hover:bg-violet-50 dark:border-zinc-700 dark:bg-zinc-900/80 dark:text-zinc-200 dark:hover:border-violet-500"
            >
              <span className="text-sm" aria-hidden="true">
                {isDark ? "🌙" : "☀️"}
              </span>
              <span className="hidden sm:inline">
                {isDark ? "Dark" : "Light"} mode
              </span>
            </button>
          </div>
        </header>

        {/* Content area */}
        <main className="flex-1 overflow-y-auto px-4 py-4 md:px-6 md:py-6">
          <div className="mx-auto max-w-6xl space-y-4 md:space-y-6">
            {children}
          </div>
        </main>

        {/* Bottom nav (mobile) */}
        <nav className="md:hidden fixed inset-x-0 bottom-0 z-20 border-t border-zinc-200 bg-white/95 backdrop-blur dark:border-zinc-800 dark:bg-zinc-950/95">
          <div className="flex h-14 items-stretch justify-around text-xs text-zinc-500 dark:text-zinc-400">
            <BottomNavItem icon="📝" label="Today" to="/today" />
            <BottomNavItem icon="📊" label="History" to="/history" />
            <BottomNavItem icon="💨" label="Coping" to="/coping" />
            <BottomNavItem icon="⚙️" label="Settings" to="/settings" />
          </div>
        </nav>
      </div>
    </div>
  );
}

type NavItemProps = {
  icon: string;
  label: string;
  to: string;
};

function NavItem({ icon, label, to }: NavItemProps) {
  return (
    <NavLink
      to={to}
      className={({ isActive }) =>
        [
          "flex w-full items-center gap-2 rounded-lg px-3 py-2 transition-colors",
          isActive
            ? "bg-violet-100 text-violet-800 dark:bg-violet-950/80 dark:text-violet-100"
            : "text-zinc-500 hover:bg-zinc-100 hover:text-zinc-900 dark:text-zinc-400 dark:hover:bg-zinc-900/70 dark:hover:text-zinc-100",
        ].join(" ")
      }
    >
      <span className="text-base">{icon}</span>
      <span>{label}</span>
    </NavLink>
  );
}

function BottomNavItem({ icon, label, to }: NavItemProps) {
  return (
    <NavLink
      to={to}
      className={({ isActive }) =>
        [
          "flex flex-1 flex-col items-center justify-center gap-0.5",
          isActive
            ? "text-violet-600 dark:text-violet-300"
            : "text-zinc-500 dark:text-zinc-400",
        ].join(" ")
      }
    >
      <span className="text-lg">{icon}</span>
      <span>{label}</span>
    </NavLink>
  );
}
