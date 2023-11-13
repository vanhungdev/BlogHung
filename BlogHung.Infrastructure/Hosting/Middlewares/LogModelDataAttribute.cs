﻿using BlogHung.Infrastructure.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Infrastructure.Hosting.Middlewares
{
    public class LogModelDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                var model = viewResult.Model;
                LoggingHelper.SetProperty("viewResultModel", JsonConvert.SerializeObject(model));
            }

            base.OnActionExecuted(context);
        }
    }
}
