import { EmailState, SetEmailAction } from '../types';

const initialState: EmailState = {
    email: ''
};

const emailReducer = (state = initialState, action: SetEmailAction): EmailState => {
    switch (action.type) {
        case 'SET_EMAIL':
            return {
                ...state,
                email: action.payload
            };
        default:
            return state;
    }
};

export default emailReducer;