﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPILessonOne.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "資料驗證失敗", Error = actionContext.ModelState });
            }
            base.OnActionExecuting(actionContext);
        }
    }
}