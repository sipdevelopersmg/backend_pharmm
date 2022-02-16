using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utility.OKResponse.Helper;


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = (context.HttpContext.Items["userId"]);
            
            if (userId is null)
            {
                context.Result = new OkObjectResult(ResponseHelper.GetResponse(
                    _data: "",
                    _responseResult: false,
                    _message: "anda belum login!"
                ));
            }
        }
    }
