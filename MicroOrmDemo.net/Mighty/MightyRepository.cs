using Dasync.Collections;
using Mighty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.Mighty
{
    public class Orders
    {
        public Orders() { }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Date { get; set; }
    }

    public class WorkOrder
    {
        public WorkOrder() { }

        public int WorkOrderId { get; set; }
        public int ProductID { get; set; }
        public int? OrderQty { get; set; }
        public int? StockedQty { get; set; }
        public int? ScrappedQty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? ScrapReasonID { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Product Product { get; set; }
    }

    //Db entity
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
    }

    public class MightyRepository
    {
        public MightyRepository()
        { }

        public async Task<List<Orders>> GetOrders()
        {
            var db = new MightyOrm<Orders>("AdventureWorks2014");
            var data = await db.QueryAsync(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                        FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                        INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID");

            return await data.ToListAsync();
        }

        public async Task<List<WorkOrder>> GetWorkOrdersWithProduct()
        {
            new MightyOrm();
            var db = new MightyOrm<WorkOrder>("AdventureWorks2014");
            return await GetWorkOrdersWithProduct(db);
        }

        private async Task<List<WorkOrder>> GetWorkOrdersWithProduct(MightyOrm<WorkOrder> db)
        {
            var data = await db.QueryAsync(@"SELECT TOP 500 WO.*, P.* 
                                                  FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                  INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID");

            return await data.ToListAsync();
        }

        //public Tuple<List<WorkOrder>,List<Product>> GetWorkOrdersAndProducts()
        //{
        //    var db = new MightyOrm("AdventureWorks2014");

        //    using (var db = new Database("AdventureWorks2014"))
        //    {
        //        return db.FetchMultiple<WorkOrder, Product>("SELECT TOP 500 * FROM[AdventureWorks2014].[Production].[WorkOrder];SELECT * FROM [Production].[Product];");
        //    }
        //}

        public async Task Add(WorkOrder workOrder)
        {
            var db = new MightyOrm<WorkOrder>("AdventureWorks2014");
            await db.InsertAsync(workOrder);
        }

        public async Task Update(WorkOrder workOrder)
        {
            var db = new MightyOrm<WorkOrder>("AdventureWorks2014");
            await db.UpdateAsync(workOrder);
        }
        public async Task Delete(WorkOrder workOrder)
        {
            var db = new MightyOrm<WorkOrder>("AdventureWorks2014");
            await db.DeleteAsync(workOrder);
        }
    }
}
