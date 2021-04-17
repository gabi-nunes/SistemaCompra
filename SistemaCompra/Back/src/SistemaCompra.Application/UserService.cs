using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;


namespace SistemaCompra.Application
{
    public class UserService : IUserService
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IUserPersist _UserPresist;

        private User UserUsuario;
        public UserService(IUserPersist UserPresist, IGeralPersist geral)
        {
            _UserPresist = UserPresist;
            FGeralPersist = geral;

        }
          public async Task<User> AddUser(User model)
    {
        try
        {
            FGeralPersist.Add<User>(model);

            if (await FGeralPersist.SaveChangesAsync())
            {
                var UserRetorno = await _UserPresist.GetAllUserByIdAsync(model.Id);

                return UserRetorno;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

        public async Task<User> UpdateUser(int UserId, User model)
        {
            try
            {
                var LEUser = await _UserPresist.GetAllUserByIdAsync(UserId);
                if (LEUser == null) return null;
                 //atenção aqui
                model.Id = LEUser.Id;

                FGeralPersist.Update<User>(model);
                if (await FGeralPersist.SaveChangesAsync())        
                {
                    return await _UserPresist.GetAllUserByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
         public async Task<bool> DeleteUser(int UserId)
    {
        try
        {
            var usuario = await _UserPresist.GetAllUserByIdAsync(UserId);
            if (usuario == null) throw new Exception("Usuario para delete não encontrado.");

            FGeralPersist.Delete<User>(usuario);
            return await FGeralPersist.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

        public async Task<User[]> GetAllUserAsync()
        {
            try
            {
                var usuario = await _UserPresist.GetAllUserAsync();
                if (usuario == null) return null;
                return usuario; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User[]> GetAllUserbyNameAsync(string nome)
        {
            try
            {
                var usuarios = await _UserPresist.GetUserByNameAsync(nome);
                if (usuarios == null) return null;
                return usuarios; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserbyIdAsync(int UserId)
        {
            try
            {
                var usuarios = await _UserPresist.GetAllUserByIdAsync(UserId);
                if (usuarios == null) return null;
                return usuarios; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public async Task<User> Login(Login login)
        {
            if(login==null || login.email==null || login.senha==null ){
                return null;
            }
            try
            {
                var Userlogin = await _UserPresist.GetLogin(login.email, login.senha);
                if (Userlogin == null) return null;
                return Userlogin; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public async Task<User> RecuperarSenha(string email)
        {
            try
            {
                var LEUser = await _UserPresist.GetUserByEmailAsync(email);
                var emails = EnviarEmail(email);
                if (LEUser == null && emails == false) return null;
                LEUser.Senha = "Senha@123";
    

                FGeralPersist.Update<User>(LEUser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _UserPresist.GetAllUserByIdAsync(LEUser.Id);
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

        public async Task<User> AlterarSenha(int id, string senha)
        {
            try
            {
                var LEUser = await _UserPresist.GetAllUserByIdAsync(id);
                if (LEUser == null) return null;
                //atenção aqui
                LEUser.Senha = senha;

                FGeralPersist.Update<User>(LEUser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _UserPresist.GetAllUserByIdAsync(LEUser.Id);
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
