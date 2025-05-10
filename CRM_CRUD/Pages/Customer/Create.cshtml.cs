using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CRM_CRUD.Pages.Customer
{
    public class CreateModel : PageModel
    {

        [BindProperty, Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;
        
        [BindProperty, Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;
        
        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [BindProperty, Phone]
        public string? Phone { get; set; } = string.Empty;
        
        [BindProperty]
        public string? Address { get; set; } = string.Empty;
        
        [BindProperty, Required]
        public string? Company { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
        public void OnGet()
        {
          
        }

        //create a new customer
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            //optional data
            if (Phone == null) Phone = "";
            if (Address == null) Address = "";
            if (Company == null) Company = "";

            try
            {
                string connectionString = "Data Source=RAIJIN\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Customers "+ 
                        "(first_Name, last_Name, Email, Phone, Address, Company) "+
                        "VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @Company)";
                    
                    SqlCommand command = new SqlCommand(query, connection);

                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Company",Company);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage= ex.Message;
                return;
            }

            // Redirect to the index page after successful creation
            Response.Redirect("/Customer/Index");


        }
    }
}
