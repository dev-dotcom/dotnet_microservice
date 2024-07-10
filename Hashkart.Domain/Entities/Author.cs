﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashkart.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string CountryOfOrigin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}