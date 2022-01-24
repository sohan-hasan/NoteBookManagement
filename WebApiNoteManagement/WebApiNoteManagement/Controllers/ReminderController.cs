using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNoteManagement.DAL.IRepository;
using WebApiNoteManagement.Helper;
using WebApiNoteManagement.Models;
using WebApiNoteManagement.ViewModels;

namespace WebApiNoteManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderRepository _iIReminderRepository;
        public ReminderController(IReminderRepository iReminderRepository)
        {
            _iIReminderRepository = iReminderRepository;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllByUserId")]
        public async Task<ActionResult> GetAllByUserId(string userId)
        {
            try
            {
                var reminderNoteList = await _iIReminderRepository.GetAllByUserId(userId);
                return Ok(reminderNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetById")]
        public async Task<ActionResult<TblReminderViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iIReminderRepository.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Insert")]
        public async Task<object> Insert([FromBody] TblReminderViewModel obj)
        {
            try
            {
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object missing", null));
                }
                var reminderNote = await _iIReminderRepository.GetById(obj.ReminderNoteId);
                if (reminderNote != null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data already exist", reminderNote));
                }
                obj.ReminderDateTime = Convert.ToDateTime(obj.ReminderDate + " " + obj.ReminderTime);
                var returnObj = await _iIReminderRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data entry successful", returnObj));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Update")]
        public async Task<object> Update([FromBody] TblReminderViewModel obj)
        {
            try
            {

                var reminderNote = await _iIReminderRepository.GetById(obj.ReminderNoteId);
                if (reminderNote == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object misssing", null));
                }
                obj.ReminderDateTime = Convert.ToDateTime(obj.ReminderDate + " " + obj.ReminderTime);
                var returnObj = await _iIReminderRepository.Update(obj);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data updated succesfully", returnObj));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var reminderNote = await _iIReminderRepository.GetById(id);
                if (reminderNote == null)
                {
                    return NotFound();
                }
                await _iIReminderRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetTodayReminderList")]
        public async Task<ActionResult> GetTodayReminderList(string userId)
        {
            try
            {
                var reminderNoteList = await _iIReminderRepository.GetTodayReminderList(userId);
                return Ok(reminderNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetWeeklyReminderList")]
        public async Task<ActionResult> GetWeeklyReminderList(string userId)
        {
            try
            {
                var reminderNoteList = await _iIReminderRepository.GetWeeklyReminderList(userId);
                return Ok(reminderNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetMonthlyReminderList")]
        public async Task<ActionResult> GetMonthlyReminderList(string userId)
        {
            try
            {
                var reminderNoteList = await _iIReminderRepository.GetMonthlyReminderList(userId);
                return Ok(reminderNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }
        

    }
}
