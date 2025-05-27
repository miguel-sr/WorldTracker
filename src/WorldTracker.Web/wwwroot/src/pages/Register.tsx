import useRequestHandler from "@/common/hooks/useRequestHandler";
import Button from "@/components/Button";
import Footer from "@/components/Footer";
import { Form, Input } from "@/components/Form";
import { Logo } from "@/components/Logo";
import Navbar from "@/components/Navbar";
import Notistack from "@/lib/Notistack";
import UserRepository, { ICreateUserData } from "@/repository/UserRepository";

import { ChangeEvent, useState } from "react";

const INITIAL_REGISTER_DATA: ICreateUserData = {
  name: "",
  email: "",
  password: "",
};

export default function Register() {
  const { showLoading } = useRequestHandler();
  const [registerData, setRegisterData] = useState<ICreateUserData>(
    INITIAL_REGISTER_DATA
  );

  function registerUser() {
    showLoading(async () => {
      await UserRepository()
        .CreateUser(registerData)
        .then(() => {
          Notistack.showSuccessMessage(
            "Usuário criado!",
            () => (location.href = "/login")
          );
        });
    });
  }

  return (
    <>
      <Navbar position="relative" />
      <section className="flex flex-col justify-center items-center min-h-full-screen w-full">
        <Logo />
        <h1 className="text-sky-blue mb-5">Registro</h1>
        <Form
          className="w-full max-w-md md:w-1/4 px-4"
          onSubmit={(e) => {
            e.preventDefault();
            registerUser();
          }}
        >
          <Input
            label="Nome"
            field="name"
            type="text"
            value={registerData.name}
            onChange={(e: ChangeEvent<HTMLInputElement>) =>
              setRegisterData({ ...registerData, name: e.target.value })
            }
          />
          <Input
            label="Email"
            field="email"
            type="email"
            className="mt-5"
            value={registerData.email}
            onChange={(e: ChangeEvent<HTMLInputElement>) =>
              setRegisterData({ ...registerData, email: e.target.value })
            }
          />
          <Input
            label="Senha"
            field="password"
            type="password"
            className="mt-5"
            value={registerData.password}
            onChange={(e: ChangeEvent<HTMLInputElement>) =>
              setRegisterData({ ...registerData, password: e.target.value })
            }
          />
          <Button
            text="REGISTRAR"
            className="mt-5 rounded-lg bg-sky-blue text-white hover:bg-light-blue"
            type="submit"
          />
          <div className="mt-4 text-center text-sky-blue">
            <span>Já tem uma conta? </span>
            <a href="/login" className="font-bold">
              Faça login
            </a>
          </div>
        </Form>
      </section>
      <Footer />
    </>
  );
}
