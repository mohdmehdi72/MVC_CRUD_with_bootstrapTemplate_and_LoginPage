using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CRUDAppWithLogin.Models
{
    public class LogInModel
    {
        public int id { get; set; }
        
        public string uname { get; set; }
        
        
        public string email { get; set; }
        
        public string password { get; set; }
    }
}