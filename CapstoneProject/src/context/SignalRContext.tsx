import React, {createContext, useContext, ReactNode} from 'react';
import useSignalR from '../types/useSignalR';
import {HubConnection} from "@microsoft/signalr";

const BASE_URL = import.meta.env.VITE_CRAWLERHUB_URL;

type SignalRContextType = {
    startConnection: () => Promise<void>;
    stopConnection: () => Promise<void>;
    sendCommand: (command: string, ...args: any[]) => Promise<void>;
    connection: HubConnection | null;
    connectionStarted: boolean;
};

const SignalRContext = createContext<SignalRContextType | undefined>(undefined);

type SignalRProviderProps = {
    children: ReactNode;
};

export const SignalRProvider: React.FC<SignalRProviderProps> = ({ children }) => {
    const signalRValues = useSignalR(BASE_URL);
    return <SignalRContext.Provider value={signalRValues}>{children}</SignalRContext.Provider>;
};

export const useSignalRService = (): SignalRContextType => {
    const context = useContext(SignalRContext);
    if (!context) {
        throw new Error("useSignalRService must be used within a SignalRProvider");
    }
    return context;
};