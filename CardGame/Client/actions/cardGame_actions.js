import { CONNECTION } from './types';

export const connection = datas =>{
    console.log("connection Passage Action Redux");
    return {type: CONNECTION, payload: datas}
};