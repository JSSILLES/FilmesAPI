using AutoMapper;
using FilmesAPI.Data.DTOs;

namespace FilmesAPI.Models.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDTO, Filme>();
            CreateMap<UpdateFilmeDTO, Filme>();
            CreateMap<Filme, UpdateFilmeDTO>();
            CreateMap<Filme, CreateFilmeDTO>();
        }

    }
}
