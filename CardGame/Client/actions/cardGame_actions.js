import {
    ADD_PLAYER,
    ADD_PUBLIC,
    BEGIN,
    CARD_PLAYED,
    CONFIRM_CARD,
    CONNECTION,
    EJECTED,
    CREATE_ROOM,
    ESCAPE_PLAYER,
    ESCAPE_PUBLIC,
    NEW_PLAYER,
    NEW_PUBLIC,
    NEW_ROOM,
    NEW_USER,
    PLAYER_HAS_PLAYED,
    RECEIVE_HAND,
    REMOVE_PLAYER,
    REMOVE_PUBLIC,
    REMOVE_ROOM,
    REMOVE_USER,
    SELECT_ROOM,
    DISCOVER,
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

export const confirmCard = datas =>{
    return {type: CONFIRM_CARD, payload: datas};
};

export const receiveHand = datas =>{
    return {type: RECEIVE_HAND, payload: datas};
};

export const playerHasPlayed = datas =>{
    return {type: PLAYER_HAS_PLAYED, payload: datas};
};

export const discover = datas =>{
    return {type: DISCOVER, payload: datas};
};