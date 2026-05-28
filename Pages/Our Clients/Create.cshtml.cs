using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DemoCrudWebApplication_DotNet_Core.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clients = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clients.name = Request.Form["name"];
            clients.email = Request.Form["email"];
            clients.phone = Request.Form["phone"];
            clients.address = Request.Form["address"];

            if (clients.name.Length == 0 || clients.email.Length == 0 ||
                clients.phone.Length == 0 || clients.address.Length == 0)
            {
                errorMessage = "All the fields are required !";
                return;
            }

            try
            {
                String connectionString = "Data Source=LAPTOP-VRHBS533\\SQLEXPRESS;Initial Catalog=crud_oprCore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String sqlQuery = "Insert INTO clients (name, email, phone, address) values (@name, @email, @phone, @address)";

                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        command.Parameters.AddWithValue("@name", clients.name);
                        command.Parameters.AddWithValue("@email", clients.email);
                        command.Parameters.AddWithValue("@phone", clients.phone);
                        command.Parameters.AddWithValue("@address", clients.address);

                        command.ExecuteNonQuery();

                        successMessage = "New Client Added Successfully !";
                        Response.Redirect("/Clients/Index");
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }
}
