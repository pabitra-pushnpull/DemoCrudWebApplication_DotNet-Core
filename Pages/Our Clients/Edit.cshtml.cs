using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DemoCrudWebApplication_DotNet_Core.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clients = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
             if (id.Length == 0)
             {
                 errorMessage = "Client ID is required !";
                 return;
             }
             try
             {
                 String connectionString = "Data Source=LAPTOP-VRHBS533\\SQLEXPRESS;Initial Catalog=crud_oprCore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                 using (SqlConnection conn = new SqlConnection(connectionString))
                 {
                     conn.Open();
                     String sqlQuery = "SELECT * FROM clients WHERE id=@id";
                     using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                     {
                         command.Parameters.AddWithValue("@id", id);
                         using (SqlDataReader reader = command.ExecuteReader())
                         {
                             if (reader.Read())
                             {
                                 clients.Id = "" + reader.GetInt32(0);
                                 clients.name = reader.GetString(1);
                                 clients.email = reader.GetString(2);
                                 clients.phone = reader.GetString(3);
                                 clients.address = reader.GetString(4);
                             }
                             else
                             {
                                 errorMessage = "Client not found !";
                                 return;
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 errorMessage = ex.Message;
                 return;
            }
        }


        public void OnPost()
        {
            clients.Id = Request.Form["id"];
            clients.name = Request.Form["name"];
            clients.email = Request.Form["email"];
            clients.phone = Request.Form["phone"];
            clients.address = Request.Form["address"];

            if (clients.Id.Length == 0 || clients.name.Length == 0 || clients.email.Length == 0 ||
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
                    String sqlQuery = "UPDATE clients SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        command.Parameters.AddWithValue("@name", clients.name);
                        command.Parameters.AddWithValue("@email", clients.email);
                        command.Parameters.AddWithValue("@phone", clients.phone);
                        command.Parameters.AddWithValue("@address", clients.address);
                        command.Parameters.AddWithValue("@id", clients.Id);
                        command.ExecuteNonQuery();
                        successMessage = "Client Updated Successfully !";
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
