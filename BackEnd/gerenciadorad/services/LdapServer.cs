namespace GerenciadoraAD.Services
{
    /// <summary>
    /// Contém atributos para se conectar a um servidor LDAP.
    /// </summary>
    public class LdapServer
    {
        public int Porta { get; set; } = 389;
        public string? Dominio { get; set; }
        public string? Subdominio { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }
        public string? Servidor { get; set; }
        public string? Escopo { get; set; }
        public string? Managers { get; set; }
     

        public LdapServer(int porta, string dominio, 
            string subdominio, string usuario, string senha,
            string managers)
        {
            Porta = porta;
            Dominio = dominio;
            Subdominio = subdominio;
            Usuario = usuario;
            Senha = senha;
            Managers = managers;

            Servidor = $"{Subdominio}.{Dominio}";
            Escopo = $"LDAP://OU=Santillana-Nuevo,DC={Subdominio},DC={Dominio}";
        }

    }
}
