"use client";
import {
  Group,
  Button,
  UnstyledButton,
  Text,
  Flex,
  Avatar,
  Card,
  Popover,
  Space,
  Skeleton,
} from "@mantine/core";
import classes from "./header.module.css";
import { LogOut } from "lucide-react";
import { signOut } from "next-auth/react";
import { useSession } from "next-auth/react";
import ToggleMode from "../toggleMode";

export default function AvatarLogin() {
  const { data: session, status } = useSession();
  const user = session?.user.email;
  return (
    <>
      {
        // status === "loading" ? (
        //   <Flex
        //     justify="flex-end"
        //     gap="sm"
        //     align="center"
        //     direction="row"
        //     w={400}
        //   >
        //     <Skeleton height={50} circle radius={40} />

        //     <Skeleton height={15} mt={6} width="15%" radius="xl" />

        //     <Skeleton height={30} mt={6} width="20%" radius="xl" />
        //   </Flex>
        // ) :
        session ? (
          <Group>
            <Popover
              width={400}
              zIndex={1000001}
              position="bottom"
              withArrow
              shadow="md"
            >
              <Popover.Target>
                <UnstyledButton className={classes.user}>
                  <Group>
                    <Avatar
                      src={
                        "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-9.png"
                      }
                      radius="xl"
                    />

                    <div style={{ flex: 1 }}>
                      <Text size="sm" fw={500}>
                        {user}
                      </Text>
                    </div>
                  </Group>
                </UnstyledButton>
              </Popover.Target>
              <Popover.Dropdown>
                <AvartarContent />
              </Popover.Dropdown>
            </Popover>
            <ToggleMode />
          </Group>
        ) : (
          <></>
        )
      }
    </>
  );
}

export function AvartarContent() {
  const { data: session, status } = useSession();
  const user = session?.user.usuario;
  const sessao = session?.expires;
  return (
    <Card withBorder padding="xl" radius="md" className={classes.card}>
      <Card.Section
        h={80}
        style={{
          backgroundImage:
            "url(https://images.unsplash.com/photo-1488590528505-98d2b5aba04b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=500&q=80)",
        }}
      />
      <Avatar
        src="https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-9.png"
        size={80}
        radius={80}
        mx="auto"
        mt={-30}
        className={classes.avatar}
      />
      <Text ta="center" fz="lg" fw={500} mt="sm">
        {user}
      </Text>

      <Text ta="center" c="dimmed" size="xs">
        {sessao}
      </Text>

      <Button
        fullWidth
        radius="md"
        mt="sm"
        size="md"
        variant="transparent"
        onClick={() => signOut()}
      >
        Sair
        <Space m="sm"></Space>
        <LogOut />
      </Button>
    </Card>
  );
}
