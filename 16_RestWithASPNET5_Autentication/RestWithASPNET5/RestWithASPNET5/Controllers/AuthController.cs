using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET5.Business;
using RestWithASPNET5.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Injeção de loginBusiness
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            // se o user for igual a nulo vai retornar um badresquest
            if (user == null) return BadRequest("Invalid client request");
            //caso nãom seja nulo vamos gerar o token
            var token = _loginBusiness.ValidateCredentials(user);
            //se o token estiver nulo retornará um não autorizado
            if (token == null) return Unauthorized();
            //se o token não estiver null vai retornar o token
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            // se o tokenVO for igual a nulo vai retornar um badresquest
            if (tokenVO == null) return BadRequest("Invalid client request");
            //caso não seja nulo tentar validar as credenciais retornando um token
            var token = _loginBusiness.ValidateCredentials(tokenVO);
            //se o token estiver nulo retornará um não autorizado
            if (token == null) return BadRequest("Invalid client request");
            //se o token não estiver null vai retornar o token
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            //Aqui nem precisamos passar parametros. só com o bearer o framework já sabe quem é
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);
            //se o result for false vamos retornar um badRequest             
            if (!result) return BadRequest("Invalid client request");
            //se o token não estiver null vai retornar o token
            return NoContent();
        }
    }
}
