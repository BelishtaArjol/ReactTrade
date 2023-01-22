import AuthManager from "../../../utils/authManager";
import { AppThunk } from "../../redux/appThunk";
import { navigateTo } from "../navigation/navigation.store";
import { setUser } from "./user.store";
import ILoginRequest from "../../../interfaces/ILoginRequest";
import IUser from "../../../interfaces/IUser";

export const onLogin =
  (payload: ILoginRequest): AppThunk =>
    async (dispatch) => {
      debugger;
      try {
        debugger;
        const response = await AuthManager.loginWithCredentials(payload);
        if (response.user && response.token) {
          dispatch(setUser(response.user));
          dispatch(navigateTo("/"));
        }
      } catch (err: any) {
        debugger;
        Error(err.message);
      }
    };

export const onRegister =
  (payload: IUser): AppThunk =>
    async () => {
      try {
        const response = await AuthManager.register({ ...payload });
        return response;
      } catch (err: any) {
        Error(err.message);
      }
    };
