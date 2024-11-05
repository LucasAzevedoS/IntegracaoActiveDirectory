"use client";
import { useToggle, upperFirst, useDisclosure } from "@mantine/hooks";
import { useForm } from "@mantine/form";
import {
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  Button,
  Divider,
  Checkbox,
  Anchor,
  Stack,
  Input,
  Switch,
  Notification,
} from "@mantine/core";
import { GoogleButton } from "./GoogleButton";
import { signIn, signOut } from "next-auth/react";
import { IMaskInput } from "react-imask";
import { InserirUsuario } from "@/app/api/usuarios";
import { notifications } from "@mantine/notifications";
import { useRouter } from "next/navigation";
import classes from "./Form.module.css";
export function Sair() {
  return <Button onClick={() => signOut()}>Logoff</Button>;
}

export default function AuthenticationForm(props: any) {
  const router = useRouter();
  const [type, tptoggle] = useToggle(["login", "cadastro"]);
  const [doctoggle, { toggle }] = useDisclosure();
  const form = useForm({
    initialValues: {
      username: "",
      password: "",

    },

    // validate: {
    //   username: (val) => (/^\S+@\S+$/.test(val) ? null : "Email inválido"),
    //   // password: (val) =>
    //   //   val.length <= 6
    //   //     ? "Password should include at least 6 characters"
    //   //     : null,
    //   // confirmPassword: (value, values) =>
    //   //   value !== values.password ? "Passwords did not match" : null,
    // },
  });

  const handleLogin = async (values: {
    username: string;
    password: string;
  }) => {
    const result = await signIn("credentials", {
      // Evita redirecionamento automático
      username: values.username,
      password: values.password,
    });

    if (result?.error) {
      console.error("Login failed:", result.error);
    } else {
      console.log("Login successful:", result);
      // Aqui você pode redirecionar o usuário manualmente, se necessário
    }
  };
  const cadUsuario = async (values: any) => {
    const result = await InserirUsuario(values);
    console.log("result");
    console.log(result);

    if (result.ok) {
      notifications.show({
        title: "Usuário criado com sucesso",
        message: "Por favor acesse com o Login e senha! 🎉",
        color: "Teal",
        position: "top-right",
        withBorder: true,
        radius: "md",
        classNames: classes,
      });
    } else {
      notifications.show({
        title: "Usuário criado com sucesso",
        message:
          "Por favor tente novamente ou entre em contato como administrador do sistema! 🚫 ",
        color: "red",
        position: "top-right",
        withBorder: true,
        radius: "md",
        classNames: classes,
      });
    }
    tptoggle();
  };

  return (
    <Paper m="auto" maw="1024" radius="md" p="xl" withBorder {...props}>
      <Text size="lg" fw={500}></Text>
      <Text size="lg" fw={500}>
        Bem vindo, faça o {type} com seu Usuário e Senha
      </Text>



      <form
        onSubmit={form.onSubmit((values) => {
          type === "cadastro" ? cadUsuario(values) : handleLogin(values);
        })}
      >
        <Stack>
          {type === "cadastro" && (
            <>
              {/* <TextInput
                label="Nome"
                placeholder="Seu nome"
                value={form.values.name}
                onChange={(event) =>
                  form.setFieldValue("name", event.currentTarget.value)
                }
                radius="md"
              /> */}
              {/* <Switch
                checked={doctoggle}
                onChange={toggle}
                label="Pessoa Juridica"
              />
              <Input.Label required>{doctoggle ? "CNPJ" : "CPF"}</Input.Label>
              <Input
                component={IMaskInput}
                mask={doctoggle ? "00.000.000/0000-00" : "000.000.000-00"}
                value={form.values.docto}
                onChange={(event) =>
                  form.setFieldValue("docto", event.currentTarget.value)
                }
                radius="md"
              /> */}
              {/* {doctoggle && (
                <TextInput
                  label="Empresa"
                  placeholder="Nome da Empresa"
                  value={form.values.empresa}
                  onChange={(event) =>
                    form.setFieldValue("empresa", event.currentTarget.value)
                  }
                  radius="md"
                />
              )} */}

              {/* <Input.Label required>Telefone</Input.Label>
              <Input
                placeholder="Seu nome"
                component={IMaskInput}
                mask="(00)0 0000-0000"
                value={form.values.fone}
                onChange={(event) =>
                  form.setFieldValue("fone", event.currentTarget.value)
                }
                radius="md"
              /> */}
            </>
          )}

          <TextInput
            required
            label="Email"
            placeholder="brocha"
            value={form.values.username}
            onChange={(event) =>
              form.setFieldValue("username", event.currentTarget.value)
            }
            error={form.errors.username && "Invalid Username"}
            radius="md"
          />

          <PasswordInput
            required
            label="Senha"
            placeholder="Sua senha"
            value={form.values.password}
            {...form.getInputProps("password")}
            onChange={(event) =>
              form.setFieldValue("password", event.currentTarget.value)
            }
            error={
              form.errors.password &&
              "A senha deverá ter pelo menos 6 caracteres!"
            }
            radius="md"
          />
          {type === "cadastro" && (
            <>
              <PasswordInput
                mt="sm"
                label="Confirm password"
                placeholder="Confirm password"
                key={form.key("confirmPassword")}
                {...form.getInputProps("confirmPassword")}
              />
              {/* <Checkbox
                label="Sou Fotografo"
                checked={form.values.ehFotografo}
                onChange={(event) =>
                  form.setFieldValue("ehFotografo", event.currentTarget.checked)
                }
              /> */}

              {/* <Checkbox
                label="Eu aceito os termos e condições"
                checked={form.values.termo}
                onChange={(event) =>
                  form.setFieldValue("termo", event.currentTarget.checked)
                }
              /> */}
            </>
          )}
        </Stack>

        <Group justify="space-between" mt="xl">
          <Anchor
            component="button"
            type="button"
            c="dimmed"
            onClick={() => tptoggle()}
            size="xs"
          >

          </Anchor>
          <Button type="submit" radius="xl">
            {upperFirst(type)}
          </Button>
        </Group>
      </form>
    </Paper>
  );
}
