import {
    CONNECTION,
    NEW_ROOM,
    REMOVE_ROOM,
    NEW_USER,
    REMOVE_USER,
    CREATE_ROOM,
    ROOM_CREATED
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
    return {type: NEW_USER, payload: datas};
};

export const removeUser = datas =>{
    return {type: REMOVE_USER, payload: datas};
};

export const createRoom = () => {
    return {type: CREATE_ROOM};
};

export const roomCreated = datas => {
    return {type: ROOM_CREATED, payload: datas};
};