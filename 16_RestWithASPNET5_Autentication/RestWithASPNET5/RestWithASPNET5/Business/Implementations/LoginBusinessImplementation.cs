using Microsoft.IdentityModel.JsonWebTokens;
using RestWithASPNET5.Configurations;
using RestWithASPNET5.Data.VO;
using RestWithASPNET5.Repository;
using RestWithASPNET5.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestWithASPNET5.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private TokenConfiguration _configuration;

        private IUserRepository _repository;

        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            // pega as credenciais e valida no banco
            var user = _repository.UserValidateCredentials(userCredentials);
            // se for nulo vai retronar null e depois vamos aplicar a validação no controller
            if (user == null) return null;

            //Definindo as claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            // criar o accesstoken (que é o token usado para se autenticar)
            // e o refreshtoken (que é o accestoken estiver expirado ele vai usar o refreshtoken)
            // usando o tokenservice
            var accessToken = _tokenService.GenerateAccesToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            //vamos setar o valor do user. aquele usuário que recuperamos na base de dados
            //setando o refresh token
            user.RefreshToken = refreshToken;
            //o _configuration.DaysToExpiry e .Minutes pega os dados que setamos lá no appsettings
            //setando a data de expiração
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            // agora que geramos o token e atualizamos as informações precisamos persistir na base
            _repository.RefreshUserInfo(user);

            // definindo quando gera o token (agora)
            DateTime createDate = DateTime.Now;
            //setando quando ele vai expirar
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            // setar as informações do token
            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            // criar o accesstoken (que é o token usado para se autenticar)
            // e o refreshtoken (que é o accestoken estiver expirado ele vai usar o refreshtoken)
            // usando o tokenservice
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principal.Identity.Name;

            var user = _repository.UserValidateCredentials(userName);

            // Se o usuário for nulo, ou se o refreshtoken for diferente do que recebemos no início do método,
            // ou se a data for menor que a data atual: retornaremos nulo
            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            // Atualizando o accessToken e o RefreshToken
            //precisamos atualizar os dois. O primeiro para autoriação
            //o segundo porque das próximas vezes que fizer o refresh teremos que usar um novo refresh
            // e não o anterior
            accessToken = _tokenService.GenerateAccesToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            // agora que geramos o novo token e atualizamos as informações precisamos persistir na base
            _repository.RefreshUserInfo(user);

            // definindo quando gera o token (agora)
            DateTime createDate = DateTime.Now;
            //setando quando ele vai expirar
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            // setar as informações do token
            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
