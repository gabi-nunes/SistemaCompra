using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IFornecedorPersist _FornecedorPresist;
        public FornecedorService(IFornecedorPersist FornecedorPresist, IGeralPersist geral)
        {
            _FornecedorPresist = FornecedorPresist;
            FGeralPersist = geral;
        }
        public async Task<Fornecedor> GetbyemailAsync(string email)
        {
            try
            {
                var usuarios = await _FornecedorPresist.GetFornecedorByEmailAsync(email);
                if (usuarios == null) return null;
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> TheLastPosition(int idFamilia)
        {
            try
            {
                int idLast;
                var user = await _FornecedorPresist.GetIdLast(idFamilia);
                if (user == null) return 0;

                idLast = user.Posicao;
                return idLast;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> AddFornecedor(FornecedorDto model)
        {
            try
            { 
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.CNPJ = model.CNPJ;
                fornecedor.Nome = model.Nome;
                fornecedor.Cidade = model.Cidade;
                fornecedor.Endereco = model.Endereco;
                fornecedor.Bairro = model.Bairro;
                fornecedor.Numero = model.Numero;
                fornecedor.Complemento = model.Complemento;
                fornecedor.Estado = model.Estado;
                fornecedor.CEP = model.CEP;
                fornecedor.InscricaoMunicipal = model.InscricaoMunicipal;
                fornecedor.InscricaoEstadual = model.InscricaoEstadual;
                fornecedor.Email = model.Email;
                fornecedor.Telefone= model.Telefone;
                fornecedor.Celular = model.Celular;
                fornecedor.PontuacaoRanking = model.PontuacaoRanking;
                fornecedor.FamiliaProdutoId = model.FamiliaProdutoId;
                fornecedor.Senha = "for@123";

                int lastPosiciao = await TheLastPosition(fornecedor.FamiliaProdutoId);

                fornecedor.Posicao = lastPosiciao + 1;

                FGeralPersist.Add<Fornecedor>(fornecedor);
                EnviarEmailCadastro(fornecedor.Email);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    var FornecedorRetorno = await _FornecedorPresist.GetAllFornecedorByIdAsync(fornecedor.Id);

                    return FornecedorRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> AlterarSenha(int id, string senha)
        {
            try
            {
                var LEuser = await _FornecedorPresist.GetAllFornecedorByIdAsync(id);
                if (LEuser == null) return null;
                //atenção aqui
                LEuser.Senha = senha;

                FGeralPersist.Update<Fornecedor>(LEuser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _FornecedorPresist.GetAllFornecedorByIdAsync(LEuser.Id);
                }
                return null;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public bool EnviarEmailCadastro(string email)
        {
            try
            {
               // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress("goodplacecompras@gmail.com");

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                _mailMessage.CC.Add(email);
                _mailMessage.Subject = "Sistema Compra :)";
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = "<b>Olá Tudo bem?</b><p>Informamos que você foi cadastrado no sistema! Sua senha é for@123.Parabens!! entre no http://localhost:4200/user/login para acessar sua conta</p>";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("goodplacecompras@gmail.com", "Tcc123456");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFornecedor(int FornecedorId)
        {
            try
            {
                var fornecedor = await _FornecedorPresist.GetAllFornecedorByIdAsync(FornecedorId);
                if (fornecedor == null) throw new Exception("Usuario para delete não encontrado.");

                FGeralPersist.Delete<Fornecedor>(fornecedor);
                return await FGeralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EnviarEmail(string email)
        {
            try
            {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress("goodplacecompras@gmail.com");

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                _mailMessage.CC.Add(email);
                _mailMessage.Subject = "Sistema Compra :)";
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = "<b>Olá Tudo bem?</b><p>Informamos que sua nova senha de acesso será Senha123@, após a primeira entrada no sistema sua senha deverá ser alterada!.</p>";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("goodplacecompras@gmail.com", "Tcc123456");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
    }

        public async Task<Fornecedor[]> GetAllFornecedorAsync()
        {
            try
            {
                var fornecedor = await _FornecedorPresist.GetAllFornecedorAsync();
                if (fornecedor == null) return null;
                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> GetAllFornecedorbyemailAsync(string email)
        {
            try
            {
                var usuarios = await _FornecedorPresist.GetFornecedorByEmailAsync(email);
                if (usuarios == null) return null;
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor[]> GetAllFornecedorbyNameAsync(string nome)
        {
            try
            {
                var fornecedor = await _FornecedorPresist.GetFornecedorByNameAsync(nome);
                if (fornecedor == null) return null;
                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> GetFornecedorbyIdAsync(int FornecedorId)
        {
            try
            {
                var fornecedor = await _FornecedorPresist.GetAllFornecedorByIdAsync(FornecedorId);
                if (fornecedor == null) return null;
                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> Login(Login login)
        {
            if (login == null || login.email == null || login.senha == null)
            {
                return null;
            }
            try
            {
                var fornecedorlogin = await _FornecedorPresist.GetLogin(login.email, login.senha);
                if (fornecedorlogin == null) return null;
                return fornecedorlogin;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> RecuperarSenha(string email)
        {
            try
            {
                var LEuser = await _FornecedorPresist.GetFornecedorByEmailAsync(email);
                var emails = EnviarEmail(email);
                if (LEuser == null && emails == false) return null;
                LEuser.Senha = "Senha@123";


                FGeralPersist.Update<Fornecedor>(LEuser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _FornecedorPresist.GetAllFornecedorByIdAsync(LEuser.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> UpdateFornecedor(int FornecedorId, Fornecedor model)
        {
            try
            {
                var LEuser = await _FornecedorPresist.GetAllFornecedorByIdAsync(FornecedorId);
                if (LEuser == null) return null;
                //atenção aqui
                model.Id = LEuser.Id;

                FGeralPersist.Update<Fornecedor>(model);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _FornecedorPresist.GetAllFornecedorByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
