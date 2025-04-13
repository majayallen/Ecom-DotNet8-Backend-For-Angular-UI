using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Core.Sharing;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecom.Infrastructure.Repositries
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepositry
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepositry(AppDbContext db,IMapper mapper, IImageManagementService imageManagementService) : base(db)
        {
            _mapper = mapper;
            _db = db;
            _imageManagementService = imageManagementService;
        }


        public async Task<ReturnProductDTO> GetAllAsync(ProductParams productParams)
        {
            var query = _db.Products.OrderBy(m => m.Name).Include(x => x.Category).Include(x => x.Photos)
                .AsNoTracking();


            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWord = productParams.Search.Split(' ');
                query = query.Where(m => searchWord.All(woed =>
                m.Name.ToLower().Contains(woed.ToLower()) ||
                m.Description.ToLower().Contains(woed.ToLower())
                ));
            }

            
             if(productParams.categoryId.HasValue)
                query = query.Where(x=>x.CategoryId == productParams.categoryId);

            if (!string.IsNullOrEmpty(productParams.sort))
            {
                query = productParams.sort switch
                {
                    "PriceAce" => query.OrderBy(m => m.NewPrice),
                    "PriceDce" => query.OrderByDescending(m => m.NewPrice),
                    _ => query.OrderBy(m => m.Name),
                };
            }

            ReturnProductDTO returnProductDTO = new ReturnProductDTO();
            returnProductDTO.TotalCount = query.Count();

            query = query.Skip((productParams.pageSize) * (productParams.pageNumber - 1)).Take(productParams.pageSize);


             returnProductDTO.Products = _mapper.Map<List<ProductDTO>>(query);
            return returnProductDTO;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if(productDTO == null) return false;

            var product = _mapper.Map<Product>(productDTO);
            await _db.AddAsync(product);
            await _db.SaveChangesAsync();

            var ImagePath = await _imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();
            await _db.AddRangeAsync(photo);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
           if (updateProductDTO == null) return false;
           var FindProduct = await _db.Products.Include(x=>x.Category).Include(x=>x.Photos).FirstOrDefaultAsync(m=>m.Id==updateProductDTO.Id);
            if (FindProduct == null) return false;
            _mapper.Map(updateProductDTO,FindProduct);
            var findPhoto = await _db.Photos.Where(x => x.ProductId == updateProductDTO.Id).ToListAsync();
            foreach(var photos in findPhoto)
            {
                _imageManagementService.DeleteImageAsync(photos.ImageName);
            }
             _db.Photos.RemoveRange(findPhoto);
            var ImagePath = await _imageManagementService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);
            var photo = ImagePath.Select(Path => new Photo
            {

                ImageName = Path,
                ProductId = updateProductDTO.Id,
            }).ToList();
            await _db.AddRangeAsync(photo);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task DeleteAsync(Product product)
        {
            var photo = await _db.Photos.Where(x=>x.ProductId == product.Id).ToListAsync();
            foreach (var item in photo) { 
               
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
             _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}
