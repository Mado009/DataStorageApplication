import { createRouter, createWebHistory } from "vue-router";
import LoginPage from "@/components/LoginPage.vue";
import UploadForm from "@/components/UploadForm.vue";

const routes = [
    { path: "/", component: LoginPage },
    { path: "/upload", component: UploadForm, meta: { requiresAuth: true } },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const token = localStorage.getItem("token");

    if (to.meta.requiresAuth && !token) {
        next("/");
    } else {
        next();
    }
});

export default router;
