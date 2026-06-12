using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramiento.Vista
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null || Session["IdRol"] == null)
            {
                Response.Redirect("~/Vista/Login.aspx");
                return;
            }
            if (Session["NombreCompleto"] != null)
            {
                string nombre = Session["NombreCompleto"].ToString();
                lblNombreUsuario.Text = nombre;

                string[] palabras = nombre.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (palabras.Length > 1)
                {
                    lblInicialesMaster.Text = (palabras[0][0].ToString() + palabras[1][0].ToString()).ToUpper();
                }
                else if (palabras.Length == 1)
                {
                    lblInicialesMaster.Text = palabras[0][0].ToString().ToUpper();
                }
            }
            if (!IsPostBack)
            {
                string idRol = Session["IdRol"].ToString();

                if (idRol == "1") 
                {
                    pnlMenuAdministrador.Visible = true;
                    pnlMenuInstructor.Visible = false;
                    pnlMenuAprendiz.Visible = false;
                }
                else if (idRol == "2")
                {
                    pnlMenuAdministrador.Visible = false;
                    pnlMenuInstructor.Visible = true;
                    pnlMenuAprendiz.Visible = false;
                }
                else if (idRol == "3")
                {
                    pnlMenuAdministrador.Visible = false;
                    pnlMenuInstructor.Visible = false;
                    pnlMenuAprendiz.Visible = true;
                }
            }
        }
        protected void btnVerPerfil_Click(object sender, EventArgs e)
        {
            if (Session["NombreCompleto"] == null || Session["IdRol"] == null)
            {
                Response.Redirect("~/Vista/Login.aspx");
                return;
            }
            lblModalNombre.Text = Session["NombreCompleto"].ToString();
            lblModalIniciales.Text = lblInicialesMaster.Text;
            lblModalRol.Text = Session["Rol"].ToString();
            lblModalDocumento.Text = Session["Documento"] != null ? Session["Documento"].ToString() : "No registrado";

            string idRol = Session["IdRol"].ToString();

            if (idRol == "3")
            {
                lblModalPrograma.Text = Session["Programa"] != null ? Session["Programa"].ToString() : "No asignado";
                lblModalFicha.Text = Session["Ficha"] != null ? Session["Ficha"].ToString() : "No asignada";
            }
            else
            {
                lblModalPrograma.Text = "No Aplica (Personal Asistencial)";
                lblModalFicha.Text = "No Aplica";
            }
            pnlModalPerfil.Visible = true;
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            pnlModalPerfil.Visible = false;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}