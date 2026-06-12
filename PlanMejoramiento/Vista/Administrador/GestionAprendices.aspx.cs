using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PlanMejoramiento.Logica;
using PlanMejoramiento.Modelo;

namespace PlanMejoramiento.Vista.Administrador
{
    public partial class GestionAprendices : System.Web.UI.Page
    {
        private readonly AprendizLN oAprendizLN = new AprendizLN();
        private readonly FichaL oFichaL = new FichaL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFichasCombo();
            }
        }

        private void CargarFichasCombo()
        {
            ddlFicha.DataSource = oFichaL.MtListarFicha();
            ddlFicha.DataValueField = "Id";
            ddlFicha.DataTextField = "NumeroFicha";
            ddlFicha.DataBind();
            ddlFicha.Items.Insert(0, new ListItem("-- Seleccione una Ficha --", ""));
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario oUsuario = new Usuario
                {
                    NumeroDocumento = txtDocumento.Text.Trim(),
                    Nombres = txtNombres.Text.Trim(),
                    Apellidos = txtApellidos.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Contrasena = txtDocumento.Text.Trim() 
                };

                Modelo.Aprendiz oAprendiz = new Modelo.Aprendiz
                {
                    IdFicha = Convert.ToInt32(ddlFicha.SelectedValue),
                    Estado = "Formación" 
                };
                bool exito = oAprendizLN.MtRegistrarAprendiz(oUsuario, oAprendiz);

                if (exito)
                {
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void LimpiarFormulario()
        {
            txtDocumento.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCorreo.Text = "";
            ddlFicha.SelectedIndex = 0;
        }
    }
}