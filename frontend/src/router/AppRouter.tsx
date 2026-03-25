// src/router/AppRouter.tsx
import React from "react";
import {Routes, Route, Navigate} from "react-router-dom";
import {AppShell} from "../AppShell";
import {TodayPage} from "../pages/TodayPage";
import {HistoryPage} from "../pages/HistoryPage";
import {CopingPage} from "../pages/CopingPage";
import {SettingsPage} from "../pages/SettingsPage";
import {HealthCheckPage} from "../pages/HealthCheck.tsx";

export const AppRouter: React.FC = () => {
    return (
        <AppShell>
            <Routes>
                <Route path="/" element={<Navigate to="/today" replace/>}/>
                <Route path="/today" element={<TodayPage/>}/>
                <Route path="/history" element={<HistoryPage/>}/>
                <Route path="/coping" element={<CopingPage/>}/>
                <Route path="/settings" element={<SettingsPage/>}/>
                <Route path="/health" element={<HealthCheckPage/>}/>
            </Routes>
        </AppShell>
    );
};
