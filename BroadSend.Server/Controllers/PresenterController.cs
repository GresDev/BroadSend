using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils;
using BroadSend.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class PresenterController : Controller
    {
        private readonly IPresenterRepository _repository;

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly LogMessages _logMessages = new LogMessages();

        public PresenterController(
            IPresenterRepository presenterRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager)
        {
            _repository = presenterRepository;
            _sharedLocalizer = sharedLocalizer;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllItemsAsync());
        }

        public IActionResult Create()
        {
            ViewBag.ErrorMessage = string.Empty;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PresenterCreateViewModel presenterCreateViewModel)
        {
            ViewBag.ErrorMessage = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.CreateItemAsync(new Presenter { Name = presenterCreateViewModel.Name },
                        presenterCreateViewModel.Alias);
                    Log.Information($"User {_userManager.GetUserName(User)} " +
                                    $"added new entry: {presenterCreateViewModel.Name}|{presenterCreateViewModel.Alias}");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            return View(presenterCreateViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            Presenter presenter = await _repository.GetItemAsync(id);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            return View(presenter);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Presenter presenter)
        {
            ViewBag.ErrorMessage = string.Empty;


            if (ModelState.IsValid)
            {
                try
                {
                    Presenter presenterOriginal = await _repository.GetItemAsync(presenter.Id);

                    if (presenterOriginal == null)
                    {
                        ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                        return View();
                    }

                    if (!await _repository.ItemNameIsUniqueAsync(presenter.Name) &&
                        presenterOriginal.Name != presenter.Name)
                    {
                        ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                        return View(presenter);
                    }

                    await _repository.UpdateItemAsync(presenter);
                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {presenterOriginal.Name}" +
                        $" -> {presenter.Name}");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            return View(presenter);
        }


        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            Presenter presenter = await _repository.GetItemAsync(id);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            return View(presenter);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Presenter presenter)
        {
            try
            {
                await _repository.DeleteItemAsync(presenter.Id);
                Log.Information($"User {_userManager.GetUserName(User)} deleted entry: {presenter.Name}");
            }
            catch (ArgumentNullException e)
            {
                Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                return View();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Aliases(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            Presenter presenter = await _repository.GetItemAsync(id);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new List<PresenterAlias>());
            }

            IEnumerable<PresenterAlias> aliasList = await _repository.GetItemAliasesAsync(presenter.Id);
            if (aliasList == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new List<PresenterAlias>());
            }

            ViewBag.OnlyOneAlias = aliasList.Count() == 1 ? true : false;
            ViewBag.PresenterName = presenter.Name;
            ViewBag.PresenterId = id;
            return View(aliasList);
        }

        public async Task<IActionResult> AliasEdit(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            PresenterAlias presenterAlias = await _repository.GetItemAliasAsync(id);
            if (presenterAlias == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new PresenterAlias());
            }

            Presenter presenter = await _repository.GetItemAsync(presenterAlias.PresenterId);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new PresenterAlias());
            }

            ViewBag.PresenterName = presenter.Name;
            return View(presenterAlias);
        }

        [HttpPost]
        public async Task<IActionResult> AliasEdit(PresenterAlias presenterAlias)
        {
            ViewBag.ErrorMessage = string.Empty;

            Presenter presenter = await _repository.GetItemAsync(presenterAlias.PresenterId);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new PresenterAlias());
            }

            ViewBag.PresenterName = presenter?.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    PresenterAlias presenterAliasOriginal = await _repository.GetItemAliasAsync(presenterAlias.Id);

                    if (presenterAliasOriginal == null)
                    {
                        ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                        return View(new PresenterAlias());
                    }

                    if (!await _repository.ItemAliasIsUniqueAsync(presenterAlias.Alias) &&
                        presenterAlias.Alias != presenterAliasOriginal.Alias)
                    {
                        ModelState.AddModelError("Alias", _sharedLocalizer["ErrorDuplicateRecord"]);

                        return View(presenterAlias);
                    }

                    await _repository.UpdateItemAliasAsync(presenterAlias);

                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {presenterAliasOriginal.Alias}" +
                        $" -> {presenterAlias.Alias}");

                    return RedirectToAction("Aliases", new { id = presenterAlias.PresenterId });
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(presenterAlias);
                }
            }

            return View(presenterAlias);
        }

        public async Task<IActionResult> AliasCreate(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            Presenter presenter = await _repository.GetItemAsync(id);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new PresenterAliasCreateViewModel());
            }

            PresenterAliasCreateViewModel presenterAliasCreateViewModel = new PresenterAliasCreateViewModel
            {
                PresenterId = id
            };

            ViewBag.PresenterName = presenter.Name;
            return View(presenterAliasCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AliasCreate(PresenterAliasCreateViewModel presenterAliasCreateViewModel)
        {
            ViewBag.ErrorMessage = string.Empty;

            Presenter presenter = await _repository.GetItemAsync(presenterAliasCreateViewModel.PresenterId);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new PresenterAliasCreateViewModel());
            }

            ViewBag.PresenterFullName = presenter.Name;

            if (ModelState.IsValid)
            {
                PresenterAlias presenterAlias = new PresenterAlias
                {
                    Alias = presenterAliasCreateViewModel.Alias,
                    PresenterId = presenterAliasCreateViewModel.PresenterId
                };

                try
                {
                    await _repository.CreateItemAliasAsync(presenterAlias);
                    Log.Information(
                        $"User {_userManager.GetUserName(User)} added new entry: {presenterAlias.Alias}");
                    return RedirectToAction("Aliases", new { id = presenterAliasCreateViewModel.PresenterId });
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(presenterAliasCreateViewModel);
                }
            }

            return View(presenterAliasCreateViewModel);
        }


        public async Task<IActionResult> AliasDelete(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            PresenterAlias presenterAlias = await _repository.GetItemAliasAsync(id);
            if (presenterAlias == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return RedirectToAction("Aliases", new { id = presenterAlias.PresenterId });
            }

            Presenter presenter = await _repository.GetItemAsync(presenterAlias.PresenterId);
            if (presenter == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new PresenterAlias());
            }

            var presenterAliases = await _repository.GetItemAliasesAsync(presenter.Id);
            if (presenterAliases?.Count() == 1)
            {
                return RedirectToAction("Aliases", new { id = presenter.Id });
            }

            ViewBag.PresenterName = presenter.Name;
            return View(presenterAlias);
        }


        [HttpPost]
        public async Task<IActionResult> AliasDelete(PresenterAlias presenterAlias)
        {
            try
            {
                await _repository.DeleteItemAliasAsync(presenterAlias.Id);
                Log.Information($"User {_userManager.GetUserName(User)} deleted entry: {presenterAlias.Alias}");
            }
            catch (ArgumentNullException e)
            {
                Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                return View(new PresenterAlias());
            }

            return RedirectToAction("Aliases", new
            {
                id = presenterAlias.PresenterId
            });
        }
    }
}