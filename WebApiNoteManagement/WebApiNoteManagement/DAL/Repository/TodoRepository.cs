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
    public class TodoRepository : ITodoRepository
    {
        private readonly NoteManagementContext _context;
        public TodoRepository(NoteManagementContext contex)
        {
            _context = contex;
        }
        public async Task<IEnumerable<TblTodoViewModel>> GetAllByUserId(string userId)
        {
            IEnumerable<TblTodoViewModel> listOfTodoList = await _context.TblTodos.Where(a => a.UserId == userId).Select(e => new TblTodoViewModel
            {
                TodoNoteId=e.TodoNoteId,
                Note=e.Note,
                TodoDateTime=e.TodoDateTime,
                IsComplete=e.IsComplete,
                UserId = e.UserId,
            }).ToListAsync();
            return listOfTodoList;
        }

        public async Task<IEnumerable<TblTodoViewModel>> GetTodayTodoList(string userId)
        {
            IEnumerable<TblTodoViewModel> listOfTodoList = await _context.TblTodos.Where(a => a.UserId == userId && a.TodoDateTime.Date == DateTime.Now.Date).Select(e => new TblTodoViewModel
            {
                TodoNoteId = e.TodoNoteId,
                Note = e.Note,
                TodoDateTime = e.TodoDateTime,
                IsComplete = e.IsComplete,
                UserId = e.UserId,

            }).ToListAsync();
            return listOfTodoList;
        }

        public async Task<IEnumerable<TblTodoViewModel>> GetWeeklyTodoList(string userId)
        {
            DateTime weekStartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime weekEndDate = weekStartDate.Date.AddDays(6);
            IEnumerable<TblTodoViewModel> listOfTodoList = await _context.TblTodos.Where(a => a.UserId == userId && weekStartDate.Date <= a.TodoDateTime.Date && a.TodoDateTime.Date <= weekEndDate).Select(e => new TblTodoViewModel
            {
                TodoNoteId = e.TodoNoteId,
                Note = e.Note,
                TodoDateTime = e.TodoDateTime,
                IsComplete = e.IsComplete,
                UserId = e.UserId,

            }).ToListAsync();
            return listOfTodoList;
        }
        public async Task<IEnumerable<TblTodoViewModel>> GetMonthlyTodoList(string userId)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            IEnumerable<TblTodoViewModel> listOfTodoList = await _context.TblTodos.Where(a => a.UserId == userId && firstDayOfMonth.Date <= a.TodoDateTime.Date && a.TodoDateTime.Date <= lastDayOfMonth.Date).Select(e => new TblTodoViewModel
            {
                TodoNoteId = e.TodoNoteId,
                Note = e.Note,
                TodoDateTime = e.TodoDateTime,
                IsComplete = e.IsComplete,
                UserId = e.UserId,
            }).ToListAsync();
            return listOfTodoList;
        }
        public async Task<TblTodoViewModel> GetById(int id)
        {
            TblTodo e = await _context.TblTodos.AsNoTracking().FirstOrDefaultAsync(e => e.TodoNoteId == id);
            if (e != null)
            {
                TblTodoViewModel todoNote = new TblTodoViewModel
                {
                    TodoNoteId = e.TodoNoteId,
                    Note = e.Note,
                    TodoDateTime = e.TodoDateTime,
                    IsComplete = e.IsComplete,
                    UserId = e.UserId,
                };
                return todoNote;
            }
            return null;
        }

        public async Task<TblTodoViewModel> Insert(TblTodoViewModel e)
        {
            TblTodoViewModel returnObj = new TblTodoViewModel();
            if (e != null)
            {
                TblTodo obj = new TblTodo()
                {
                    TodoNoteId = e.TodoNoteId,
                    Note = e.Note,
                    TodoDateTime = e.TodoDateTime,
                    IsComplete = e.IsComplete,
                    UserId = e.UserId,
                };
                await _context.TblTodos.AddAsync(obj);
                await Save();
                returnObj = await GetById(obj.TodoNoteId);

            }
            return returnObj;
        }

        public async Task<TblTodoViewModel> Update(TblTodoViewModel e)
        {
            var result = await _context.TblTodos.FirstOrDefaultAsync(h => h.TodoNoteId == e.TodoNoteId);
            TblTodoViewModel returnObj = new TblTodoViewModel();
            if (result != null)
            {
                result.TodoNoteId = e.TodoNoteId;
                result.Note = e.Note;
                result.TodoDateTime = e.TodoDateTime;
                result.IsComplete = e.IsComplete;
                result.UserId = e.UserId;
            }
            await Save();
            returnObj = await GetById(result.TodoNoteId);
            return returnObj;
        }
        public async Task Delete(int id)
        {
            var result = await _context.TblTodos.FirstOrDefaultAsync(p => p.TodoNoteId == id);
            if (result != null)
            {
                _context.TblTodos.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
