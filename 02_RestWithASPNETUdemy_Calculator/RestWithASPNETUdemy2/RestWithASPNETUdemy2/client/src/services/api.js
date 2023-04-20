import axios from "axios";

const api = axios.create({
    baseURL : 'http://localhost44300'
})

export default api;