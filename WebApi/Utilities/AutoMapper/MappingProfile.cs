﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookUpdateDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, BookUpdateDto>();
        }
    }
}
