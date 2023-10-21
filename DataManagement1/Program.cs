using DataManagement1;

var personHelper = new DataBaseHelper<Person>();

var values = new Dictionary<string, object>
{
    { "Name", "John" },
    { "Surname", "Doe" }
};
personHelper.InsertItem("Persons", values);

var persons = personHelper.GetItems("Persons", rdr => new Person
{
    Id = rdr.GetInt32(0),
    Name = rdr.GetString(1),
    Surname = rdr.GetString(2)
});

persons.ForEach(x=>Console.WriteLine($"Id: {x.Id}\t Name: {x.Name}\t Surname: {x.Surname}"));

personHelper.DeleteItem("Persons", "Id", 1);