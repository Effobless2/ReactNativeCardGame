import {
    CONNECTION,
    NEW_ROOM,
    REMOVE_ROOM,
    NEW_USER
} from './types';

export const connection = datas =>{
    return {type: CONNECTION, payload: datas};
};

export const newRoom = datas =>{
    return {type: NEW_ROOM, payload: datas};

};

export const removeRoom = datas =>{
    return {type: REMOVE_ROOM, payload: datas};
};

export const newUser = datas =>{
    console.log("Action newUser");
    return {type: NEW_USER, payload: datas};
};