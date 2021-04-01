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
                    return await _userPresist.GetAllUserByIdAsync(model.CodigoSolicitante);
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
                var User = await _userPresist.GetAllUserByIdAsync(userId);
                if (User == null) throw new Exception("Usuarui não foi Deletado pois não foi encontrado");
                 
                FGeralPersist.Delete<user>(User);
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

         public async Task<user> RecuperarSenha(string email)
        {
            try
            {
                var login = await _userPresist.recuperarSenha(email);
               // var senha = EnviarEmail(email);
                if (login == null) return null;
                return login; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
          public bool EnviarEmail()
        {
            try
            {
                MailMessage _mailMensagem = new MailMessage();
                _mailMensagem.From= new MailAddress("gabiedununes@hotmail.com");

                _mailMensagem.CC.Add("gabrielleeduarda348@gmail.com");
                _mailMensagem.Subject= "Senha Nova";
                _mailMensagem.Body="Ola, sua nova senha é Senha123!";
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials= new NetworkCredential("gabiedununes@hotmail.com","XXXXX");

                _smtpClient.EnableSsl=true;
                _smtpClient.Send(_mailMensagem);

                return true;


                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}
