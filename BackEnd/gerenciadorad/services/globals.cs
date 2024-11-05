namespace GerenciadoraAD.Services.GlobalVariables
{
    /// <summary>
    /// Vari�veis globais
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Configura��es do servidor LDAP
        /// </summary>
        public static LdapServer? ServidorLdap;

        /// <summary>
        /// Inicializa servidor Ldap
        /// </summary>
        public static void SetServidorLdap()
        {
            ServidorLdap = new LdapServer(
                389,
                "LOCAL",
                "TESTEDC",
                "svc.ldap.br",
                "agomod@22",
                "dd-teste-grupo");
        }
    }
}