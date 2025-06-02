using Application.Dtos.Client.Response;
using Application.Dtos.ClientDtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    internal class ClientMappers : Profile
    {

        public ClientMappers()
        {
            CreateMap<Client, ClientSummaryResponse>();
            CreateMap<Client, ClientResponse>();
            CreateMap<Client, AuthenticatedClientResponse>();
        }
    }
}
