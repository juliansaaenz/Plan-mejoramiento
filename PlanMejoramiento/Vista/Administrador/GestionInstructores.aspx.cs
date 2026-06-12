using PlanMejoramiento.Logica;
using PlanMejoramiento.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PlanMejoramiento.Vista.Administrador
{
    public partial class GestionInstructores : System.Web.UI.Page
    {
        private readonly InstructorL oInstructorL = new InstructorL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefrescarGrilla();
            }
        }

        private void RefrescarGrilla()
        {
            // MtListarInstructores() retorna un DataTable directo de InstructorD
            gvInstructores.DataSource = oInstructorL.MtListarInstructores();
            gvInstructores.DataBind();
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
                    Contrasena = txtContrasena.Text.Trim() // Obligatoria en tus validaciones de InstructorL
                };

                Modelo.Instructor oInstructor = new Modelo.Instructor
                {
                    Especialidad = txtEspecialidad.Text.Trim() // Ej: ADSO, Redes, etc.
                };

                bool exito = oInstructorL.MtRegistrarInstructor(oUsuario, oInstructor);

                if (exito)
                {
                    LimpiarFormulario();
                    RefrescarGrilla();
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
            txtContrasena.Text = "";
            txtEspecialidad.Text = "";
        }
    }
}