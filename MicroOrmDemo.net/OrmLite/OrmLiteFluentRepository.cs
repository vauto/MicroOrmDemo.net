using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace MicroOrmDemo.net.OrmLite
{
    public class OrmLiteFluentRepository : OrmLiteRepositoryBase
    {
        public override async Task<List<Orders>> GetOrders()
        {
            using (var dbConnection = ConnectionFactory.OpenDbConnection())
            {
                var sql = dbConnection
                          .From<WorkOrder>()
                          .Join<Product>((w, p) => w.ProductID == p.ProductID)
                          .Select<WorkOrder,Product>((w,p) => new { Id = w.WorkOrderId, ProductName =  p.Name, Quantity = w.OrderQty, Date = w.DueDate })
                          .Limit(0,500);

                var data = await dbConnection.SelectAsync<Orders>(sql);
                return data.ToList();
            }
        }

        public override async Task<Orders> GetOrderById(int id)
        {
            using (var dbConnection = ConnectionFactory.OpenDbConnection())
            {
                var sql = dbConnection
                          .From<WorkOrder>()
                          .Join<Product>((w, p) => w.ProductID == p.ProductID)
                          .Select<WorkOrder, Product>((w, p) => new { Id = w.WorkOrderId, ProductName = p.Name, Quantity = w.OrderQty, Date = w.DueDate })
                          .Where<WorkOrder>(w => w.WorkOrderId == id);

                var data =  await dbConnection.SelectAsync<Orders>(sql);
                return data.FirstOrDefault();
            }
        }
    }
}
