import ApiService from "@/common/api/api.service";

const servicesPost = {
  async adicionar(URL, params) {
    try {
      let response = await ApiService.post(URL, params);

      return response
    } catch(e) {
      console.log(e)
    }
  },
  async listar(URL) {
    try {
      let response = await ApiService.get(URL);

      return response
    } catch(e) {
      console.log(e)
    }
  },
  async deletar(URL) {
    try {
      let response = await ApiService.delete(URL);

      return response
    } catch(e) {
      console.log(e)
    }
  },
  async editar(URL, params) {
    try {
      let response = await ApiService.put(URL, params);

      return response
    } catch(e) {
      console.log(e)
    }
  }
}

export { servicesPost }