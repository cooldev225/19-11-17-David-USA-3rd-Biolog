using Biolog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Biolog.WebReference;
using Newtonsoft.Json.Linq;
using Biolog.services;

namespace Biolog.Controllers
{
    public class HomeController : Controller
    {
        private const string URL = "http://52.191.118.216:80/reports/";
        private DashboardService _dbService;

        public ActionResult Index()
        {
            if (CheckUserSession())
            {              
                _dbService = new DashboardService();
                Dashboard board = new Dashboard();
                board = _dbService.KpiDetails(board , (Models.User)Session["user"]);

                //Get PowerBiReports
                //board = _dbService.PowerBIDetails(board, (Models.User)Session["user"]);
                //Get Reports
                board = _dbService.SSRSReportDetails(board, (Models.User)Session["user"]);

                ReportingService2010 rs = new ReportingService2010();
                Models.User user = (Models.User)Session["user"];
                string url = WebConfigurationManager.AppSettings["ReportingServerURL"];
                rs.Credentials = new System.Net.NetworkCredential(user.Username, user.Password, url);
                var items = rs.ListChildren("/", true);
                board.PowerBiModelList = new List<PowerBiModel>();
                int pcnt = 0;
                for (var i = 0; i < items.Length; i++) {
                    if (items[i].TypeName.Equals("PowerBIReport"))
                    {//Report   Kpi
                        PowerBiModel pbi = new PowerBiModel();
                        pbi.Name = items[i].Name;
                        board.PowerBiModelList.Add(pbi);
                        pcnt++;
                    }
                }
                board.PowerBiCount = pcnt;
                return View(board);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public ActionResult Platelet()
        {
            if (CheckUserSession())
            {
                //ReportViewer reportViewer = new ReportViewer();
                //reportViewer.ProcessingMode = ProcessingMode.Remote;
                //reportViewer.ServerReport.ReportServerCredentials = new BiologRSrCredentials();
                //reportViewer.ServerReport.ReportPath = "/Hoxworth/Hoxworth Platelet Study";
                //reportViewer.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ReportingServerURL"]);
                ////reportViewer.SizeToReportContent = true;
                ////reportViewer.Width = Unit.Percentage(100);
                ////reportViewer.Height = Unit.Percentage(100);
                //ViewBag.ReportViewer = reportViewer;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult PlateletProfile()
        {
            if (CheckUserSession())
            {
                //ReportViewer reportViewer = new ReportViewer();
                //reportViewer.ProcessingMode = ProcessingMode.Remote;
                //reportViewer.ServerReport.ReportServerCredentials = new BiologRSrCredentials();
                //reportViewer.ServerReport.ReportPath = "/Hoxworth/Hoxworth Platelet Study";
                //reportViewer.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ReportingServerURL"]);
                ////reportViewer.SizeToReportContent = true;
                ////reportViewer.Width = Unit.Percentage(100);
                ////reportViewer.Height = Unit.Percentage(100);
                //ViewBag.ReportViewer = reportViewer;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Bloodunit()
        {
            if (CheckUserSession())
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public ActionResult RFID()
        {
            if (CheckUserSession())
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult DataSummary()
        {
            if (CheckUserSession())
            {
                //ReportViewer reportViewer = new ReportViewer();
                //reportViewer.ProcessingMode = ProcessingMode.Remote;
                //reportViewer.ServerReport.ReportServerCredentials = new BiologRSrCredentials();
                //reportViewer.ServerReport.ReportPath = "/Hoxworth/Blood Unit Tracking";
                //reportViewer.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ReportingServerURL"]);
                ////reportViewer.SizeToReportContent = true;
                ////reportViewer.Width = Unit.Percentage(100);
                ////reportViewer.Height = Unit.Percentage(100);
                //ViewBag.ReportViewer = reportViewer;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult ExpiringUnit()
        {
            if (CheckUserSession())
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        private bool CheckUserSession()
        {
            if (Session["user"] != null)
            {

                var user = (Models.User)Session["user"];
                if (user.IsLoggedIn)
                {
                    Session["user"] = user;
                    ViewBag.username = user.Username;
                    return true;
                }

            }
            return false;
        }
    }
}