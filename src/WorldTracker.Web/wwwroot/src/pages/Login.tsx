import { TOKEN_KEY } from "@/common/Constants";
import useRequestHandler from "@/common/hooks/useRequestHandler";
import Button from "@/components/Button";
import Footer from "@/components/Footer";
import { Form, Input } from "@/components/Form";
import { Logo } from "@/components/Logo";
import Navbar from "@/components/Navbar";
import UserRepository, { IAuthData } from "@/repository/UserRepository";

import { ChangeEvent, useState } from "react";

const INITIAL_LOGIN_DATA: IAuthData = {
  email: "",
  password: "",
};

export default function Login() {
  const { showLoading } = useRequestHandler();
  const [loginData, setLoginData] = useState<IAuthData>(INITIAL_LOGIN_DATA);

  function authenticateUser() {
    showLoading(async () => {
      const token = await UserRepository().AuthenticateUser(loginData);

      localStorage.setItem(TOKEN_KEY, token);
      location.href = "/";
    });
  }

  return (
    <>
      <Navbar position="relative" />
      <section className="flex flex-col justify-center items-center min-h-full-screen w-full">
        <Logo />
        <h1 className="text-sky-blue mb-5">Login</h1>
        <Form
          className="w-full max-w-md md:w-1/4 px-4"
          onSubmit={(e) => {
            e.preventDefault();
            authenticateUser();
          }}
        >
          <Input
            label="Email"
            field="email"
            type="email"
            value={loginData.email}
            onChange={(e: ChangeEvent<HTMLInputElement>) =>
              setLoginData({ ...loginData, email: e.target.value })
            }
          />
          <Input
            label="Password"
            field="password"
            type="password"
            className="mt-5"
            value={loginData.password}
            onChange={(e: ChangeEvent<HTMLInputElement>) =>
              setLoginData({ ...loginData, password: e.target.value })
            }
          />
          <Button
            text="LOGIN"
            className="mt-5 rounded-lg bg-sky-blue text-white hover:bg-light-blue"
            type="submit"
          />
          <div className="mt-4 text-center text-sky-blue">
            <span>Não tem uma conta? </span>
            <a href="/register" className="font-bold">
              Registre-se
            </a>
          </div>
        </Form>
      </section>
      <Footer />
    </>
  );
}
