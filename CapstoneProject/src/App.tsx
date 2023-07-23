import './App.css';
import { BrowserRouter as Router } from "react-router-dom";
import Login from './pages/LoginPage';
import Orders from './pages/OrdersPage';
import LiveLogs from './pages/LiveLogs.tsx';
import Users from "./pages/Users.tsx";
import SocialLogin from "./pages/SocialLogin.tsx";
import { useEffect, useState } from "react";
import { useSignalRService } from "./context/SignalRContext.tsx";
import { Provider } from 'react-redux';
import store from './store';
import ProtectedRoute from "./components/ProtectedRoute.tsx";
import { AppUserContext } from "./context/StateContext.tsx";
import { LocalUser } from "./types/AuthTypes.ts";
import { useNavigate, Routes, Route } from "react-router-dom";

function AuthHandler() {
    const navigate = useNavigate();

    useEffect(() => {
        const jwtJson = localStorage.getItem("softwarehouse_user");
        if (!jwtJson) {
            navigate("/");
        }
    }, [navigate]);

    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/social-login" element={<SocialLogin />} />
            <Route path="/orders" element={<ProtectedRoute><Orders /></ProtectedRoute>} />
            <Route path="/users" element={<ProtectedRoute><Users /></ProtectedRoute>} />
            <Route path="/livelogs" element={<ProtectedRoute><LiveLogs /></ProtectedRoute>} />
        </Routes>
    );
}

function App() {
    const { startConnection } = useSignalRService();

    const [appUser, setAppUser] = useState<LocalUser | undefined>(undefined);

    useEffect(() => {
        startConnection().catch(error => {
            console.error("SignalR bağlantısı başlatılamadı:", error);
        });
    }, [startConnection]);

    return (
        <AppUserContext.Provider value={{ appUser, setAppUser }}>
            <Provider store={store}>
                <Router>
                    <AuthHandler />
                </Router>
            </Provider>
        </AppUserContext.Provider>
    )
}

export default App;