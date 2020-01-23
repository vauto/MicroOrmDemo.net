using Mighty;
using System.Collections.Generic;
using System.Linq;

namespace MicroOrmDemo.net.Mighty
{
    public class MightyQueries
    {
        public List<Orders> GetOrders()
        {
            var db = new MightyOrm<Orders>("AdventureWorks2014");
            return db.Query(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                            FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                            INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
        }

        public List<Orders> GetOrders(int iteration)
        {
            var db = new MightyOrm<Orders>("AdventureWorks2014");
            var listOrders = new List<Orders>();
            for (int i = 1; i <= iteration; i++)
                listOrders.Add(GetOrder(db, i));

            return listOrders;
        }

        private Orders GetOrder(MightyOrm<Orders> db, int id)
        {
            return db.Query(@"SELECT [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                              FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                              INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                              WHERE WorkOrderID = @0", id).FirstOrDefault();
        }
    }
}
