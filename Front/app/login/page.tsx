"use server";
import AuthenticationForm from "./components/formlogin";

export default async function LoginPage() {
  return (
    <div>
      <AuthenticationForm />

      {/* Importa o componente de login */}
    </div>
  );
}
