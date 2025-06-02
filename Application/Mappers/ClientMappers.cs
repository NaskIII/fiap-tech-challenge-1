using Application.Dtos.Client.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    internal class ClientMappers : Profile
    {

        public ClientMappers()
        {
            CreateMap<Client, ClientSummaryResponse>();
        }
    }
}
