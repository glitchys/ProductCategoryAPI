using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductCategoryApi.Services {
    public class CrudService<T, TDto> where T : class {
        private readonly List<T> _items = new ();
        private int _nextId = 1;
        //create entity  
        public TDto Create(T item, Action<T, int> setId, Func<T, TDto> mapToDto){
            //here needed a maptodto because the "_nextid++ didnt map to the dto"
            setId (item, _nextId++);
            _items.Add(item);
            return mapToDto(item);
        }
        //get all entites
        public List<TDto> GetAll (Func<T,TDto> mapToDto) {
            return _items.Select(mapToDto).ToList();
        } 
        //get entity by id
        public TDto GetById(int id, Func<T, int> getId, Func<T, TDto> mapToDto){
            //ai helped me in this
            var item = _items.FirstOrDefault(i => getId(i) == id);
            //if else
            return item != null ? mapToDto(item) : default;

        }
        //update entity by id 
        public TDto Update(int id, T updatedItem, Func<T, int> getId, Func<T, TDto> mapToDto)
        {
            // Find the entity with the matching ID
            var item = _items.FirstOrDefault(i => getId(i) == id);

            // If found, update its properties
            if (item != null)
            {
                // Get all properties of the entity using reflection
                var properties = typeof(T).GetProperties();

                // Update each property except "Id"
                foreach (var property in properties)
                {
                    if (property.Name != "Id") // Skip the Id property
                    {
                        property.SetValue(item, property.GetValue(updatedItem));
                    }
                }
            }

            // Map the updated entity to its DTO and return
            return item != null ? mapToDto(item) : default;
        }
        //delete by id 
        public bool Delete (int id, Func<T,int> getID) {
            var item = _items.FirstOrDefault(i => getID(i) == id);
            if (item != null){
                _items.Remove(item);
                return true;
            }
            return false;
        }



    }
}