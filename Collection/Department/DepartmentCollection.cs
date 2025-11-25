namespace KYAPI.Collection;

public class DepartmentCollection : TextBaseCollection<Department>
{
    public DepartmentCollection() =>
        Collection = new List<Department>
        {
            new Department { Code = "PD", Description = "Packing Department" },
            new Department { Code = "SD", Description = "Sales Department" },
            new Department { Code = "STD", Description = "Store Department" },
            new Department { Code = "MD", Description = "Marketing Department" },
            new Department { Code = "HRD", Description = "Human Resource Department" },
        };
}

public class Department : TextBaseEntity { }
