// configureStore.js
import { applyMiddleware, createStore, compose } from 'redux';
import rootReducer from './rootReducer';
import rootInitialState from './RootState';

export default function configureStore(initialState = rootInitialState) {

  const enhancers = [
    createInjectorsEnhancer({
      createReducer: rootReducer,
    })
  ]
 
  
  const store = createStore(
    rootReducer,
    initialState,
  );
  return store;
}
