﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_EXAMPLE.Models
{
    public class Product
    {

        [Key]
        public int ProductID { get; set; }

        [Required]
        [DisplayName("Product name")] 
        
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }


        [Required]
        public int Qty { get; set; }

        public string Remarks { get; set; }



    }
}