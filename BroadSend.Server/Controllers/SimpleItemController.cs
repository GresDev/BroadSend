using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;
using System;
using System.Threading.Tasks;

namespace BroadSend.Server.Controllers
{
    public class SimpleItemController<T> : Controller where T : class, ISimpleItem
    {
        private readonly ISimpleItemRepository<T> _repository;

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly LogMessages _logMessages = new LogMessages();

        public SimpleItemController(
            ISimpleItemRepository<T> repository,
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
        public async Task<IActionResult> Create([Bind("Name")] T item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _repository.ItemNameIsUniqueAsync(item.Name))
                    {
                        await _repository.CreateItemAsync(item);
                        Log.Information($"User {_userManager.GetUserName(User)} added new entry: {item.Name}");
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            ViewBag.ErrorMessage = string.Empty;
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            T item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            ViewBag.ErrorMessage = string.Empty;
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(T item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    T itemOriginal = await _repository.GetItemAsync(item.Id);
                    if (!await _repository.ItemNameIsUniqueAsync(item.Name) && itemOriginal.Name != item.Name)
                    {
                        ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                        ViewBag.ErrorMessage = string.Empty;
                        return View(item);
                    }

                    await _repository.UpdateItemAsync(item);
                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {itemOriginal.Name} -> {item.Name}");
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            ViewBag.ErrorMessage = string.Empty;
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            T item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            ViewBag.ErrorMessage = string.Empty;
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(T item)
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

    }
}