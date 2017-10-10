using Clients.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClientList.Models
{
    public class ClientViewModel
    {
        public Client Client { get; set; }

        public List<SelectListItem> Statuses { get; set; }
    }
}