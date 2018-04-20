import { AsyncStorage } from "react-native";

class Authentication {
    url = "";
    
    signIn(username, password) {
        headers = {
            method: 'post',
            mode: 'no-cors',
            body: "username=" + username + "&password=" + password,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8' }
        }
        console.log(this.url);
        console.log(headers);
        return fetch(this.url, headers)
        .then(response => {
            if (!response.ok) throw new Error(response.status);
            return response.json();
        })
        .then(token => {
            AsyncStorage.setItem("access_token", token.access_token);
        });
    }

    signOut() {
        AsyncStorage.removeItem("access_token");
    }

    getAccessToken() {
        return AsyncStorage.getItem("access_token");
    }
}

export default new Authentication();