import {
    SELECT_ROOM,
    ADD_PUBLIC,
    BEGIN,
    CARD_PLAYED,
    CONNECTION,
    EJECTED,
    CREATE_ROOM,
    ESCAPE_PLAYER,
    ESCAPE_PUBLIC,
    NEW_PLAYER,
    NEW_PUBLIC,
    NEW_ROOM,
    NEW_USER,
    REMOVE_PLAYER,
    REMOVE_PUBLIC,
    REMOVE_ROOM,
    REMOVE_USER,
    ADD_PLAYER,
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

export const newPublic = datas => {
    return {type: NEW_PUBLIC, payload: datas};
};

export const newPlayer = datas =>{
    return {type: NEW_PLAYER, payload: datas};
};

export const removePublic = datas =>{
    return {type: REMOVE_PUBLIC, payload: datas};
};

export const removePlayer = datas =>{
    return {type: REMOVE_PLAYER, payload: datas};
};

export const addPublic = datas =>{
    return {type: ADD_PUBLIC, payload: datas};
};

export const addPlayer = datas =>{
    return {type: ADD_PLAYER, payload: datas};
};

export const escapePublic = datas =>{
    return {type: ESCAPE_PUBLIC, payload: datas};
};

export const escapePlayer = datas =>{
    return {type: ESCAPE_PLAYER, payload: datas};
};

export const ejected = datas =>{
    return {type: EJECTED, payload: datas};
};

export const selectRoom = datas =>{
    return {type: SELECT_ROOM, payload: datas};
};

export const begin = datas =>{
    return {type: BEGIN, payload: datas};
};

export const cardPlayed = datas =>{
    return {type: CARD_PLAYED, payload: datas};
};