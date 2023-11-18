using AutoMapper;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Repository;
using skolesystem.Service;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserCreateDto, Users>();
        CreateMap<Users, UserReadDto>();
        CreateMap<UserUpdateDto, Users>();
        CreateMap<Users, UserUpdateDto>();

        
        CreateMap<Absence, AbsenceReadDto>();
        CreateMap<AbsenceCreateDto, Absence>();
        CreateMap<AbsenceUpdateDto, Absence>();
        CreateMap<AbsenceReadDto, Absence>();
        CreateMap<Bruger, BrugerReadDto>();
        CreateMap<Bruger, BrugerUpdateDto>();
        CreateMap<Bruger, BrugerCreateDto>();
        CreateMap<BrugerUpdateDto, Bruger>();




    }
}