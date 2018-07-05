using System.Collections.Generic;
using TC.SCBS.DataAccess.Interfaces;
using TC.SCBS.DTO;

namespace TC.SCBS.BL.Services
{
    public class CenterService : ICenterService
    {
        private readonly ICenterRepository _centerRepository;

        public CenterService(ICenterRepository centerRepository)
        {
            _centerRepository = centerRepository;
        }

        public List<Center> GetCenters()
        {

            return _centerRepository.GetCenters();
        }

        public Center GetCenter(int id)
        {
            return _centerRepository.GetCenter(id);
        }
    }
}
