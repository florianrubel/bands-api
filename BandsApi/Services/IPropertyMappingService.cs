using System.Collections.Generic;

namespace BandsApi.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        public bool ValidMappingExists<TSource, TDestination>(string fields);
    }
}