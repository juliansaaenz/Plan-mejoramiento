using PlanMejoramiento.Datos;
using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data;

namespace PlanMejoramiento.Logica
{
    public class PlanMejoramientoLN
    {
        private PlanMejoramientoD oPlanD = new PlanMejoramientoD();

        public DataTable MtConsultarAprendices(int idInstructor)
        {
            if (idInstructor <= 0)
                throw new ArgumentException("El identificador del instructor no es válido.");

            return oPlanD.MtConsultarAprendicesPorInstructor(idInstructor);
        }

        public DataTable MtConsultarResultadosPendientes(int idAprendiz)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El identificador del aprendiz no es válido.");

            return oPlanD.MtConsultarResultadosPendientes(idAprendiz);
        }

        public bool MtCrearPlanMejoramientoCompleto(PlanesMejoramiento oPlanesMejoramiento, List<string> listaActividades, List<int> listaIdResultados)
        {
            if (oPlanesMejoramiento == null)
                throw new ArgumentNullException("Los datos del plan de mejoramiento no pueden estar vacíos.");

            if (oPlanesMejoramiento.IdAprendiz <= 0)
                throw new ArgumentException("Debe seleccionar un aprendiz válido para asignarle el plan.");

            if (oPlanesMejoramiento.IdInstructor <= 0)
                throw new ArgumentException("El identificador del instructor no es válido.");

            if (string.IsNullOrEmpty(oPlanesMejoramiento.TipoPlan))
                throw new ArgumentException("El tipo de plan (Interno o Comité) es obligatorio.");

            if (oPlanesMejoramiento.FechaLimite <= DateTime.Now)
                throw new ArgumentException("La fecha límite establecida debe ser posterior a la fecha y hora actual.");

            if (string.IsNullOrEmpty(oPlanesMejoramiento.EstadoPlan))
            {
                oPlanesMejoramiento.EstadoPlan = "Asignado";
            }

            if (listaActividades == null || listaActividades.Count == 0)
                throw new ArgumentException("Debe registrar al menos una actividad pedagógica para poder crear el plan.");

            foreach (string actividad in listaActividades)
            {
                if (string.IsNullOrEmpty(actividad))
                    throw new ArgumentException("Las descripciones de las actividades no pueden estar vacías o contener solo espacios.");
            }

            if (listaIdResultados == null || listaIdResultados.Count == 0)
                throw new ArgumentException("Debe asociar obligatoriamente al menos un resultado de aprendizaje (RAP) al plan.");

            foreach (int idResultado in listaIdResultados)
            {
                if (idResultado <= 0)
                    throw new ArgumentException("Uno o más identificadores de los resultados seleccionados no son válidos.");
            }

            return oPlanD.MtCrearPlanMejoramientoCompleto(oPlanesMejoramiento, listaActividades, listaIdResultados);
        }

        public bool MtRegistrarGestionInstructor(int idPlan, string resultadoIncumplido, string actividadesARealizar, DateTime fechaLimite, string observacionesInstructor)
        {
            if (idPlan <= 0)
                throw new ArgumentException("El código del plan de mejoramiento no es válido para actualizar.");

            if (string.IsNullOrEmpty(resultadoIncumplido))
                throw new ArgumentException("Debe asociar obligatoriamente el resultado de aprendizaje incumplido.");

            if (string.IsNullOrEmpty(actividadesARealizar))
                throw new ArgumentException("Es obligatorio registrar las actividades pedagógicas a realizar.");

            if (fechaLimite <= DateTime.Now)
                throw new ArgumentException("La fecha límite establecida debe ser una fecha posterior al día de hoy.");

            if (string.IsNullOrEmpty(observacionesInstructor))
                observacionesInstructor = "Sin observaciones adicionales.";
            string textoEstructurado = $"[RESULTADO]: {resultadoIncumplido} | " +
                                       $"[ACTIVIDADES]: {actividadesARealizar} | " +
                                       $"[NOTAS]: {observacionesInstructor}";

            return oPlanD.MtAsignarDetallesInstructor(idPlan, fechaLimite, textoEstructurado);
        }
        public bool MtAprendizSubirEvidencia(string rutaArchivo, string tipoArchivo, int idActividad)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
                throw new ArgumentException("Debe seleccionar o cargar un archivo válido para la evidencia.");

            if (string.IsNullOrEmpty(tipoArchivo))
                throw new ArgumentException("El tipo de archivo no puede estar vacío.");

            if (idActividad <= 0)
                throw new ArgumentException("Debe seleccionar la actividad pedagógica a la cual le va a subir la evidencia.");

            string extensionFormateada = tipoArchivo.ToUpper().Replace(".", "");

            return oPlanD.MtAprendizInsertarEvidencia(rutaArchivo, extensionFormateada, idActividad);
        }
        public string MtAprendizConsultarEstadoAcademico(int idAprendiz)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El identificador del aprendiz no es válido.");

            return oPlanD.MtAprendizConsultarEstadoAcademico(idAprendiz);
        }
        public DataTable MtAprendizConsultarObservaciones(int idPlan)
        {
            if (idPlan <= 0)
                throw new ArgumentException("Debe seleccionar un plan de mejoramiento válido para ver sus observaciones.");

            return oPlanD.MtAprendizConsultarObservacionesPlan(idPlan);
        }
        public DataTable MtAprendizConsultarPlanesAsignados(int idAprendiz)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El identificador del aprendiz no es válido para consultar los planes.");

            return oPlanD.MtAprendizConsultarPlanesAsignados(idAprendiz);
        }
        public DataTable MtAprendizConsultarResultadosPendientes(int idAprendiz)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El identificador del aprendiz no es válido para consultar sus resultados.");

            return oPlanD.MtConsultarResultadosPendientes(idAprendiz);
        }
        public DataTable MtAprendizConsultarFicha(int idAprendiz)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El identificador del aprendiz no es válido para consultar la ficha.");

            return oPlanD.MtConsultarFichaAsignada(idAprendiz);
        }
        public DataTable MtAprendizConsultarDatos(int idAprendiz)
        {
            if (idAprendiz <= 0)
                throw new ArgumentException("El identificador del aprendiz no es válido para consultar sus datos.");

            return oPlanD.MtConsultarDatosAprendiz(idAprendiz);
        }
    }
}