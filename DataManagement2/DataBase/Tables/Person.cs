using System.ComponentModel.DataAnnotations;

namespace DataManagement2.DataBase.Tables;

public class Person
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}