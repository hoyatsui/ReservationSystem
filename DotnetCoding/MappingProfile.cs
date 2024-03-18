using AutoMapper;
using DotnetCoding.Core.Models;
using DotnetCoding.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Client, ClientDto>().ForMember(dest =>dest.ReservationIds, opt=>opt.MapFrom(src=>src.Reservations.Select(r=>r.Id)));
            CreateMap<Provider, ProviderDto>().ForMember(dest => dest.AppointmentSlotIds, opt=>opt.MapFrom(src => src.AppointmentSlots.Select(a=>a.Id)));
            CreateMap<AppointmentSlot, AppointmentSlotDto>()
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Provider.Name))
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Reservations != null ? src.Reservations.Id : (int?)null))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Reservations != null ? src.Reservations.Client.Name : string.Empty));

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.AppointmentStartTime, opt => opt.MapFrom(src => src.AppointmentSlot.StartTime))
                .ForMember(dest => dest.AppointmentEndTime, opt => opt.MapFrom(src => src.AppointmentSlot.EndTime))
                .ForMember(dest => dest.AppointmentId, opt=> opt.MapFrom(src => src.AppointmentSlot.Id))
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.AppointmentSlot.Provider.Name))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name));
        }
    }
}
