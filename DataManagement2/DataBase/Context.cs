using DataManagement2.DataBase.Tables;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
}