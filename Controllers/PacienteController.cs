using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parcial1_Programacion3_28_4.Data;
using Parcial1_Programacion3_28_4.Models;

namespace Parcial1_Programacion3_28_4.Controllers
{
    public class PacienteController : Controller
    {
        PacienteDatos _DB = new PacienteDatos();
        // GET: PacienteController
        public ActionResult ListaPaciente()
        {
            return View(_DB.ListarPacientes());
        }

        // GET: PacienteController/Create
        public ActionResult Create()
        {
            List<ObraSocial> lista1 = _DB.ListarObrasSociales(0);
            ViewBag.ObrasSociales = new SelectList(lista1, "Id", "NombreObraSocial");
            return View();
        }

        // POST: PacienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Paciente paciente1 = new Paciente
                {
                    NombrePaciente = collection["NombrePaciente"].ToString(),
                    ObraSocialId = int.Parse(collection["ObraSocialId"]),
                    Edad = int.Parse(collection["Edad"]),
                    Sintomas = collection["Sintomas"].ToString(),
                };
                ViewBag.Error = _DB.CrearPaciente(paciente1);
                if (ViewBag.Error != null)
                   return View("Create", paciente1);
                

                return RedirectToAction(nameof(ListaPaciente));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListarInscriptos()
        {
            List<CantidadInscriptosObraSocial> lista1 = _DB.ListaInscriptos();
            return View(lista1);
        }
    }
}
