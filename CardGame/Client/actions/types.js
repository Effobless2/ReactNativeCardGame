/*Sent from Server*/

export const CONNECTION    = 'connection';
export const EJECTED       = 'ejected'
export const NEW_PLAYER    = "new_player";
export const NEW_PUBLIC    = 'new_public';
export const NEW_ROOM      = 'new_room';
export const NEW_USER      = 'new_user';
export const REMOVE_PLAYER = 'remove_player';
export const REMOVE_PUBLIC = 'remove_public';
export const REMOVE_ROOM   = 'remove_room';
export const REMOVE_USER   = 'remove_user';

/*Sent to Server*/

export const CREATE_ROOM   = 'create_room';
export const ADD_PLAYER    = 'add_player';
export const ADD_PUBLIC    = 'add_public';
export const ESCAPE_PLAYER = 'escape_player';
export const ESCAPE_PUBLIC = 'escape_public';

/*Inside the App*/

export const SELECT_ROOM = 'select_room';