using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNoteManagement.DAL.IRepository;
using WebApiNoteManagement.Helper;
using WebApiNoteManagement.Models;
using WebApiNoteManagement.ViewModels;

namespace WebApiNoteManagement.DAL.Repository
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly NoteManagementContext _context;
        public ReminderRepository(NoteManagementContext contex)
        {
            _context = contex;
        }
        public async Task<IEnumerable<TblReminderViewModel>> GetAll()
        {
            IEnumerable<TblReminderViewModel> listOfReminderNote = await _context.TblReminders.Select(e => new TblReminderViewModel
            {
                ReminderNoteId = e.ReminderNoteId,
                Note = e.Note,
                ReminderDateTime = e.ReminderDateTime,
                UserId = e.UserId,

            }).ToListAsync();
            return listOfReminderNote;
        }

        public async Task<IEnumerable<TblReminderViewModel>> GetAllByUserId(string userId)
        {
            IEnumerable<TblReminderViewModel> listOfReminderNote = await _context.TblReminders.Where(a => a.UserId == userId).Select(e => new TblReminderViewModel
            {
                ReminderNoteId = e.ReminderNoteId,
                Note = e.Note,
                ReminderDateTime = e.ReminderDateTime,
                UserId = e.UserId,

            }).ToListAsync();
            return listOfReminderNote;
        }

        public async Task<IEnumerable<TblReminderViewModel>> GetTodayReminderList(string userId)
        {
            IEnumerable<TblReminderViewModel> listOfReminderNote = await _context.TblReminders.Where(a => a.UserId == userId && a.ReminderDateTime.Date==DateTime.Now.Date).Select(e => new TblReminderViewModel
            {
                ReminderNoteId = e.ReminderNoteId,
                Note = e.Note,
                ReminderDateTime = e.ReminderDateTime,
                UserId = e.UserId,

            }).ToListAsync();
            return listOfReminderNote;
        }
        
        public async Task<IEnumerable<TblReminderViewModel>> GetWeeklyReminderList(string userId)
        {
            DateTime weekStartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime weekEndDate = weekStartDate.Date.AddDays(6);
            IEnumerable<TblReminderViewModel> listOfReminderNote = await _context.TblReminders.Where(a => a.UserId == userId && weekStartDate.Date <= a.ReminderDateTime.Date && a.ReminderDateTime.Date <= weekEndDate).Select(e => new TblReminderViewModel
            {
                ReminderNoteId = e.ReminderNoteId,
                Note = e.Note,
                ReminderDateTime = e.ReminderDateTime,
                UserId = e.UserId,

            }).ToListAsync();
            return listOfReminderNote;
        }
        public async Task<IEnumerable<TblReminderViewModel>> GetMonthlyReminderList(string userId)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            IEnumerable<TblReminderViewModel> listOfReminderNote = await _context.TblReminders.Where(a => a.UserId == userId && firstDayOfMonth.Date <= a.ReminderDateTime.Date && a.ReminderDateTime.Date <= lastDayOfMonth.Date).Select(e => new TblReminderViewModel
            {
                ReminderNoteId = e.ReminderNoteId,
                Note = e.Note,
                ReminderDateTime = e.ReminderDateTime,
                UserId = e.UserId,
            }).ToListAsync();
            return listOfReminderNote;
        }
        
        public async Task<TblReminderViewModel> GetById(int id)
        {
            TblReminder e = await _context.TblReminders.AsNoTracking().FirstOrDefaultAsync(e => e.ReminderNoteId == id);
            if (e != null)
            {
                TblReminderViewModel reminderNote = new TblReminderViewModel
                {
                    ReminderNoteId = e.ReminderNoteId,
                    Note = e.Note,
                    ReminderDateTime = e.ReminderDateTime,
                    UserId = e.UserId,
                };
                return reminderNote;
            }
            return null;
        }

        public async Task<TblReminderViewModel> Insert(TblReminderViewModel e)
        {
            TblReminderViewModel returnObj = new TblReminderViewModel();
            if (e != null)
            {
                TblReminder obj = new TblReminder()
                {
                    ReminderNoteId = e.ReminderNoteId,
                    Note = e.Note,
                    ReminderDateTime = e.ReminderDateTime,
                    UserId = e.UserId,
                };
                await _context.TblReminders.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.ReminderNoteId);

            }
            return returnObj;
        }

        public async Task<TblReminderViewModel> Update(TblReminderViewModel e)
        {
            var result = await _context.TblReminders.FirstOrDefaultAsync(h => h.ReminderNoteId == e.ReminderNoteId);
            TblReminderViewModel returnObj = new TblReminderViewModel();
            if (result != null)
            {
                result.ReminderNoteId = e.ReminderNoteId;
                result.Note = e.Note;
                result.ReminderDateTime = e.ReminderDateTime;
                result.UserId = e.UserId;
            }
            await Save();
            returnObj = await GetById(result.ReminderNoteId);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.TblReminders.FirstOrDefaultAsync(p => p.ReminderNoteId == id);
            if (result != null)
            {
                _context.TblReminders.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
