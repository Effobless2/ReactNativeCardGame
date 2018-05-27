/*Sent from Server*/

export const BEGIN         = 'begin';
export const CONFIRM_CARD  = 'confirm_card';
export const CONNECTION    = 'connection';
export const EJECTED       = 'ejected'
export const NEW_PLAYER    = "new_player";
export const NEW_PUBLIC    = 'new_public';
export const NEW_ROOM      = 'new_room';
export const NEW_USER      = 'new_user';
export const PLAYER_HAS_PLAYED = "player_has_played";
export const REMOVE_PLAYER = 'remove_player';
export const REMOVE_PUBLIC = 'remove_public';
export const REMOVE_ROOM   = 'remove_room';
export const REMOVE_USER   = 'remove_user';
export const RECEIVE_HAND  = 'receive_hand';
export const DISCOVER      = "discover";
export const ROUND_WON    = "round_won";
export const LOOSE         = "loose";

/*Sent to Server*/

export const CREATE_ROOM   = 'create_room';
export const ADD_PLAYER    = 'add_player';
export const ADD_PUBLIC    = 'add_public';
export const ESCAPE_PLAYER = 'escape_player';
export const ESCAPE_PUBLIC = 'escape_public';
export const CARD_PLAYED   = 'card_played';

/*Inside the App*/

export const SELECT_ROOM = 'select_room';