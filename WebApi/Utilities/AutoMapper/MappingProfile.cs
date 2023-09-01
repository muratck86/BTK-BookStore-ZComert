﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, BookUpdateDto>();
            CreateMap<BookCreateDto,BookDto>();
            CreateMap<BookCreateDto, Book>();
            CreateMap<UserForRegistrationDto, User>();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto,Category>();
            CreateMap<CategoryUpdateDto, Category>();


            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
        }
    }
}
