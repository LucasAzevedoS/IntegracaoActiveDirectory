using GerenciadoraAD.Services.GlobalVariables;
using System.DirectoryServices;
using GerenciadoraAD.Model;

namespace GerenciadoraAD.Services
{
    public class Buscador
    {
        /// <summary>
        ///  Busca por um usuário no AD
        /// </summary>
        /// <param name="logon">logon do usuário (sAMAccountName)</param>
        /// <returns>Retorna o objeto usuário preenchido, se falhar retorna null</returns>        
        public static Usuario GetUser(string logon)
        {
            Usuario usr = new Usuario();
            List<string> Propriedades = new List<string>()
            {
                "sAMAccoutName",
                "userPrincipalName",
                "givenName",
                "sn",
                "cn",
                "mail",
                "displayName",
                "userAccountControl",
                "physicalDelivery",
                "memberOf",
                "description",
                "telephoneNumber",
                "IpPhone",
                "streetAddress",
                "l",
                "st",
                "Postal Code",
                "c",
                "scriptPath",
                "proxyAddresses",
                "extensionAttribute4",
                "extensionAttribute6",
                "extensionAttribute12",
                "extensionAttribute15",
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

                if (result != null)
                {
                    DirectoryEntry uentry = result.GetDirectoryEntry();
                    usr = ClassMaps.EntryToUser(uentry);
                }       
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro: \n" + e.GetType().Name);
            }

            return usr;
        }
    }
}
