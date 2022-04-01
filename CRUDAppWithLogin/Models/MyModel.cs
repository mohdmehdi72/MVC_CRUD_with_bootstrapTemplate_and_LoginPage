using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CRUDAppWithLogin.Models
{
    
    public class MyModel
    {
        public int id { get; set; }
        [Required (ErrorMessage ="Please enter the name")]
        public string ename { get; set; }
        [Required(ErrorMessage = "Please enter the Age")]
        public Nullable<int> age { get; set; }
        [Required(ErrorMessage = "Please enter the salary")]
        public Nullable<int> salay { get; set; }
        [Required(ErrorMessage = "Please enter the city")]
        public string city { get; set; }
    }
}