using System;
using System.Data;
using PlanMejoramiento.Modelo;
using PlanMejoramiento.Datos;

namespace PlanMejoramiento.Logica
{
    public class AprendizLN
    {
        private AprendizD oAprendizD = new AprendizD();

        public bool MtRegistrarAprendiz(Usuario oUsuario, Aprendiz oAprendiz)
        {
            if (oUsuario == null || oAprendiz == null)
                throw new ArgumentNullException("Los datos del aprendiz no pueden estar completamente vacíos.");

            if (string.IsNullOrEmpty(oUsuario.NumeroDocumento))
                throw new ArgumentException("El número de documento del aprendiz es obligatorio.");

            if (string.IsNullOrEmpty(oUsuario.Nombres) || string.IsNullOrEmpty(oUsuario.Apellidos))
                throw new ArgumentException("El nombre y apellido del aprendiz son obligatorios.");

            if (string.IsNullOrEmpty(oUsuario.Correo))
                throw new ArgumentException("El correo electrónico es un campo obligatorio.");

            if (string.IsNullOrEmpty(oUsuario.Contrasena))
                oUsuario.Contrasena = oUsuario.NumeroDocumento;

            if (oAprendiz.IdFicha <= 0)
                throw new ArgumentException("Debe seleccionar una ficha de formación válida para el aprendiz.");

            if (string.IsNullOrEmpty(oAprendiz.Estado))
                oAprendiz.Estado = "Formación";

            oUsuario.IdRol = 3;

            return oAprendizD.MtRegistrarAprendiz(oUsuario, oAprendiz);
        }

        public bool MtModificarAprendiz(Usuario oUsuario, Aprendiz oAprendiz)
        {
            if (oUsuario == null || oAprendiz == null)
                throw new ArgumentNullException("Los datos para la modificación no pueden ser nulos.");

            if (oUsuario.Id <= 0 || oAprendiz.Id <= 0)
                throw new ArgumentException("Los identificadores del aprendiz no son válidos para efectuar la modificación.");

            if (string.IsNullOrEmpty(oUsuario.NumeroDocumento) || string.IsNullOrEmpty(oUsuario.Nombres))
                throw new ArgumentException("El número de documento y los nombres no pueden quedar vacíos.");

            if (oAprendiz.IdFicha <= 0)
                throw new ArgumentException("Debe asignar una ficha válida al aprendiz.");

            return oAprendizD.MtModificarAprendiz(oUsuario, oAprendiz);
        }

        public bool MtEliminarAprendiz(int idAprendiz)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El ID del aprendiz no es válido para procesar la baja.");

            return oAprendizD.MtEliminarAprendiz(idAprendiz);
        }

        public bool MtCambiarEstadoAcademico(int idAprendiz, string nuevoEstado)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El ID del aprendiz no es válido.");

            if (string.IsNullOrEmpty(nuevoEstado))
                throw new ArgumentException("El nuevo estado académico no puede estar en blanco.");

            if (nuevoEstado != "Formación" && nuevoEstado != "Condicionado" && nuevoEstado != "Cancelado" && nuevoEstado != "Etapa Productiva" && nuevoEstado != "Inactivo")
                throw new ArgumentException("El estado académico no pertenece a las opciones válidas del sistema.");

            return oAprendizD.MtCambiarEstadoAcademico(idAprendiz, nuevoEstado);
        }

        public bool MtAsociarFicha(int idAprendiz, int idNuevaFicha)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El ID del aprendiz no es válido.");

            if (idNuevaFicha <= 0)
                throw new ArgumentException("Debe seleccionar una ficha de destino válida para realizar el traslado.");

            return oAprendizD.MtAsociarFicha(idAprendiz, idNuevaFicha);
        }
    }
}