using AutoMapper;
using MOS.Application.DTOs.Responses.Users;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.ProductNames,
                    opt => opt.MapFrom(src =>
                        src.UserProductPermissions != null
                            ? src.UserProductPermissions
                                .Where(p => p.Product != null)
                                .Select(p => p.Product!.Name)
                                .ToList()
                            : new List<string>()))
                .ForMember(dest => dest.TemporaryPassword,
                    opt => opt.Ignore());
        }
    }
}
