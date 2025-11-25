namespace MyWebApi.Services;

public interface IIdHasher
{
    string Hash(long id);
    long Unhash(string hash);
}
