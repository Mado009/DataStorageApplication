<template>
    <div class="login-container">
        <div class="login-card">
            <h2>Login</h2>
            <form @submit.prevent="handleLogin">
                <input type="text" v-model="username" placeholder="Username" required />
                <input type="password" v-model="password" placeholder="Password" required />
                <button type="submit">Login</button>
            </form>
            <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
        </div>
    </div>
</template>

<script>
import { login } from "@/services/auth";

export default {
    data() {
        return {
            username: "",
            password: "",
            errorMessage: "",
        };
    },
    methods: {
        async handleLogin() {
            try {
                await login(this.username, this.password);
                this.$router.push("/upload"); // Redirect to upload page after login
            } catch (error) {
                this.errorMessage = error.message;
            }
        },
    },
};
</script>

<style scoped>
/* Centering the login container */
.login-container {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background: #f4f4f4;
}

/* Styled login card */
.login-card {
    background: #fff;
    padding: 2rem;
    border-radius: 10px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    text-align: center;
    width: 100%;
    max-width: 400px;
}

/* Headline */
h2 {
    margin-bottom: 1rem;
    color: #333;
}

/* Input styles */
input {
    width: 100%;
    padding: 12px;
    margin: 10px 0;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 16px;
    transition: border 0.3s ease-in-out;
}

/* Input focus effect */
input:focus {
    border-color: #007bff;
    outline: none;
}

/* Submit button */
button {
    width: 100%;
    padding: 12px;
    margin-top: 10px;
    background: #007bff;
    color: #fff;
    font-size: 16px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background 0.3s;
}

/* Button hover effect */
button:hover {
    background: #0056b3;
}

/* Error message */
.error-message {
    color: #d9534f;
    margin-top: 10px;
}
</style>
