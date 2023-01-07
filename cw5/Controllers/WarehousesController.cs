using Cw3.Models;
using Cw3.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    //[Route("[controller]")]
    //HTTP http://localhost:5000/api/warehouses
    public class WarehousesController : ControllerBase
    {
        private readonly IFileDbService _fileDbService;

        public WarehousesController(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }

        [HttpPost("{json}")]
        public IActionResult GetStudents(String json)
        {
            dynamic product = JsonConvert.DeserializeObject(json);
            Product productFromJson = new Product();
            productFromJson.IdProduct = (product.IdProduct);
            productFromJson.IdWarehouse = product.IdWarehouse;
            productFromJson.Amount = product.Amount;
            productFromJson.CreatedAt = Convert.ToDateTime( product.CreatedAt);
            if (String.IsNullOrEmpty(productFromJson.IdProduct.ToString())|| String.IsNullOrEmpty(productFromJson.IdWarehouse.ToString()) || String.IsNullOrEmpty(productFromJson.CreatedAt.ToString()) || String.IsNullOrEmpty(productFromJson.Amount.ToString()))
            {
                return BadRequest("404 żadne z pól nie moze byc puste");
            }
            // sprawdzanie czy produk i hurtownie istnieje

            if (productFromJson.Amount <= 0)
            {
                return BadRequest("404 Amount nie może byc mniejszy rowny 0");
            }
            if (!_fileDbService.checkIfProductExist(productFromJson.IdProduct) || !_fileDbService.checkIfWareHouseExists(productFromJson.IdWarehouse))
            {
                return BadRequest("404 produkt lub magazy nie istnieją");
            }
            // pobranie orderu z bazy danych 
          Order order =  _fileDbService.getOrderWithFromDb(productFromJson.IdProduct);
            if (order == null)
            {
                return BadRequest("404 nie ma takiego orderu");
            }
            if(order.CreatedAt> productFromJson.CreatedAt)
            {
                return BadRequest("404 data zamowienia powinna byc mniejsza niz podana w jsonie");
            }
            if(!String.IsNullOrEmpty(order.FulfilledAt.ToString()))
            {
                return BadRequest("404 jest juz wykoany ten order");
            }

           if(_fileDbService.checkIfOrderExists(order.IdOrder))
            {
                return BadRequest("order juz istenieje w tabeli Product_WareHouse");
            }
            // pobranie akutalnej daty

            DateTime actual = DateTime.Now;
            order.FulfilledAt = actual;
            productFromJson.Price = productFromJson.Amount * order.Amount;
            order.CreatedAt = DateTime.Now;
            ProductWareHouse productWareHouse = new ProductWareHouse();

            // generowanie nowego klucza do bazy danych
           int key = _fileDbService.maxkey();
            productWareHouse.IdProductWarehouse = key;

            productWareHouse.IdWarehouse = productFromJson.IdWarehouse;
            productWareHouse.IdProduct = productFromJson.IdProduct;
            productWareHouse.IdOrder = order.IdOrder;
            productWareHouse.Amout = order.Amount;
            productWareHouse.Price = productFromJson.Price;
            productWareHouse.CreateAt = actual;

         int id =   _fileDbService.productWareHouseAdding(productWareHouse);



            return Ok(key);
        }

      
    }
}
