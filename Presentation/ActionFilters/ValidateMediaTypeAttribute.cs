using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class ValidateMediaTypeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var acceptHeaderPresent = context.HttpContext
                .Request.Headers
                .ContainsKey("Accept");
            if (!acceptHeaderPresent) 
            {
                context.Result = new BadRequestObjectResult($"Accept header is missing.");
                return;
            }
            
            var mediaType = context.HttpContext
                .Request.Headers["Accept"].FirstOrDefault();

            if(!MediaTypeHeaderValue
                .TryParse(mediaType, out MediaTypeHeaderValue? outMediaType))
            {
                context.Result = new BadRequestObjectResult($"Supported MediaType not present.");
                return;
            }

            context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
        }
    }
}
