using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery]ProductParams productParams)
        {
            try
            {
                var product = await _unitOfWork.ProductRepositry.GetAllAsync(productParams);
                return Ok(new Pagination<ProductDTO>(productParams.pageNumber, productParams.pageSize,product.TotalCount,product.Products));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-Id/{id}")]
        public async Task<IActionResult>GetgById(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepositry.GetByIdAsync(id,x=>x.Photos,x=>x.Category);
                if (product is null)
                    return NotFound();
                var productDTO = _mapper.Map<ProductDTO>(product);
                return Ok(productDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> Add(AddProductDTO productDTO)
        {
            try
            {
                if (productDTO is null)
                    return BadRequest(new ResponseAPI(400));
                await _unitOfWork.ProductRepositry.AddAsync(productDTO);
                return Ok(new ResponseAPI(200, "Item has been Added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-product")]
        public async Task<IActionResult> Update(UpdateProductDTO productDTO)
        {
            try
            {
                if (productDTO is null)
                    return BadRequest(new ResponseAPI(400));
                await _unitOfWork.ProductRepositry.UpdateAsync(productDTO);
                return Ok(new ResponseAPI(200, "Item has been Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest(new ResponseAPI(400));
                var product = await _unitOfWork.ProductRepositry.GetByIdAsync(id,x=>x.Category,x=>x.Photos);
                await _unitOfWork.ProductRepositry.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Item has been deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
