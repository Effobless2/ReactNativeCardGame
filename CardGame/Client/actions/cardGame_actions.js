import {
    CONNECTION,
    NEW_ROOM
} from './types';

export const connection = datas =>{
    return {type: CONNECTION, payload: datas}
};

export const newRoom = datas =>{
    return {type: NEW_ROOM, payload: datas};

}