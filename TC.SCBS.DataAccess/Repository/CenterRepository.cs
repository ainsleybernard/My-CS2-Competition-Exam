using System.Collections.Generic;
using System.Linq;
using TC.SCBS.DataAccess.Interfaces;
using TC.SCBS.Entities;
using Center = TC.SCBS.DTO.Center;

namespace TC.SCBS.DataAccess.Repository
{
    public class CenterRepository : ICenterRepository
    {

        private readonly TCSCBSContext _dbContext;
        public CenterRepository(TCSCBSContext tcscbsContext)
        {
            _dbContext = tcscbsContext;
        }


        public List<Center> GetCenters()
        {

            var query = from center in _dbContext.Centers
                        join centerType in _dbContext.CenterTypes on center.Center_Type_Id equals centerType.Center_Type_Id into gj
                        from subCenter in gj.DefaultIfEmpty()
                        select new Center
                        {
                            Id = center.Center_Id,
                            Name = center.Name,
                            StreetAddress = center.Street_Address,
                            CenterTypeId = subCenter.Center_Type_Id,
                            CenterTypeValue = subCenter.Value
                        };

            return query.ToList();

        }

        public Center GetCenter(int id)
        {
            var query = from center in _dbContext.Centers.Where(x=> x.Center_Id==id)
                        join centerType in _dbContext.CenterTypes on center.Center_Type_Id equals centerType.Center_Type_Id into gj
                        from subCenter in gj.DefaultIfEmpty()
                        select new Center
                        {
                            Id = center.Center_Id,
                            Name = center.Name,
                            StreetAddress = center.Street_Address,
                            CenterTypeValue = subCenter.Value,
                            CenterTypeId = subCenter.Center_Type_Id,
                            MaxAccommodationQuantityPerDay = center.Accommodation_Quantity

                        };

            return query.FirstOrDefault();

        }

        
    }
}
