"use server";

import { useState } from 'react';
import { useForm } from "@mantine/form";
import { Card, Center, Image, Stack, Title } from "@mantine/core";
import { auth } from "@/auth";
import CadUser from '../components/modals/modalcad';
import axios from 'axios';
export default async function AreaFotografo() {
  const session = await auth();

  return (
    <>
      <Card m="auto" maw="1024" mb='2rem'>
        <Center>
          <Stack>
            <Title>Home</Title>
          </Stack>
        </Center>
      </Card>

      <CadUser/>



    </>
  );
}



// uentry.Properties["sAMAccountName"].Add(usr.sAMAccountName);
// uentry.Properties["userPrincipalName"].Add($"{usr.sAMAccountName}@{Globals.ServidorLdap.Servidor}");
// uentry.Properties["givenName"].Add(usr.givenName);
// uentry.Properties["sn"].Add(usr.sn);
// uentry.Properties["mail"].Add(usr.mail);
// uentry.Properties["displayName"].Add(usr.DisplayName);
// uentry.Properties["physicalDeliveryOfficeName"].Add(usr.PhysicalDeliveryOfficeName);
// uentry.Properties["description"].Add(usr.Description);
// uentry.Properties["ipPhone"].Add(usr.IpPhone);
// uentry.Properties["streetAddress"].Add(usr.StreetAddress);
// uentry.Properties["l"].Add(usr.l);
// uentry.Properties["st"].Add(usr.st);
// uentry.Properties["postalcode"].Add(usr.PostalCode);

// // o país tem que ser um nome exato e único, no nosso caso é "BR"
// uentry.Properties["c"].Add(usr.c);

// uentry.Properties["ExtensionAttribute4"].Add(usr.Vinculo);
// uentry.Properties["ExtensionAttribute6"].Add(usr.Responsavel);
// uentry.Properties["ExtensionAttribute12"].Add(usr.Nascimento);
// uentry.Properties["ExtensionAttribute15"].Add(usr.Office);
