using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista.Aprendiz
{
    public partial class MisPlanes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMisPlanesDeMejoramiento();
            }
        }

        private void CargarMisPlanesDeMejoramiento()
        {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPlan");
                dt.Columns.Add("Competencia");
                dt.Columns.Add("FechaLimite", typeof(DateTime));
                dt.Columns.Add("Estado");
                dt.Columns.Add("Instructor");
                dt.Rows.Add("101", "Desarrollo de Software según arquitectura", DateTime.Now.AddDays(4), "Asignado", "Carlos Mario Restrepo");
                dt.Rows.Add("95", "Implantación de solución de software", DateTime.Now.AddDays(-10), "Aprobado", "Carlos Mario Restrepo");

                gvMisPlanes.DataSource = dt;
                gvMisPlanes.DataBind();
                int pendientes = 0, entregados = 0, aprobados = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string estado = row["Estado"].ToString();
                    if (estado == "Asignado") pendientes++;
                    else if (estado == "Entregado") entregados++;
                    else if (estado == "Aprobado") aprobados++;
                }

                lblPendientes.Text = pendientes.ToString();
                lblEntregados.Text = entregados.ToString();
                lblAprobados.Text = aprobados.ToString();
        }

        protected void gvMisPlanes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Responder")
            {
                string idPlan = e.CommandArgument.ToString();
                Response.Redirect("SubirEvidencias.aspx?plan=" + idPlan);
            }
        }
        protected string ObtenerEstiloEstado(string estado)
        {
            switch (estado.Trim())
            {
                case "Asignado":
                    return "px-2.5 py-1 text-xs font-black rounded-xl bg-blue-50 text-blue-600 border border-blue-100";
                case "Entregado":
                    return "px-2.5 py-1 text-xs font-black rounded-xl bg-amber-50 text-amber-600 border border-amber-100";
                case "Aprobado":
                    return "px-2.5 py-1 text-xs font-black rounded-xl bg-emerald-50 text-emerald-600 border border-emerald-100";
                case "No Aprobado":
                    return "px-2.5 py-1 text-xs font-black rounded-xl bg-red-50 text-red-600 border border-red-100";
                default:
                    return "px-2.5 py-1 text-xs font-black rounded-xl bg-gray-50 text-gray-500 border border-gray-100";
            }
        }
        protected string ObtenerEstiloBoton(string estado)
        {
            if (estado == "Aprobado")
            {
                return "inline-flex items-center justify-center py-1.5 px-3 rounded-lg bg-gray-100 text-gray-400 font-bold text-xs gap-1 cursor-not-allowed pointer-events-none";
            }
            return "inline-flex items-center justify-center py-1.5 px-3 rounded-lg bg-orange-600 hover:bg-orange-700 text-white font-bold transition shadow-sm text-xs gap-1 cursor-pointer";
        }
    }
}