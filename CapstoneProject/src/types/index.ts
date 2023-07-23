export interface EmailState {
    email: string;
}

export interface RootState {
    email: EmailState;
}

export interface SetEmailAction {
    type: 'SET_EMAIL';
    payload: string;
}