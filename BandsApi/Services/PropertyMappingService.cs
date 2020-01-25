using BandsApi.Entities;
using BandsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BandsApi.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _bandPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() {"Id"}) },
                { "Name", new PropertyMappingValue(new List<string>() {"Name"}) },
                { "MainGenre", new PropertyMappingValue(new List<string>() {"MainGenre"}) },
                { "FoundedYearsAgo", new PropertyMappingValue(new List<string>() {"Founded"}, true) }
            };

        private IList<IPropertyMappingMarker> _propertyMappings = new List<IPropertyMappingMarker>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<BandDto, Band>(_bandPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            IEnumerable<PropertyMapping<TSource, TDestination>> matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception("No mapping was found");
        }

        public bool ValidMappingExists<TSource, TDestination>(string fields)
        {
            Dictionary<string, PropertyMappingValue> propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
                return true;

            string[] fieldsAfterSplit = fields.Split(",");

            foreach (string field in fieldsAfterSplit)
            {
                string trimmedField = field.Trim();
                int indexOfSpace = trimmedField.IndexOf(" ");
                string propertyName = indexOfSpace == -1 ? trimmedField : trimmedField.Remove(indexOfSpace);
                if (!propertyMapping.ContainsKey(propertyName))
                    return false;
            }

            return true;
        }
    }
}
