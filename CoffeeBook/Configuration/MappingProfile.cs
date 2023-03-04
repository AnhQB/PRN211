using AutoMapper;
using CoffeeBook.DTOs;
using CoffeeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBook.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountDTO, Account>();
        }
    }
}
