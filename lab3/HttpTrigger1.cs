using Microsoft.AspNetCore.Http;
using Company.People.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;


namespace Company.Function
{
    public class HttpTrigger1
    {
        private readonly ILogger<HttpTrigger1> _logger;

        public HttpTrigger1(ILogger<HttpTrigger1> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")]
        HttpRequest req, [FromBody] Person newPerson)
        {

        var res = new List<Person>();
        using (SqlConnection connection = new SqlConnection("Server=tcp:natann.database.windows.net,1433;Initial Catalog=cdv-db;Persist Security Info=False;User ID=natann;Password=lis123!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
        {
            SqlCommand command = new SqlCommand("select * from Person ", connection);
            command.Connection.Open();

            using (SqlDataReader oReader = command.ExecuteReader())
                {

                    while (oReader.Read())
                    {
                        var matchingPerson = new Person();
                        matchingPerson.Id = int.Parse(oReader["Id"].ToString());
                        matchingPerson.FirstName = oReader["FirstName"].ToString();
                        matchingPerson.LastName = oReader["LastName"].ToString();
                        res.Add(matchingPerson);
                    }

                    connection.Close();
                }
        }

            var people = new List<Person>{
                new Person{FirstName = "Natan", LastName = "Lis", Id = 1}
            };

            switch (req.Method)
            {
                case "POST":
                    people.Add(newPerson);
                    return new JsonResult(people);
                case "GET":
                    return new JsonResult(res);
                default:
                    throw new ArgumentException("Incorrect method.");
            }

        }
    }
}
