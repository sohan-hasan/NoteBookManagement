using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNoteManagement.ViewModels;

namespace WebApiNoteManagement.DAL.IRepository
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<TblBookmarkViewModel>> GetAllByUserId(string userId);
        Task<TblBookmarkViewModel> GetById(int id);
        Task<TblBookmarkViewModel> Insert(TblBookmarkViewModel e);
        Task<TblBookmarkViewModel> Update(TblBookmarkViewModel e);
        Task Delete(int id);
        Task Save();
    }
    public interface IRegularNoteRepository
    {
        Task<IEnumerable<TblRegularNoteViewModel>> GetAllByUserId(string userId);
        Task<TblRegularNoteViewModel> GetById(int id);
        Task<TblRegularNoteViewModel> Insert(TblRegularNoteViewModel e);
        Task<TblRegularNoteViewModel> Update(TblRegularNoteViewModel e);
        Task Delete(int id);
        Task Save();
    }
    public interface IReminderRepository
    {

        Task<IEnumerable<TblReminderViewModel>> GetAll();
        Task<IEnumerable<TblReminderViewModel>> GetAllByUserId(string userId);
        Task<IEnumerable<TblReminderViewModel>> GetTodayReminderList(string userId);
        Task<IEnumerable<TblReminderViewModel>> GetWeeklyReminderList(string userId); 
        Task<IEnumerable<TblReminderViewModel>> GetMonthlyReminderList(string userId); 
         Task<TblReminderViewModel> GetById(int id);
        Task<TblReminderViewModel> Insert(TblReminderViewModel e);
        Task<TblReminderViewModel> Update(TblReminderViewModel e);
        Task Delete(int id);
        Task Save();
    }
    public interface ITodoRepository
    {
        Task<IEnumerable<TblTodoViewModel>> GetAllByUserId(string userId);
        Task<IEnumerable<TblTodoViewModel>> GetTodayTodoList(string userId);
        Task<IEnumerable<TblTodoViewModel>> GetWeeklyTodoList(string userId);
        Task<IEnumerable<TblTodoViewModel>> GetMonthlyTodoList(string userId);
        Task<TblTodoViewModel> GetById(int id);
        Task<TblTodoViewModel> Insert(TblTodoViewModel e);
        Task<TblTodoViewModel> Update(TblTodoViewModel e);
        Task Delete(int id);
        Task Save();
    }
}
