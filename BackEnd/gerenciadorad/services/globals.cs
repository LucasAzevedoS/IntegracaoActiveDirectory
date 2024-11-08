namespace GerenciadoraAD.Services.GlobalVariables
{
    /// <summary>
    /// Variáveis globais
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Configurações do servidor LDAP
        /// </summary>
        public static LdapServer? ServidorLdap;

        /// <summary>
        /// Inicializa servidor Ldap
        /// </summary>
        public static void SetServidorLdap()
        {
            ServidorLdap = new LdapServer(
                111,
                "**********",
                "**********",
                "**********",
                "**********",
                "**********");
        }
    }
}
