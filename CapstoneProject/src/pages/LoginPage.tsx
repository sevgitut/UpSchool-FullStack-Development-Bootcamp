import React, { useContext, useState } from "react";
import {Grid} from "semantic-ui-react";
import {AuthLoginCommand, LocalJwt} from "../types/AuthTypes.ts";
import {AppUserContext} from "../context/StateContext.tsx";
import './LoginPage.css';
import {Link, useNavigate} from "react-router-dom";
import api from "../utils/axiosInstance.ts";
import { getClaimsFromJwt } from "../utils/jwtHelper.ts";
import { toast } from "react-toastify";
import { setEmail } from "../actions/emailActions.ts";
import { useDispatch } from 'react-redux'

const BASE_URL = import.meta.env.VITE_API_URL;

const LoginPage = () => {
    const [isFormOpen, setFormOpen] = useState(false);

    const [isSignUpActive, setSignUpActive] = useState(false);

    const navigate = useNavigate();

    const { appUser, setAppUser } = useContext(AppUserContext);

    const [authLoginCommand, setAuthLoginCommand] = useState<AuthLoginCommand>({email:"",password:""});

    const dispatch = useDispatch();

    const [passwordVisibility, setPasswordVisibility] = useState({
        login: false,
        signup: false,
        confirm: false
    });


    const handleSubmit = async (event:React.FormEvent) => {

        event.preventDefault();

        try {
            const response = await api.post("/Authentication/Login", authLoginCommand);

            if(response.status === 200){
                const accessToken = response.data.accessToken;
                const { uid, email, given_name, family_name } = getClaimsFromJwt(accessToken);
                const expires:string = response.data.expires;

                setAppUser({ id:uid, email, firstName:given_name, lastName:family_name, expires, accessToken });

                dispatch(setEmail(email));

                const localJwt:LocalJwt ={
                    accessToken,
                    expires
                }

                localStorage.setItem("softwarehouse_user",JSON.stringify(localJwt));
                navigate("/");

            } else{
                toast.error(response.statusText);
            }
        } catch (error) {
            toast.error("Something went wrong!");
        }
    }

    const toggleForm = () => setFormOpen(!isFormOpen);
    const switchForm = (event: React.MouseEvent) => {
        event.preventDefault();
        setSignUpActive(!isSignUpActive);
    };
    const togglePasswordVisibility = (field: string) => {
        setPasswordVisibility(prevState => ({...prevState, [field]: !prevState[field]}));
    };

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAuthLoginCommand({
            ...authLoginCommand,
            [event.target.name]: event.target.value
        });
    }

    const onGoogleLoginClick = (e:React.FormEvent) => {
        e.preventDefault();

        window.location.href = `${BASE_URL}/Authentication/GoogleLoginInStart`;

    };

    return (

        <Grid textAlign='center' style={{ height: '100vh' }}>
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
                        <Link to="/users">
                            <img src="/user.png" alt="User Icon" className="user-icon" />
                        </Link>
                    ) : (
                        <button className="button" id="form-open" onClick={toggleForm}>Login</button>
                    )}
                </nav>
            </header>
            <section className={`home ${isFormOpen ? "show" : ""}`}>
                <div className="form_container">
                    <i className="uil uil-times form_close" onClick={toggleForm}></i>
                    <div style={{textAlign:'center'}} className={`form ${isSignUpActive ? "active" : ""}`}>
                        <form action="#">
                            <h2  className="minBackground">Login</h2>
                            <div className="input_box">
                                <input type="email" placeholder="Enter your email" required/>
                                <i className="uil uil-envelope-alt email"></i>
                            </div>
                            <div className="input_box">
                                <input
                                    type={passwordVisibility.login ? "text" : "password"}
                                    placeholder="Enter your password"
                                    required
                                />
                                <i className="uil uil-lock password"></i>
                                <i
                                    className={`uil ${passwordVisibility.login ? "uil-eye" : "uil-eye-slash"} pw_hide`}
                                    onClick={() => togglePasswordVisibility('login')}
                                ></i>
                            </div>
                            <div className="option_field">
                            <span className="checkbox">
                                <input type="checkbox" id="check"/>
                                <label htmlFor="check">Remember me</label>
                            </span>
                                <a href="#" className="forgot_pw">Forgot password?</a>
                            </div>
                            <button className="button" onClick={onGoogleLoginClick}>
                                <img src="./google-logo.jpeg" style={{width: '20px', height: '20px'}}/>
                                Sign in with Google
                            </button>
                            <div className="login_signup">Don't have an account? <a href="#" id="signup" onClick={switchForm}>Signup</a></div>
                        </form>
                    </div>
                    <div className={`form signup_form ${!isSignUpActive ? "active" : ""}`}>
                        <form action="#" onSubmit={handleSubmit}>
                            <h2>Signup</h2>
                            <div className="input_box">
                                <input type="email" placeholder="Enter your email" required onChange={handleInputChange}/>
                                <i className="uil uil-envelope-alt email"></i>
                            </div>
                            <div className="input_box">
                                <input
                                    type={passwordVisibility.signup ? "text" : "password"}
                                    placeholder="Create password"
                                    required
                                />
                                <i className="uil uil-lock password"></i>
                                <i
                                    className={`uil ${passwordVisibility.signup ? "uil-eye" : "uil-eye-slash"} pw_hide`}
                                    </div>onClick={() => togglePasswordVisibility('signup')}>
                                </i>
                            </div>
                            <div className="input_box">
                                <input
                                    type={passwordVisibility.confirm ? "text" : "password"}
                                    placeholder="Confirm password"
                                    required
                                />
                                <i className="uil uil-lock password"></i>
                                <i
                                    className={`uil ${passwordVisibility.confirm ? "uil-eye" : "uil-eye-slash"} pw_hide`}
                                    </div>onClick={() => togglePasswordVisibility('confirm')}>

                                </i>
                            </div>
                            <button className="button">Signup Now</button>
                            <div className="login_signup">Already have an account? <a href="#" id="login" onClick={switchForm}>Login</a></div>
                        </form>
                    </div>
                </div>
            </section>
        </Grid.Column>
        </Grid>
    );
}

export default LoginPage;