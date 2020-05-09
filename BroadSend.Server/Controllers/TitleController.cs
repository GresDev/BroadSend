using System;
using System.Collections.Generic;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Models;
using BroadSend.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class TitleController : Controller
    {
        private readonly ITitleRepository _repository;

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TitleController(ITitleRepository titleRepository, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _repository = titleRepository;
            _sharedLocalizer = sharedLocalizer;
        }

        public IActionResult Index()
        {
            return View(_repository.AllTitles);
        }

        public IActionResult Create()
        {
            ViewBag.ErrorMessage = string.Empty;
            return View();
        }

        [HttpPost]
        public IActionResult Create(TitleCreateViewModel titleCreateViewModel)
        {
            if ((titleCreateViewModel.TitleAlias != null) &&
                !_repository.TitleAliasIsUnique(titleCreateViewModel.TitleAlias))
            {
                ModelState.AddModelError("titleAlias",_sharedLocalizer["ErrorDuplicateRecord"]);
            }

            if (!_repository.TitleNameIsUnique(titleCreateViewModel.Title.Name))
            {
                ModelState.AddModelError("Title.Name",_sharedLocalizer["ErrorDuplicateRecord"]);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.CreateTitle(titleCreateViewModel.Title,
                        titleCreateViewModel.TitleAlias);
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View();
                }

                return RedirectToAction("Index");
            }

            return View(titleCreateViewModel);
        }

        public IActionResult Edit(int id)
        {
            Title title = _repository.GetTitle(id);
            return View(title);
        }

        [HttpPost]
        public IActionResult Edit(Title title)
        {
            if (ModelState.IsValid)
            {
                Title _title = _repository.GetTitle(title.Id);
                if (!_repository.TitleNameIsUnique(title.Name) &&
                    _title.Name != title.Name)
                {
                    ModelState.AddModelError("Name",_sharedLocalizer["ErrorDuplicateRecord"]);
                    return View(title);
                }

                try
                {
                    _repository.UpdateTitle(title);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(title);
                }
            }

            return View(title);
        }

        public IActionResult Delete(int id)
        {
            Title title = _repository.GetTitle(id);
            return View(title);
        }

        [HttpPost]
        public IActionResult Delete(Title title)
        {
            try
            {
                _repository.DeleteTitle(title.Id);
            }
            catch (ArgumentNullException)
            {
                ;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Aliases(int id)
        {
            Title title = _repository.GetTitle(id);
            ViewBag.TitleName = title.Name;
            ViewBag.PresenterId = id;
            List<TitleAlias> aliasList = _repository.GetAliases(title.Id);
            return View(aliasList);
        }

        public IActionResult AliasEdit(int id)
        {
            TitleAlias titleAlias = _repository.GetAlias(id);
            ViewBag.TitleName = _repository.GetTitle(titleAlias.TitleId).Name;
            return View(titleAlias);
        }

        [HttpPost]
        public IActionResult AliasEdit(TitleAlias titleAlias)
        {
            if (ModelState.IsValid)
            {
                TitleAlias titleAliasPrev = _repository.GetAlias(titleAlias.Id);

                if (!_repository.TitleAliasIsUnique(titleAlias.Alias) && titleAliasPrev.Alias != titleAlias.Alias)
                {
                    ModelState.AddModelError("Alias",_sharedLocalizer["ErrorDuplicateRecord"]);
                    ViewBag.TitleName = _repository.GetTitle(titleAlias.TitleId).Name;
                    return View(titleAlias);
                }


                try
                {
                    _repository.UpdateAlias(titleAlias);
                    return RedirectToAction("Aliases", new {id = titleAlias.TitleId});
                }
                catch (DbUpdateException)
                {
                    ViewBag.TitleName = _repository.GetTitle(titleAlias.TitleId).Name;
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorDbUpdate"];
                    return View(titleAlias);
                }
            }

            ViewBag.TitleName = _repository.GetTitle(titleAlias.TitleId).Name;
            return View(titleAlias);
        }

        public IActionResult AliasCreate(int id)
        {
            TitleAliasCreateViewModel titleAliasCreateViewModel = new TitleAliasCreateViewModel
            {
                TitleId = id
            };
            ViewBag.TitleName = _repository.GetTitle(id).Name;
            return View(titleAliasCreateViewModel);
        }

        [HttpPost]
        public IActionResult AliasCreate(TitleAliasCreateViewModel titleAliasCreateViewModel)
        {
            ViewBag.TitleName = _repository.GetTitle(titleAliasCreateViewModel.TitleId).Name;

            if (!_repository.TitleAliasIsUnique(titleAliasCreateViewModel.titleAlias))
            {
                ModelState.AddModelError("titleAlias",_sharedLocalizer["ErrorDuplicateRecord"]);
            }
            
            if (ModelState.IsValid)
            {
                TitleAlias titleAlias = new TitleAlias
                {
                    Alias = titleAliasCreateViewModel.titleAlias,
                    TitleId = titleAliasCreateViewModel.TitleId
                };

                try
                {
                    _repository.CreateAlias(titleAlias);
                    return RedirectToAction("Aliases", new { id = titleAliasCreateViewModel.TitleId });
                }
                catch (DbUpdateException)
                {

                    ViewBag.Message = _sharedLocalizer["ErrorDbUpdate"];
                    return View(titleAliasCreateViewModel);
                }
            }

            return View(titleAliasCreateViewModel);
        }

        public IActionResult AliasDelete(int id)
        {
            ViewBag.TitleName = _repository.GetTitle(_repository.GetAlias(id).TitleId).Name;
            return View(_repository.GetAlias(id));
        }

        [HttpPost]
        public IActionResult AliasDelete(TitleAlias titleAlias)
        {
            int titleId = _repository.GetAlias(titleAlias.Id).TitleId;
            try
            {
                _repository.DeleteAlias(titleAlias.Id);
            }
            catch
            {
            }

            return RedirectToAction("Aliases", new {id = titleId});
        }
    }
}