import ApiService from "@/common/api/api.service";

const serviceLogin = {
  async login(URL, params) {
    try {
      let response = await ApiService.post(URL, params);

      return response
    } catch(e) {
      console.log(e)
    }
  }
}

export { serviceLogin }