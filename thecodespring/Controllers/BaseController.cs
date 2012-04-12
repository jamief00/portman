using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace thecodespring.Controllers
{
    public abstract class BaseController : Controller
    {
        public IDocumentSession DocumentSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction) return;
            this.DocumentSession = DataDocumentStore.DocStore.OpenSession(); 
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext) 
        { 
            if (filterContext.IsChildAction) return;

            if (this.DocumentSession != null && filterContext.Exception == null)
            {
                this.DocumentSession.SaveChanges();
            }
            
            this.DocumentSession.Dispose(); 
            base.OnActionExecuted(filterContext); }
    }

}