using BandsApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace BandsApi.Helpers
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            string orderByString = "";

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (mappingDictionary == null)
                throw new ArgumentNullException(nameof(mappingDictionary));

            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            string[] orderBySplit = orderBy.Split(",");

            foreach (string orderByClause in orderBySplit)
            {
                string trimmedOrderBy = orderByClause.Trim();
                bool orderDesc = trimmedOrderBy.EndsWith(" desc");
                int indexOfSpace = trimmedOrderBy.IndexOf(" ");
                string propertyName = indexOfSpace == -1 ? trimmedOrderBy : trimmedOrderBy.Remove(indexOfSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                    throw new ArgumentException($"Mapping doesn't exist for {propertyName}");

                PropertyMappingValue propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                    throw new ArgumentNullException(nameof(propertyMappingValue));


                foreach (string destination in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                        orderDesc = !orderDesc;

                    orderByString = orderByString + (!string.IsNullOrWhiteSpace(orderByString) ? "," : "") + destination + (orderDesc ? " descending" : " ascending");
                }
            }

            return source.OrderBy(orderByString);
        }
    }
}
