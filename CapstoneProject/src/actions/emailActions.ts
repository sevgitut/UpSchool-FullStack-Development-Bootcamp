import { SetEmailAction } from '../types';

export const setEmail = (email: string): SetEmailAction => ({
    type: 'SET_EMAIL',
    payload: email
});