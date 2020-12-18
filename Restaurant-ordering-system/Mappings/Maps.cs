using AutoMapper;
using Restaurant_ordering_system.Data;
using Restaurant_ordering_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Mappings
{
    public class Maps: Profile

    {
        public Maps()
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<MenuItem, MenuItemVM>().ReverseMap();
            CreateMap<MenuItem, AdminViewVM>().ReverseMap();
            CreateMap<MenuItem, EditMenuItemVM>().ReverseMap();

        }
    }
}
