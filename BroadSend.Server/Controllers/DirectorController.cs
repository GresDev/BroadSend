using System;
using System.Threading.Tasks;
using BroadSend.Server.Models;
using BroadSend.Server.Utils;
using BroadSend.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _repository;

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly LogMessages _logMessages = new LogMessages();

        public DirectorController(
            IDirectorRepository directorRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager)
        {
            _repository = directorRepository;
            _sharedLocalizer = sharedLocalizer;
            _userManager = userManager;
        }

        public async Task<ViewResult> Index()
        {
            return View(await _repository.GetAllItemsAsync());
        }

        public IActionResult Create()
        {
            ViewBag.ErrorMessage = string.Empty;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Alias, Name")] Director director)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _repository.ItemNameIsUniqueAsync(director.Name) &&
                        await _repository.ItemNameIsUniqueAsync(director.Alias))
                    {
                        await _repository.CreateItemAsync(director);
                        Log.Information(
                            $"User {_userManager.GetUserName(User)} added new entry: {director.Name}|{director.Alias}");
                        return RedirectToAction("Index");
                    }

                    if (!await _repository.ItemNameIsUniqueAsync(director.Name))
                    {
                        ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                    }

                    if (!await _repository.ItemNameIsUniqueAsync(director.Alias))
                    {
                        ModelState.AddModelError("Alias", _sharedLocalizer["ErrorDuplicateRecord"]);
                    }
                }
                catch (DbUpdateException e)
                {
                    Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }
            }

            ViewBag.ErrorMessage = string.Empty;
            return View(director);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Director director = await _repository.GetItemAsync(id);

            if (director == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            DirectorEditViewModel directorEditViewModel = new DirectorEditViewModel
            {
                Id = director.Id,
                Alias = director.Alias,
                Name = director.Name
            };

            ViewBag.ErrorMessage = string.Empty;
            return View(directorEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DirectorEditViewModel directorEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Director directorOriginal = await _repository.GetItemAsync(directorEditViewModel.Id);

                    if (!await _repository.ItemAliasIsUniqueAsync(directorEditViewModel.Alias) &&
                        directorEditViewModel.Alias != directorOriginal.Alias)
                    {
                        ModelState.AddModelError("Alias", _sharedLocalizer["ErrorDuplicateRecord"]);
                    }

                    if (!await _repository.ItemNameIsUniqueAsync(directorEditViewModel.Name) &&
                        directorEditViewModel.Name != directorOriginal.Name)
                    {
                        ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(directorEditViewModel);
                    }

                    Director director = new Director
                    {
                        Id = directorEditViewModel.Id,
                        Alias = directorEditViewModel.Alias,
                        Name = directorEditViewModel.Name
                    };

                    await _repository.UpdateItemAsync(director);

                    Log.Information(
                        $"User {_userManager.GetUserName(User)} edited entry: {directorOriginal.Alias}:{directorOriginal.Name}" +
                        $" -> {directorEditViewModel.Alias}:{directorEditViewModel.Name}");

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
            return View(directorEditViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var director = await _repository.GetItemAsync(id);

            if (director == null)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorNotFound"];
                return View();
            }

            ViewBag.ErrorMessage = string.Empty;
            return View(director);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Director director)
        {
            try
            {
                await _repository.DeleteItemAsync(director.Id);
                Log.Information($"User {_userManager.GetUserName(User)} deleted entry: {director.Name}");
            }
            catch (ArgumentNullException e)
            {
                Log.Error(_logMessages.ErrorMessage(e, _userManager.GetUserName(User)));
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult CheckForItemNameIsUnique(string name)
        {
            var itemNameIsUnique = _repository.ItemNameIsUniqueAsync(name).Result;
            return itemNameIsUnique == true
                ? Json(true)
                : Json(_sharedLocalizer["ErrorDuplicateRecord"].ToString());
        }

        public IActionResult CheckForItemAliasIsUnique(string alias)
        {
            var itemAliasIsUnique = _repository.ItemAliasIsUniqueAsync(alias).Result;
            return itemAliasIsUnique == true
                ? Json(true)
                : Json(_sharedLocalizer["ErrorDuplicateRecord"].ToString());
        }
    }
}