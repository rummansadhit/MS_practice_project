
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using V1.Entity;

namespace V1.Repositories
{

  public class ItemsRepository{

       private const string collectionName = "items";
       private readonly IMongoCollection<Item> dbcollection;
       private readonly FilterDefinitionBuilder<Item> filterBuilder =  Builders<Item>.Filter;

       public ItemsRepository(){

              var mongoClient = new MongoClient("mongodb://localhost:27017");
              var dataBase = mongoClient.GetDatabase("Catalog");
              dbcollection = dataBase.GetCollection<Item>(collectionName);
       }

      
       public async Task<IReadOnlyCollection<Item>> GetAllAsync(){

         return await dbcollection.Find(filterBuilder.Empty).ToListAsync();

       }

       public async Task<Item> GetAsync(Guid id){

          FilterDefinition<Item> filter=filterBuilder.Eq(entity => entity.Id, id);

          return await dbcollection.Find(filter).FirstOrDefaultAsync();


       }


       public async Task CreateAsync(Item entity){

          if(entity==null){

              throw new ArgumentNullException(nameof(entity));
          }

          await dbcollection.InsertOneAsync(entity);

       }

        public async Task UpdateAsync(Item entity){

            if(entity==null){

                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Item> filter = filterBuilder.Eq(existingEntity=> existingEntity.Id, entity.Id);

            await dbcollection.ReplaceOneAsync(filter, entity);

        }


        public async Task RemoveAsync(Guid id){

            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);

            await dbcollection.DeleteOneAsync(filter);

        }

             

  }

}


