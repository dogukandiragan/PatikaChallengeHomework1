using Microsoft.AspNetCore.Mvc;
using PatikaChallenge.Presentation.Web.Models;
using System.Diagnostics;
using PatikaChallenge.Application.Process.get_app_id;
using PatikaChallenge.Application.Process.create_a_new_user;
using PatikaChallenge.Application.Process.initialize_user;
using PatikaChallenge.Application.Process.acquire_session_token;
using PatikaChallenge.Application.Domain.Entities;
using PatikaChallenge.Application.Process;
using System.Configuration;
using System.Xml;
namespace PatikaChallenge.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        { 
            @ViewBag.AppId = TempData["AppId"];
            @ViewBag.UserId = TempData["UserId"];
            @ViewBag.Date = TempData["Date"];
            @ViewBag.UserToken = TempData["UserToken"];
            @ViewBag.EncryptionKey = TempData["EncryptionKey"];
            @ViewBag.ChallengeId = TempData["ChallengeId"];

            return View();
        }


        [HttpPost]
        public ActionResult CreateUser()
        {
            GetAppId ap = new GetAppId();
            CreateUser cu = new CreateUser();
            GetUserToken tp = new GetUserToken();
            InitializeUser iu = new InitializeUser();
            string app_id = ap.Get().data.appId;
            TempData["AppId"] = app_id;
            UpdateAppConfig("APP_ID", app_id);
            Root udata = new Root();
            udata = cu.Create();
            string uid = udata.data.id;
            TempData["UserId"] = uid;
            UpdateAppConfig("USER_ID", uid);
            TempData["Date"] = udata.data.createDate.ToString();
            Root td = new Root();
            td = tp.Get(uid);
            string ut = td.data.userToken;
            TempData["UserToken"] = ut;
            UpdateAppConfig("USER_TOKEN", ut);
            string ek = td.data.encryptionKey;
            TempData["EncryptionKey"] = ek;
            UpdateAppConfig("ENCRYPTION_KEY", ek);
            var tst = iu.Get(ut);
            string cid = tst.data.challengeId;
            TempData["ChallengeId"] = cid;
            UpdateAppConfig("CHALLENGE_ID", cid);
            return RedirectToAction("Index");
        }

        public void UpdateAppConfig(string key, string value)
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string appConfigPath = Path.Combine(exePath, "app.config");

            XmlDocument doc = new XmlDocument();
            doc.Load(appConfigPath);

            XmlNode appSettingsNode = doc.SelectSingleNode("configuration/appSettings");

            if (appSettingsNode == null)
            {
                throw new InvalidOperationException("appSettings section not found in the app.config file.");
            }

            XmlElement element = (XmlElement)appSettingsNode.SelectSingleNode($"//add[@key='{key}']");

            if (element != null)
            {
                element.SetAttribute("value", value);
            }
            else
            {
                XmlElement newElement = doc.CreateElement("add");
                newElement.SetAttribute("key", key);
                newElement.SetAttribute("value", value);
                appSettingsNode.AppendChild(newElement);
            }

            doc.Save(appConfigPath);
        }

    }
}
