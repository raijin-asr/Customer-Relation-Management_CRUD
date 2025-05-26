using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;

namespace CRM_CRUD.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public void OnGet()
        {
        }

        public void OnPost(int id) 
        {
            deleteCustomer(id);
            Response.Redirect("/Customer/Index");
        }

        private void deleteCustomer(int id)
        {
            try
            {
                string connectionString = "Data Source=RAIJIN\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM customers WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
        }
    }
}
