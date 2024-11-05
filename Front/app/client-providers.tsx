"use client"; // Este é o Client Component

import { SessionProvider } from "next-auth/react";

export default function ClientProviders({
  children,
}: {
  children: React.ReactNode;
}) {
  return <SessionProvider>{children}</SessionProvider>;
}
