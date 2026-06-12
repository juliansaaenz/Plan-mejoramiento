using PlanMejoramiento.Logica;
using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista.Administrador
{
    public partial class GestionFichas : System.Web.UI.Page
    {
        private readonly FichaL oFichaL = new FichaL();
        private readonly ProgramaL oProgramaL = new ProgramaL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProgramasCombo();
                RefrescarGrilla();
            }
        }

        private void CargarProgramasCombo()
        {
            ddlPrograma.DataSource = oProgramaL.MtListarProgramas();
            ddlPrograma.DataValueField = "Id";
            ddlPrograma.DataTextField = "NombrePrograma";
            ddlPrograma.DataBind();
            ddlPrograma.Items.Insert(0, new ListItem("-- Seleccione el Programa --", ""));
        }

        private void RefrescarGrilla()
        {
            gvFichas.DataSource = oFichaL.MtListarFicha();
            gvFichas.DataBind();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Ficha nuevaFicha = new Ficha
                {
                    NumeroFicha = txtNumeroFicha.Text.Trim(),
                    Jornada = ddlJornada.SelectedValue,
                    FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                    FechaFinalizacion = Convert.ToDateTime(txtFechaFin.Text),
                    IdPrograma = Convert.ToInt32(ddlPrograma.SelectedValue),
                    IdCentroFormacion = Convert.ToInt32(Session["IdCentroFormacion"] ?? 1), // O un control oculto/combo
                    Estado = "Activo"
                };

                bool exito = oFichaL.MtCrearPrograma(nuevaFicha); // Tu método de inserción en FichaL

                if (exito)
                {
                    txtNumeroFicha.Text = "";
                    ddlJornada.SelectedIndex = 0;
                    ddlPrograma.SelectedIndex = 0;
                    RefrescarGrilla();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}