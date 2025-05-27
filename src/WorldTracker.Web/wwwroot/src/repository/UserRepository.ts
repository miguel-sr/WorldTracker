import API from "@/lib/axios";

export interface IUser {
  id: string;
  name: string;
  email: string;
}

export interface IAuthData {
  email: string;
  password: string;
}

export interface ICreateUserData {
  name: string;
  email: string;
  password: string;
}

export default function UserRepository() {
  async function CreateUser(user: ICreateUserData): Promise<IUser> {
    const response = await API.post("/user", user);

    return response.data;
  }

  async function AuthenticateUser(authData: IAuthData): Promise<string> {
    const response = await API.post("/user/auth", authData);

    return response.data;
  }

  return {
    CreateUser,
    AuthenticateUser,
  };
}
