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
        int AddCuenta(Cuenta cuenta);
        [OperationContract]
        List<Cuenta> GetCuentas();
        [OperationContract]
        Cuenta IniciarSesion(String usuario, String contrasenia);







        // TODO: Add your service operations here
    }
    public interface IPartida {
        int NewPartida();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "MessageService.ContractType".

   

    [DataContract]
    public partial class Jugador
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Apellido { get; set; }
        [DataMember]
        public string Email { get; set; }
        
    }

    public partial class Cuenta {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Contrasenia { get; set; }
        [DataMember]
        public string key_confirmation { get; set; }
        [DataMember]
        public string fecha_registro { get; set; }
        [DataMember]

        public virtual Jugador Jugador { get; set; }
        [DataMember]
        public virtual Historial Historial { get; set; }
    }

    public partial class Historial {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Nullable<int> Multijugador_PuntajeMaximo { get; set; }
        [DataMember]
        public Nullable<int> Historia_PuntajeMaximo { get; set; }
        [DataMember]
        public Nullable<int> Mult_PartidasJugadas { get; set; }
        [DataMember]
        public string Mult_PartidasGanadas { get; set; }
        
    }

    public partial class Partida {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public System.DateTime Fecha_creacion { get; set; }
        [DataMember]
        public virtual ICollection<Cuenta> Cuenta { get; set; }
        [DataMember]
        public virtual Chat Chat { get; set; }
    }

    public partial class Chat {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public System.DateTime Fecha_Creacion { get; set; }
        [DataMember]
        public virtual Partida Partida { get; set; }
        [DataMember]
        public virtual ICollection<Mensaje> Mensaje { get; set; }
    }

    public partial class Mensaje {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Texto { get; set; }
        [DataMember]
        public System.DateTime Fecha_creacion { get; set; }
        [DataMember]
        public string MAC { get; set; }
        [DataMember]
        public virtual Chat Chat { get; set; }
        [DataMember]
        public virtual Cuenta Cuenta { get; set; }
    }


}
