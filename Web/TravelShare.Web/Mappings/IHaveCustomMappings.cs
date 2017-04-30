using AutoMapper;

namespace TravelShare.Web.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression config);
    }
}
