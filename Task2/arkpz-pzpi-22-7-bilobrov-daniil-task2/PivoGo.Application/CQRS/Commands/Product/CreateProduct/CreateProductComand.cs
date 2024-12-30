using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivoGo.Application.CQRS.Commands.Products.CreateProduct
{
    public record CreateProductCommand
   (
       string Name,          // Название продукта
       string Description,   // Описание продукта
       decimal Price,        // Цена продукта
       int StockQuantity     // Количество на складе
   ) : IRequest;
}
