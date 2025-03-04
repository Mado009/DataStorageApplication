<template>
    <div class="container mt-4">
        <!-- Header Section -->
        <div class="d-flex justify-content-between align-items-center mb-4 bg-light p-3 rounded shadow-sm">
            <h2 class="m-0 text-primary">Data uploader</h2>
            <button @click="logout" class="btn btn-danger">Logout</button>
        </div>

        <!-- Main Form Section -->
        <div class="card shadow-lg p-4">
            <form @submit.prevent="uploadFile">
                <div class="row">
                    <!-- Left Side: File Upload & Dropdown -->
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="file" class="form-label">Image</label>
                            <div class="image-upload-box" @click="triggerFileInput">
                                <img v-if="previewImage" :src="previewImage" class="img-fluid" />
                                <i v-else class="bi bi-image text-muted">Drag and drop</i>
                            </div>
                            <input type="file" id="file" class="d-none" @change="handleFile" required />
                            <!-- <button class="btn mt-3 w-100" @click.prevent="triggerFileInput">
                                <i class="bi bi-upload"></i> Upload
                            </button> -->
                        </div>


                    </div>

                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="inputText" class="form-label">Name of product</label>
                            <input type="text" id="inputText" v-model="inputText" class="form-control" maxlength="100"
                                required />
                        </div>

                        <div class="mb-3">
                            <label for="freeText" class="form-label">Description</label>
                            <textarea id="freeText" v-model="freeText" class="form-control" maxlength="500" rows="3"
                                required></textarea>
                        </div>
                        <div class="mb-3">
                            <label for="dropdown" class="form-label">Category</label>
                            <select id="dropdown" v-model="dropdown" class="form-select" required>
                                <option value="" disabled>Select an option</option>
                                <option value="Option1">Option 1</option>
                                <option value="Option2">Option 2</option>
                            </select>
                        </div>
                    </div>
                </div>

                <!-- Submit Button -->
                <button type="submit" class="btn btn-primary w-100 mt-3">Send</button>
            </form>

            <!-- Success/Error Message -->
            <p v-if="message" class="alert alert-info mt-3 text-center">{{ message }}</p>
        </div>
    </div>
</template>

<script>
import { logout } from "@/services/auth";
import { uploadFile } from "@/services/uploadService";
export default {
    data() {
        return {
            file: null,
            dropdown: "",
            inputText: "",
            freeText: "",
            message: "",
            previewImage: null
        };
    },
    methods: {

        logout() {
            logout();
            this.$router.push("/");
        },
        handleFile(event) {
            const file = event.target.files[0];
            this.file = file;

            if (file) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    this.previewImage = e.target.result;
                };
                reader.readAsDataURL(file);
            }
        },
        triggerFileInput() {
            document.getElementById("file").click();
        },
        async uploadFile() {
            try {
                const token = localStorage.getItem("token");
                if (!token) {
                    this.$router.push("/");
                    return;
                }

                if (!this.file || !this.dropdown || !this.inputText || !this.freeText) {
                    this.message = "All fields are required.";
                    return;
                }

                const result = await uploadFile(this.file, this.dropdown, this.inputText, this.freeText);

                if (result.success) {
                    this.message = "File uploaded successfully!";
                } else {
                    console.error("Upload error:", result.error);
                    this.message = result.error || "File upload failed!";
                }

            } catch (error) {
                console.error("Upload failed:", error);
                this.message = "Upload failed. Please try again.";

            }
        }
    }
};
</script>

<style scoped>
.container {
    max-width: 800px;
}

label {
    font-size: 1rem;
    border-bottom: 1px solid #007bff;
    width: 100%;
    margin-bottom: 10px;
}

.card {
    border-radius: 10px;
    background: #f8f9fa;
}

h2 {
    color: #007bff;
}

.btn {
    font-size: 1rem;
    padding: 10px;
}

.alert {
    font-size: 0.9rem;
}

.image-upload-box {
    width: 100%;
    height: 250px;
    border: 2px dashed #ccc;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    background-color: #f9f9f9;
    border-radius: 10px;
}

.image-upload-box img {
    max-width: 100%;
    max-height: 100%;
    border-radius: 10px;
}

.image-upload-box i {
    font-size: 3rem;
}

@media (max-width: 768px) {
    .row {
        flex-direction: column;
    }

    .image-upload-box {
        width: 100%;
        height: 200px;
    }
}
</style>