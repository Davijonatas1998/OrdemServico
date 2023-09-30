using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using OrdemServico.Models.ServicoOrdem;
using OrdemServico.Repository.Interface;
using OrdemServico.Repository.Interfaces;
using OrdemServico.Services;
using ReflectionIT.Mvc.Paging;
using System.Data;

namespace OrdemServico.Areas.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly IProfileUser _profileUser;

        public ManagerController(IManagerService managerService, IProfileUser profileUser)
        {
            _managerService = managerService;
            _profileUser = profileUser;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Titulo")
        {
            var result = _managerService.QuerybleServico();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(p => p.Titulo.Contains(filter));
            }

            var model = await PagingList.CreateAsync(result, 4, pageindex, sort, "Titulo");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var UserProfile = await _profileUser.ListProfilesAsync();
            ViewData["Id"] = new SelectList(UserProfile, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,Titulo,Descricao,DataAbertura,DataConclusao,Tipo,Status,Id,Preco,Desconto")] ClassOrdemServico ordemServico)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
            }

            var newOrdemServico = new ClassOrdemServico()
            {
                Id = ordemServico.Id,
                Descricao = ordemServico.Descricao,
                Titulo = ordemServico.Titulo,
                Tipo = ordemServico.Tipo,
                Status = ordemServico.Status,
                DataAbertura = ordemServico.DataAbertura,
                DataConclusao = ordemServico.DataConclusao,
                Preco = ordemServico.Preco,
                Desconto = ordemServico.Desconto,
                NumeroSerie = ServicesManager.External(9),
            };

            await _managerService.CreateServico(newOrdemServico);
            var UserProfile = await _profileUser.ListProfilesAsync();
            ViewData["Id"] = new SelectList(UserProfile, "Id", "FullName", ordemServico.Id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string IdResult)
        {
            int? id = ServicesManager.IdAction(IdResult);
            var servico = await _managerService.GetServico(id);

            if (servico == null)
            {
                return NotFound();
            }

            var UserProfile = await _profileUser.ListProfilesAsync();
            ViewData["Id"] = new SelectList(UserProfile, "Id", "FullName", servico.Id);

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdServico,Titulo,Descricao,DataAbertura,DataConclusao,Tipo,Status,Id,Preco,Desconto,NumeroSerie,Pagamento")] ClassOrdemServico ordemServico)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
            }

            await _managerService.UpdateServico(ordemServico);
            var UserProfile = await _profileUser.ListProfilesAsync();
            ViewData["Id"] = new SelectList(UserProfile, "Id", "FullName", ordemServico.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string IdResult)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
            }

            int? id = ServicesManager.IdAction(IdResult);
            await _managerService.DeleteServico(id);
            return RedirectToAction(nameof(Index));
        }
    }
}