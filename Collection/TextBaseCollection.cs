namespace KYAPI.Collection;

public abstract class TextBaseCollection<T>
    where T : TextBaseEntity
{
    public List<T> Collection { get; set; } = new();

    public T? GetByCode(string code)
    {
        return Collection.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }

    public List<T> GetAll()
    {
        return Collection;
    }
}
