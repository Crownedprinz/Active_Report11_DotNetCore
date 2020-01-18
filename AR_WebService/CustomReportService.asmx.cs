using GrapeCity.ActiveReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Text;
using GrapeCity.ActiveReports.Data;

namespace AR_WebService
{
    /// <summary>
    /// Summary description for CustomReportService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomReportService : GrapeCity.ActiveReports.Web.ReportService
    {
        [WebMethod]
        protected override object OnCreateReportHandler(string reportPath)
        {
            SectionReport rptOrders;

            switch (reportPath)
            {
                case "Reports/BillingInvoice.rdlx":
                case "Reports/Orders.rdlx":
                    PageReport rptPageRDL = new PageReport();
                    rptPageRDL.Load(new FileInfo(Server.MapPath(reportPath)));
                    rptPageRDL.Report.DataSources[0].ConnectionProperties.ConnectString = "data source=" + Server.MapPath("~/App_Data/NWind.mdb") + ";provider=Microsoft.Jet.OLEDB.4.0;";
                    return rptPageRDL;

                case "Reports/Invoice.rpx":

                    rptOrders = new SectionReport();
                    XmlTextReader xtr = new XmlTextReader(Server.MapPath(reportPath));
                    rptOrders.LoadLayout(xtr);
                    (rptOrders.DataSource as OleDBDataSource).ConnectionString = "data source=" + Server.MapPath("~/App_Data/NWind.mdb") + ";provider=Microsoft.Jet.OLEDB.4.0;";
                    return rptOrders;
                default:
                    return base.OnCreateReportHandler(reportPath);
            }
        }
    }
}
