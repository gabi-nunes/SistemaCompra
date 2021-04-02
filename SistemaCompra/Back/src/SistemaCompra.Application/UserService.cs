using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;


namespace SistemaCompra.Application
{
    public class UserService : IuserService
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IUserPersist _userPresist;
        public UserService(IUserPersist UserPresist, IGeralPersist geral)
        {
            _userPresist = UserPresist;
            FGeralPersist = geral;

        }
          public async Task<user> AddUser(user model)
    {
        try
        {
            FGeralPersist.Add<user>(model);

            if (await FGeralPersist.SaveChangesAsync())
            {
                var userRetorno = await _userPresist.GetAllUserByIdAsync(model.CodigoSolicitante);

                return userRetorno;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

        public async Task<user> UpdateUser(int userId, user model)
        {
            try
            {
                var LEuser = await _userPresist.GetAllUserByIdAsync(userId);
                if (LEuser == null) return null;
                 //atenção aqui
                model.CodigoSolicitante = LEuser.CodigoSolicitante;

                FGeralPersist.Update<user>(model);
                if (await FGeralPersist.SaveChangesAsync())        
                {
                    return await _userPresist.GetAllUserByIdAsync(model.CodigoSolicitante);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
         public async Task<bool> DeleteUser(int userId)
    {
        try
        {
            var usuario = await _userPresist.GetAllUserByIdAsync(userId);
            if (usuario == null) throw new Exception("Usuario para delete não encontrado.");

            FGeralPersist.Delete<user>(usuario);
            return await FGeralPersist.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

        public async Task<user[]> GetAllUserAsync()
        {
            try
            {
                var user = await _userPresist.GetAllUserAsync();
                if (user == null) return null;
                return user; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<user[]> GetAllUserbyNameAsync(string nome)
        {
            try
            {
                var usuarios = await _userPresist.GetUserByNameAsync(nome);
                if (usuarios == null) return null;
                return usuarios; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<user> GetuserbyIdAsync(int userId)
        {
            try
            {
                var usuarios = await _userPresist.GetAllUserByIdAsync(userId);
                if (usuarios == null) return null;
                return usuarios; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public async Task<user> Login(string email, string senha)
        {
            try
            {
                var login = await _userPresist.GetLogin(email, senha);
                if (login == null) return null;
                return login; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public async Task<bool> RecuperarSenha(string email)
        {
            try
            {
                //var login = await _userPresist.recuperarSenha(email);
                var enviarEmail = EnviarEmail(email);
                return  enviarEmail; 
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
                _mailMessage.Body = "<b>Olá Tudo bem??</b><p>Informamos que sua nova senha de acesso será Senha123@, após a primeira entrada trocar a senha.</p>";

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

       
    }
}
