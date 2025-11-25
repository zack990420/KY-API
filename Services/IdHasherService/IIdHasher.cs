namespace KYAPI.Services;

public interface IIdHasher
{
    string Hash(long id);
    long Unhash(string hash);
}
