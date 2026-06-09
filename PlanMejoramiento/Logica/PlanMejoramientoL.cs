using System;
using System.Data;
using PlanMejoramiento.Datos;
using PlanMejoramiento.Modelo;

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

        public int MtCrearPlanMejoramiento(PlanesMejoramiento oPlanesMejoramiento)
        {
            if (oPlanesMejoramiento == null)
                throw new ArgumentNullException("Los datos del plan de mejoramiento no pueden estar vacíos.");

            if (oPlanesMejoramiento.IdAprendiz <= 0 || oPlanesMejoramiento.IdInstructor <= 0)
                throw new ArgumentException("Debe seleccionar un aprendiz y un instructor válidos.");

            if (string.IsNullOrWhiteSpace(oPlanesMejoramiento.TipoPlan))
                throw new ArgumentException("El tipo de plan (Interno / Comité) es obligatorio.");

            oPlanesMejoramiento.EstadoPlan = "En Proceso";

            return oPlanD.MtCrearPlanMejoramiento(oPlanesMejoramiento);
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
    }
}