using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNoteManagement.DAL.IRepository;
using WebApiNoteManagement.Models;
using WebApiNoteManagement.ViewModels;

namespace WebApiNoteManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository _iBookmarkRepository;
        public BookmarkController(IBookmarkRepository iBookmarkRepository)
        {
            _iBookmarkRepository = iBookmarkRepository;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllByUserId")]
        public async Task<ActionResult> GetAllByUserId(string userId)
        {
            try
            {
                var bookmarkList = await _iBookmarkRepository.GetAllByUserId(userId);
                return Ok(bookmarkList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error"); ;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetById")]
        public async Task<ActionResult<TblBookmarkViewModel>> GetById(int id)
        {
            try
            {
                var result = await _iBookmarkRepository.GetById(id);
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
        public async Task<object> Insert([FromBody] TblBookmarkViewModel obj)
        {
            try
            {
                if (obj == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object missing", null));
                }
                var bookmark = await _iBookmarkRepository.GetById(obj.BookmarkId);
                if (bookmark != null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data already exist", bookmark));
                }

                var returnObj = await _iBookmarkRepository.Insert(obj);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data entry successful", returnObj));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Update")]
        public async Task<object> Update([FromBody] TblBookmarkViewModel obj)
        {
            try
            {

                var bookmark = await _iBookmarkRepository.GetById(obj.BookmarkId); 
                if (obj.UserId != bookmark.UserId)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "User miss match", null));
                }
                if (bookmark == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Data object misssing", null));
                }

                var returnObj = await _iBookmarkRepository.Update(obj);
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
                var bookmark = await _iBookmarkRepository.GetById(id);
                if (bookmark == null)
                {
                    return NotFound();
                }
                await _iBookmarkRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }
    }
}
