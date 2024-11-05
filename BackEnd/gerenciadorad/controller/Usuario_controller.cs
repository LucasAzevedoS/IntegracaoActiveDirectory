using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using GerenciadoraAD.Services;
using GerenciadoraAD.Model;
using Microsoft.AspNetCore.Cors;

namespace GerenciadoraAD.Controller
{
    [ApiController]
    [Route("api")]
    public class Usuario_controller : ControllerBase
    {

        private readonly ActiveDirectoryService _adService;

        public Usuario_controller()
        {
            _adService = new ActiveDirectoryService("TESTEDC.LOCAL");
        }

        [HttpPost("User/usuario-in")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (_adService.Authenticate(login.Username, login.Password))
            {
                return Ok(new { message = "Login efetuado" });
            }
            else
            {
                return Unauthorized(new { message = "Login não efetuado" });
            }
        }

        [HttpGet("BuscarUsuario/")]
        public ActionResult<Usuario> UserSearch(string logon) {
            Usuario usuario = Buscador.GetUser(logon);
            return usuario;
        }

        [HttpPatch("EditarUsuario/")]
        public IActionResult EditUser(string logon, 
            [FromBody]JsonPatchDocument<Usuario> patchUsr)
        {
            if (patchUsr != null)
            {
                /* Aqui é melhor fazer a busca com GetEntry, mapear para um objeto 
                 * Usuário e depois chamar EditarUsuario passando o novo objeto com
                 * o patch aplicado.
                 * 
                 * Se usar o GetUser direto seria feita uma requisição ao servidor e
                 * depois teriámos que chamar outro GetEntry para passar ao EditarUsuário,
                 * ou seja, 2x mais processamento.
                 */
                var uentry = Gerenciador.GetEntry(logon);
                if (uentry == null)
                {
                    return NotFound();
                }

                var usr = ClassMaps.EntryToUser(uentry);

                patchUsr.ApplyTo(usr);

                if (Gerenciador.EditarUsuario(usr, uentry) != 0)
                {
                    return BadRequest();
                }
                
                return new ObjectResult(usr);
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPost("User/CriarUsuario")]
        public IActionResult CriarUsuario(Usuario usr)
        {
            if (Gerenciador.CriarUsuario(usr) == 0)
            {
                return Ok("Usuário criado");
            }
            else
            {
                return BadRequest(usr);
            }
        }

        [HttpPost("DesablitarUsuario/")]
        public IActionResult DesabilitarUsuario(string logon)
        {
            var entry = Gerenciador.GetEntry(logon);
            Gerenciador.DesabilitaUsuario(entry);
            return Ok("MÉTODO EXECUTADO");
        }

        [HttpPost ("HabilitarUsuario/")]
        public IActionResult HabilitarUsuario(string logon)
        {
            var entry = Gerenciador.GetEntry(logon);
            Gerenciador.HabilitaUsuario(entry);
            return Ok("MÉTODO EXECUTADO");
        }
        

    }
}
