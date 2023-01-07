using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public class FileDbService : IFileDbService
    {
     
        public bool checkIfOrderExists(int IdOrder)
        {
            ConnectToDb connect = new ConnectToDb();
           return  connect.ifOrderExists(IdOrder).Result;
        }

        public bool checkIfProductExist(int idProduct)
        {
            ConnectToDb connectToDb = new ConnectToDb();
         return   connectToDb.ifProdctExistsAsync(idProduct).Result;
        }

        public bool checkIfWareHouseExists(int wareHouse)
        {
            ConnectToDb connectToDb = new ConnectToDb();
         return   connectToDb.ifWareHouseExists(wareHouse).Result;
        }

        public Order getOrderWithFromDb(int IdProduct)
        {
            ConnectToDb connect = new ConnectToDb();
           return connect.getOrderByid(IdProduct).First();
        }

        public int maxkey()
        {
            ConnectToDb connect = new ConnectToDb();
          return  connect.maxId();
        }

        public int productWareHouseAdding(ProductWareHouse pr)
        {
            ConnectToDb connect = new ConnectToDb();
          return  connect.AddProductWareHouse(pr);
        }
    }
}
