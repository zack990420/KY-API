namespace KYAPI.Collection;

public class PositionCollection : TextBaseCollection<Position>
{
    public PositionCollection() =>
        Collection = new List<Position>
        {
            new Position { Code = "DIR", Description = "Director" },
            new Position { Code = "CTO", Description = "Chief Technology Officer" },
            new Position { Code = "CFO", Description = "Chief Financial Officer" },
            new Position { Code = "MGR", Description = "Manager" },
            new Position { Code = "STAFF", Description = "Staff" },
        };
}

public class Position : TextBaseEntity { }
