using Application.Dtos;
using Application.Models;
using AutoMapper;

namespace Application.Mappings
{
    public class NodeProfile : Profile
    {
        public NodeProfile()
        {
            CreateMap<Node, NodeDto>();
            CreateMap<Reading, ReadingDto>();
        }
    }
}