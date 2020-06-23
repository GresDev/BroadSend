using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BroadSend.Server.Controllers
{
    [SuppressMessage("ReSharper", "Mvc.ViewNotResolved")]
    public class ComplexItemController<T1, T2, T3, T4> : Controller
        where T1 : class, IComplexItem, new()
        where T2 : class, IComplexItemAlias, new()
        where T3 : class, IComplexItemCreateViewModel<T1, T2>
        where T4 : class, IComplexItemAliasCreateViewModel, new()

    {
        private readonly IComplexItemRepository<T1, T2> _repository;

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly LogMessages _logMessages = new LogMessages();

        public ComplexItemController(
            IComplexItemRepository<T1, T2> repository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
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
        public async Task<IActionResult> Create(T3 itemCreateViewModel)
        {
            ViewBag.ErrorMessage = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.CreateItemAsync(itemCreateViewModel.ComplexItem,
                        itemCreateViewModel.ComplexItemAlias);

                    Log.Information($"User {_userManager.GetUserName(User)} " +
                                    $"added new entry: {itemCreateViewModel.ComplexItem.Name}|{itemCreateViewModel.ComplexItemAlias.Alias}");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            return View(itemCreateViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(T1 item)
        {
            ViewBag.ErrorMessage = string.Empty;

            var itemOriginal = await _repository.GetItemAsync(item.Id);

            if (itemOriginal == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            if (itemOriginal.Name == item.Name)
            {
                ModelState.ClearValidationState("Name");
                ModelState.MarkFieldValid("Name");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateItemAsync(item);
                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {itemOriginal.Name}" +
                        $" -> {item.Name}");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            return View(item);
        }


        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            return View(item);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(T1 item)
        {
            try
            {
                await _repository.DeleteItemAsync(item.Id);
                Log.Information($"User {_userManager.GetUserName(User)} deleted entry: {item.Name}");
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

            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new List<T2>());
            }

            IEnumerable<T2> aliasList = await _repository.GetItemAliasesAsync(item.Id);
            if (aliasList == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new List<T2>());
            }

            ViewBag.OnlyOneAlias = aliasList.Count() == 1;

            ViewBag.ParentName = item.Name;
            ViewBag.ParentId = id;
            return View(aliasList);
        }

        public async Task<IActionResult> AliasEdit(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            var parentAlias = await _repository.GetItemAliasAsync(id);
            if (parentAlias == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new T2());
            }

            var item = await _repository.GetItemAsync(parentAlias.ParentId);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new T2());
            }

            ViewBag.ParentName = item.Name;
            return View(parentAlias);
        }

        [HttpPost]
        public async Task<IActionResult> AliasEdit(T2 itemAlias)
        {
            ViewBag.ErrorMessage = string.Empty;

            var item = await _repository.GetItemAsync(itemAlias.ParentId);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new T2());
            }

            ViewBag.PresenterName = item.Name;

            var parentAliasOriginal = await _repository.GetItemAliasAsync(itemAlias.Id);

            if (parentAliasOriginal == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new T2());
            }

            if (parentAliasOriginal.Alias == itemAlias.Alias)
            {
                ModelState.ClearValidationState("Alias");
                ModelState.MarkFieldValid("Alias");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateItemAliasAsync(itemAlias);

                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {parentAliasOriginal.Alias}" +
                        $" -> {itemAlias.Alias}");

                    return RedirectToAction("Aliases", new { id = itemAlias.ParentId });
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(itemAlias);
                }
            }

            return View(itemAlias);
        }

        public async Task<IActionResult> AliasCreate(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            var item = await _repository.GetItemAsync(id);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new T4());
            }

            var itemAliasCreateViewModel = new T4
            {
                ParentId = id
            };

            ViewBag.ParentName = item.Name;
            return View(itemAliasCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AliasCreate(T4 itemAliasCreateViewModel)
        {
            ViewBag.ErrorMessage = string.Empty;

            var item = await _repository.GetItemAsync(itemAliasCreateViewModel.ParentId);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new T4());
            }

            ViewBag.ParentName = item.Name;

            if (ModelState.IsValid)
            {
                var itemAlias = new T2
                {
                    Alias = itemAliasCreateViewModel.Alias,
                    ParentId = itemAliasCreateViewModel.ParentId
                };

                try
                {
                    await _repository.CreateItemAliasAsync(itemAlias);
                    Log.Information(
                        $"User {_userManager.GetUserName(User)} added new entry: {itemAlias.Alias}");
                    return RedirectToAction("Aliases", new { id = itemAliasCreateViewModel.ParentId });
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(itemAliasCreateViewModel);
                }
            }

            return View(itemAliasCreateViewModel);
        }


        public async Task<IActionResult> AliasDelete(int id)
        {
            ViewBag.ErrorMessage = string.Empty;

            var itemAlias = await _repository.GetItemAliasAsync(id);
            if (itemAlias == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            var item = await _repository.GetItemAsync(itemAlias.ParentId);
            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View(new T2());
            }

            var itemAliases = await _repository.GetItemAliasesAsync(item.Id);
            if (itemAliases?.Count() == 1)
            {
                return RedirectToAction("Aliases", new { id = item.Id });
            }

            ViewBag.ParentName = item.Name;
            return View(itemAlias);
        }


        [HttpPost]
        public async Task<IActionResult> AliasDelete(T2 itemAlias)
        {
            try
            {
                await _repository.DeleteItemAliasAsync(itemAlias.Id);
                Log.Information($"User {_userManager.GetUserName(User)} deleted entry: {itemAlias.Alias}");
            }
            catch (ArgumentNullException e)
            {
                Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                return View(new T2());
            }

            return RedirectToAction("Aliases", new { id = itemAlias.ParentId });
        }
    }
}