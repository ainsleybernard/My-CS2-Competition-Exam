using System.Collections.Generic;
using TC.SCBS.DTO;

namespace TC.SCBS.DataAccess.Interfaces
{
    public interface ICenterRepository
    {
        List<Center> GetCenters();
        Center GetCenter(int id); 
    }
}
