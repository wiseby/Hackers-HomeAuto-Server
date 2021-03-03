using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess
{
    public class DummyDb<T> : IDataProvider<T> where T : EntityBase
    {
        private List<T> entities;

        public DummyDb(List<T> initialData)
        {
            this.entities = initialData;
        }

        public async Task<T> Create(T entity)
        {
            await Task.Run(() => {});
            var nextId = entities.Max((item) => item.Id) + 1;
            entity.Id = nextId;
            entity.CreatedAt = entity.LastModified = DateTime.Now;
            entities.Add(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            await Task.Run(() => {});
            return entities;
        }

        public async Task<T> GetById(int id)
        {
            await Task.Run(() => {});
            return entities.FirstOrDefault((entity) => entity.Id == id);
        }

        public async Task<int> Remove(int id)
        {
            await Task.Run(() => {});
            var matches = entities.FindAll((entity) => entity.Id == id);
            matches.ForEach((entity) => entities.Remove(entity));
            return matches.Count;
        }

        public async Task<int> Update(int id, T entity)
        {
            await Task.Run(() => {});
            var oldEntities = entities.Select((item) => item.Id == id);
            if (oldEntities.Count() < 1) 
            {
                throw new Exception("no entity matched");
            }
            else if (oldEntities.Count() > 1)
            {
                throw new Exception($"to many entites excists with id: {id}");
            }

            var oldEntity = entities.FirstOrDefault((item) => item.Id == id);
            oldEntity = entity;

            return oldEntities.Count();
        }
        
    }
}
