using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTestEntities;
using OnlineTestRepository;

namespace OnlineTestBL
{
    public class LocationManager
    {
        public void CreateLocation(Location location)
        {
            using (var repository = new CommonRepository<Location>())
            {
                repository.Create(location);
            }
        }

        public IEnumerable<Location> GetAllLocation()
        {
            using (var repository = new CommonRepository<Location>())
            {
                return repository.GetAll().ToList();
            }
        }
    }
}
