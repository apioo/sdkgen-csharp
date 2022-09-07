
namespace Sdkgen.Client.TokenStore;

public class MemoryTokenStore : TokenStoreInterface 
{
    private AccessToken? token;
    public AccessToken? get()
    {
        return this.token;
    }
    public void persist(AccessToken token)
    {
        this.token = token;
    }
    public void remove()
    {
        this.token = null;
    }
}
