using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LismanService {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUser {
        [OperationContract]
        int AddAcceso(Acceso cuenta);
        [OperationContract]
        int UpdateAcceso(Acceso cuenta);
        [OperationContract]
        Acceso Login(String usuario, String contrasenia);

        [OperationContract]
        Acceso GetAccesoById(String cuentaId);

        [OperationContract]
        int getConexion();
       



        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "MessageService.ContractType".
    [DataContract]
    //Es como marcado para serializar, como va a salir de la red, debe tenr una forma especifica, 


    public class Acceso {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Constrasenia { get; set; }
        [DataMember]
        public string Tipo_acceso { get; set; }
        [DataMember]
        public string key_confirmation { get; set; }
        [DataMember]
        public string Email { get; set; }
       

    }
}
