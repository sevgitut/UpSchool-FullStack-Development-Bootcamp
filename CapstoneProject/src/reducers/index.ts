import { combineReducers } from 'redux';
import emailReducer from './emailReducer';
import { RootState } from '../types';


const rootReducer = combineReducers<RootState>({
    email: emailReducer
    // Diğer reducer'larınızı burada ekleyebilirsiniz...
});

export default rootReducer;