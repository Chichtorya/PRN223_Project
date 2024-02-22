import { configureStore, createAction, createReducer } from '@reduxjs/toolkit';

// actions
const increasement = createAction('increasement');
const decreasement = createAction('decreasement');

const initialState = { count: 0 };

// Add missing commas here
const rootReducer = createReducer(initialState, {
  [increasement]: state => ({ count: state.count + 1 }),
  [decreasement]: state => ({ count: state.count - 1 })
});

const store = configureStore({
  reducer: rootReducer
});

export default store;
