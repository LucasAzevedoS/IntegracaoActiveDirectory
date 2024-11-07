# ActiveDirectory Interface

![Badge de Status](https://img.shields.io/badge/status-Em_Desenvolvimento-green)

O **ActiveDirectory Interface** é uma solução desenvolvida para simplificar a criação e gerenciamento de usuários no Active Directory, respondendo a uma necessidade levantada por colegas de trabalho que enfrentavam dificuldades com a interface padrão do sistema. 

Este projeto visa oferecer uma interface web amigável para facilitar o processo de criação, edição e listagem de usuários no Active Directory, integrada por meio de uma API desenvolvida especificamente para essa finalidade. Além dessas funcionalidades, planejo implementar uma função para dar baixa em usuários ativos, o que agilizará os processos de desligamento na empresa.

## Tecnologias Utilizadas

- **Back-end**
  - C# e .NET 8
  - System.DirectoryServices
  
- **Front-end**
  - TypeScript
  - React com MantineUI
  - Axios
  - NextAuth para autenticação



## Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Pré-requisitos](#pré-requisitos)
- [Instalação e Configuração](#instalação-e-configuração)
- [Como Usar](#como-usar)
- [Contribuindo](#contribuindo)
- [Licença](#licença)

## Sobre o Projeto

O **ActiveDirectory Interface** foi criado para agilizar os processos internos da empresa, especialmente nas tarefas de criação e desativação de usuários no Active Directory. Com uma interface web amigável e intuitiva, o projeto visa simplificar o trabalho da equipe de suporte, reduzindo o tempo e o esforço necessários para gerenciar usuários no sistema.

Além de seu propósito funcional, o projeto serve como uma base de aprendizado abrangente, permitindo uma imersão completa em diversas etapas de desenvolvimento e infraestrutura. Estou envolvido desde o preparo do servidor e a criação das máquinas virtuais (VMs) até a configuração do ambiente Windows Server e GitLab, além do desenvolvimento e implementação do código em si. Esse processo tem sido fundamental para o aprimoramento de minhas habilidades em gestão de servidores e desenvolvimento full stack.


## Funcionalidades

- **Criação de Usuários**: Permite adicionar novos usuários ao Active Directory por meio de uma interface simplificada, facilitando o trabalho da equipe de suporte.
- **Edição de Usuários**: Possibilita a atualização de informações de usuários ativos, garantindo que os dados estejam sempre corretos e atualizados.
- **Listagem de Usuários**: Exibe uma lista de usuários registrados no Active Directory, facilitando a busca e consulta de informações.
- **Desativação de Usuários (futura implementação)**: Planejada para facilitar o processo de desativação de usuários, agilizando o desligamento no sistema.
- **Autenticação Segura**: Utiliza autenticação com NextAuth para garantir a segurança no acesso à aplicação.


## Tecnologias Utilizadas

- **Back-end**: 
  - .NET 8, C#
  - System.DirectoryServices para integração com Active Directory
  
- **Front-end**: 
  - Next.js, React.js com Mantine
  - TypeScript
  
- **Infraestrutura**:
  - Configuração da ILO do servidor
  - GitLab para controle de versão e CI/CD
  - Vmware para virtualização
  - Windows Server e Active Directory para gerenciamento de usuários

- **Outros**:
  - Axios para requisições HTTP
  - NextAuth para autenticação segura


## Pré-requisitos

Antes de iniciar, certifique-se de ter as seguintes ferramentas e configurações:

### Ambiente de Desenvolvimento
- **.NET SDK (versão 8 ou superior)**: Para compilar e rodar o back-end em C#.
- **Node.js (versão 18 ou superior)**: Para executar o ambiente de desenvolvimento do front-end em Next.js.
- **JavaScript Package Manager**: npm ou yarn, para instalar as dependências do front-end.

### Banco de Dados e Infraestrutura
- **Active Directory**: Necessário para integração com o System.DirectoryServices.
- **Servidor Windows e Vmware**: Configuração do ambiente para o Active Directory e virtualização (para ambiente de produção ou desenvolvimento replicável).

### Ferramentas de Gerenciamento
- **Git**: Para controle de versão do projeto.
- **GitLab**: Para CI/CD, se houver integrações específicas configuradas.
- **GitLab Runner**: Opcional, caso use pipelines de CI/CD.

### Variáveis de Ambiente
Certifique-se de definir as variáveis de ambiente necessárias:
- **Conexão com o Active Directory**: Informações de conexão, como nome de usuário, senha e domínio.
- **Configurações de Autenticação**: Configurações para NextAuth, como chaves secretas e URL do servidor de autenticação.

## Instalação e Configuração

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-projeto.git

