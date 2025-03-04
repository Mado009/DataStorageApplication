import axios from "axios";

const API_URL = "http://localhost:5087/api/auth/";

export async function login(username, password) {
    try {
        const response = await axios.post(API_URL + "login", { username, password });

        if (response.data.token) {
            localStorage.setItem("token", response.data.token);
        }

        return response.data;
    } catch (error) {

        return Promise.reject(error);
    }
}

export function logout() {
    localStorage.removeItem("token");
}
