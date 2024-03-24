namespace ProjectPRN231.DTO;
public class TokenRequest
{
    public TokenRequest(string token, int? role,int? id)
    {
        Token = token;
        Role = role;
        Id = id;
    }
    public string Token { get; set; } = default!;
    public int? Role { get; set; }
    public int? Id { get; set; }

}
