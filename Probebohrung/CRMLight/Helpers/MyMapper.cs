using AutoMapper;

namespace CRMLight
{
    public class MyMapper<T1, T2>
    {
        public IMapper mapperFromDB { get; set; }
        public IMapper mapperToDB { get; set; }

        public MyMapper()
        {
            var configFromDB = new MapperConfiguration(cfg => {
                cfg.CreateMap<T1, T2>();
            });
            var configToDB = new MapperConfiguration(cfg => {
                cfg.CreateMap<T2, T1>();
            });
            mapperFromDB = new Mapper(configFromDB);
            mapperToDB = new Mapper(configToDB);
        }
    }
}
