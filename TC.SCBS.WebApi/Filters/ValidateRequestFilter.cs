using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TC.SCBS.WebApi.Filters
{
    public class ValidateRequestFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    actionContext.ModelState);
            }

        }
    }
}