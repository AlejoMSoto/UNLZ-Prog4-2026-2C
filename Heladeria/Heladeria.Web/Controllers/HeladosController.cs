using Heladeria.Data.Contexto;
using Heladeria.Data.Modelos;
using Heladeria.Data.Repositorios;
using Heladeria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Heladeria.Web.Controllers
{
    public class HeladosController : Controller
    {
        private readonly IHeladoRepositorio _helados;

        public HeladosController(IHeladoRepositorio helados)
        {
            _helados = helados;
        }

        public async Task<IActionResult> Index()
        {

            List<HeladoVM> heladosVm = new List<HeladoVM>();
            var heladosDb = _helados.ObtenerHelados();

            heladosVm = heladosDb.Select(h => new HeladoVM { Id = h.Id, Sabor = h.Sabor, Tamanio = h.Tamanio, Precio = h.Precio }).ToList();
            ViewBag.Titulo = "Helados preparados";
            return View(heladosVm);
        }

        public ActionResult Details(int id)
        {
            var heladoDb = _helados.ObtenerHeladoPorId(id);

            if (heladoDb == null)
            {
                return NotFound();
            }

            var heladoVm = new HeladoVM
            {
                Id = heladoDb.Id,
                Sabor = heladoDb.Sabor,
                Tamanio = heladoDb.Tamanio,
                Precio = heladoDb.Precio
            };
            ViewBag.Mensaje = "Helado buscado";
            return View(heladoVm);
        }

        public IActionResult Create()
        {
            var pedido = new HeladoPrepararVM();
            CargarListasDesplegables(pedido);
            ViewBag.Mensaje = "Preparar Helado";
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HeladoPrepararVM heladoPedido)
        {
            if (ModelState.IsValid)
            {
               
                var nuevoHelado = new Helado
                {
                    Sabor = heladoPedido.Sabor,
                    Tamanio = heladoPedido.Tamanio,
                    Precio = heladoPedido.Precio
                };

                _helados.AgregarHelado(nuevoHelado);
                TempData["Mensaje"] = "Helado armado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mensaje = "Error, mal preparado";
            CargarListasDesplegables(heladoPedido);
            return View(heladoPedido);
        }

        private void CargarListasDesplegables(HeladoPrepararVM heladoPedido)
        {
            heladoPedido.ListaSabores = new List<SelectListItem>
            {
                new() { Value = "Chocolate", Text = "Chocolate" },
                new() { Value = "Dulce de leche", Text = "Dulce de leche" },
                new() { Value = "Frutilla", Text = "Frutilla" },
                new() { Value = "Menta Granizada", Text = "Menta Granizada" },
                new() { Value = "Limon", Text = "Limon" },
                new() { Value = "Banana Split", Text = "Banana Split" },
                new() { Value = "Americana", Text = "Americana" },
                new() { Value = "Vainilla", Text = "Vainilla" },
                new() { Value = "Granizado", Text = "Granizado" }
            };

            heladoPedido.ListaTamanios = new List<SelectListItem>
            {
                new() { Value = "Vasito", Text = "Vasito" },
                new() { Value = "Cucurucho", Text = "Cucurucho" },
                new() { Value = "1/4 KG", Text = "1/4 KG" },
                new() { Value = "1/2 KG", Text = "1/2 KG" },
                new() { Value = "1 KG", Text = "1 KG" }
            };
        }

        public async Task<IActionResult> Edit(int id)
        {
            var heladoDb = _helados.ObtenerHeladoPorId(id);
            if (heladoDb == null) return NotFound();

            var helprepVm = new HeladoPrepararVM
            {
                Id = heladoDb.Id,
                Sabor = heladoDb.Sabor,
                Tamanio = heladoDb.Tamanio,
                Precio = heladoDb.Precio
            };

            CargarListasDesplegables(helprepVm);
            ViewBag.Titulo = "Modificar helado";
            return View(helprepVm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HeladoPrepararVM hpVm)
        {
            if (id != hpVm.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                CargarListasDesplegables(hpVm);
                return View(hpVm);
            }

            var heladoDb = _helados.ObtenerHeladoPorId(id);
            if (heladoDb == null) return NotFound();

            heladoDb.Sabor = hpVm.Sabor;
            heladoDb.Tamanio = hpVm.Tamanio;
            heladoDb.Precio = hpVm.Precio;

            _helados.ActualizarHelado(heladoDb);
            TempData["MensajeExito"] = "Helado modificado exitosamente";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            bool seBorro = _helados.BorrarHeladoPorId(id);

            if (seBorro == false)
            {
                TempData["MensajeError"] = "no se borro el helado, ID invalido";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensajeExito"] = "El pedido se elimino correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
