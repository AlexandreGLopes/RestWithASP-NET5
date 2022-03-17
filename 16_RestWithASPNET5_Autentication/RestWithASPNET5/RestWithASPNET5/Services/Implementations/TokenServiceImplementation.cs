using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestWithASPNET5.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET5.Services.Implementations
{
    // Implementando o método responsável por Atualizar as informações do usuário
    public class TokenServiceImplementation : ITokenService
    {
        //Injetando o TokenConfiguration que é a classe responsável por armazenar as configurações do nosso token
        private TokenConfiguration _configuration;

        public TokenServiceImplementation(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccesToken(IEnumerable<Claim> claims)
        {
            //gerando uma chave simétrica baseada na chave que configuramos no appsettings.json
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            //Passamos a chave simétrica para gerar o signingCredential
            var thisSigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //Adicionando as opções do token
            // explicando as claims. A claims é o payload. E estes por sua vez são objetos JSON que contém as claims ou informações. É nessa parte que trabalhamos com a 'carga de dados' ou os 'dados enviados'. Basicamente existem 3 tipos de claims em paylos: Reservados, Publicos e privados
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                signingCredentials: thisSigningCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCulture)) throw new SecurityTokenException("Invalid Token");

            return principal;
        }
    }
}
