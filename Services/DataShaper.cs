﻿using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            //Get properties of T that are public and can be instantiated ("new"able)
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requredProperties = GetRequiredProperties(fieldsString);
            return FetchData(entities, requredProperties);
        }

        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchDataForEntity(entity, requiredProperties);
        }
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFields = new List<PropertyInfo>();
            if(!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',',
                    StringSplitOptions.RemoveEmptyEntries);

                foreach( var field in fields)
                {
                    var property = Properties
                        .FirstOrDefault(pi =>
                        pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                    if (property != null)
                        requiredFields.Add(property);
                }

            }
            else
            {
                requiredFields = Properties.ToList();
            }

            return requiredFields;
        }
    
        private ExpandoObject FetchDataForEntity(
            T entity,
            IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObj = new ExpandoObject();

            foreach( var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObj.TryAdd(property.Name, objectPropertyValue);
            }

            return shapedObj;
        }
    
        private IEnumerable<ExpandoObject> FetchData(
            IEnumerable<T> entities,
            IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();
            foreach( var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}
