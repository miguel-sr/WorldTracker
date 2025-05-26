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

export default function UserRepository() {
  async function GetAllUsers(): Promise<IUser[]> {
    const response = await API.get("/user");

    return response.data;
  }

  async function GetUserById(id: string): Promise<IUser> {
    const response = await API.get(`/user/${id}`);
    return response.data;
  }

  async function CreateUser(user: IUser): Promise<IUser> {
    const response = await API.post("/user", user);

    return response.data;
  }

  async function UpdateUser(user: IUser): Promise<void> {
    await API.put("/user", user);
  }

  async function DeleteUser(id: string): Promise<void> {
    await API.delete(`/user/${id}`);
  }

  async function AuthenticateUser(authData: IAuthData): Promise<string> {
    const response = await API.post("/user/auth", authData);

    return response.data;
  }

  return {
    GetAllUsers,
    GetUserById,
    CreateUser,
    UpdateUser,
    DeleteUser,
    AuthenticateUser
  };
}
