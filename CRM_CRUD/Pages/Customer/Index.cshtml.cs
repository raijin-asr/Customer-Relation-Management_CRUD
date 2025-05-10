using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRM_CRUD.Pages.Customer
{
    public class IndexModel : PageModel
    {
        public List<CustomerInfo> CustomersList { get; set; } = new List<CustomerInfo>();
        
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=RAIJIN\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Customers";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CustomerInfo customer = new CustomerInfo
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone = reader.GetString(4),
                            Address = reader.GetString(5),
                            Company = reader.GetString(6),
                            CreatedAt = reader.GetDateTime(7).ToString("yyyy-MM-dd")
                        };
                        CustomersList.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }

    public class CustomerInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
      
        public string Company { get; set; } = string.Empty;
        public string CreatedAt { get; set; }= string.Empty;

    }
}
