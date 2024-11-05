import { SessionProvider } from "next-auth/react";
import AvatarLogin from "./avatar";
import { Box, Group, Image } from "@mantine/core";

import classes from "./header.module.css";

export default function HeaderPage() {
  return (
    <div>
      <SessionProvider>
        <Box pb={30}>
          <header className={classes.header}>
            <Group
              display="flex"
              justify="space-between"
              m="auto"
              pt="md"
              h="90%"
              maw="1024"
            >
              <Image
                // src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ9doqr0w99NvSTFIC2qMn7vMwv8WUWh-zXbg&s'
                mt="3rem"
                height="auto"
                alt="Logo Santillana"
              />
              <AvatarLogin />
            </Group>
          </header>
        </Box>
      </SessionProvider>
    </div>
  );
}
