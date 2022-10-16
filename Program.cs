// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Bogus;
using Bogus.DataSets;

var addressFaker = new Faker<Address>()
    .RuleFor(p => p.AddressLine1, f => f.Address.StreetAddress(false))
    .RuleFor(p => p.AddressLine2, f => f.Address.City())
    .RuleFor(p => p.Country, f => f.Address.Country());

var customerFaker = new Faker<Customer>()
    .RuleFor(p => p.FirstName,
        f => f.Name.FirstName(Name.Gender.Male))
    .RuleFor(p => p.LastName,
        f => f.Name.LastName(Name.Gender.Male))
    .RuleFor(p => p.Email, (f, customer) =>
        f.Internet.Email(customer.FirstName, customer.LastName, "gmail"))
    .RuleFor(p => p.Company, f => f.Company.CompanyName())
    .RuleFor(p => p.RegisteredDate,
        (f => f.Date.Between(new DateTime(2022, 1, 1), DateTime.Now)))
    .RuleFor(p => p.Addresses, f => addressFaker.Generate(5).ToList());


// Generator according to rule set
var customer = customerFaker.Generate();

Console.WriteLine(JsonSerializer.Serialize(customer));


Console.ReadKey();

public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Company { get; set; }
    public DateTime RegisteredDate { get; set; }
    public List<Address> Addresses { get; set; }
}

public class Address
{
    public string StreetNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Country { get; set; }
}

/*
 {
   "FirstName":"Jose",
   "LastName":"Crist",
   "Email":"Jose.Crist@gmail",
   "Company":"Carter - Nolan",
   "RegisteredDate":"2022-08-21T12:19:37.3359242",
   "Addresses":[
      {
         "StreetNumber":null,
         "AddressLine1":"945 Litzy Lodge",
         "AddressLine2":"Hilpertstad",
         "Country":"Malta"
      },
      {
         "StreetNumber":null,
         "AddressLine1":"544 Noel Forks",
         "AddressLine2":"Hagenesfurt",
         "Country":"Namibia"
      },
      {
         "StreetNumber":null,
         "AddressLine1":"97532 O\u0027Reilly Islands",
         "AddressLine2":"Wiegandchester",
         "Country":"Cape Verde"
      },
      {
         "StreetNumber":null,
         "AddressLine1":"56664 Maxine Trafficway",
         "AddressLine2":"New Susannaton",
         "Country":"Armenia"
      },
      {
         "StreetNumber":null,
         "AddressLine1":"721 Satterfield Stream",
         "AddressLine2":"Okunevafort",
         "Country":"Germany"
      }
   ]
}
*/