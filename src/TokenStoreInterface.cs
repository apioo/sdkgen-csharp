
namespace Sdkgen.Client;

public interface TokenStoreInterface
{
    public AccessToken? get();
    public void persist(AccessToken token);
    public void remove();
}
