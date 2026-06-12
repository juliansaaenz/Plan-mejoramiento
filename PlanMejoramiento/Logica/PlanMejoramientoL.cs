using ExcelDataReader;
using PlanMejoramiento.Datos;
using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

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
        public DataTable MtConsultarEvidencias(int idPlan)
        {
            if (idPlan <= 0)
                throw new ArgumentException("Debe seleccionar un plan de mejoramiento válido para consultar sus evidencias.");

            return oPlanD.MtConsultarEvidenciasPorPlan(idPlan);
        }

        public bool MtRegistrarObservacionEvidencia(int idEvidencia, string observaciones)
        {
            if (idEvidencia <= 0)
                throw new ArgumentException("Debe seleccionar una evidencia válida para poder calificarla.");

            if (string.IsNullOrWhiteSpace(observaciones))
                throw new ArgumentException("La observación o retroalimentación del instructor no puede estar vacía.");

            return oPlanD.MtRegistrarObservacionInstructorEvidencia(idEvidencia, observaciones);
        }
        public bool MtProcesarCargaMasivaExcel(Stream flujoArchivo, out List<string> listaErrores)
        {
            listaErrores = new List<string>();
            DataTable dtExcel = new DataTable();

            using (var reader = ExcelReaderFactory.CreateReader(flujoArchivo))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                });
                dtExcel = result.Tables[0];
            }

            string[] columnasRequeridas = { "TipoDocumento", "NumeroDocumento", "Nombres", "Apellidos", "Correo", "Telefono", "IdCentroFormacion", "IdFicha", "IdCiudadResidencia" };
            foreach (string col in columnasRequeridas)
            {
                if (!dtExcel.Columns.Contains(col))
                {
                    listaErrores.Add($"Error de Estructura: La columna obligatoria '{col}' no se encuentra en el archivo Excel.");
                }
            }

            if (listaErrores.Count > 0) return false;

            int numeroFila = 2;

            List<string> documentosEnExcel = new List<string>();
            List<string> correosEnExcel = new List<string>();

            foreach (DataRow fila in dtExcel.Rows)
            {
                string tipoDoc = fila["TipoDocumento"].ToString().Trim();
                string numDoc = fila["NumeroDocumento"].ToString().Trim();
                string nombres = fila["Nombres"].ToString().Trim();
                string apellidos = fila["Apellidos"].ToString().Trim();
                string correo = fila["Correo"].ToString().Trim();

                if (string.IsNullOrEmpty(numDoc) || string.IsNullOrEmpty(nombres) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(correo))
                {
                    listaErrores.Add($"Fila {numeroFila}: Contiene campos obligatorios vacíos.");
                    numeroFila++;
                    continue;
                }

                if (documentosEnExcel.Contains(numDoc))
                    listaErrores.Add($"Fila {numeroFila}: El documento '{numDoc}' está duplicado dentro del mismo archivo Excel.");
                else
                    documentosEnExcel.Add(numDoc);

                if (correosEnExcel.Contains(correo))
                    listaErrores.Add($"Fila {numeroFila}: El correo '{correo}' está duplicado dentro del mismo archivo Excel.");
                else
                    correosEnExcel.Add(correo);

                if (oPlanD.MtExisteDocumento(numDoc))
                    listaErrores.Add($"Fila {numeroFila}: El número de documento '{numDoc}' ya está registrado en el sistema.");

                if (oPlanD.MtExisteCorreo(correo))
                    listaErrores.Add($"Fila {numeroFila}: El correo electrónico '{correo}' ya se encuentra asignado a otro usuario.");

                numeroFila++;
            }
            if (listaErrores.Count > 0)
            {
                return false;
            }

            return oPlanD.MtInsertarAprendicesMasivo(dtExcel);
        }
    }
}