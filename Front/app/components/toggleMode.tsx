"use client";

import {
  ActionIcon,
  useMantineColorScheme,
  useComputedColorScheme,
  Switch,
  useMantineTheme,
  rem,
} from "@mantine/core";
import { IconSun, IconMoonStars } from "@tabler/icons-react";

export default function ToggleMode() {
  const theme = useMantineTheme();

  const sunIcon = (
    <IconSun
      style={{ width: rem(24), height: rem(24) }}
      stroke={2.5}
      color={theme.colors.yellow[4]}
    />
  );

  const moonIcon = (
    <IconMoonStars
      style={{ width: rem(24), height: rem(24) }}
      stroke={2.5}
      color={theme.colors.blue[6]}
    />
  );

  const { setColorScheme } = useMantineColorScheme();
  const computedColorScheme = useComputedColorScheme("light", {
    getInitialValueInEffect: true,
  });

  // return (
  //   <ActionIcon
  //     onClick={() =>
  //       setColorScheme(computedColorScheme === "light" ? "dark" : "light")
  //     }
  //     variant="default"
  //     size="xl"
  //     aria-label="Toggle color scheme"
  //   >
  //     {computedColorScheme === "dark" ? (
  //       <IconSun stroke={1.5} />
  //     ) : (
  //       <IconMoonStars stroke={1.5} />
  //     )}
  //   </ActionIcon>
  return (
    <Switch
      onChange={() =>
        setColorScheme(computedColorScheme === "dark" ? "light" : "dark")
      }
      size="xl"
      color="#e4e6ed"
      onLabel={moonIcon}
      offLabel={sunIcon}
    />
  );
}
