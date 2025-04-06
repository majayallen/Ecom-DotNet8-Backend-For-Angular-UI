using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var cat = await _unitOfWork.CategoryRepositry.GetByIdAsync(100);
            if (cat == null) return NotFound();
            return Ok(cat);
        }
        [HttpGet("server-error")]
        public async Task<ActionResult> GetErrorServer()
        {
            var cat = await _unitOfWork.CategoryRepositry.GetByIdAsync(100);
            cat.Name = ""; 
            return Ok(cat);
        }
        [HttpGet("bad-request/{id}")]
        public async Task<ActionResult> GetBadRequest(int id)
        {         
            return Ok();
        }
        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
