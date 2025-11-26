namespace KYAPI.Interfaces;

public interface IIdHasher
{
    string Hash(long id);
    long Unhash(string hash);
}
