using System.Diagnostics;
using Firma.PortalWWW.Models;
using Firma.PortalWWW.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly CmsApiClient _cmsClient;

        public HomeController(CmsApiClient cmsClient)
        {
            _cmsClient = cmsClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = await _cmsClient.GetContent("Home", "Title") ?? "Witamy w Stajni Blue Knight\r\n";
            ViewBag.Subtitle = await _cmsClient.GetContent("Home", "Subtitle") ?? "Profesjonalne lekcje jeŸdziectwa dla ka¿dego";
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewBag.Title = await _cmsClient.GetContent("About", "Title") ?? "O Naszej Stajni";
            ViewBag.Subtitle = await _cmsClient.GetContent("About", "Subtitle") ?? "Od ponad 20 lat tworzymy miejsce, gdzie pasja do jeŸdziectwa ³¹czy siê z profesjonalizmem";

            ViewBag.Experience = await _cmsClient.GetContent("About", "Experience") ?? "20+";
            ViewBag.Horses = await _cmsClient.GetContent("About", "Horses") ?? "30";
            ViewBag.Students = await _cmsClient.GetContent("About", "Students") ?? "1000+";
            ViewBag.Instructors = await _cmsClient.GetContent("About", "Instructors") ?? "15";

            ViewBag.Card1Title = await _cmsClient.GetContent("About", "Card1Title") ?? "Nowoczesny Obiekt";
            ViewBag.Card1Text = await _cmsClient.GetContent("About", "Card1Text") ?? "Nasza stajnia to nowoczesny kompleks jeŸdziecki wyposa¿ony w kryt¹ uje¿d¿alniê, parkur oraz profesjonalne zaplecze socjalne. Dbamy o najwy¿sze standardy bezpieczeñstwa i komfortu.";

            ViewBag.Card2Title = await _cmsClient.GetContent("About", "Card2Title") ?? "Doœwiadczona Kadra";
            ViewBag.Card2Text = await _cmsClient.GetContent("About", "Card2Text") ?? "Nasi instruktorzy to certyfikowani specjaliœci z wieloletnim doœwiadczeniem. Ka¿dy z nich regularnie uczestniczy w szkoleniach i rozwija swoje umiejêtnoœci.";

            ViewBag.Card3Title = await _cmsClient.GetContent("About", "Card3Title") ?? "Indywidualne Podejœcie";
            ViewBag.Card3Text = await _cmsClient.GetContent("About", "Card3Text") ?? "Dla ka¿dego ucznia tworzymy spersonalizowany plan rozwoju, uwzglêdniaj¹cy jego umiejêtnoœci, cele i tempo nauki. Zapewniamy tak¿e regularne konsultacje.";

            return View();
        }

    }
}
