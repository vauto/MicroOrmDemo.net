using ServiceStack.DataAnnotations;

namespace MicroOrmDemo.net.OrmLite
{
    //Db entity
    [Schema("Production")] //OrmLite set schema
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
    }
}
