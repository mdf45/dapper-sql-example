using System.Text.Json.Serialization;

namespace MsSQL.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public int UserId { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
