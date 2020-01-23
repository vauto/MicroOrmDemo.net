using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.OrmLite
{
    public abstract class OrmLiteRepositoryBase
    {
        protected OrmLiteConnectionFactory ConnectionFactory { get; }

        protected OrmLiteRepositoryBase()
        {
            ConnectionFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString(), SqlServer2014Dialect.Provider);
        }

        public abstract Task<List<Orders>> GetOrders();

        public abstract Task<Orders> GetOrderById(int id);

        public async Task Add(WorkOrder workOrder)
        {
            using (var dbConnection = ConnectionFactory.OpenDbConnection())
            {
                await dbConnection.InsertAsync(workOrder);
            }
        }
        public async Task Update(WorkOrder workOrder)
        {
            using (var dbConnection = ConnectionFactory.OpenDbConnection())
            {
                await dbConnection.UpdateAsync(workOrder);
            }
        }

        public async Task Delete(WorkOrder workOrder)
        {
            using (var dbConnection = ConnectionFactory.OpenDbConnection())
            {
                await dbConnection.DeleteAsync<WorkOrder>(p => p.WorkOrderId == workOrder.WorkOrderId);
                //dbConnection.DeleteById<WorkOrder>(workOrder.WorkOrderId);
            }
        }
    }
}
