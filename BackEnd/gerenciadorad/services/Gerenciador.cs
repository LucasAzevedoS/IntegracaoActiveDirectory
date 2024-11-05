using GerenciadoraAD.Model;
using GerenciadoraAD.Model;
using GerenciadoraAD.Services.GlobalVariables;
using System.Collections;
using System.DirectoryServices;


namespace GerenciadoraAD.Services
{
    /// <summary>
    /// Contém métodos para gerenciar entradas no AD
    /// </summary>
    public class Gerenciador
    {
        /// <summary>
        /// Busca por uma DirectoryEntry de um usuário.
        /// </summary>
        /// <param name="logon">Logon do usuário</param>
        /// <returns>Retorna a DirectoryEntry do usuário, se falhar retorna null</returns>
        public static DirectoryEntry? GetEntry(string logon)
        {
            Usuario usr = new Usuario();
            List<string> Propriedades = new List<string>()
            {
                "sAMAccoutName",
                "displayName",
                "givenName",
                "sn",
                "cn"
            };

            DirectoryEntry entry = new DirectoryEntry(
            Globals.ServidorLdap.Escopo,
            Globals.ServidorLdap.Usuario,
            Globals.ServidorLdap.Senha);
            DirectorySearcher searcher = new DirectorySearcher(entry)
            {
                Filter = $"(sAMAccountName={logon})",
                SearchScope = SearchScope.Subtree
            };

            foreach (string item in Propriedades)
                searcher.PropertiesToLoad.Add(item);


            try
            {
                SearchResult result = searcher.FindOne();
                var em = result.GetDirectoryEntry();
                return em;
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro: \n" + e.GetType().Name);
            }

            return null;
        }

        /// <summary>
        /// Habilita um usuário no AD.
        /// </summary>
        /// <param name="entrada">DirectoryEntry do usuário</param>
        public static void HabilitaUsuario(DirectoryEntry entrada)
        {
            try
            {
                entrada.Invoke("Put", new object[] { "userAccountControl", "512" });
                entrada.CommitChanges();
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro: \n" + e.GetType().Name);
            }
        }

        /// <summary>
        /// Desabilita um usuário no AD.
        /// </summary>
        /// <param name="entrada">DirectoryEntry do usuário</param>
        public static void DesabilitaUsuario(DirectoryEntry entrada)
        {
            try
            {
                entrada.Invoke("Put", new object[] { "userAccountControl", "514" });
                entrada.CommitChanges();
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro: \n" + e.GetType().Name);
            }
        }

