using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IEstateService
    {
        List<Estate> Get();
        Estate Get(string id);
        Estate Create(Estate estate);
        void Update(string id, Estate estate);
        void Remove(string id);
    }
}
