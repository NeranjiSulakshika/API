const axios = require("axios").default;

export default class AxiosServices {

  // SignIn & SignUp
  post(url, data, isRequiredHeader = false, header) {
    return axios.post(url, data, isRequiredHeader && header);
  }

  // Get Citizen List
  get(url, IsRequired = false, Header) {
    return axios.get(url, IsRequired && Header);
  }

  // Delete Citizen
  delete(url, IsRequired = false, Header) {
    return axios.delete(url, IsRequired && Header);
  }
}
