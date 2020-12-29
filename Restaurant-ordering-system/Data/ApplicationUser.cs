﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Data
{
    public class ApplicationUser: IdentityUser 
    {
        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string PostCode { get; set; }
    }
}
