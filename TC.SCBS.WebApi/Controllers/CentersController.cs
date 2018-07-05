using System.Web.Http;
using TC.SCBS.BL.Services;

namespace TC.SCBS.WebApi.Controllers
{
    public class CentersController : ApiController
    {

        private readonly ICenterService _centerService;

        public CentersController(ICenterService centerService)
        {
            _centerService = centerService;
        }



        // GET api/centers

        [HttpGet]
        public IHttpActionResult GetAllCenters()
        {
            var centers = _centerService.GetCenters();
            return (centers == null || centers.Count==0 ) ?  NotFound(): (IHttpActionResult)Ok(centers);
        }

        // GET api/centers/5

        [HttpGet]
        public IHttpActionResult GetCenter( int id)
        {
            var center = _centerService.GetCenter(id);
            return center == null ? (IHttpActionResult) NotFound() : Ok(center);
        }
    }
}
