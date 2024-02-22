// rootState.js
import { userInitialState } from './UserState';

const rootInitialState = {
    user: userInitialState,
    // Thêm các initialState khác nếu cần
};
const rootReducer = combineReducers({
    user: userReducer,
    // Thêm các reducer khác nếu cần
});
  
export {rootInitialState};

export default rootReducer;