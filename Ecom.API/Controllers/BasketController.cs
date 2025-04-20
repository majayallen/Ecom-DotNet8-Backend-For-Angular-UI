using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Entites;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class BasketController : BaseController
    {
        public BasketController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-basket-item/{id}")]
        public async Task<IActionResult>get(string id)
        {
            var result=await _unitOfWork.CustomerBasket.GetBasketAsync(id);
            if(result is null)
            {
                return Ok(new CustomerBasket());
            }
            return Ok(result);
        }
        [HttpPost("update-basket")]
        public async Task<IActionResult> add(CustomerBasket basket)
        {
            var result = await _unitOfWork.CustomerBasket.UpdateBasketAsync(basket);
            return Ok(result);
        }
        [HttpDelete("delete-basket-item/{id}")]
        public async Task<IActionResult> delete(string id)
        {
            var result = await _unitOfWork.CustomerBasket.DeleteBasketAsync(id);
            return result ? Ok(new ResponseAPI(200,"item deleted")) : BadRequest(new ResponseAPI(400));
        }

    }
}
