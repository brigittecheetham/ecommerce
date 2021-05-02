using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
using AutoMapper;
using api.Dtos;
using System.Linq;
using core.RepositoryObjects;
using Microsoft.AspNetCore.Http;
using api.Errors;
using api.Helpers;

namespace api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductBrandRepository _productBrandRepository;

        public ProductsController(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IProductBrandRepository productBrandRepository, IMapper mapper)
        {
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResponseDto>>> GetProducts([FromQuery] ProductRequestDto productRequestDto)
        {

            var productRepositoryObject = new ProductRepositoryObject
            {
                BrandId = productRequestDto?.BrandId,
                SortBy = productRequestDto?.SortBy,
                SortOrder = productRequestDto?.SortOrder,
                TypeId = productRequestDto?.TypeId,
                Skip = productRequestDto?.PageIndex ?? 1,
                Take = productRequestDto?.PageSize ?? 50,
                Search = productRequestDto.Search,
                IsPagingEnabled = true
            };
            
            var products = await _productRepository.GetAllAsync(productRepositoryObject);
            var totalItems = await _productRepository.CountAsync(productRepositoryObject);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductResponseDto>>(products);
            var pagination = new Pagination<ProductResponseDto>(productRepositoryObject.Skip, productRepositoryObject.Take, totalItems, data);

            return Ok(pagination);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse(404));

            var productDto = _mapper.Map<Product, ProductResponseDto>(product);
            return Ok(productDto);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepository.GetAllAsync(null);
            return Ok(productTypes);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepository.GetAllAsync(null);
            return Ok(productBrands);
        }

    }
}