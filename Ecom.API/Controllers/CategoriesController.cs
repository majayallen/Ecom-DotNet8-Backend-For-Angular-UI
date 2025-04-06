using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var category = await _unitOfWork.CategoryRepositry.GetAllAsync();
                if (category == null)
                    return BadRequest(new ResponseAPI(400));
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepositry.GetByIdAsync(id);
                if (category == null)
                    return BadRequest(new ResponseAPI(400));
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> Add(CategoryDTO entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                if (category == null)
                    return BadRequest(new ResponseAPI(400));
                await _unitOfWork.CategoryRepositry.AddAsync(category);

                return Ok(new ResponseAPI(200 , "Item has been Added"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> Update(UpdateCategoryDTO entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                if (category == null)
                    return BadRequest(new ResponseAPI(400));
                await _unitOfWork.CategoryRepositry.UpdateAsync(category);

                return Ok(new ResponseAPI(200 , "Item has been Updated"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {               
                if (id <= 0)
                    return BadRequest(new ResponseAPI(400));
                await _unitOfWork.CategoryRepositry.DeleteAsync(id);

                return Ok(new ResponseAPI(200,"Item has been deleted"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
