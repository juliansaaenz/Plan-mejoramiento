using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista.Instructor
{
    public partial class ConsultarAprendices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFichasDelInstructor();
                lblContador.Text = "Resultados: 0";
            }
        }

        private void CargarFichasDelInstructor()
        {
            DataTable dtFichas = new DataTable();
            dtFichas.Columns.Add("IdFicha");
            dtFichas.Columns.Add("NumeroFicha");
            dtFichas.Rows.Add("1", "2670142 - ADSO (Diurna)");
            dtFichas.Rows.Add("2", "2560891 - Redes (Nocturna)");

            ddlFichasAsignadas.DataSource = dtFichas;
            ddlFichasAsignadas.DataValueField = "IdFicha";
            ddlFichasAsignadas.DataTextField = "NumeroFicha";
            ddlFichasAsignadas.DataBind();

            ddlFichasAsignadas.Items.Insert(0, new ListItem("-- Seleccione una Ficha Asignada --", ""));

        }

        protected void ddlFichasAsignadas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlFichasAsignadas.SelectedValue))
            {
                gvAprendicesFicha.DataSource = null;
                gvAprendicesFicha.DataBind();
                lblContador.Text = "Resultados: 0";
                return;
            }

            CargarAprendicesPorFicha(Convert.ToInt32(ddlFichasAsignadas.SelectedValue));
        }

        private void CargarAprendicesPorFicha(int idFicha)
        {
            DataTable dtAprendices = new DataTable();
            dtAprendices.Columns.Add("IdAprendiz");
            dtAprendices.Columns.Add("NumeroDocumento");
            dtAprendices.Columns.Add("Nombres");
            dtAprendices.Columns.Add("Apellidos");
            dtAprendices.Columns.Add("Correo");
            dtAprendices.Columns.Add("Estado");

            if (idFicha == 1)
            {
                dtAprendices.Rows.Add("1", "10555444", "Andrés Felipe", "Torres Castro", "aftorres@soy.sena.edu.co", "En Formacion");
                dtAprendices.Rows.Add("2", "10666777", "Diana Marcela", "Rojas Pinzón", "dmrojas@soy.sena.edu.co", "Condicionado");
            }
            else if (idFicha == 2)
            {
                dtAprendices.Rows.Add("3", "10777888", "Kevin Alexander", "Gómez", "kagomez@soy.sena.edu.co", "En Formacion");
            }

            gvAprendicesFicha.DataSource = dtAprendices;
            gvAprendicesFicha.DataBind();

            lblContador.Text = "Resultados: " + dtAprendices.Rows.Count.ToString();
            pnlMensaje.Visible = false;
        }



        protected void gvAprendicesFicha_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Gestionar")
            {
                string idAprendiz = e.CommandArgument.ToString();

                Response.Redirect("HistorialPlanes.aspx?id=" + idAprendiz);
            }
        }
        protected string ObtenerEstiloEstado(string estado)
        {
            switch (estado.Trim())
            {
                case "En Formacion":
                    return "px-2.5 py-1 text-xs font-bold rounded-full bg-green-50 text-green-700 border border-green-200";
                case "Condicionado":
                    return "px-2.5 py-1 text-xs font-bold rounded-full bg-amber-50 text-amber-700 border border-amber-200";
                case "Aplazado":
                case "Retiro Voluntario":
                    return "px-2.5 py-1 text-xs font-bold rounded-full bg-blue-50 text-blue-700 border border-blue-200";
                case "Desertado":
                case "Cancelado":
                    return "px-2.5 py-1 text-xs font-bold rounded-full bg-red-50 text-red-700 border border-red-200";
                default:
                    return "px-2.5 py-1 text-xs font-bold rounded-full bg-gray-50 text-gray-700 border border-gray-200";
            }
        }

        private void MostrarAlerta(string mensaje, bool esExito)
        {
            lblTextoMensaje.Text = mensaje;
            pnlMensaje.Visible = true;

            if (esExito)
            {
                pnlMensaje.CssClass = "bg-emerald-50 border-l-4 border-emerald-500 text-emerald-700 p-4 rounded-r-xl text-sm flex items-start gap-3 shadow-sm";
                iconoMensaje.Attributes["class"] = "fas fa-check-circle text-emerald-600 text-base mt-0.5";
            }
            else
            {
                pnlMensaje.CssClass = "bg-red-50 border-l-4 border-red-500 text-red-700 p-4 rounded-r-xl text-sm flex items-start gap-3 shadow-sm";
                iconoMensaje.Attributes["class"] = "fas fa-exclamation-circle text-red-600 text-base mt-0.5";
            }
        }
    }
}