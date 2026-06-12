using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Tus espacios de nombres del proyecto
using PlanMejoramiento.Logica;
using PlanMejoramiento.Modelo;

namespace PlanMejoramiento.Vista.Administrador
{
    public partial class CargaMasiva : System.Web.UI.Page
    {
        private readonly FichaL oFichaL = new FichaL();
        private readonly PlanMejoramientoLN oPlanMejoramientoLN = new PlanMejoramientoLN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFichas();
            }
        }

        private void CargarFichas()
        {
            List<Ficha> listaFichas = oFichaL.MtListarFicha();

            ddlFichaDestino.DataSource = listaFichas;
            ddlFichaDestino.DataValueField = "Id";
            ddlFichaDestino.DataTextField = "NumeroFicha";
            ddlFichaDestino.DataBind();

            ddlFichaDestino.Items.Insert(0, new ListItem("-- Seleccione la Ficha Destino --", ""));
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlFichaDestino.SelectedValue))
            {
                MostrarAlerta("Por favor, seleccione la ficha a la cual desea matricular los aprendices.", false);
                return;
            }

            if (!fuListado.HasFile)
            {
                MostrarAlerta("Por favor, seleccione un archivo CSV o Excel válido para continuar.", false);
                return;
            }
            string extension = Path.GetExtension(fuListado.FileName).ToLower();
            if (extension != ".xlsx" && extension != ".xls")
            {
                MostrarAlerta("Extensión de archivo no permitida. Por favor suba un archivo de Excel (.xlsx o .xls).", false);
                return;
            }
            List<string> erroresDeCarga;
            bool procesadoExitoso = oPlanMejoramientoLN.MtProcesarCargaMasivaExcel(fuListado.FileContent, out erroresDeCarga);

            if (procesadoExitoso)
            {
                MostrarAlerta("¡Carga Masiva Exitosa! Todos los aprendices se han registrado correctamente en la base de datos.", true);
            }
            else
            {
                string resumenErrores = "No se pudo procesar el archivo. Errores encontrados:<br/>" + string.Join("<br/>", erroresDeCarga);
                MostrarAlerta(resumenErrores, false);
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