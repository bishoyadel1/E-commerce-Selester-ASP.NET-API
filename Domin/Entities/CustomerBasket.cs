﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Entities
{
    public  class CustomerBasket
    {

        public CustomerBasket(string Id)
        {
            Id = Id;
        }
        public string Id { get; set; }
        public int? DeliveryMethod { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
