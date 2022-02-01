using Aircon.Business.Extensions;
using Aircon.Business.Models.Shared;
using Aircon.Data;
using Aircon.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services.Shared
{
    public interface INoteEntityService<T>
    {
        List<NoteModel> GetAll(int Id);
        NoteModel Add(int id, NoteModel noteModel);
    }

    public class NoteEntityService<T> : INoteEntityService<T> where T : NoteEntity
    {
        private readonly AirconDbContext _airconDbContext;
        public NoteEntityService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }
        public List<NoteModel> GetAll(int Id)
        {
            return _airconDbContext.Set<T>().AsNoTracking().Where(x => x.Id == Id).Select(x => new NoteModel
            {
                Text = x.Note.Text,
                CreatedById = x.Note.CreatedById,
                NoteId = x.NoteId,
                CreatedOnUtc = x.Note.CreatedOnUtc
            }).ToList();
        }
        public NoteModel Add(int id, NoteModel noteModel)
        {
            var note = noteModel.GetNoteEntity<T>();
            note.Id = id;
            note.Note = new Note { Text = noteModel.Text,CreatedById = noteModel.CreatedById };
            _airconDbContext.Set<T>().Add(note);
            _airconDbContext.SaveChanges();
            noteModel.NoteId = note.Note.Id;
            return noteModel;
        }
    }

}
