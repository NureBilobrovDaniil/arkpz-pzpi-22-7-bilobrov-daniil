using MediatR;
using Microsoft.AspNetCore.Mvc;
using PivoGo.Application.CQRS.Commands.Products.CreateProduct;
using PivoGo.Application.CQRS.Commands.Products.DeleteProduct;
using PivoGo.Application.CQRS.Commands.Products.UpdateProduct;

//using PivoGo.Application.CQRS.Commands.Products.UpdateProduct;
//using PivoGo.Application.CQRS.Queries.Product.GetAllProducts;
//using PivoGo.Application.CQRS.Queries.Product.GetProductById;
using System.ComponentModel.DataAnnotations;

namespace PivoGo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController] // Атрибут для автоматической регистрации контроллера
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Конструктор с внедрением зависимостей
        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //// Получить все продукты
        //[HttpGet]
        //public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        //{
        //    var products = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
        //    return Ok(products);
        //}

        //// Получить продукт по ID
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        //{
        //    var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(product);
        //}

        // Создать продукт
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody, Required] CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command, cancellationToken);

            return Ok(); // Вы можете вернуть объект с ID нового продукта, если нужно
        }

        // Обновить продукт
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody, Required] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        // Удалить продукт по ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return Ok();
        }
    }
}
