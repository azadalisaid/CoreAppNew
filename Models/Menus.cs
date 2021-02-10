using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class Menus
    {
        [Key]
        public int MenuId { get; set; }
        
        [Column(TypeName ="varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Path { get; set; }
    }

    public class Routes
    {
        public string Path {get;set; }

        public string Component { get; set; }
    }
}
