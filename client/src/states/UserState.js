import React from "react";
export const userInitialState = {
    role: '',
    username: '',
    id:'',
    flag: false,
};

export const userReducer = (state = userInitialState, action) => {
    switch (action.type) {
        default:
            return state;
    }
};