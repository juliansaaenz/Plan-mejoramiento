using PlanMejoramiento.Logica;
using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;

namespace PlanMejoramiento.Vista.Administrador
{
    public partial class GestionProgramas : System.Web.UI.Page
    {
        private readonly ProgramaL oProgramaL = new ProgramaL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefrescarGrilla();
            }
        }

        private void RefrescarGrilla()
        {
            gvProgramas.DataSource = oProgramaL.MtListarProgramas();
            gvProgramas.DataBind();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
                Programa nuevoPrograma = new Programa
                {
                    CodigoPrograma = txtCodigo.Text.Trim(),
                    NombrePrograma = txtNombre.Text.Trim(),
                    Version = txtVersion.Text.Trim(),
                    NivelFormacion = ddlNivel.SelectedValue,
                    Duracion = Convert.ToInt32(txtDuracion.Text.Trim()),
                    Estado = "Activo"
                };

                bool exito = oProgramaL.MtCrearPrograma(nuevoPrograma);

                if (exito)
                {
                    LimpiarCampos();
                    RefrescarGrilla();
                    // Aquí puedes llamar a tu método de alertas/mensajes de éxito
                }
            
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtVersion.Text = "";
            txtDuracion.Text = "";
            ddlNivel.SelectedIndex = 0;
        }
    }
}