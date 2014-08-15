﻿#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Kooboo.CMS.Content.Services;
using Kooboo.CMS.Content.Models;


namespace Kooboo.CMS.Web2.Areas.Contents.Controllers
{
    public class ManagerControllerBase : ControllerBase
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Repository == null)
            {
                filterContext.Result = RedirectToAction("Index", "Repository");
            }
            base.OnActionExecuting(filterContext);
        }
        public virtual Repository Repository
        {
            get
            {
                return Repository.Current;
            }
            set
            {
                Repository.Current = value;
            }
        }
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
            {
                ((RedirectToRouteResult)filterContext.Result).RouteValues["repositoryName"] = filterContext.RequestContext.GetRequestValue("repositoryName");
            }

            base.OnResultExecuting(filterContext);
        }

        protected virtual ActionResult RedirectToIndex()
        {
            var routes = this.ControllerContext.RequestContext.AllRouteValues();
            routes.Remove("name");
            return RedirectToAction("Index", routes);
        }
    }
}