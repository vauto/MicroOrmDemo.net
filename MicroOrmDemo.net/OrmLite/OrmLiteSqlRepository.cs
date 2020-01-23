using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace MicroOrmDemo.net.OrmLite
{

    public class OrmLiteSqlRepository : OrmLiteRepositoryBase
    {
        public override async Task<List<Orders>> GetOrders()
        {
            using (var dbConnection = ConnectionFactory.OpenDbConnection())
            {
                return await dbConnection.SelectAsync<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID");
            }
        }

        public override async Task<Orders> GetOrderById(int id)
        {
            using (var dbConnection = ConnectionFactory.OpenDbConnection())
            {
                var data = await dbConnection.SelectAsync<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                                     WHERE WorkOrderID = @Id", new { Id = id });
                return data.FirstOrDefault();
            }
        }
    }
}
