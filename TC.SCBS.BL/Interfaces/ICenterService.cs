using System.Collections.Generic;
using TC.SCBS.DTO;

namespace TC.SCBS.BL.Services
{
    public interface ICenterService
    {
        List<Center> GetCenters();
        Center GetCenter(int id);

    }
}
