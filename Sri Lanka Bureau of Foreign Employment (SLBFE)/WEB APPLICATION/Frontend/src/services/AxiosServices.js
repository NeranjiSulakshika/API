const axios = require('axios').default

export default class AxiosServices {
  post(url, data, isRequiredHeader = false, header) {
    return axios.post(url, data, isRequiredHeader && header)
  }

  get(url, IsRequired = false, Header) {
    return axios.get(url, IsRequired && Header);
  }

  delete(url, IsRequired = false, Header) {
    return axios.delete(url, IsRequired && Header);
  }
}
