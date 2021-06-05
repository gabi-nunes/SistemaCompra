using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
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
          public async Task<user> AddUser(UserDto model)
    {
        try
        {
                user usuario = new user();
                usuario.nome = model.nome;
                usuario.Id = model.Id;
                usuario.Setor = model.Setor;
                usuario.Cargo = model.Cargo;
                usuario.email = model.email;
                usuario.Senha = "Senha@123";



            FGeralPersist.Add<user>(usuario);

            if (await FGeralPersist.SaveChangesAsync())
            {
                var userRetorno = await _userPresist.GetAllUserByIdAsync(model.Id);

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
                model.Id = LEuser.Id;

                FGeralPersist.Update<user>(model);
                if (await FGeralPersist.SaveChangesAsync())        
                {
                    return await _userPresist.GetAllUserByIdAsync(model.Id);
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
        public async Task<bool> GetIsUserAsync(string email)
        {
            try
            {
                var usuarios = await _userPresist.GetUserByEmailAsync(email);
                if (usuarios == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<user> GetUserByEmailAsync(string email)
        {
            try
            {
                var usuarios = await _userPresist.GetUserByEmailAsync(email);
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

         public async Task<user> Login(Login login)
        {
            if(login==null || login.email==null || login.senha==null ){
                return null;
            }
            try
            {
                var userlogin = await _userPresist.GetLogin(login.email, login.senha);
                if (userlogin == null) return null;
                return userlogin; 
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
                var LEuser = await _userPresist.GetUserByEmailAsync(email);
                var emails = EnviarEmail(email);
                if (LEuser == null && emails == false) return null;
                LEuser.Senha = "Senha@123";
    

                FGeralPersist.Update<user>(LEuser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _userPresist.GetAllUserByIdAsync(LEuser.Id);
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

        public async Task<user> AlterarSenha(int id, string senha)
        {
            try
            {
                var LEuser = await _userPresist.GetAllUserByIdAsync(id);
                if (LEuser == null) return null;
                //atenção aqui
                LEuser.Senha = senha;

                FGeralPersist.Update<user>(LEuser);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _userPresist.GetAllUserByIdAsync(LEuser.Id);
                }
                return null;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> TheLastID()
        {
            try
            {
                int idLast;
                var user= await _userPresist.GetIdLast();
                if (user == null) return 0;

                idLast = user.Id;
                return idLast;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
