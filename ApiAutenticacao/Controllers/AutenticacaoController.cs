using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositorioInterface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiAutenticacao.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ILoginRepositorio negocio;

        public AutenticacaoController(
               ILoginRepositorio _negocio
            )
        {
            negocio = _negocio;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Login> Logar(Login entrada)
        {

            var retorno = negocio.Autenticar(entrada.LoginNome, entrada.Senha);

            if (retorno != null)
            {
                var login = retorno;
                return Ok(GerarAcessoLogin(login));
            }
            else
            {
                throw new Exception("Login não encontrado");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Login> Inserir(Login entrada)
        {

            var retorno = negocio.Inserir(entrada);

            if (retorno != null)
            {
                var login = retorno;
                return Ok(GerarAcessoLogin(login));
            }
            else
            {
                throw new Exception("Login não encontrado");
            }
        }

        private LoginUser GerarAcessoLogin(Login login)
        {


            if (login is null)
                return null;

            var token = Service.TokenService.GenerateToken(login);

            TokenAccount tk = new TokenAccount
            {
                access_token = token
            };

            Login saida = negocio.GetPorIdLogin(login.LoginId);


            

            var loginlogado = new LoginUser(saida, tk)
            {
                LoginNome = login.LoginNome
            };

            return loginlogado;
        }

        [HttpGet]
        [Authorize]
        public string Teste()
        {

            return "teste";
        }
    }
}
