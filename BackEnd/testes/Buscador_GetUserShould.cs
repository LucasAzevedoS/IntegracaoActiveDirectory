using GerenciadoraAD.Model;
using GerenciadoraAD.Services;
using Microsoft.VisualBasic;

namespace testes
{
    public class Buscador_GetUserShould
    {
        [Fact]
        public void GetUser_InputInvalid_ReturnNull()
        {
            // arranjar
            string logon = "fakeusr";
            var msg = "Object reference not set to an instance of an object.";

            //agir
            Action check = () => Buscador.GetUser(logon);

            //afirmar
            var ex = Assert.Throws<NullReferenceException>(check);
            Assert.Equal(msg, ex.Message);
        }
    }
}