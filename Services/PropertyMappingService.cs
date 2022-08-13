using System;
using System.Collections.Generic;
using System.Linq;
using MediaManager.Entities;

namespace MediaManager.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _DefaultPropertyMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
               { "Id", new PropertyMappingValue(new List<string>() { "Id" }) },
               { "Name", new PropertyMappingValue(new List<string>() { "Name" }) },
               { "Title", new PropertyMappingValue(new List<string>() { "Title" }) }
           };

        private readonly IList<IPropertyMapping> _PropertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            //TODO
            //_PropertyMappings.Add(new PropertyMapping<AuthorDto, Author>(_AuthorPropertyMapping));
            _PropertyMappings.Add(new PropertyMapping<MediaTypeReadApi, MediaType>(_DefaultPropertyMapping));
            _PropertyMappings.Add(new PropertyMapping<MovieReadApi, Movie>(_DefaultPropertyMapping));
        }
        
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = _PropertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;

        }

    }
}