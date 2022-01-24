using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNoteManagement.DAL.IRepository;
using WebApiNoteManagement.Models;
using WebApiNoteManagement.ViewModels;

namespace WebApiNoteManagement.DAL.Repository
{
    public class RegularNoteRepository : IRegularNoteRepository
    {
        private readonly NoteManagementContext _context;
        public RegularNoteRepository(NoteManagementContext contex)
        {
            _context = contex;
        }
        public async Task<IEnumerable<TblRegularNoteViewModel>> GetAllByUserId(string userId)
        {
            IEnumerable<TblRegularNoteViewModel> listOfregulerNote = await _context.TblRegularNotes.Where(a => a.UserId == userId).Select(e => new TblRegularNoteViewModel
            {
                RegularNoteId = e.RegularNoteId,
                Note = e.Note,
                NoteDateTime = e.NoteDateTime,
                UserId = e.UserId,

            }).ToListAsync();
            return listOfregulerNote;
        }

        public async Task<TblRegularNoteViewModel> GetById(int id)
        {
            TblRegularNote e = await _context.TblRegularNotes.AsNoTracking().FirstOrDefaultAsync(e => e.RegularNoteId == id);
            if (e != null)
            {
                TblRegularNoteViewModel regulerNote = new TblRegularNoteViewModel
                {

                    RegularNoteId = e.RegularNoteId,
                    Note = e.Note,
                    NoteDateTime = e.NoteDateTime,
                    UserId = e.UserId,
                };
                return regulerNote;
            }
            return null;
        }

        public async Task<TblRegularNoteViewModel> Insert(TblRegularNoteViewModel e)
        {
            TblRegularNoteViewModel returnObj = new TblRegularNoteViewModel();
            if (e != null)
            {
                TblRegularNote obj = new TblRegularNote()
                {
                    RegularNoteId = e.RegularNoteId,
                    Note = e.Note,
                    NoteDateTime = e.NoteDateTime,
                    UserId = e.UserId,
                };
                await _context.TblRegularNotes.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.RegularNoteId);

            }
            return returnObj;
        }

        public async Task<TblRegularNoteViewModel> Update(TblRegularNoteViewModel e)
        {
            var result = await _context.TblRegularNotes.FirstOrDefaultAsync(h => h.RegularNoteId == e.RegularNoteId);
            TblRegularNoteViewModel returnObj = new TblRegularNoteViewModel();
            if (result != null)
            {
                result.RegularNoteId = e.RegularNoteId;
                result.Note = e.Note;
                result.NoteDateTime = e.NoteDateTime;
                result.UserId = e.UserId;
            }
            await Save();
            returnObj = await GetById(result.RegularNoteId);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.TblRegularNotes.FirstOrDefaultAsync(p => p.RegularNoteId == id);
            if (result != null)
            {
                _context.TblRegularNotes.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
