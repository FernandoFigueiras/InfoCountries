using CoreMVCStudy.Web.Data.Entities;

namespace CoreMVCStudy.Web.Data
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {

        public CountryRepository(DataContext context) : base(context)
        {
        }
    }
}
