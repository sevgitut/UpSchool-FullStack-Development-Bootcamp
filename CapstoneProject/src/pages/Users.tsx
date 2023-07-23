import React, {useState, ChangeEvent, FormEvent, useContext} from 'react';
import {Link} from "react-router-dom";
import {Grid} from "semantic-ui-react";
import {AppUserContext} from "../context/StateContext.tsx";
import {getClaimsFromJwt} from "../utils/jwtHelper.ts";
import { Table } from "semantic-ui-react";
function UsersPage() {
    const { appUser } = useContext(AppUserContext);
    const userInfo = getClaimsFromJwt(appUser?.accessToken);


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
                        { appUser ? (
                            <img src="/user.png" alt="User Icon" className="user-icon" />
                        ) : (
                            <button className="button" id="form-open" >Login</button>
                        )}
                    </nav>
                </header>
                <section className={`home show usersComponent`}>
                    <div className="form_container ">
                        <div>
                            <h1 style={{marginBottom:'20px'}} className="minBackground">Active Users</h1>
                            <form>
                                <Table celled style={{ textAlign: "center" }} className="userTable">
                                    <Table.Header>
                                        <Table.Row>
                                            <Table.HeaderCell>No</Table.HeaderCell>
                                            <Table.HeaderCell>Name</Table.HeaderCell>
                                            <Table.HeaderCell>E-mail</Table.HeaderCell>
                                        </Table.Row>
                                    </Table.Header>
                                    <Table.Body>
                                        <Table.Row>
                                            <Table.Cell>1</Table.Cell>
                                            <Table.Cell>{userInfo?.given_name} {userInfo?.family_name}</Table.Cell>
                                            <Table.Cell>{userInfo?.email}</Table.Cell>
                                        </Table.Row>
                                    </Table.Body>
                                </Table>
                            </form>
                        </div>
                    </div>
                </section>
            </Grid.Column>
        </Grid>

    );
}

export default UsersPage;