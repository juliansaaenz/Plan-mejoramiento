using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista.Aprendiz
{
    public partial class SubirEvidencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["plan"] != null)
                {
                    string idPlan = Request.QueryString["plan"];
                    CargarDetalleDelPlan(idPlan);
                }
                else
                {
                    Response.Redirect("MisPlanes.aspx");
                }
            }
        }

        private void CargarDetalleDelPlan(string idPlan)
        {

            lblCodPlan.Text = "PLAN-" + idPlan;

            if (idPlan == "101")
            {
                lblInstructor.Text = "Carlos Mario Restrepo";
                lblFechaLimite.Text = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy");

                lblActividadesDesc.Text = "1. Desarrollar el diagrama de clases correspondiente al caso de estudio de la veterinaria.\n" +
                                         "2. Implementar la arquitectura n-tier en C# Web Forms dividiendo los proyectos en Datos, Lógica y Presentación.\n" +
                                         "3. Adjuntar script SQL Server con el esquema corregido.";
            }
            else
            {
                lblInstructor.Text = "Instructores del Centro de Formación";
                lblFechaLimite.Text = DateTime.Now.ToString("dd/MM/yyyy");

                lblActividadesDesc.Text = "No se encontraron especificaciones detalladas para este plan.";
            }

        }
        protected void btnEnviarEntrega_Click(object sender, EventArgs e)
        {
            if (!fuEvidencia.HasFile)
            {
                MostrarAlerta("Por favor, seleccione el archivo con sus evidencias antes de enviar.", false);
                return;
            }

            string extension = Path.GetExtension(fuEvidencia.FileName).ToLower();
            if (extension != ".pdf" && extension != ".zip" && extension != ".rar")
            {
                MostrarAlerta("Formato no válido. Solo se permite la entrega en archivos .pdf, .zip o .rar", false);
                return;
            }
            MostrarAlerta("¡Evidencias enviadas con éxito! El estado de tu plan cambió a 'Entregado'. Tu instructor ha sido notificado.", true);

            fuEvidencia.Enabled = false;
            txtComentariosAprendiz.Enabled = false;
            btnEnviarEntrega.Enabled = false;

        }

        private void MostrarAlerta(string mensaje, bool esExito)
        {
            lblTextoMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
            if (esExito)
            {
                pnlMensaje.CssClass = "bg-emerald-50 border-l-4 border-emerald-500 text-emerald-700 p-4 rounded-r-xl text-sm flex items-start gap-3 shadow-sm mb-4";
                iconoMensaje.Attributes["class"] = "fas fa-check-circle text-emerald-600 text-base mt-0.5";
            }
            else
            {
                pnlMensaje.CssClass = "bg-red-50 border-l-4 border-red-500 text-red-700 p-4 rounded-r-xl text-sm flex items-start gap-3 shadow-sm mb-4";
                iconoMensaje.Attributes["class"] = "fas fa-exclamation-circle text-red-600 text-base mt-0.5";
            }
        }
    }
}