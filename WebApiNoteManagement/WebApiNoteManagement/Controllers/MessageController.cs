using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNoteManagement.DAL.IRepository;
using WebApiNoteManagement.DAL.Repository;
using WebApiNoteManagement.Helper;
using WebApiNoteManagement.Models;
using WebApiNoteManagement.ViewModels;

namespace WebApiNoteManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<NotifyHub, ITypeHubClient> _hubContext;
        private readonly IReminderRepository _iIReminderRepository;
        public MessageController(IHubContext<NotifyHub, ITypeHubClient> hubContext, IReminderRepository iReminderRepository)
        {
            _hubContext = hubContext;
            _iIReminderRepository = iReminderRepository;
        }
        [HttpGet("GetSendNotification")]
        public string GetSendNotification(TblReminderViewModel obj)
        {
            string retMessage = string.Empty;
            var message = new Message() { UserId = obj.UserId, Note = obj.Note };
            try
            {
                _hubContext.Clients.All.BroadcastMessage(message);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            return retMessage;
        }
        [HttpGet("GetRequestForDo")]
        public async Task GetRequestForDo()
        {
            DateTime a = DateTime.Now;
            DateTime time = new DateTime(a.Year, a.Month, a.Day, a.Hour, a.Minute, 0, a.Kind);
            IEnumerable<TblReminderViewModel> reminderNoteList = await _iIReminderRepository.GetAll();
            if (reminderNoteList != null)
            {
                foreach (var item in reminderNoteList)
                {
                    if (item.ReminderDateTime == time)
                    {
                        this.GetSendNotification(item);
                    }
                }
            }
        }
    }
}
