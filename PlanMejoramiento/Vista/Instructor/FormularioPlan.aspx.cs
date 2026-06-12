using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista.Instructor
{
    public partial class FormularioPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFechaAsignacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaLimite.Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

                if (Request.QueryString["id"] != null)
                {
                    CargarInfoAprendiz(Request.QueryString["id"]);
                    CargarCompetencias();
                }
                else if (Request.QueryString["plan"] != null)
                {
                    CargarPlanExistente(Request.QueryString["plan"]);
                }
                else
                {
                    Response.Redirect("ConsultarAprendices.aspx");
                }
            }
        }

        private void CargarInfoAprendiz(string id)
        {
            lblAprendiz.Text = (id == "1") ? "Andrés Felipe Torres Castro" : "Diana Marcela Rojas Pinzón";
        }

        private void CargarCompetencias()
        {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Nombre");
                dt.Rows.Add("1", "Desarrollo de Software según arquitectura");
                dt.Rows.Add("2", "Implantación de solución de software");

                ddlCompetencia.DataSource = dt;
                ddlCompetencia.DataValueField = "Id";
                ddlCompetencia.DataTextField = "Nombre";
                ddlCompetencia.DataBind();
                ddlCompetencia.Items.Insert(0, new ListItem("-- Seleccione una Competencia --", ""));
        }

        protected void ddlCompetencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlCompetencia.SelectedValue)) return;
            cblResultados.Items.Clear();
            if (ddlCompetencia.SelectedValue == "1")
            {
                cblResultados.Items.Add(new ListItem(" RAP-01: Codificar componentes de interfaz", "101"));
                cblResultados.Items.Add(new ListItem(" RAP-02: Construir capa de datos", "102"));
            }
            else
            {
                cblResultados.Items.Add(new ListItem(" RAP-03: Diseñar arquitectura tecnológica", "103"));
            }
        }

        protected void btnGuardarPlan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtActividades.Text) || cblResultados.SelectedIndex == -1)
            {
                MostrarAlerta("Debe seleccionar al menos un RAP y describir las actividades.", false);
                return;
            }


            MostrarAlerta("¡El Plan de Mejoramiento se ha generado y asignado correctamente!", true);
            btnGuardarPlan.Enabled = false;

        }

        private void CargarPlanExistente(string idPlan)
        {
            lblTituloPagina.Text = "Evaluación de Plan #" + idPlan;
            lblEstadoPlan.Text = "Entregado";
            btnGuardarPlan.Text = "Finalizar Evaluación";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            Response.Redirect("HistorialPlanes.aspx?id=" + id);
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