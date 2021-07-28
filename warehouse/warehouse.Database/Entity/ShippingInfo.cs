﻿using System;
using System.ComponentModel.DataAnnotations;

namespace warehouse.Database.Entity
{
    public class ShippingInfo
    {
        [Key]
        public int Id { get; set; }

        public Client Client { get; set; }
        public Order Order { get; set; }

        public string TrackingNumber { get; set; }
        public string TargetLocation { get; set; }

        public bool IsInsurance { get; set; }

        public User WhoCreated { get; set; }
        public bool IsPriority { get; set; }

        public DateTime DateTime { get; set; }

        public double ShippingPrice { get; set; }
    }
}