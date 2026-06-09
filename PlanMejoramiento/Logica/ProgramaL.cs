using PlanMejoramiento.Datos;
using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Logica
{
    public class ProgramaL
    {
        private ProgramaD oProgramaD = new ProgramaD();

        public bool MtCrearPrograma(Programa oPrograma)
        {
            if (oPrograma == null)
                throw new ArgumentNullException("Los datos del programa no pueden ser nulos.");

            if (string.IsNullOrEmpty(oPrograma.CodigoPrograma))
                throw new ArgumentException("El código del programa es obligatorio.");

            if (string.IsNullOrEmpty(oPrograma.NombrePrograma))
                throw new ArgumentException("El nombre del programa es obligatorio.");

            if (string.IsNullOrEmpty(oPrograma.Version))
                throw new ArgumentException("La versión del programa es obligatoria.");

            if (string.IsNullOrEmpty(oPrograma.NivelFormacion))
                throw new ArgumentException("El nivel de formación es obligatorio.");

            if (oPrograma.Duracion <= 0)
                throw new ArgumentException("La duración del programa debe ser en meses y mayor a cero.");

            if (string.IsNullOrEmpty(oPrograma.Estado))
                oPrograma.Estado = "Activo";

            return oProgramaD.MtCrearPrograma(oPrograma);

        }

        public List<Programa> MtListarProgramas()
        {

            return oProgramaD.MtListarProgramas();

        }

        public bool MtModificarPrograma(Programa oPrograma)
        {
            if (oPrograma == null)
                throw new ArgumentNullException("Los datos para la modificación no pueden ser nulos.");

            if (oPrograma.Id <= 0)
                throw new ArgumentException("El ID del programa no es válido");

            if (string.IsNullOrEmpty(oPrograma.CodigoPrograma) || string.IsNullOrEmpty(oPrograma.NombrePrograma))
                throw new ArgumentException("El código y el nombre del programa son campos obligatorios.");

            if (oPrograma.Duracion <= 0)
                throw new ArgumentException("La duración modificada debe ser superior a cero meses.");

            if (oPrograma.Estado != "Activo" && oPrograma.Estado != "Inactivo")
                throw new ArgumentException("El estado del programa modificado debe ser 'Activo' o 'Inactivo'.");

            return oProgramaD.MtModificarPrograma(oPrograma);


        }

        public bool MtEliminarPrograma(int idPrograma)
        {
            if (idPrograma <= 0)
                throw new ArgumentException("El ID del programa a eliminar no es válido.");


            return oProgramaD.MtEliminarPrograma(idPrograma);

        }

        public bool MtAsociarProgramaCentro(int idCentro, int idPrograma)
        {
            if (idCentro <= 0)
                throw new ArgumentException("El ID del centro de formación no es válido.");

            if (idPrograma <= 0)
                throw new ArgumentException("El ID del programa no es válido.");


            return oProgramaD.MtAsociarProgramaCentro(idCentro, idPrograma);

        }
    }
}