using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

using Data.Warehouse;

namespace DataWarehouse
{
    [ServiceContract]
    [DataContract]
    public class DataWarehouse
    {
        private DataWarehouseProvider WarehouseProvider = default(DataWarehouseProvider);

        [OperationContract]
        public IEnumerable<Dictionary<string, IEnumerable<object>>> DigInformation(string question)
        {
            this.WarehouseProvider = new MongoWarehouseProvider();
            return this.WarehouseProvider.DigInformation(question);
        }
    }
}
