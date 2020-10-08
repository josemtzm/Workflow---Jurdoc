using AutoMapper;
using Jurdoc.Api.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Api
{
    //public static class AutoMapperConfig
    //{
    //    //public static void Configure()
    //    //{
    //    //    Mapper.Initialize(cfg =>
    //    //    {
    //    //        cfg.AddProfile(new UserProfile());
    //    //    });
    //    //}
    //}

    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //this.CreateMap<IDataRecord, Escritura>();
            CreateMap<OracleDataReader, Escritura>()
                .ForMember(x => x.IDESCRITURA, opt => opt.MapFrom(s => s["IDESCRITURA"]))
                .ForMember(x => x.NUMEROESCRITURA, opt => opt.MapFrom(s => s["NUMEROESCRITURA"]))
                .ForMember(x => x.SOLICITANTE, opt => opt.MapFrom(s => s["SOLICITANTE"]));


            CreateMap<Escritura, OracleDataReader>();

            //CreateMap<OracleDataReader, List<Escritura>>();

            

        }

    }
}
