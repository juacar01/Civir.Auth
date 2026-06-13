using AutoMapper;
using Civir.Auth.Application.Features.Login;
using Civir.Auth.Application.Features.Register;
using Civir.Auth.Application.Features.Users;
using Civir.Domain.Entities;

namespace Civir.Auth.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Aquí puedes configurar tus mapeos
        // CreateMap<Source, Destination>();

        CreateMap<User, RegisterVm>().ReverseMap();
        CreateMap<User, UserVm>().ReverseMap();
/*
        CreateMap<Author, AuthorVm>();
        CreateMap<Book, BookVm>();
        CreateMap<Loan, LoanVm>();

        CreateMap<CreateBookCommand, Book>();
        CreateMap<UpdateBookCommand, Book>();
        CreateMap<DeleteBookCommand, Book>();
        CreateMap<CreateAuthorCommand, Author>();
        CreateMap<CreateLoanCommand, Loan>();
*/


    }
}