namespace Sdkgen.Client;

public interface ITokenStore
{
    public AccessToken? Get();
    public void Persist(AccessToken token);
    public void Remove();
}