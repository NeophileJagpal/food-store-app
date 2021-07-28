using Ardalis.GuardClauses;
using AutoMapper;
using FoodStore.Domain.Entities.Aggregates.Product;
using FoodStore.Domain.ValueObjects;
using FoodStore.SharedKernal.Interfaces;
using FoodStore.SharedKernal.Mappings;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStore.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
    }
    public class CreateProductResponse : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, CreateProductResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ProductInfo.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.ProductInfo.Description))
                .ForMember(d => d.Cost, opt => opt.MapFrom(s => s.Cost.Amount));
        }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            Guard.Against.Null(productRepository, nameof(productRepository));
            Guard.Against.Null(mapper, nameof(mapper));
            this._productRepository = productRepository;
            this._mapper = mapper;
        }
        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.StoreId, ProductInfo.Create(request.Name, request.Description), Money.Create("USD", request.Cost));
            var newProduct = await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<CreateProductResponse>(newProduct);
        }
    }
}
