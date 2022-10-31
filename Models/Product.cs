using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string ProductCode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string Price { get; set; }

        public int QuantityTypeId { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Quantity { get; set; }

        public QuantityType QuantitiesRel { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public Category CategoryRel { get; set; }

        public SubCategory subCategoryRel { get; set; }

        [NotMapped]
        public string QuantityTypeName { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        [NotMapped]
        public string SubCategoryName { get; set; }


        [Column(TypeName = "varbinary(MAX)")]
        public byte[] photo { get; set; }

        [NotMapped]
        public IFormFile PhotoFile { get; set; }
    }


    public class QuantityType
    {
        [Key]
        public int QuantityTypeId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string QuantityTypeName { get; set; }

    }

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CategoryName { get; set; }

        //public ICollection<SubCategory> SubCategory { get; set; }
    }

    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string SubCategoryName { get; set; }

        public int CategoryId { get; set; }

        public Category CategoryRel { get; set; }
    }




    public class ProductStock
    {
        [Key]
        public int ProductStockId { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Quntity { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public Product products { get; set; }

        public int Multiply { get; set; }

    }


    public class ProductSearch
    {
        public int CategorySearch { get; set; }
        public int SubCategorySearch { get; set; }
        public string ProductCodeSearch { get; set; }
    }

    public class productStockList
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Quantity { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal ActualAmount { get; set; }
        public decimal Price { get; set; }
    }


    public class ProductQuantity
    {
        public int ProductId { get; set; }
        public decimal quantity { get; set; }
    }

    public class ProductExistansy
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
    }


}
