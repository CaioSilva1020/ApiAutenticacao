using Entidades;
using System.Runtime.Serialization;

namespace ApiAutenticacao
{
    public class info_key_user
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    [DataContract]
    public class TokenAccount
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public long expires_in { get; set; }

        [DataMember]
        public string error { get; set; }

        [DataMember]
        public string error_description { get; set; }
    }

    [DataContract]
    public class LoginUser
    {
        [DataMember]
        public info_key_user info_key_user { get; set; }

        [DataMember]
        public decimal LoginId { get; set; }
        [DataMember]
        public string? LoginNome { get; set; }

        [DataMember]
        public TokenAccount Token { get; set; }

        [DataMember]
        public DateTime TokenDataExpiracao { get; set; }
        [DataMember]
        public string strTokenDataExpiracao { get; set; }


        public LoginUser(Login login, TokenAccount token)
        {
            if (login != null)
            {

                this.LoginId = login.LoginId;
                this.LoginNome = login.LoginNome;
                this.Token = token;
                this.TokenDataExpiracao = DateTime.Now.AddSeconds(token.expires_in);
                this.strTokenDataExpiracao = DateTime.Now.AddSeconds(token.expires_in).ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}
