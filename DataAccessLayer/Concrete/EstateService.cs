using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class EstateService : IEstateService
    {
        private readonly IMongoCollection<Estate> _estate;

        public EstateService(IEstateStoreDatabaseSetting settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _estate = database.GetCollection<Estate>(settings.EstateCollectionName);
        }
        public Estate Create(Estate estate)
        {
            _estate.InsertOne(estate);
            return estate;
        }

        public List<Estate> Get()
        {
            return _estate.Find(estate => true).ToList();
        }

        public Estate Get(string id)
        {
            return _estate.Find(estate => estate.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _estate.DeleteOne(estate => estate.Id == id);
        }

        public void Update(string id, Estate estate)
        {
            _estate.ReplaceOne(estate => estate.Id == id, estate);
        }
    }
}
