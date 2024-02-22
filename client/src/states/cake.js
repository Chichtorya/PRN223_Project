import redux,{createStore   } from 'redux'
import reduxLogger from 'redux-logger'



const CAKE_ORDERED= 'CAKE_ORDERED';
const CAKE_RESTORED = 'CAKE_RESTORED';
const ICECREAM_ORDERED= 'ICECREAM_ORDERED';
const ICECREAM_RESTORED = 'ICECREAM_RESTORED';  


const logger= reduxLogger.createLogger();
function orderCake(){
    return{
        type: CAKE_ORDERED,
        payload:1,
    }
}
function cakeReStore(amount){
    return{
        type: CAKE_RESTORED,
        payload:amount,
    }
}

function orderIceCream(qty =1){
    return{
        type: ICECREAM_ORDERED,
        payload:qty,
    }
}
function IceCreamReStore(amount){
    return{
        type: ICECREAM_RESTORED,
        payload:amount,
    }
}
const initialState={
    numOfCake:10,
    numOfIceCream:20,
    
}


const reducer =(state=initialState,action)=>{
    switch(action.type){
        case CAKE_ORDERED:
            return{
                ...state,
                numOfCake: state.numOfCake-1
            }
            
    case CAKE_RESTORED:
        return{
            ...state,
            numOfCake: state.numOfCake + action.payload,
        }
        }

}

const store = createStore(reducer);

console.log("st" ,store.getState());

store.subscribe(()=>{console.log("state update " , store.getState())})


store.dispatch(orderCake());