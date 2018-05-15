import { createStore } from 'redux';
import reducers from '../reducers';

const store = () => {
    console.log("Création store");
    return createStore(reducers);};

export default store();