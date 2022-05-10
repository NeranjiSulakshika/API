import AxiosServices from "./AxiosServices";
import Configurations from "../configurations/Configurations";

const axiosServices = new AxiosServices();

export default class AuthServices {

  // User SignUp
  SignUp(data) {
    return axiosServices.post(Configurations.SignUp, data, false);
  }

  // User SignIn
  SignIn(data) {
    return axiosServices.post(Configurations.SignIn, data, false);
  }

  // User Admin SignIn
  AdminSignIn(data) {
    return axiosServices.post(Configurations.AdminSignIn, data, false);
  }

  // Get Citizen List
  GetCitizenList(data) {
    return axiosServices.get(Configurations.GetCitizenList, data, false);
  }

  // Delete Citizen
  DeleteCitizen(data) {
    console.log('Url: ', Configurations.DeleteCitizen + data.userId);
    return axiosServices.delete(Configurations.DeleteCitizen + data.userId, data, false);
  }
}
