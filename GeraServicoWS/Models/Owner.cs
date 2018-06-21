using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeraServicoWS.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public bool Remenber { get; set; }
    }
}