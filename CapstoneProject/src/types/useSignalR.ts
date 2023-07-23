import { useState, useEffect } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

const BASE_URL = import.meta.env.VITE_CRAWLERHUB_URL;
const useSignalR = (url: string) => {
    const [connection, setConnection] = useState<HubConnection | null>(null);

    const [connectionStarted, setConnectionStarted] = useState<boolean>(false);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl(BASE_URL)
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, [url]);

    const startConnection = async () => {
        if (connection && !connectionStarted) {
            try {
                await connection.start();
                setConnectionStarted(true);
                console.log('Connection started successfully.');
            } catch (error) {
                console.error('Failed to start connection: ', error);
            }
        }
    };

    const stopConnection = async () => {
        if (connection) {
            try {
                await connection.stop();
                setConnectionStarted(false);
                console.log('Connection stopped successfully.');
            } catch (error) {
                console.error('Failed to stop connection: ', error);
            }
        }
    };

    const sendCommand = async (command: string, ...args: any[]) => {
        if (connection) {
            try {
                await connection.invoke(command, ...args);
                console.log(`Command ${command} sent successfully.`);
            } catch (error) {
                console.error(`Failed to send command ${command}: `, error);
            }
        }
    };

    return { startConnection, stopConnection, sendCommand, connection, connectionStarted };
};

export default useSignalR;