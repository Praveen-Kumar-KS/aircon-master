using Aircon.Business.Services.Shared;
using Aircon.Data.Entities;
using Aircon.Data.Helper;
using Aircon.Extensions;
using Aircon.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Controllers.Shared
{
    [Authorize]
    public class NoteEntityBaseController<T> : Controller where T : NoteEntity
    {
        private readonly INoteEntityService<T> _noteEntityService;
        public NoteEntityBaseController(INoteEntityService<T> noteEntityService)
        {
            _noteEntityService = noteEntityService;
        }

        [HttpPost("AddNote")]
        public ActionResult AddNote(string text, int entityId)
        {
            if (ModelState.IsValid)
            {
                var noteModel = new NoteViewModel { Text = text };
                noteModel.CreatedById = HttpContextHelper.UserId.Value;
                var result = _noteEntityService.Add(entityId, noteModel.ToModel()).ToViewModel();
                return Json(new
                {
                    success = true,
                    NoteId = result.NoteId,
                    Text = result.Text,
                    CreatedById = result.CreatedById,
                    CreatedOn = result.CreatedOn
                });
            }

            return Json(new { success = false }); 
        }

        [HttpGet("TabGrid/{Id}")]
        public IActionResult TabGrid(int Id)
        {
            NoteListViewModel noteListViewModel = new NoteListViewModel();
            noteListViewModel.EntityId = Id;
            noteListViewModel.Notes = _noteEntityService.GetAll(Id).Select(x => x.ToViewModel()).ToList();
            return PartialView("~/Views/NoteEntity/Index.cshtml", noteListViewModel);
        }


        //public IActionResult NotePartial(int noteId)
        //{
        //    List<CustomerAdminViewModel> customers = new List<CustomerAdminViewModel>();
        //    var searchText = SearchText();
        //    customers = _customerAdminService.GetCustomers(isActive, isAll, searchText).Select(x => x.ToViewModel()).ToList();
        //    return PartialView("_AdminCustomerContainer", customers);
        //}
    }

}
