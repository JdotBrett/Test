using ClientList.Models;
using Clients.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ClientList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Setting up the location of the server for access to the db
            using (SqlConnection connection = new SqlConnection("Server=localhost;DataBase=Clients;Integrated Security=SSPI"))
            {

                connection.Open();

                //Defining the command created within the db
                SqlCommand command = new SqlCommand("GetClients", connection);
                //Defiining the type of command
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sqlReader = command.ExecuteReader())
                {
                    //Creating a list of the all clients to be displayed
                    ViewBag.Clients = new List<Client>();

                    while (sqlReader.Read())
                    {
                        //Creating a new client object instance based on the model of client
                        var client = new Client();

                        //Setting the ID and Name for the name to be displayed and the ID to be used as a reference 
                        client.ClientId = (int)sqlReader["ClientId"];

                        client.ClientName = sqlReader["ClientName"].ToString();

                        //Adding all the clients names and IDs to the list which will be shown on the view
                        ViewBag.Clients.Add(client);
                    }
                }
            }
                return View();
        }

        //Keeping the default contact page accesible for any additional testing or perhaps to populate with real data
        public ActionResult Contact()
        {
            return View();
        }

        //As we do not need data regarding clients all we will need is some forms which can then call a save method to add the new data to the db
        public ActionResult AddClient()
        {
            return View();
        }

        public ActionResult ClientDetails(int clientId)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost;DataBase=Clients;Integrated Security=SSPI"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetClient", connection);
                command.CommandType = CommandType.StoredProcedure;

                //Getting the client ID so the only client data shown is the one which has been clicked
                SqlParameter clientIdParam = new SqlParameter("@ClientId", SqlDbType.Int);
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);

                using (SqlDataReader sqlReader = command.ExecuteReader())
                {
                    //While the reader is grabbing all data from the db it populates the client model with data which has been read from the db
                    while (sqlReader.Read())
                    {
                        var client = new Client();
                        client.Status = new Status();

                        client.ClientId = (int)sqlReader["ClientId"];
                        client.ClientName = sqlReader["ClientName"].ToString();
                        client.ClientAddress = sqlReader["ClientAddress"].ToString();
                        client.ClientTelephone = sqlReader["ClientTelephone"].ToString();
                        client.ClientEmail = sqlReader["ClientEmail"].ToString();
                        client.StatusId = (int)sqlReader["StatusId"];
                        client.Status.StatusState = sqlReader["StatusState"].ToString();

                        ViewBag.ClientDetails = client;
                    }
                }
            }
            return View();
        }

        public ActionResult EditClientDetails(int clientId)
        {
            var clientViewModel = new ClientViewModel();

            using (SqlConnection connection = new SqlConnection("Server=localhost;DataBase=Clients;Integrated Security=SSPI"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetClient", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter clientIdParam = new SqlParameter("@ClientId", SqlDbType.Int);
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);

                using (SqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        var client = new Client();
                        client.Status = new Status();

                        client.ClientId = (int)sqlReader["ClientId"];
                        client.ClientName = sqlReader["ClientName"].ToString();
                        client.ClientAddress = sqlReader["ClientAddress"].ToString();
                        client.ClientTelephone = sqlReader["ClientTelephone"].ToString();
                        client.ClientEmail = sqlReader["ClientEmail"].ToString();
                        client.StatusId = (int)sqlReader["StatusId"];
                        client.Status.StatusState = sqlReader["StatusState"].ToString();

                        List<SelectListItem> statuses = new List<SelectListItem>();
                        statuses.Add(new SelectListItem() { Text = "Active", Value = "1" });
                        statuses.Add(new SelectListItem() { Text = "Inactive", Value = "2" });
                        statuses.Add(new SelectListItem() { Text = "Archived", Value = "3" });

                        clientViewModel.Client = client;
                        clientViewModel.Statuses = statuses;
                    }
                }
            }
            return View(clientViewModel);
        }

        public ActionResult Save(Client client)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost;DataBase=Clients;Integrated Security=SSPI"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UpdateClientDetails", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter clientIdParameter = new SqlParameter("@ClientId", SqlDbType.Int);
                clientIdParameter.Value = client.ClientId;
                command.Parameters.Add(clientIdParameter);

                SqlParameter clientNameParameter = new SqlParameter("@ClientName", SqlDbType.VarChar);
                clientNameParameter.Value = client.ClientName;
                command.Parameters.Add(clientNameParameter);

                SqlParameter clientAddressParameter = new SqlParameter("@ClientAddress", SqlDbType.VarChar);
                clientAddressParameter.Value = client.ClientAddress;
                command.Parameters.Add(clientAddressParameter);

                SqlParameter clientTelephoneParameter = new SqlParameter("@ClientTelephone", SqlDbType.VarChar);
                clientTelephoneParameter.Value = client.ClientTelephone;
                command.Parameters.Add(clientTelephoneParameter);

                SqlParameter clientFaxParameter = new SqlParameter("@ClientFax", SqlDbType.VarChar);
                clientFaxParameter.Value = client.ClientFax;
                command.Parameters.Add(clientFaxParameter);

                SqlParameter clientEmailParameter = new SqlParameter("@ClientEmail", SqlDbType.VarChar);
                clientEmailParameter.Value = client.ClientEmail;
                command.Parameters.Add(clientEmailParameter);

                SqlParameter clientStatusParameter = new SqlParameter("@StatusId", SqlDbType.Int);
                clientStatusParameter.Value = client.StatusId;
                command.Parameters.Add(clientStatusParameter);

                command.ExecuteNonQuery();
            }
            return View();
        }

        public ActionResult SaveNewClient(Client client)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost;DataBase=Clients;Integrated Security=SSPI"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddNewClient", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter clientNameParameter = new SqlParameter("@ClientName", SqlDbType.VarChar);
                clientNameParameter.Value = client.ClientName;
                command.Parameters.Add(clientNameParameter);

                SqlParameter clientAddressParameter = new SqlParameter("@ClientAddress", SqlDbType.VarChar);
                clientAddressParameter.Value = client.ClientAddress;
                command.Parameters.Add(clientAddressParameter);

                SqlParameter clientTelephoneParameter = new SqlParameter("@ClientTelephone", SqlDbType.VarChar);
                clientTelephoneParameter.Value = client.ClientTelephone;
                command.Parameters.Add(clientTelephoneParameter);

                SqlParameter clientFaxParameter = new SqlParameter("@ClientFax", SqlDbType.VarChar);
                clientFaxParameter.Value = client.ClientFax;
                command.Parameters.Add(clientFaxParameter);

                SqlParameter clientEmailParameter = new SqlParameter("@ClientEmail", SqlDbType.VarChar);
                clientEmailParameter.Value = client.ClientEmail;
                command.Parameters.Add(clientEmailParameter);

                SqlParameter clientStatusParameter = new SqlParameter("@StatusId", SqlDbType.Int);
                clientStatusParameter.Value = client.StatusId;
                command.Parameters.Add(clientStatusParameter);

                ViewBag.NewClientName = client.ClientName;

                command.ExecuteNonQuery();
            }
            return View();
        }

        public ActionResult DeleteClient(int clientId)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost;DataBase=Clients;Integrated Security=SSPI"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteClient", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter clientIdParam = new SqlParameter("@ClientId", SqlDbType.Int);
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);

                command.ExecuteNonQuery();
            }

            return View();
        }
    }
}