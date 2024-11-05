using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using GerenciadoraAD.Model;

namespace GerenciadoraAD.Services
{
    public class ActiveDirectoryService
    {
        private readonly string _domain;

        public ActiveDirectoryService(string domain)
        {
            _domain = domain;
        }

        public bool Authenticate(string username, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain, _domain))
            {
                return context.ValidateCredentials(username, password);
            }
        }

    }
}
