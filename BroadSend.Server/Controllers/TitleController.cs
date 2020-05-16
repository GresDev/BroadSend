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
    public class TitleController : Controller
    {
        private readonly ITitleRepository _repository;

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly LogMessages _logMessages = new LogMessages();

        public TitleController(
            ITitleRepository titleRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager)
        {
            _repository = titleRepository;
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
        public async Task<IActionResult> Create(TitleCreateViewModel titleCreateViewModel)
        {
            ViewBag.ErrorMessage = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.CreateItemAsync(new Title { Name = titleCreateViewModel.Name, Anons = titleCreateViewModel.Anons },
                        titleCreateViewModel.Alias);
                    Log.Information($"User {_userManager.GetUserName(User)} " +
                                    $"added new entry: {titleCreateViewModel.Name}|{titleCreateViewModel.Alias}");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            return View(titleCreateViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            Title title = await _repository.GetItemAsync(id);

            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            return View(title);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Title title)
        {
            ViewBag.ErrorMessage = string.Empty;


            if (ModelState.IsValid)
            {
                try
                {
                    Title titleOriginal = await _repository.GetItemAsync(title.Id);

                    if (titleOriginal == null)
                    {
                        ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                        return View();
                    }

                    if (!await _repository.ItemNameIsUniqueAsync(title.Name) &&
                        titleOriginal.Name != title.Name)
                    {
                        ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                        return View(title);
                    }

                    await _repository.UpdateItemAsync(title);
                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {titleOriginal.Name}" +
                        $" -> {title.Name}");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            return View(title);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            Title title = await _repository.GetItemAsync(id);

            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            return View(title);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Title title)
        {
            try
            {
                await _repository.DeleteItemAsync(title.Id);
                Log.Information($"User {_userManager.GetUserName(User)} deleted entry: {title.Name}");
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

            Title title = await _repository.GetItemAsync(id);
            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new List<TitleAlias>());
            }

            IEnumerable<TitleAlias> aliasList = await _repository.GetItemAliasesAsync(title.Id);
            if (aliasList == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new List<TitleAlias>());
            }

            ViewBag.OnlyOneAlias = aliasList.Count() == 1;

            ViewBag.TitleName = title.Name;
            ViewBag.TitleId = id;

            return View(aliasList);
        }

        public async Task<IActionResult> AliasEdit(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            TitleAlias titleAlias = await _repository.GetItemAliasAsync(id);
            if (titleAlias == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new TitleAlias());
            }

            Title title = await _repository.GetItemAsync(titleAlias.TitleId);
            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new TitleAlias());
            }

            ViewBag.TitleName = title.Name;
            return View(titleAlias);
        }

        [HttpPost]
        public async Task<IActionResult> AliasEdit(TitleAlias titleAlias)
        {
            ViewBag.ErrorMessage = string.Empty;

            Title title = await _repository.GetItemAsync(titleAlias.TitleId);
            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new TitleAlias());
            }

            ViewBag.TitleName = title.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    TitleAlias titleAliasOriginal = await _repository.GetItemAliasAsync(titleAlias.Id);

                    if (titleAliasOriginal == null)
                    {
                        ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                        return View(new TitleAlias());
                    }

                    if (!await _repository.ItemAliasIsUniqueAsync(titleAlias.Alias) &&
                        titleAlias.Alias != titleAliasOriginal.Alias)
                    {
                        ModelState.AddModelError("Alias", _sharedLocalizer["ErrorDuplicateRecord"]);

                        return View(titleAlias);
                    }

                    await _repository.UpdateItemAliasAsync(titleAlias);

                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {titleAliasOriginal.Alias}" +
                        $" -> {titleAlias.Alias}");

                    return RedirectToAction("Aliases", new { id = titleAlias.TitleId });
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(titleAlias);
                }
            }

            return View(titleAlias);
        }

        public async Task<IActionResult> AliasCreate(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            Title title = await _repository.GetItemAsync(id);
            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new TitleAliasCreateViewModel());
            }

            TitleAliasCreateViewModel titleAliasCreateViewModel = new TitleAliasCreateViewModel
            {
                TitleId = id
            };

            ViewBag.TitleName = title.Name;
            return View(titleAliasCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AliasCreate(TitleAliasCreateViewModel titleAliasCreateViewModel)
        {
            ViewBag.ErrorMessage = string.Empty;

            Title title = await _repository.GetItemAsync(titleAliasCreateViewModel.TitleId);
            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new TitleAliasCreateViewModel());
            }

            ViewBag.TitleFullName = title.Name;

            if (ModelState.IsValid)
            {
                TitleAlias titleAlias = new TitleAlias
                {
                    Alias = titleAliasCreateViewModel.Alias,
                    TitleId = titleAliasCreateViewModel.TitleId
                };

                try
                {
                    await _repository.CreateItemAliasAsync(titleAlias);
                    Log.Information(
                        $"User {_userManager.GetUserName(User)} added new entry: {titleAlias.Alias}");
                    return RedirectToAction("Aliases", new { id = titleAliasCreateViewModel.TitleId });
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(titleAliasCreateViewModel);
                }
            }

            return View(titleAliasCreateViewModel);
        }

        public async Task<IActionResult> AliasDelete(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            TitleAlias titleAlias = await _repository.GetItemAliasAsync(id);
            if (titleAlias == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new TitleAlias());
            }

            Title title = await _repository.GetItemAsync(titleAlias.TitleId);
            if (title == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new TitleAlias());
            }

            var titleAliases = await _repository.GetItemAliasesAsync(title.Id);
            if (titleAliases?.Count() == 1)
            {
                return RedirectToAction("Aliases", new { id = title.Id });
            }

            ViewBag.TitleName = title.Name;
            return View(titleAlias);
        }

        [HttpPost]
        public async Task<IActionResult> AliasDelete(TitleAlias titleAlias)
        {
            try
            {
                await _repository.DeleteItemAliasAsync(titleAlias.Id);
                Log.Information($"User {_userManager.GetUserName(User)} deleted entry: {titleAlias.Alias}");
            }
            catch (ArgumentNullException e)
            {
                Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                return View(new TitleAlias());
            }

            return RedirectToAction("Aliases", new { id = titleAlias.TitleId });
        }
    }
}