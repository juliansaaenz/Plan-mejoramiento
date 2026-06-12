using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista.Instructor
{
    public partial class HistorialPlanes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    string idAprendiz = Request.QueryString["id"];
                    CargarDatosAprendiz(idAprendiz);
                    ListarHistorialPlanes(idAprendiz);
                }
                else
                {
                    Response.Redirect("ConsultarAprendices.aspx");
                }
            }
        }

        private void CargarDatosAprendiz(string id)
        {
            if (id == "1")
            {
                lblNombreCompleto.Text = "Andrés Felipe Torres Castro";
                lblDocumento.Text = "10555444";
                lblPrograma.Text = "Análisis y Desarrollo de Software (ADSO)";
                lblFicha.Text = "2670142";
                lblIniciales.Text = "AT";
            }
            else if (id == "2")
            {
                lblNombreCompleto.Text = "Diana Marcela Rojas Pinzón";
                lblDocumento.Text = "10666777";
                lblPrograma.Text = "Análisis y Desarrollo de Software (ADSO)";
                lblFicha.Text = "2670142";
                lblIniciales.Text = "DR";
            }

        }

        private void ListarHistorialPlanes(string idAprendiz)
        {
            DataTable dtPlanes = new DataTable();
            dtPlanes.Columns.Add("IdPlan");
            dtPlanes.Columns.Add("FechaAsignacion", typeof(DateTime));
            dtPlanes.Columns.Add("TipoPlan");
            dtPlanes.Columns.Add("EstadoPlan");
            dtPlanes.Columns.Add("Instructor");

            if (idAprendiz == "1")
            {
                dtPlanes.Rows.Add("101", DateTime.Now.AddMonths(-2), "Interno", "Aprobado", "Carlos Mario Restrepo");
                dtPlanes.Rows.Add("205", DateTime.Now.AddDays(-5), "Interno", "Asignado", "Carlos Mario Restrepo");
            }
            else if (idAprendiz == "2")
            {
                dtPlanes.Rows.Add("88", DateTime.Now.AddMonths(-1), "Comite", "No Aprobado", "Angela María Patiño");
            }

            gvHistorial.DataSource = dtPlanes;
            gvHistorial.DataBind();

        }

        protected void btnNuevoPlan_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            Response.Redirect("FormularioPlan.aspx?id=" + id);
        }

        protected void gvHistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalle")
            {
                string idPlan = e.CommandArgument.ToString();
                Response.Redirect("FormularioPlan.aspx?plan=" + idPlan);
            }
        }

        protected string ObtenerEstiloEstadoPlan(string estado)
        {
            switch (estado.Trim())
            {
                case "Asignado":
                    return "px-2 py-1 text-[10px] font-black rounded-lg bg-blue-50 text-blue-600 border border-blue-100";
                case "Aprobado":
                    return "px-2 py-1 text-[10px] font-black rounded-lg bg-emerald-50 text-emerald-600 border border-emerald-100";
                case "No Aprobado":
                    return "px-2 py-1 text-[10px] font-black rounded-lg bg-red-50 text-red-600 border border-red-100";
                case "Entregado":
                    return "px-2 py-1 text-[10px] font-black rounded-lg bg-amber-50 text-amber-600 border border-amber-100";
                default:
                    return "px-2 py-1 text-[10px] font-black rounded-lg bg-gray-50 text-gray-500 border border-gray-100";
            }
        }
    }
}