        /// <summary>
        /// Cria um usuário no AD usando os atributos de usr.
        /// Todos os atributos são setados independente do seu valor.
        /// </summary>
        /// <param name="usr">Usuário que será criado</param>
        /// <returns>Retorno 0 indica que o usuário foi criado, 1 indica que falha na criação</returns>
        public static int CriarUsuario(Usuario usr)
        {
            try
            {
                string caminho = $"LDAP://OU=Usuarios,OU=BR,OU=Santillana-Nuevo,DC=TESTEDC,DC=LOCAL";

                var conexao = new DirectoryEntry(caminho,
                    Globals.ServidorLdap.Usuario,
                    Globals.ServidorLdap.Senha);

                var uentry = conexao.Children.Add($"CN={usr.cn}", "user");

                uentry.Properties["sAMAccountName"].Add(usr.sAMAccountName);
                uentry.Properties["userPrincipalName"].Add($"{usr.sAMAccountName}@{Globals.ServidorLdap.Servidor}");
                uentry.Properties["givenName"].Add(usr.givenName);
                uentry.Properties["sn"].Add(usr.sn);
                uentry.Properties["mail"].Add(usr.mail);
                uentry.Properties["displayName"].Add(usr.DisplayName);
                uentry.Properties["physicalDeliveryOfficeName"].Add(usr.PhysicalDeliveryOfficeName);
                uentry.Properties["description"].Add(usr.Description);
                uentry.Properties["ipPhone"].Add(usr.IpPhone);
                uentry.Properties["streetAddress"].Add(usr.StreetAddress);
                uentry.Properties["l"].Add(usr.l);
                uentry.Properties["st"].Add(usr.st);
                uentry.Properties["postalcode"].Add(usr.PostalCode);

                // o país tem que ser um nome exato e único, no nosso caso é "BR"
                uentry.Properties["c"].Add(usr.c);

                uentry.Properties["ExtensionAttribute4"].Add(usr.Vinculo);
                uentry.Properties["ExtensionAttribute6"].Add(usr.Responsavel);
                uentry.Properties["ExtensionAttribute12"].Add(usr.Nascimento);
                uentry.Properties["ExtensionAttribute15"].Add(usr.Office);

                if (usr.ProxyAddresses.Count > 0)
                {
                    foreach (string proxy in usr.ProxyAddresses)
                        uentry.Properties["proxyAddresses"].Add("SMTP:" + proxy);
                }
                uentry.CommitChanges();

                SetarSenha("moderna@10", uentry);
                HabilitaUsuario(uentry);

                if (usr.MemberOf.Count > 0)
                    AdicionarGrupos(usr.MemberOf, uentry);

            }
            catch (IOException e)
            {
                Console.WriteLine("Erro: \n" + e.GetType().Name);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Atribui uma senha para o usuário. Pode ser usado para alterar senhas.
        /// </summary>
        /// <param name="senha">Nova senha</param>
        /// <param name="entry">DirectoryEntry do usuário</param>
        public static void SetarSenha(string senha, DirectoryEntry entry)
        {
            try
            {
                entry.Invoke("SetPassword", senha);
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro ao trocar a senha");
            }
        }

        /// <summary>
        /// Busca e retorna uma DirectoryEntry de grupo.
        /// </summary>
        /// <param name="grupo">Nome do grupo a ser buscado</param>
        /// <returns>Retorna a DirectoryEntry do grupo, se falhar retorna null</returns>
        public static DirectoryEntry? GetGroupEntry(string grupo)
        {
            var conexao = new DirectoryEntry(
                Globals.ServidorLdap.Escopo,
                Globals.ServidorLdap.Usuario,
                Globals.ServidorLdap.Senha);

            var searcher = new DirectorySearcher(conexao);
            searcher.Filter = $"(&(objectClass=group)(cn={grupo}))";
            searcher.SearchScope = SearchScope.Subtree;

            try
            {
                var result = searcher.FindOne();

                if (result != null)
                    return result.GetDirectoryEntry();
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro: \n" + e.GetType().Name);
            }

            return null;
        }

        /// <summary>
        /// Adiciona um usuário ao grupo.
        /// </summary>
        /// <param name="grupo">Nome do grupo</param>
        /// <param name="uentry">DirectoryEntry do usuário</param>
        /// <returns>0 Indica que o usuário foi adicionado ao grupo, 1 indica falha</returns>
        public static int AdicionarAoGrupo(string grupo, DirectoryEntry uentry)
        {
            var gentry = GetGroupEntry(grupo);

            try
            {
                if (gentry != null)
                {
                    gentry.Invoke("Add", new object[] { uentry.Path.ToString() });
                    return 0;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erro: \n" + ex.GetType().Name);
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// Adicionar um usuário a varios grupos.
        /// </summary>
        /// <param name="grupos">Lista com nome dos grupos</param>
        /// <param name="uentry">DirectoryEntry do úsuário</param>
        /// <returns>
        /// 0 indica que o usuário foi adicionado a todos os grupos.
        /// 1 indica que houve falha ao adicionar a um ou mais grupos.
        /// </returns>
        public static int AdicionarGrupos(List<string> grupos, DirectoryEntry uentry)
        {
            if (grupos.Count == 0)
                return 1;

            try
            {
                foreach (string grupo in grupos)
                {
                    var gentry = GetGroupEntry(grupo);
                    if (gentry != null)
                        gentry.Invoke("Add", new object[] { uentry.Path.ToString() });
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erro \n" + ex.GetType().Name);
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Edita o usuário usando as novas informações armazenadas em usrPatched.
        /// Esse método verifica se um atributo de usrPatched difere do equivalente em
        /// uentry, fazendo um update com o usrPatched quando eles diferem.
        /// </summary>
        /// <param name="usrPatched">usuário com suas novas informações</param>
        /// <param name="uentry">DirectoryEntry do usuário</param>
        /// <returns>0 indica que houve sucesso na atualização dos campos, 1 indica falha</returns>
        public static int EditarUsuario(Usuario usrPatched, DirectoryEntry uentry)
        {
            var toUpdate = new Hashtable();

            if (usrPatched.sAMAccountName != uentry.Properties["sAMAccountName"].Value)
            {
                toUpdate.Add("sAMAccountName", usrPatched.sAMAccountName);
                // se você muda o sAMAccountName, o userPrincipalName tem que mudar também
                toUpdate.Add("userPrincipalName", usrPatched.sAMAccountName + Globals.ServidorLdap.Servidor);
            }

            if (usrPatched.cn != uentry.Properties["cn"].Value)
            {
                // mudança no cn, implica na mudança de givenName, sn e displayName 
                toUpdate.Add("cn", usrPatched.cn);
                toUpdate.Add("sn", usrPatched.sn);
                toUpdate.Add("displayName", usrPatched.DisplayName);
            }

            if (usrPatched.mail != uentry.Properties["mail"].Value)
            {
                toUpdate.Add("mail", usrPatched.mail);
            }

            if (usrPatched.PhysicalDeliveryOfficeName != uentry.Properties["physicalDeliveryOfficeName"].Value)
            {
                toUpdate.Add("physicalDeliveryOfficeName", usrPatched.PhysicalDeliveryOfficeName);
            }

            if (usrPatched.Description != uentry.Properties["description"].Value)
            {
                toUpdate.Add("description", usrPatched.Description);
            }

            if (usrPatched.TelephoneNumber != uentry.Properties["telephoneNumber"].Value)
            {
                toUpdate.Add("telephoneNumber", usrPatched.TelephoneNumber);
            }

            if (usrPatched.IpPhone != uentry.Properties["ipPhone"].Value)
            {
                toUpdate.Add("ipPhone", usrPatched.IpPhone);
            }

            if (usrPatched.StreetAddress != uentry.Properties["streetAddress"].Value)
            {
                toUpdate.Add("streetAddress", usrPatched.l);
            }

            if (usrPatched.l != uentry.Properties["l"].Value)
            {
                toUpdate.Add("l", usrPatched.l);
            }

            if (usrPatched.st != uentry.Properties["st"].Value)
            {
                toUpdate.Add("st", usrPatched.st);
            }

            if (usrPatched.PostalCode != uentry.Properties["postalCode"].Value)
            {
                toUpdate.Add("postalCode", usrPatched.PostalCode);
            }

            if (usrPatched.c != uentry.Properties["c"].Value)
            {
                toUpdate.Add("c", usrPatched.c);
            }

            if (usrPatched.Vinculo != uentry.Properties["extensionAttribute4"].Value)
            {
                toUpdate.Add("extensionAttribute4", usrPatched.Vinculo);
            }

            if (usrPatched.Responsavel != uentry.Properties["extensionAttribute6"].Value)
            {
                toUpdate.Add("extensionAttribute6", usrPatched.Responsavel);
            }

            if (usrPatched.Nascimento != uentry.Properties["extensionAttribute12"].Value)
            {
                toUpdate.Add("extensionAttribute12", usrPatched.Vinculo);
            }

            if (usrPatched.Office != uentry.Properties["extensionAttribute15"].Value)
            {
                toUpdate.Add("extensionAttribute15", usrPatched.Office);
            }

            try
            {
                foreach (DictionaryEntry pair in toUpdate)
                {
                    if (pair.Value != null)
                        uentry.Invoke("Put", new object[] { pair.Key.ToString(), pair.Value.ToString() });
                    else
                    {
                        uentry.Properties[pair.Key.ToString()].Clear();
                    }
                }

                uentry.CommitChanges();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: \n", ex.ToString());
                return 1;
            }

            return 0;
        }
    }
}
