using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCompra.API.Controllers
{
    public class GerenciarFornecedorController : Controller
    {
        [ApiController]
        [Route("[controller]")]
        public class UserController : ControllerBase
        {
            private readonly IFornecedor FornecedorService;
            public UserController(IFornecedor _fornecedorService)
            {
                this.FornecedorService = _fornecedorService;
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
                try
                {
                    var usuarios = await FornecedorService.GetAllFornecedorAsync();
                    if (usuarios == null) return NotFound("Nenhum fornecedor encontrado!");
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
                    var usuarios = await FornecedorService.GetFornecedorbyIdAsync(Id);
                    if (usuarios == null) return NotFound("Nenhum usuarios foi Encontrado com o Id informado.");
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
                    var usuarios = await FornecedorService.GetAllFornecedorbyNameAsync(nome);
                    if (usuarios == null) return NotFound("Nenhum usuario foi Encontrado com o nome informado.");
                    return Ok(usuarios);
                }
                catch (Exception ex)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo tema. Erro: {ex.Message}");
                }
            }

            [HttpPost("Registrar")]
            public async Task<IActionResult> Post([FromBody] user model)
            {
                try
                {
                    var usuario = await FornecedorService.AddFornecedor(model);
                    if (usuario == null) return BadRequest("Erro ao tentar Adicionar o Fornecedor.");
                    return Ok(usuario);
                }
                catch (Exception ex)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o Fornecedor. Erro: {ex.Message}");
                }
            }

            [HttpPut("Atualiza/{Id}")]
            public async Task<IActionResult> Put(int Id, user model)
            {
                try
                {
                    var usuario = await FornecedorService.UpdateFornecedor(Id, model);
                    if (usuario == null) return BadRequest("Não foi possível encontrar o ususario");
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
                    var usuario = await FornecedorService.GetFornecedorbyIdAsync(id);
                    if (usuario == null) return NoContent();

                    return await FornecedorService.DeleteFornecedor(id) ?
                           Ok(new { messagem = "Deletado" }) :
                           throw new Exception("Ocorreu um problem não específico ao tentar deletar Fornecedor.");
                }
                catch (Exception ex)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar deletar Fornecedor. Erro: {ex.Message}");
                }
            }

            [HttpPost("Login")]
            public async Task<ActionResult> Login([FromBody] Login login)
            {
                if (login == null || login.email == null || login.senha == null)
                {
                    return BadRequest("Fornecedor ou senha invalidos!");
                    try
                    {

                        var usuario = await FornecedorService.LoginFornecedor(login);

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
            }

            [HttpPost("RecuperarSenha")]
            public async Task<IActionResult> RecuperarSenha([FromBody] Login login)
            {
                try
                {
                    var usuario = await FornecedorService.RecuperarSenhaFornecedor(login.email);
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
                    var usuario = await FornecedorService.AlterarSenhaFornecedor(login.id, login.senha);

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
                    var usuarios = await FornecedorService.GetAllFornecedorbyemailAsync(email);
                    if (usuarios == null) return NotFound("Nenhum Fornecedor foi Encontrado com o Id informado.");
                    return Ok(usuarios);
                }
                catch (Exception ex)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo Id. Erro: {ex.Message}");
                }
            }

            [HttpGet("Visualizarcotacao")]
            public async Task<IActionResult> GetVisualizarCotacao(user model)
            {
                try
                {
                    var usuarios = await FornecedorService.GetAllFornecedorbyemailAsync(email);
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
}
}
