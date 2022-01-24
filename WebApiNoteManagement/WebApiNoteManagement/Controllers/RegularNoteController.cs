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
    public class RegularNoteController : ControllerBase
    {
        private readonly IRegularNoteRepository _iRegularNoteRepository;
        public RegularNoteController(IRegularNoteRepository iRegularNoteRepository)
        {
            _iRegularNoteRepository = iRegularNoteRepository;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllByUserId")]
        public async Task<ActionResult> GetAllByUserId(string userId)
        {
            try
            {
                var regularNoteList = await _iRegularNoteRepository.GetAllByUserId(userId);
                return Ok(regularNoteList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetById")]
        public async Task<ActionResult<TblRegularNoteViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iRegularNoteRepository.GetById(id);
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
        public async Task<object> Insert([FromBody] TblRegularNoteViewModel obj)
        {
            try
            {
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object missing", null));
                }
                var regularNote = await _iRegularNoteRepository.GetById(obj.RegularNoteId);
                if (regularNote != null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data already exist", regularNote));
                }
                obj.NoteDateTime = DateTime.Now;
                var returnObj = await _iRegularNoteRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data entry successful", returnObj));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Update")]
        public async Task<object> Update([FromBody] TblRegularNoteViewModel obj)
        {
            try
            {

                var regularNote = await _iRegularNoteRepository.GetById(obj.RegularNoteId);
                if (obj.UserId != regularNote.UserId)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User miss match", null));
                }
                if (regularNote == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object misssing", null));
                }
                obj.NoteDateTime = regularNote.NoteDateTime;
                var returnObj = await _iRegularNoteRepository.Update(obj);
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
                var regularNote = await _iRegularNoteRepository.GetById(id);
                if (regularNote == null)
                {
                    return NotFound();
                }
                await _iRegularNoteRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
    }
}
