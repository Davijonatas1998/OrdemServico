using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrdemServico.Models;
using OrdemServico.Models.ServicoOrdem;
using OrdemServico.Repository.Interface;
using OrdemServico.Repository.Interfaces;
using OrdemServico.Services;
using ReflectionIT.Mvc.Paging;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace OrdemServico.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IPayment _payment;
        private readonly IProfileUser _profileUser;

        public HomeController(IManagerService managerService, IProfileUser profileUser, IPayment payment)
        {
            _managerService = managerService;
            _profileUser = profileUser;
            _payment = payment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var profile = await _profileUser.GetProfileAsync(User);
            var roles = await _profileUser.GetRole(profile);

            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Manager", new {area = "admin"});
            }
            else
            {
                return RedirectToAction(nameof(OrdemServicoManager));
            }
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> OrdemServicoManager()
        {
            var result = await _managerService.ListServico();
            return View(result);
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Details(string Id)
        {
            int? id = ServicesManager.IdAction(Id);
            var Service = await _managerService.GetServico(id);

            if (!Service.Pagamento)
            {
                await GetPayment(Service);
            }

            return View(Service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pagar(string email,int id, string nomeOS, float valor)
        {
            var profile = await _profileUser.GetProfileAsync(User);
            var result = await _payment.GetPayment(profile.Id, id.ToString());

            MercadoPagoConfig.AccessToken = ServicesManager.Key;
            var Value = (int)valor /100;
            var client = new PreferenceClient();
            var item = new PreferenceItemRequest
            {
                Title = nomeOS,
                Quantity = 1,
                CurrencyId = "BRL",
                UnitPrice = Value,
            };

            var preferenceRequest = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest> { item },
                Payer = new PreferencePayerRequest
                {
                    Email = email,
                },
                ExternalReference = id.ToString(),
            };

            Preference preference = await client.CreateAsync(preferenceRequest);
            if(result == null)
            {
               await _payment.CreatePayment(preference.ExternalReference, profile.Id);
            }

            return Redirect(preference.InitPoint);
        }

        public async Task GetPayment(ClassOrdemServico classOrdems)
        {
            string accessToken = ServicesManager.Key;
            string url = $"https://api.mercadopago.com/v1/payments/search?external_reference={classOrdems.NumeroSerie}&access_token={accessToken}";

            if (!classOrdems.Pagamento)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject paymentJson = JObject.Parse(jsonResponse);

                        if (paymentJson["results"] is JArray results && results.Count > 0)
                        {
                            string status = (string)paymentJson["results"][0]["status"];
                            if (status == "approved")
                            {
                               classOrdems.Pagamento = true;
                               await _managerService.UpdateServico(classOrdems);
                            }
                        }
                    }
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}