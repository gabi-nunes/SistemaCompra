using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaCompra.Application;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Domain;
using SistemaCompra.Persistence;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        public UserController (IUserService UserService)
        {
            this.UserService = UserService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
            var usuarios= await UserService.GetAllUserAsync();
            if (usuarios== null ) return NotFound("Nenhum Usuario encontrado!");
            return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuarios. Erro: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
            var usuarios = await UserService.GetUserbyIdAsync(Id);
            if (usuarios == null ) return NotFound("Nenhum usuarios foi Encontrado com o Id informado.");
            return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo Id. Erro: {ex.Message}");
            }
        }

        [HttpGet("User/{nome}")]
        public async Task<IActionResult> GetBynome(string nome)
        {
            try
            {
            var usuarios= await UserService.GetAllUserbyNameAsync(nome);
            if (usuarios== null ) return NotFound("Nenhum usuario foi Encontrado com o nome informado.");
            return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo tema. Erro: {ex.Message}");
            }
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Post([FromBody] User model)
        {
            try
            {
            var usuario = await UserService.AddUser(model);
            if (usuario == null ) return BadRequest("Erro ao tentar Adicionar o Usuario.");
            return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o usuario. Erro: {ex.Message}");
            }
        }

        [HttpPut("Atualiza/{Id}")]
        public async Task<IActionResult> Put(int Id, User model)
        {
            try
            {
            var usuario = await UserService.UpdateUser(Id, model);
            if (usuario == null ) return BadRequest("Não foi possível encontrar o ususario");
            return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar o usuario. Erro: {ex.Message}");
            }
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await UserService.GetUserbyIdAsync(id);
                if (usuario == null) return NoContent();

                return await UserService.DeleteUser(id) ? 
                       Ok(new { messagem = "Deletado"}) : 
                       throw new Exception("Ocorreu um problem não específico ao tentar deletar Usuario.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] Login login) 
        {
            if(login == null || login.email == null || login.senha== null ){
                return BadRequest("Usuario ou senha invalidos!");
            }
            try
            {

                var usuario = await UserService.Login(login);

                // Verifica se o usuário existe
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                }
                // Gera o Token
                var token = TokenService.GenerateToken(usuario);

                Response.Headers.Add("token", token);
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar o login. Erro: {ex.Message}");
            }
        }

        [HttpPost("RecuperarSenha")]
        public async Task<IActionResult> RecuperarSenha([FromQuery]string email)
        {
            try
            {
                var usuario = await UserService.RecuperarSenha(email);
                usuario.Senha = "Senha@123";

                if (usuario == null)
                {
                    return BadRequest("Erro ao recuperar. Tente Novamente!");
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar o login. Erro: {ex.Message}");
            }
        }


        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> PutAlterarSenha(int id, string senha)
        {
            try
            {
                var usuario = await UserService.AlterarSenha(id, senha);

                if (usuario == null) return BadRequest("Erro ao alterar senha. Tente Novamente!");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar a alteração de senha. Erro: {ex.Message}");
            }
        }

    }


}
