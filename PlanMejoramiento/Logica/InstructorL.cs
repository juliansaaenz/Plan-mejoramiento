using System;
using System.Data;
using PlanMejoramiento.Modelo;
using PlanMejoramiento.Datos;

namespace PlanMejoramiento.Logica
{
    public class InstructorL
    {
        private InstructorD oInstructorD = new InstructorD();

        public bool MtRegistrarInstructor(Usuario oUsuario, Instructor oInstructor)
        {
            if (oUsuario == null || oInstructor == null)
                throw new ArgumentNullException("Los datos del instructor o usuario no pueden estar vacíos.");

            if (string.IsNullOrEmpty(oUsuario.NumeroDocumento))
                throw new ArgumentException("El número de documento es obligatorio.");

            if (string.IsNullOrEmpty(oUsuario.Nombres) || string.IsNullOrWhiteSpace(oUsuario.Apellidos))
                throw new ArgumentException("El nombre completo del instructor es obligatorio.");

            if (string.IsNullOrEmpty(oUsuario.Correo))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            if (string.IsNullOrEmpty(oUsuario.Contrasena))
                throw new ArgumentException("Debe asignar una contraseña para el acceso del instructor.");

            if (string.IsNullOrEmpty(oInstructor.Especialidad))
                throw new ArgumentException("La especialidad del instructor (ej: ADSO, Soldadura) es obligatoria.");

            oUsuario.IdRol = 2;

            return oInstructorD.MtRegistrarInstructor(oUsuario, oInstructor);
        }

        public DataTable MtListarInstructores()
        {
            return oInstructorD.MtListarInstructores();
        }

        public bool MtModificarInstructor(Usuario oUsuario, Instructor oInstructor)
        {
            if (oUsuario == null || oInstructor == null)
                throw new ArgumentNullException("Los datos para la modificación no pueden ser nulos.");

            if (oUsuario.Id <= 0)
                throw new ArgumentException("El ID del usuario no es válido para realizar la modificación.");

            if (oInstructor.Id <= 0)
                throw new ArgumentException("El ID del instructor no es válido para realizar la modificación.");

            if (string.IsNullOrEmpty(oUsuario.NumeroDocumento))
                throw new ArgumentException("El número de documento no puede quedar vacío.");

            if (string.IsNullOrEmpty(oUsuario.Nombres) || string.IsNullOrEmpty(oUsuario.Apellidos))
                throw new ArgumentException("El nombre y apellido no pueden quedar vacíos.");

            if (string.IsNullOrEmpty(oUsuario.Correo))
                throw new ArgumentException("El correo electrónico es obligatorio.");

            if (string.IsNullOrEmpty(oInstructor.Especialidad))
                throw new ArgumentException("La especialidad del instructor no puede quedar vacía.");

            return oInstructorD.MtModificarInstructor(oUsuario, oInstructor);
        }

        public bool MtAsignarInstructorFicha(int idFicha, int idInstructor)
        {
            if (idFicha <= 0)
                throw new ArgumentException("Debe seleccionar una ficha válida para realizar la asignación.");

            if (idInstructor <= 0)
                throw new ArgumentException("Debe seleccionar un instructor válido.");

            return oInstructorD.MtAsociarInstructorFicha(idFicha, idInstructor);
        }
    }
}