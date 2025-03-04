import { createApp } from 'vue';
import App from './App.vue';
import axios from 'axios';
import router from './router';

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";

axios.defaults.baseURL = "http://localhost:5087";

axios.interceptors.request.use(config => {
    const token = localStorage.getItem("token");
    if (token) {
        config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

axios.interceptors.response.use(
    response => response,
    error => {
        if (error.response && error.response.status === 401) {
            localStorage.removeItem("token");
            router.push("/");
        }
        return Promise.reject(error);
    }
);

const app = createApp(App);
app.config.globalProperties.$axios = axios;
app.use(router);
app.mount('#app');
