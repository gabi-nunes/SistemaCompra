using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService fornecedorService;

        public FornecedorController(IFornecedorService _fornecedorService)
        {
            fornecedorService = _fornecedorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuarios = await fornecedorService.GetAllFornecedorAsync();
                if (usuarios == null) return NotFound("Nenhum Fornecedor encontrado!");
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
                var usuarios = await fornecedorService.GetFornecedorbyIdAsync(Id);
                if (usuarios == null) return NotFound("Nenhum fornecedor foi Encontrado com o Id informado.");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo Id. Erro: {ex.Message}");
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetBynome(string nome)
        {
            try
            {
                var usuarios = await fornecedorService.GetAllFornecedorbyNameAsync(nome);
                if (usuarios == null) return NotFound("Nenhum usuario foi Encontrado com o nome informado.");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo tema. Erro: {ex.Message}");
            }
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Post([FromBody] FornecedorDto model)
        {
            try
            {
                var fornecedor = await fornecedorService.AddFornecedor(model);
                if (fornecedor == null) return BadRequest("Erro ao tentar Adicionar o Fornecedor.");
                return Ok(fornecedor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o usuario. Erro: {ex.Message}");
            }
        }

        [HttpPut("Atualiza/{Id}")]
        public async Task<IActionResult> Put(int Id, Fornecedor model)
        {
            try
            {
                var fornecedor = await fornecedorService.UpdateFornecedor(Id, model);
                if (fornecedor == null) return BadRequest("Não foi possível encontrar o Form");
                return Ok(fornecedor);
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
                var fornecedor = await fornecedorService.GetFornecedorbyIdAsync(id);
                if (fornecedor == null) return NoContent();

                return await fornecedorService.DeleteFornecedor(id) ?
                       Ok(new { messagem = "Deletado" }) :
                       throw new Exception("Ocorreu um problem não específico ao tentar deletar Fornecedor.");
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
            if (login == null || login.email == null || login.senha == null)
            {
                return BadRequest("Usuario ou senha invalidos!");
            }
            try
            {

                var usuario = await fornecedorService.Login(login);

                // Verifica se o usuário existe
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                }
                // Gera o Token
                var token = tokenServiceFornecedor.GenerateToken(usuario);

                Response.Headers.Add("token", token);
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar o login. Erro: {ex.Message}");
            }
        }

        [HttpPost("RecuperarSenha")]
        public async Task<IActionResult> RecuperarSenha([FromBody] Login login)
        {
            try
            {
                var usuario = await fornecedorService.RecuperarSenha(login.email);
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
        public async Task<IActionResult> PutAlterarSenha([FromBody] Login login)
        {
            try
            {
                var usuario = await fornecedorService.AlterarSenha(login.id, login.senha);

                if (usuario == null) return BadRequest("Erro ao alterar senha. Tente Novamente!");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar a alteração de senha. Erro: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByemail(string email)
        {
            try
            {
                var usuarios = await fornecedorService.GetAllFornecedorbyemailAsync(email);
                if (usuarios == null) return NotFound("Nenhum Fornecedor foi Encontrado com o Id informado.");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo Id. Erro: {ex.Message}");
            }
        }

    }
}

