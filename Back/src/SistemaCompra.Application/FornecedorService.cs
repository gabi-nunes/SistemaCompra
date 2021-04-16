using SistemaCompra.Application.Contratos;
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
    public class FornecedorService : IFornecedor
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IFornecedorPersist fornecedorPersist;


        public FornecedorService(IGeralPersist geral, IFornecedorPersist _fornecedorPersist)
        {
            FGeralPersist = geral;
            fornecedorPersist=_fornecedorPersist;
        }
       
        public async Task<user> AddFornecedor(user model)
        {
            try
            {
                FGeralPersist.Add<user>(model);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    var userRetorno = await fornecedorPersist.GetAllFornecedorByIdAsync(model.Id);

                    return userRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<user> UpdateFornecedor(int forncedorId, user model)
        {
            try
            {
                var LEuser = await fornecedorPersist.GetAllFornecedorByIdAsync(forncedorId);
                if (LEuser == null) return null;
                //atenção aqui
                model.Id = LEuser.Id;

                FGeralPersist.Update<user>(model);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await fornecedorPersist.GetAllFornecedorByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteFornecedor(int fornecedorId)
        {
            try
            {
                var usuario = await fornecedorPersist.GetAllFornecedorByIdAsync(fornecedorId);
                if (usuario == null) throw new Exception("Usuario para delete não encontrado.");

                FGeralPersist.Delete<user>(usuario);
                return await FGeralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<user[]> GetAllFornecedorAsync()
        {
            try
            {
                var user = await fornecedorPersist.GetAllFornecedorAsync();
                if (user == null) return null;
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<user[]> GetAllFornecedorbyNameAsync(string nome)
        {
            try
            {
                var usuarios = await fornecedorPersist.GetFornecedorByNameAsync(nome);
                if (usuarios == null) return null;
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<user> GetAllFornecedorbyemailAsync(string email)
        {
            try
            {
                var usuarios = await fornecedorPersist.GetFornecedorByEmailAsync(email);
                if (usuarios == null) return null;
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<user> GetFornecedorbyIdAsync(int fornecedorId)
        {
            try
            {
                var usuarios = await fornecedorPersist.GetAllFornecedorByIdAsync(fornecedorId);
                if (usuarios == null) return null;
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<user> LoginFornecedor(Login loginFornecedor)
        {
            if (loginFornecedor == null || loginFornecedor.email == null || loginFornecedor.senha == null)
            {
                return null;
            }
            try
            {
                var userlogin = await fornecedorPersist.GetLogin(loginFornecedor.email, loginFornecedor.senha);
                if (userlogin == null) return null;
                return userlogin;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<user> RecuperarSenhaFornecedor(string email)
        {
            try
            {
                var LEuser = await fornecedorPersist.GetFornecedorByEmailAsync(email);
                var emails = EnviarEmail(email);
                if (LEuser == null && emails == false) return null;
                LEuser.Senha = "Senha@123";


                FGeralPersist.Update<user>(LEuser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await fornecedorPersist.GetAllFornecedorByIdAsync(LEuser.Id);
                }

                return null;




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

        public async Task<user> AlterarSenhaFornecedor(int id, string senha)
        {
            try
            {
                var LEuser = await fornecedorPersist.GetAllFornecedorByIdAsync(id);
                if (LEuser == null) return null;
                //atenção aqui
                LEuser.Senha = senha;

                FGeralPersist.Update<user>(LEuser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await fornecedorPersist.GetAllFornecedorByIdAsync(LEuser.Id);
                }
                return null;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<user> visualizarCotacao(user model)
        {
            throw new NotImplementedException();
        }

        public async Task<user> EnviarOferta(user model)// Cotação 
        {
            try
            {
                FGeralPersist.Add<user>(model);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    var userRetorno = await fornecedorPersist.GetAllFornecedorByIdAsync(model.Id);

                    return userRetorno;
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
