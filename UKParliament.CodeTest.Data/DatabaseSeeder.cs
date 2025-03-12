namespace UKParliament.CodeTest.Data;

public class DatabaseSeeder
{
    private readonly PersonManagerContext _context;

    public DatabaseSeeder(PersonManagerContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Database.EnsureCreated())
        {
            _context.Departments.AddRange(
                new Department { Id = 1, Name = "Sales" },
                new Department { Id = 2, Name = "Marketing" },
                new Department { Id = 3, Name = "Finance" },
                new Department { Id = 4, Name = "HR" }
            );

            _context.People.AddRange(
                new Person
                {
                    Id = 1,
                    FirstName = "Betsi",
                    LastName = "Cadwaladr",
                    DateOfBirth = new DateTime(1789, 5, 24),
                    DepartmentId = 2 // Marketing
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Megan",
                    LastName = "Lloyd George",
                    DateOfBirth = new DateTime(1902, 11, 22),
                    DepartmentId = 1 // Sales
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Richard",
                    LastName = "Burton",
                    DateOfBirth = new DateTime(1925, 11, 10),
                    DepartmentId = 3 // Finance
                }
            );

            _context.SaveChanges();
        }
    }
}

