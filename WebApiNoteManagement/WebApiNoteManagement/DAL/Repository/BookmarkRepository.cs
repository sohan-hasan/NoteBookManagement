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
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly NoteManagementContext _context;
        public BookmarkRepository(NoteManagementContext contex)
        {
            _context = contex;
        }
        public async Task<IEnumerable<TblBookmarkViewModel>> GetAllByUserId(string userId)
        {
            IEnumerable<TblBookmarkViewModel> listOfBoolMark = await _context.TblBookmarks.Where(a => a.UserId == userId).Select(e => new TblBookmarkViewModel
            {
                BookmarkId = e.BookmarkId,
                SiteName = e.SiteName,
                BookmarkUrl = e.BookmarkUrl,
                UserId=e.UserId,

            }).ToListAsync();
            return listOfBoolMark;
        }

        public async Task<TblBookmarkViewModel> GetById(int id)
        {
            TblBookmark e = await _context.TblBookmarks.AsNoTracking().FirstOrDefaultAsync(e => e.BookmarkId == id);
            if (e != null)
            {
                TblBookmarkViewModel bookmark = new TblBookmarkViewModel
                {

                    BookmarkId = e.BookmarkId,
                    SiteName = e.SiteName,
                    BookmarkUrl = e.BookmarkUrl,
                    UserId = e.UserId,
                };
                return bookmark;
            }
            return null;
        }

        public async Task<TblBookmarkViewModel> Insert(TblBookmarkViewModel e)
        {
            TblBookmarkViewModel returnObj = new TblBookmarkViewModel();
            if (e != null)
            {
                TblBookmark obj = new TblBookmark()
                {
                    BookmarkId = e.BookmarkId,
                    SiteName = e.SiteName,
                    BookmarkUrl = e.BookmarkUrl,
                    UserId=e.UserId,
                };
                await _context.TblBookmarks.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.BookmarkId);

            }
            return returnObj;
        }

        public async Task<TblBookmarkViewModel> Update(TblBookmarkViewModel e)
        {
            var result = await _context.TblBookmarks.FirstOrDefaultAsync(h => h.BookmarkId == e.BookmarkId);
            TblBookmarkViewModel returnObj = new TblBookmarkViewModel();
            if (result != null)
            {
                result.BookmarkId = e.BookmarkId;
                result.SiteName = e.SiteName;
                result.BookmarkUrl = e.BookmarkUrl;
                result.UserId = e.UserId;
            }
            await Save();
            returnObj = await GetById(result.BookmarkId);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.TblBookmarks.FirstOrDefaultAsync(p => p.BookmarkId == id);
            if (result != null)
            {
                _context.TblBookmarks.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
