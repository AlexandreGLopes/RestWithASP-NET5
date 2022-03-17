namespace RestWithASPNET5.Data.VO
{
    // A classe TokenVO é a que vai gerar e armazenar o token e devolver na response
    public class TokenVO
    {
        public TokenVO(bool authenticated, string created, string expiration, string accessToken, string refreshToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public bool Authenticated { get; set; }

        public string Created { get; set; }

        public string Expiration { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
