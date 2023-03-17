using Aplication.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Aplication.Configurations
{
    public class MapperProfiles
    {
        public static MapperConfiguration InitProfiles()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompetidoresViewModel, Competidores>();
            });

            return config;
        }
    }
}
