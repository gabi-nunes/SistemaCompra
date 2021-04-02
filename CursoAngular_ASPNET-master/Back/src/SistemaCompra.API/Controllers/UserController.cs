using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Domain;
using SistemaCompra.Persistence;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IuserService UserService;
        public UserController (IuserService userService)
        {
            this.UserService = userService;
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
            var usuarios = await UserService.GetuserbyIdAsync(Id);
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


        [HttpPost("Registar")]
        public async Task<IActionResult> Post(user model)
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
        public async Task<IActionResult> Put(int Id, user model)
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

        [HttpDelete("Excluir/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
            if (await UserService.DeleteUser(Id))
                return Ok("Usuario Deletado");
            else
                return BadRequest("Erro ao Deletar o Usuario");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o Usuario. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> PostLogin(string email , string senha)
        {
            try
            {
            var usuario = await UserService.Login(email, senha);
            if (usuario == null ) return BadRequest("Erro efetuar o Usuario. Tente Novamente!");
            return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar o login. Erro: {ex.Message}");
            }
        }

        [HttpPut("RecuperarSenha")]
        public async Task<IActionResult> PostRecuperarSenha(string id, string email, user model)
        {
            try
            {
            var usuario = await UserService.RecuperarSenha(email);

           // if (usuario == null ) return BadRequest("Erro efetuar o Usuario. Tente Novamente!");
            return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar o login. Erro: {ex.Message}");
            }
        }

    }

}
