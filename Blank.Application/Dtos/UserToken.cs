namespace Blank.Application.Dtos
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Mensagem { get; set; }
    }

}
