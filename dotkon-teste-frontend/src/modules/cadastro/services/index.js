import ApiService from "@/common/api/api.service";

const servicesUser = {
  async adicionar(URL, params) {
    try {
      let response = await ApiService.post(URL, params);

      return response
    } catch(e) {
      console.log(e)
    }
  }
}

export { servicesUser }