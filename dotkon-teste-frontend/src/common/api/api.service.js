import { API_URL } from '@/common/config';
import axios from 'axios';
import { createApp } from 'vue';
import VueAxios from 'vue-axios';

const app = createApp();

app.use(VueAxios, axios);

const ApiService = {
  init() {
    app.axios.defaults.baseURL = API_URL;
    app.axios.interceptors.request.use((config) => {
      const token = localStorage.getItem('AUTH_TOKEN');
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    }, (error) => {
      return Promise.reject(error);
    });
  },

  get(resource) {
    this.init();
    return app.axios.get(`${resource}`)
  },

  post(resource, params) {
    this.init();
    return app.axios.post(`${resource}`, params)
  },

  put(resource, params) {
    this.init();
    return app.axios.put(`${resource}`, params)
  },

  delete(resource) {
    this.init();
    return app.axios.delete(`${resource}`)
  }
}

export default ApiService;