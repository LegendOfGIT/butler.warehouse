using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

using Data.Warehouse;

namespace DataWarehouse
{
    [ServiceContract]
    [DataContract]
    public class DataWarehouse : DataWarehouseProvider
    {
        private DataWarehouseProvider WarehouseProvider = default(DataWarehouseProvider);

        [OperationContract]
        public IEnumerable<Dictionary<string, IEnumerable<object>>> DigInformation(string question)
        {
            this.WarehouseProvider = new MongoWarehouseProvider();
            return this.WarehouseProvider.DigInformation(question);
        }
        [OperationContract]
        public void StoreInformation(Dictionary<string, IEnumerable<object>> information)
        {
            //  Vorbereitung der einzuspeisenden Informationen
            information = information?.PrepareInformation();

            //this.WarehouseProvider = new FilesystemStorageProvider(@"C:\Temp\Github\ButlerFramework\InformationWarehouse\InformationWarehouse\App_Data\Warehouse");
            this.WarehouseProvider = new MongoWarehouseProvider();
            this.WarehouseProvider.StoreInformation(information);
        }
    }
}
