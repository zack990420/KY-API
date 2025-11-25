using HashidsNet;

namespace KYAPI.Services;

public class IdHasher : IIdHasher
{
    private readonly Hashids _hashids;

    public IdHasher(IConfiguration configuration)
    {
        var salt = configuration["Hashids:Salt"] ?? "default-salt-for-dev";
        var minHashLength = int.Parse(configuration["Hashids:MinHashLength"] ?? "8");
        _hashids = new Hashids(salt, minHashLength);
    }

    public string Hash(long id)
    {
        return _hashids.EncodeLong(id);
    }

    public long Unhash(string hash)
    {
        var decoded = _hashids.DecodeLong(hash);
        if (decoded.Length == 0)
        {
            throw new ArgumentException("Invalid hash", nameof(hash));
        }
        return decoded[0];
    }
}
