﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Database.Entity
{
    public class IndexItem
    {
        [Key]
        public int Id { get; set; }
  
        public string Name { get; set; }

        public double Price { get; set; }
  
        public string Description { get; set; }

        public List<Items> Items { get; set; }
    }
}
