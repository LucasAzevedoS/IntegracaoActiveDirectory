"use client"
import { useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { Modal, Button, Card, Image, Text, Group, Fieldset, TextInput, Flex } from '@mantine/core';
import axios from 'axios';

export default function CadUser() {
    const [opened, { open, close }] = useDisclosure(false);
    const [formData, setFormData] = useState({
        sAMAccountName: "",
        userPrincipalName: "",
        givenName: "",
        sn: "",
        cn: " ",
        mail: "",
        displayName: " ",
        physicalDeliveryOfficeName: "",
        memberOf: [
            "  "
        ],
        description: "",
        telephoneNumber: "",
        ipPhone: "",
        streetAddress: "",
        l: " ",
        st: " ",
        postalcode: "",
        c: "",
        scriptPath: "",
        proxyAddresses: [
            ""
        ],
        vinculo: "",
        office: "",
        responsavel: "",
        nascimento: "",
    });

    // Função para atualizar o estado do formulário
    const handleInputChange = (event: { target: { name: any; value: any; }; }) => {
        const { name, value } = event.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    };

    // Função para enviar os dados para a API
    const handleSubmit = async (event: { preventDefault: () => void; }) => {
        // console.log("linha 46")
        // console.log(formData)
        // alert(JSON.stringify(formData))
        event.preventDefault();
        try {
            console.log(formData)
            const response = await axios.post('https://localhost:7129/api/User/CriarUsuario', formData);
            console.log('Usuário criado com sucesso:', response.data);
            alert('Usuário Criado com sucesso!!')
            close();  // Fecha o modal após o envio
        } catch (error) {
            console.error('Erro ao criar usuário:', error);
        }
    };


    return (
        <>
            <Flex
                  gap={{ base: 'md', sm: 'md' }}
                  justify={{ sm: 'center' }}
            >
                <Card
                    w={350}
                    shadow="sm"
                    radius="md" withBorder
                >
                    <Card.Section w={350} >
                        <Image
                            src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXDY7Oble1g6DfrHWzw7FCv1ZnERGUIhBCzg&s"
                            mt="3rem"
                            height="auto"
                            alt="Imagem de criação de usuário"
                        />
                    </Card.Section>
                    <Group p="apart" mt="md" mb="xs" ml='35%'>
                        <Text>Criar Usuário</Text>
                    </Group>
                    <Button fullWidth mt="md" radius="md" onClick={open}>
                        Criar
                    </Button>
                </Card>

                <Modal opened={opened} onClose={close} title="Cadastro de Usuário">
                    <form onSubmit={handleSubmit}>
                        <Fieldset>
                            <TextInput label="Nome de Usuário" name="sAMAccountName" value={formData.sAMAccountName} onChange={handleInputChange} required />
                            <TextInput label="User Principal Name" name="userPrincipalName" value={formData.userPrincipalName} onChange={handleInputChange} required />
                            <TextInput label="Nome" name="givenName" value={formData.givenName} onChange={handleInputChange} required />
                            <TextInput label="Sobrenome" name="sn" value={formData.sn} onChange={handleInputChange} required />
                            <TextInput label="Nome Completo" name="cn" value={formData.cn} onChange={handleInputChange} required />
                            <TextInput label="Email" name="mail" value={formData.mail} onChange={handleInputChange} required />
                            <TextInput label="Display Name" name="displayName" value={formData.displayName} onChange={handleInputChange} />
                            <TextInput label="Departamento" name="physicalDeliveryOfficeName" value={formData.physicalDeliveryOfficeName} onChange={handleInputChange} />
                            <TextInput label="Membro De" name="memberOf" value={formData.memberOf} onChange={handleInputChange} />
                            <TextInput label="Descrição" name="description" value={formData.description} onChange={handleInputChange} />
                            <TextInput label="Telefone" name="telephoneNumber" value={formData.telephoneNumber} onChange={handleInputChange} />
                            <TextInput label="IP Phone" name="ipPhone" value={formData.ipPhone} onChange={handleInputChange} />
                            <TextInput label="Endereço" name="streetAddress" value={formData.streetAddress} onChange={handleInputChange} />
                            <TextInput label="Cidade" name="l" value={formData.l} onChange={handleInputChange} />
                            <TextInput label="Estado" name="st" value={formData.st} onChange={handleInputChange} />
                            <TextInput label="CEP" name="postalcode" value={formData.postalcode} onChange={handleInputChange} />
                            <TextInput label="País" name="c" value={formData.c} onChange={handleInputChange} />
                            <TextInput label="Script Path" name="scriptPath" value={formData.scriptPath} onChange={handleInputChange} />
                            <TextInput label="Proxy Addresses" name="proxyAddresses" value={formData.proxyAddresses} onChange={handleInputChange} />
                            <TextInput label="Vínculo" name="vinculo" value={formData.vinculo} onChange={handleInputChange} />
                            <TextInput label="Office" name="office" value={formData.office} onChange={handleInputChange} />
                            <TextInput label="Responsável" name="responsavel" value={formData.responsavel} onChange={handleInputChange} />
                            <TextInput label="Data de Nascimento" name="nascimento" value={formData.nascimento} onChange={handleInputChange} type="date" />
                        </Fieldset>
                        <Button type="submit" mt="md" radius="md">
                            Criar Usuário
                        </Button>
                    </form>
                </Modal>


                <Card
                    w={350}
                    shadow="sm"
                    radius="md" withBorder
                >
                    <Card.Section w={350} >
                        <Image
                            src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXDY7Oble1g6DfrHWzw7FCv1ZnERGUIhBCzg&s"
                            mt="3rem"
                            height="auto"
                            alt="Imagem de criação de usuário"
                        />
                    </Card.Section>
                    <Group p="apart" mt="md" mb="xs" ml='35%'>
                        <Text>Editar Usuário</Text>
                    </Group>
                    <Button fullWidth mt="md" radius="md" onClick={open}>
                        Editar
                    </Button>
                </Card>

                <Card
                    w={350}
                    shadow="sm"
                    radius="md" withBorder
                >
                    <Card.Section w={350} >
                        <Image
                            src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXDY7Oble1g6DfrHWzw7FCv1ZnERGUIhBCzg&s"
                            mt="3rem"
                            height="auto"
                            alt="Imagem de criação de usuário"
                        />
                    </Card.Section>
                    <Group p="apart" mt="md" mb="xs" ml='35%'>
                        <Text>Listar Usuário</Text>
                    </Group>
                    <Button fullWidth mt="md" radius="md" onClick={open}>
                        Listar
                    </Button>
                </Card>
            </Flex>

        </>
    );
}