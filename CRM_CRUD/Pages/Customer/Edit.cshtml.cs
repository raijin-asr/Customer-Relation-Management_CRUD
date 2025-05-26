using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CRM_CRUD.Pages.Customer
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        
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
        
        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Data Source=RAIJIN\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM customers WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                FirstName = reader.GetString(1);
                                LastName = reader.GetString(2);
                                Email = reader.GetString(3);
                                Phone = reader.GetString(4);
                                Address = reader.GetString(5);
                                Company = reader.GetString(6);
                            }
                            else
                            {
                                Response.Redirect("/Customer/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
        }

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
                    string query = "UPDATE Customers " +
                        "SET first_Name=@FirstName, last_Name=@LastName, email= @Email,  " +
                        "phone=@Phone, address=@Address, company=@Company WHERE id=@id;";

                    SqlCommand command = new SqlCommand(query, connection);

                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Company", Company);
                    command.Parameters.AddWithValue("@id", Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Customer/Index");



        }


    }
}
