using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public interface IFileDbService


    {
        int maxkey();
        int productWareHouseAdding(ProductWareHouse pr);

        bool checkIfProductExist(int idProduct);

        bool checkIfWareHouseExists(int wareHouse);

        Order getOrderWithFromDb(int IdProduct);

        bool checkIfOrderExists(int IdOrder);
    }
}
