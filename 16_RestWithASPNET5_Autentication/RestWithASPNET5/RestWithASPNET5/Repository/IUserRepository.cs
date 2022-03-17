using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Model;

namespace RestWithASPNET5.Repository
{
    // Fizemos métodos genéricos mas como User é um caso especial vamos fazer um Repository só pra ele
    public interface IUserRepository
    {
        User UserValidateCredentials(UserVO user);

        User UserValidateCredentials(string userName);

        bool RevokeToken(string userName);

        public User RefreshUserInfo(User user);
    }
}
