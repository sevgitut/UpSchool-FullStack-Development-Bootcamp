import {useContext, useEffect, useState} from 'react';
import { Link } from "react-router-dom";
import { Grid } from "semantic-ui-react";
import { useSignalRService } from '../context/SignalRContext';
import {AppUserContext} from "../context/StateContext.tsx";

type CrawlerLogDto = {
    message: string;
    id: string;
};

function LiveLogs() {
    const [logs, setLogs] = useState<CrawlerLogDto[]>([]);

    const { connection,connectionStarted } = useSignalRService();

    const { appUser } = useContext(AppUserContext);

    useEffect(() => {
        if (!connection) {
            console.error("Connection failed!");
            return;
        }

        if (connectionStarted) {
            console.log("Connected successfully.");

            const handleNewCrawlerLogAdded = (crawlerLogDto: CrawlerLogDto) => {
                setLogs(prevLogs => [...prevLogs, crawlerLogDto]);
            };

            connection.on("NewCrawlerLogAdded", handleNewCrawlerLogAdded);

            return () => {
                connection.off("NewCrawlerLogAdded", handleNewCrawlerLogAdded);
            };

        } else {
            console.error("Connection not started yet.");
        }

    }, [connection, connectionStarted]);

    return (
        <Grid textAlign='center' style={{ height: '100vh',
            position: 'fixed',
            width: '100%',
            backgroundImage: `url(/bg.jpg)`,
            backgroundSize: 'cover',
            backgroundRepeat: 'no-repeat',
            backgroundAttachment: 'fixed',
            backgroundPosition:'center'}} >
            <Grid.Column style={{ maxWidth: 450 }}>
                <header className="header">
                    <nav className="nav">
                        <a href="#" className="nav_logo">SoftwareHouse</a>
                        <ul className="nav_items">
                            <li className="nav_item">
                                <a href="/" className="nav_link">Home</a>
                                <Link to="/orders" className="nav_link">Orders</Link>
                                <Link to="/users" className="nav_link">Users</Link>
                                <Link to="/livelogs" className="nav_link">LiveLogs</Link>
                            </li>
                        </ul>
                        { appUser ? ( // appUser değişkenini kontrol edin
                            <img src="/user.png" alt="User Icon" className="user-icon" />
                        ) : (
                            <button className="button" id="form-open" >Login</button>
                        )}
                    </nav>
                </header>
                <div>
                    <section className={`home show specificComponent`}>
                        <div className="form_container" >
                            <h1 className="minBackground" style={{marginBottom:'20px'}}>Crawler Logs</h1>
                            <div style={{ backgroundColor: '#16141E', color: '#7d2ae8', padding: '10px', fontFamily: 'Courier', whiteSpace: 'pre-line' }}>
                                {logs.map(log => `${new Date().toLocaleString()}: ${log.message}`).join('\n')}
                            </div>
                        </div>
                    </section>
                </div>
            </Grid.Column>
        </Grid>

    );
}

export default LiveLogs;