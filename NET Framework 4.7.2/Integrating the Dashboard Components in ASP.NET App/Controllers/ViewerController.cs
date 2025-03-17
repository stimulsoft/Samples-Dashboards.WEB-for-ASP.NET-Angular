﻿using Stimulsoft.Report;
using Stimulsoft.Report.Angular;
using Stimulsoft.Report.Mvc;
using System.Web.Mvc;

namespace Integrating_the_Dashboard_Components_in_ASP_NET_App.Controllers
{
    public class ViewerController : Controller
    {
        static ViewerController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
                base.OnActionExecuting(filterContext);
            }
        }

        [AllowCrossSiteJson]
        public ActionResult InitViewer()
        {
            var requestParams = StiMvcViewer.GetRequestParams();

            var options = new StiAngularViewerOptions();
            options.Actions.GetReport = "GetReport";
            options.Actions.ViewerEvent = "ViewerEvent";
            options.Appearance.ScrollbarsMode = true;
            options.Toolbar.ShowDesignButton = true;

            return StiAngularViewer.ViewerDataResult(requestParams, options);
        }

        [AllowCrossSiteJson]
        public ActionResult GetReport()
        {
            var report = StiReport.CreateNewReport();
            var path = Server.MapPath($"~/Reports/HotelRevenue.mrt");
            report.Load(path);

            return StiAngularViewer.GetReportResult(report);
        }

        [AllowCrossSiteJson]
        public ActionResult ViewerEvent()
        {
            return StiAngularViewer.ViewerEventResult();
        }
    }
}