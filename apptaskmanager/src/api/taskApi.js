import axios from "axios";

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL
});

export const getTasks = () => api.get("/Tasks");
export const getTask = (id) => api.get(`/Tasks/${id}`);
export const createTask = (payload) => api.post("/Tasks", payload);
export const updateTask = (id, payload) => api.put(`/Tasks/${id}`, payload);
export const deleteTask = (id) => api.delete(`/Tasks/${id}`);
