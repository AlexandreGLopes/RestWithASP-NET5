using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Model;
using RestWithASPNET5.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNET5.Repository.Implementations
{
    // Fizemos métodos genéricos mas como User é um caso especial vamos fazer um Repository só pra ele
    public class UserRepositoryImplementation : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public User UserValidateCredentials(UserVO user)
        {
            // A senha que chega está em texto plano, e a senha do banco está encriptada
            // Precisaremos encriptar para comparar com o banco
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            //Validações se o nome e a senha estão corretas e retorna
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User UserValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == userName));
            // se o user for igual a null vai retornar false
            if (user == null) return false;
            // Se tiver um user vai anular a refreshToken dele
            user.RefreshToken = null;
            // persistindo as alterações no banco em user
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            //Se não encontrar no banco ninguém com o mesmo id do User recebido como parametro vai retornar null
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;
            //Se encontrar alguém que tenha o mesmo id vai armazenar em result
            var result = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));
            //Se o result for diferente de nulo ele vai tentar atualizar as informações do usuário fazendo um update e retorna o result
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        //Método reposnsável por encriptar a senha
        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
