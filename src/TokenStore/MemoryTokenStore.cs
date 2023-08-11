namespace Sdkgen.Client.TokenStore;

public class MemoryTokenStore : ITokenStore
{
    private AccessToken? _token;

    public AccessToken? Get()
    {
        return this._token;
    }

    public void Persist(AccessToken token)
    {
        this._token = token;
    }

    public void Remove()
    {
        this._token = null;
    }
}