using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Clients.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "The Client Name is required")]
        [StringLength(5, ErrorMessage = "The Client Name is too long")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "The Client Address is required")]
        [StringLength(150, ErrorMessage = "The Client Address is too long")]
        public string ClientAddress { get; set; }

        [Required(ErrorMessage = "The Client Telephone is required")]
        [StringLength(12, ErrorMessage = "The Client Telephone is too long")]
        public string ClientTelephone { get; set; }

        [StringLength(50, ErrorMessage = "The Client Fax is too long")]
        public string ClientFax { get; set; }

        [Required(ErrorMessage = "The Client Email is required")]
        [StringLength(50, ErrorMessage = "The Client Email is too long")]
        public string ClientEmail { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }
    }
}