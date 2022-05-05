import AxiosServices from "./AxiosServices";
import Configurations from "../configurations/Configurations";

const axiosServices = new AxiosServices();

export default class AuthServices {
  SignUp(data) {
    return axiosServices.post(Configurations.SignUp, data, false);
  }

  SignIn(data) {
    return axiosServices.post(Configurations.SignIn, data, false);
  }

  AdminSignIn(data) {
    return axiosServices.post(Configurations.AdminSignIn, data, false);
  }

  GetCitizenList(data) {
    return axiosServices.get(Configurations.GetCitizenList, data, false);
  }

  DeleteCitizen(data) {
    return axiosServices.delete(
      Configurations.DeleteCitizen,
      { data: { userId: data.userId } },
      false,
    )
  }
}
