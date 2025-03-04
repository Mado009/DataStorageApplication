import axios from "axios";

const API_URL = "http://localhost:5087/api/upload";

export async function uploadFile(file, dropdown, inputText, freeText) {
    try {
        const token = localStorage.getItem("token");
        if (!token) {
            localStorage.removeItem("token");
            return { error: "Unauthorized. Please log in again." };
        }

        const formData = new FormData();
        formData.append("file", file);
        formData.append("dropdown", dropdown);
        formData.append("inputText", inputText);
        formData.append("freeText", freeText);

        console.log("🚀 Uploading file with Axios...", formData);

        const response = await axios.post(API_URL, formData, {
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "multipart/form-data"
            }
        });

        return { success: true, data: response.data };

    } catch (error) {
        return { error: error.response ? error.response.data : "Upload failed!" };
    }
}
