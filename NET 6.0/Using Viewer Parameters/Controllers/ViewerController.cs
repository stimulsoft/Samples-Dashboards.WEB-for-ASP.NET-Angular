﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stimulsoft.Report;
using Stimulsoft.Report.Angular;
using System;
using System.Text;

namespace Using_Viewer_Parameters.Controllers
{
    [Controller]
    public class ViewerController : Controller
    {
        static ViewerController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        [HttpPost]
        public IActionResult InitViewer()
        {
            var requestParams = StiAngularViewer.GetRequestParams(this);

            var options = new StiAngularViewerOptions();
            options.Actions.GetReport = "GetReport";
            options.Actions.ViewerEvent = "ViewerEvent";
            options.Appearance.ScrollbarsMode = true;

            return StiAngularViewer.ViewerDataResult(requestParams, options);
        }

        public IActionResult GetReport()
        {
            var reportName = "HotelRevenue.mrt";
            var httpContext = new Stimulsoft.System.Web.HttpContext(this.HttpContext);
            var properties = httpContext.Request.Params["properties"]?.ToString();
            if (properties != null)
            {
                var data = Convert.FromBase64String(properties);
                var json = Encoding.UTF8.GetString(data);
                JContainer container = JsonConvert.DeserializeObject<JContainer>(json);
                foreach (JToken token in container.Children())
                {
                    if (((JProperty)token).Name == "reportName")
                    {
                        reportName = ((JProperty)token).Value.Value<string>();
                    }
                }
            }

            var report = StiReport.CreateNewReport();
            var path = StiAngularHelper.MapPath(this, $"Reports/{reportName}");
            report.Load(path);

            return StiAngularViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiAngularViewer.ViewerEventResult(this);
        }
    }
}


