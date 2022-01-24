using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApiNoteManagement.DAL.IRepository;
using WebApiNoteManagement.Models;
using WebApiNoteManagement.ViewModels;

namespace WebApiNoteManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _iTodoRepository;
        public TodoController(ITodoRepository iTodoRepository)
        {
            _iTodoRepository = iTodoRepository;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllByUserId")]
        public async Task<ActionResult> GetAllByUserId(string userId)
        {
            try
            {
                var todoNoteList = await _iTodoRepository.GetAllByUserId(userId);
                return Ok(todoNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetById")]
        public async Task<ActionResult<TblTodoViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iTodoRepository.GetById(id);
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
        public async Task<object> Insert([FromBody] TblTodoViewModel obj)
        {
            try
            {
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object missing", null));
                }
                var todoNote = await _iTodoRepository.GetById(obj.TodoNoteId);
                if (todoNote != null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data already exist", todoNote));
                }

                obj.TodoDateTime = Convert.ToDateTime(obj.TodoDate + " " + obj.TodoTime);
                var returnObj = await _iTodoRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data entry successful", returnObj));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Update")]
        public async Task<object> Update([FromBody] TblTodoViewModel obj)
        {
            try
            {

                var todoNote = await _iTodoRepository.GetById(obj.TodoNoteId);
                if (todoNote == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object misssing", null));
                }

                obj.TodoDateTime = Convert.ToDateTime(obj.TodoDate + " " + obj.TodoTime);
                var returnObj = await _iTodoRepository.Update(obj);
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
                var todoNote = await _iTodoRepository.GetById(id);
                if (todoNote == null)
                {
                    return NotFound();
                }
                await _iTodoRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetTodayTodoList")]
        public async Task<ActionResult> GetTodayTodoList(string userId)
        {
            try
            {
                var reminderNoteList = await _iTodoRepository.GetTodayTodoList(userId);
                return Ok(reminderNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetWeeklyTodoList")]
        public async Task<ActionResult> GetWeeklyTodoList(string userId)
        {
            try
            {
                var reminderNoteList = await _iTodoRepository.GetWeeklyTodoList(userId);
                return Ok(reminderNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetMonthlyTodoList")]
        public async Task<ActionResult> GetMonthlyTodoList(string userId)
        {
            try
            {
                var reminderNoteList = await _iTodoRepository.GetMonthlyTodoList(userId);
                return Ok(reminderNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }
    }
}
