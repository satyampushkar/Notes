using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.API.ViewModels;

namespace Notes.API.Controllers
{
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorViewModel Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; 
            var code = 500;

            Response.StatusCode = code; 

            return new ErrorViewModel { StatusCode = code, Message = exception.Message};
        }
    }
}
