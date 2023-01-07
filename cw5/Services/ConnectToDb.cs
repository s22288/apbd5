using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public class ConnectToDb
    {
        public int maxId()
        {
           
            
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                var com = new SqlCommand($"SELECT max(IdProductWareHouse) FROM Product_Warehouse", con);
                con.Open();
                int maxId = Convert.ToInt32(com.ExecuteScalar());
                return maxId + 1;

            }
        }

            public int AddProductWareHouse(ProductWareHouse productWareHouse)
        {


            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("insert into Product_Warehouse(IdProductWareHouse,IdWareHouse,IdProduct,IdOrder,Amount,Price,CreatedAt) values(@param1,'@param2,@param3,@param4,@param5,@param6,@param7);", connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).Value = productWareHouse.IdProductWarehouse;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).Value = productWareHouse.IdWarehouse;
                    cmd.Parameters.Add("@param3", SqlDbType.Int).Value = productWareHouse.IdProduct;
                    cmd.Parameters.Add("@param4", SqlDbType.Int).Value = productWareHouse.IdOrder;
                    cmd.Parameters.Add("@param5", SqlDbType.Int).Value = productWareHouse.Amout;
                    cmd.Parameters.Add("@param6", SqlDbType.BigInt).Value = productWareHouse.Price;
                    cmd.Parameters.Add("@param7", SqlDbType.DateTime).Value = productWareHouse.CreateAt;

                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        return 0;
                    }

                }
                return productWareHouse.IdProductWarehouse;
            }
        }
        public async Task<bool> ifOrderExists(int idOrder)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                var com = new SqlCommand("SELECT 1 FROM Product_WareHouse WHERE IdOrder = @orderId", con);
                com.Parameters.AddWithValue("@orderId", idOrder);
                await con.OpenAsync();
                var result = await com.ExecuteReaderAsync();
                return result.HasRows;
            }
        }
        public IEnumerable<Order> getOrderByid(int idProduct)
        {
            var res = new List<Order>();
            //System.Data.SqlClient
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                var com = new SqlCommand($"SELECT * FROM Animals where IdProduct = @par", con);
                com.Parameters.AddWithValue("@par", idProduct);
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    res.Add(new Order
                    {
                        IdOrder = int.Parse(dr["IdOrder"].ToString()),
                        IdProduct = int.Parse(dr["IdProduct"].ToString()),
                        Amount = int.Parse(dr["Amount"].ToString()),
                        CreatedAt =Convert.ToDateTime( dr["CreatedAt"].ToString()),
                        FulfilledAt =Convert.ToDateTime( dr["FullfieldAt"].ToString()),
                    }); ;
                }
            }
            return res;
        }

        public async Task<bool> ifProdctExistsAsync(int idProduct)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                var com = new SqlCommand("SELECT 1 FROM Product WHERE IdProduct = @productId", con);
                com.Parameters.AddWithValue("@productId", idProduct);
                await con.OpenAsync();
                var result = await com.ExecuteReaderAsync();
                return result.HasRows;
            }
        }
            public async Task<bool> ifWareHouseExists(int idWarehousei)
            {
                using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
                {
                    var com = new SqlCommand("SELECT 1 FROM Product WHERE IdProduct = @IdWarehouse", con);
                    com.Parameters.AddWithValue("@productId", idWarehousei);
                    await con.OpenAsync();
                    var result = await com.ExecuteReaderAsync();
                    return result.HasRows;
                }
            }
    }
}
