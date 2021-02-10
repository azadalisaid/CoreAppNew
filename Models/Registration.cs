using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class Registration
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string name { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string address { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string phoneNo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string email { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        public byte[] photo { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string accountNo { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string city { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string street { get; set; }

        [NotMapped]
        public IFormFile PhotoFile { get; set; }

        public Country countries { get; set; }

        public State states { get; set; }
    }

   



    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CountryName { get; set; }

    }

    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string StateName { get; set; }
        public int CountryId { get; set; }

        public ICollection<Country> Countries { get; set; }
    }
}
