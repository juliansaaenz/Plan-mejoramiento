using PlanMejoramiento.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string rolParam = Request.QueryString["rol"];

                
                if (rolParam == "1")
                    lblRolSeleccionado.Text = "Módulo: Administrador";
                else if (rolParam == "2")
                    lblRolSeleccionado.Text = "Módulo: Instructor";
                else if (rolParam == "3")
                    lblRolSeleccionado.Text = "Módulo: Aprendiz";
                else
                    lblRolSeleccionado.Text = "Acceso General";
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {

            pnlError.Visible = false;

            UsuarioL oUsuarioL = new UsuarioL();

            DataTable dtUsuario = oUsuarioL.MtIniciarSesion(txtCorreo.Text, txtContrasena.Text);

            DataRow usuario = dtUsuario.Rows[0];

            Session["IdUsuario"] = usuario["Id"].ToString();
            Session["NombreCompleto"] = usuario["Nombres"].ToString() + " " + usuario["Apellidos"].ToString();
            Session["IdRol"] = usuario["IdRol"].ToString();
            Session["Rol"] = usuario["NombreRol"].ToString();

            int idRol = Convert.ToInt32(usuario["IdRol"]);
            if (idRol == 1)
            {
                Response.Redirect("~/Vista/Administrador/GestionProgramas.aspx");
            }

            if (idRol == 2)
            {
                Response.Redirect("~/Vista/Instructor/ConsultarAprendices.aspx");
            }
            else if (idRol == 3)
            {
                Response.Redirect("~/Vista/Aprendiz/MisPlanes.aspx");
            }
            else
            {
                Response.Redirect("~/Vista/Index.aspx");
            }

        }
    }
}