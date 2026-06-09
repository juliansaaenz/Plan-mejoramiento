using PlanMejoramiento.Datos;
using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Logica
{
    public class FichaL
    {
        private FichaD oFichaD = new FichaD();
        public bool MtCrearPrograma(Ficha oFicha)
        {
            if (oFicha == null)
                throw new ArgumentNullException("Los datos del programa no pueden ser nulos.");

            if (string.IsNullOrEmpty(oFicha.NumeroFicha))
                throw new ArgumentException("El numero de la ficha es obligatorio.");

            if (string.IsNullOrEmpty(oFicha.Jornada))
                throw new ArgumentException("La joranda de la ficha es obligatorio.");

            if (oFicha.FechaInicio == DateTime.MinValue)
                throw new ArgumentException("La fecha de inicio ingresada no es válida.");

            if (oFicha.FechaFinalizacion == DateTime.MinValue)
                throw new ArgumentException("La fecha de finalización ingresada no es válida.");

            if (oFicha.FechaFinalizacion <= oFicha.FechaInicio)
                throw new ArgumentException("La fecha de finalización debe ser posterior a la fecha de inicio.");
            if (string.IsNullOrEmpty(oFicha.Estado))
                oFicha.Estado = "Activo";
            if (oFicha.IdPrograma <= 0)
                throw new ArgumentException("La ficha necesita asociarse a un programa");
            if (oFicha.IdCentroFormacion <= 0)
                throw new ArgumentException("La ficha necesita estar asociada a un centro de formacion");

            return oFichaD.MtCrearFicha(oFicha);

        }
        public List<Ficha> MtListarFicha ()
        {
            return oFichaD.MtListarFicha();
        }
        public bool MtActualizarFicha(Ficha oFicha)
        {
            if (oFicha == null)
                throw new ArgumentNullException(nameof(oFicha), "Los datos no pueden ser nulos.");

            if (oFicha.Id <= 0)
                throw new ArgumentException("El ID de la ficha no es válido para modificar.");

            if (oFicha.FechaFinalizacion <= oFicha.FechaInicio)
                throw new ArgumentException("La fecha de finalización debe ser posterior a la fecha de inicio.");
            if (oFicha.Estado != "Activo" && oFicha.Estado != "Inactivo")
                throw new ArgumentException("El estado de la ficha modificado debe ser 'Activo' o 'Inactivo'.");

            return oFichaD.MtModificarFicha(oFicha);
        }
        public bool MtEliminarFicha(int idFicha)
        {
            if (idFicha <= 0)
                throw new ArgumentException("El ID del programa a eliminar no es válido.");


            return oFichaD.MtEliminarFicha(idFicha);
        }
    }
}