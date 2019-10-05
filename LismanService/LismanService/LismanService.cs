using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LismanService {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class LismanService : IUser {
        public int AddAcceso(Acceso cuenta)
        {
            throw new NotImplementedException();
        }

        public Acceso GetAccesoById(string cuentaId)
        {
            throw new NotImplementedException();
        }

        public int getConexion()
        {
            Console.WriteLine("Si jalo");
            return 1;
        }

        public Acceso Login(string usuario, string contrasenia)
        {
            throw new NotImplementedException();
        }

        public int UpdateAcceso(Acceso cuenta)
        {
            throw new NotImplementedException();
        }
    }
}
