using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccess;

namespace LismanService {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class LismanService : IUser, IPartida {
        
        public int AddCuenta(Cuenta cuenta)
        {
            using (var dataBase = new EntityModelContainer()) {
                var cuentaGuardar = new DataAccess.Cuenta
                {

                    fecha_registro = cuenta.fecha_registro,
                    Contrasenia = cuenta.Contrasenia,
                    Usuario = cuenta.Usuario,
                    Jugador = new DataAccess.Jugador
                    {
                        Nombre = cuenta.Jugador.Nombre,
                        Apellido = cuenta.Jugador.Apellido,
                        Email = cuenta.Jugador.Email,

                    },
                    Historial = new DataAccess.Historial
                    {
                        Historia_PuntajeMaximo = 0,
                        Multijugador_PuntajeMaximo = 0,
                        Mult_PartidasGanadas = 0,
                        Mult_PartidasJugadas = 0
                    }

                };

                
                dataBase.CuentaSet.Add(cuentaGuardar);
                try {
                   
                    return dataBase.SaveChanges();
                    
                }catch(DbEntityValidationException ex) {
                    foreach (var EntityValidationErrors in ex.EntityValidationErrors) {
                        foreach (var ValidationError in EntityValidationErrors.ValidationErrors) {
                            Console.WriteLine("Property: " + ValidationError.PropertyName + "Error: " + ValidationError.ErrorMessage);
                        }
                    }
                    return -1;
                }
            }
        }

        public List<Cuenta> GetCuentas()
        {
            throw new NotImplementedException();
        }

        public Cuenta IniciarSesion(string usuario, string contrasenia)
        {
            using (var dataBase = new EntityModelContainer()) {
                int existe = dataBase.CuentaSet.Where(u => u.Usuario == usuario & u.Contrasenia == contrasenia).Count();
                if (existe > 0) {
                    DataAccess.Cuenta cuenta = dataBase.CuentaSet.Where(u => u.Usuario == usuario & u.Contrasenia == contrasenia).FirstOrDefault();
                    Cuenta cuentaBack = new Cuenta();
                    cuentaBack.Id = cuenta.Id;
                    cuentaBack.Usuario = cuenta.Usuario;
                    cuentaBack.Contrasenia = cuenta.Contrasenia;
                    cuentaBack.fecha_registro = cuenta.fecha_registro;
                    cuentaBack.key_confirmation = cuenta.key_confirmation;
                    
                    cuentaBack.Jugador = new Jugador
                        {
                            Id = cuenta.Jugador.Id,
                            Nombre = cuenta.Jugador.Nombre,
                            Apellido = cuenta.Jugador.Apellido,
                            Email = cuenta.Jugador.Email
                            
                            
                        };
                        cuentaBack.Historial = new Historial
                        {
                            Id = cuenta.Historial.Id,
                            Historia_PuntajeMaximo = cuenta.Historial.Historia_PuntajeMaximo,
                            Multijugador_PuntajeMaximo = cuenta.Historial.Multijugador_PuntajeMaximo,
                            Mult_PartidasGanadas = cuenta.Historial.Mult_PartidasGanadas,
                            Mult_PartidasJugadas = cuenta.Historial.Mult_PartidasJugadas
                            
                        };

                    return cuentaBack;
                } else {
                    return null;
                }

            }
        }

        public int NewPartida()
        {
            throw new NotImplementedException();
        }
    }
}
