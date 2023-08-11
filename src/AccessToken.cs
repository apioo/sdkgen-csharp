namespace Sdkgen.Client;

public class AccessToken
{
    public AccessToken(string token, string tokenType, int expiresIn, string refreshToken, string scope)
    {
        Token = token;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
        RefreshToken = refreshToken;
        Scope = scope;
    }

    public string Token { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public string Scope { get; set; }
}