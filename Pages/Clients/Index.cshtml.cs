using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DemoCrudWebApplication_DotNet_Core.Pages.Clients
{
    public class IndexModel : PageModel
    {
            public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {

            try
            {
                string connectionString = "Data Source=LAPTOP-VRHBS533\\SQLEXPRESS;Initial Catalog=crud_oprCore;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM clients";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clients = new ClientInfo();

                                clients.Id = "" + reader.GetInt32(0);
                                clients.name = reader.GetString(1);
                                clients.email = reader.GetString(2);
                                clients.phone = reader.GetString(3);
                                clients.address = reader.GetString(4);
                                clients.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clients);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Found : " + ex.ToString());

            }
        }
    }

    public class ClientInfo
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string created_at { get; set; }
    }
}
