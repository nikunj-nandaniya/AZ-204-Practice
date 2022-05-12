using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace ConnectAzureSQL.Controllers
{
    public class StagingController : Controller
    {
        private readonly IFeatureManager _featureManager;

        public StagingController(IFeatureManagerSnapshot featureManager)
        {
            _featureManager = featureManager;
        }
        [FeatureGate(FeatureFlag.staging)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
