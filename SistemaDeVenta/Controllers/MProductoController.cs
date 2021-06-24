using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Data;
using SistemaVentas.Libreria;
using SistemaVentas.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace SistemaVentas.Controllers
{
    public class MProductoController : Controller
    {
        //instanciando la clase ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        //instanciando la clase _uploadimage
        private UploadImage _uploadimage;


        public MProductoController(ApplicationDbContext context, IWebHostEnvironment environment)
        {


            _uploadimage = new UploadImage();
            _context = context;
            _environment = environment;
        }

        // GET: Producto




        public async Task<IActionResult> Carrito()

        {

            return View(await _context.carrito.ToListAsync());
        }

        public async Task<IActionResult> Productos()

        {

            return View(await _context.producto.ToListAsync());
        }


        public async Task<IActionResult> Index2()
        {
            //retornando uuna vista con el listado del producto
              
            return View(await _context.producto.ToListAsync());
        }



        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null)
            {
                return NotFound("");
            }

            var mProducto = await _context.producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mProducto == null)
            {
                return NotFound();
            }

            return View(mProducto);
        }


        // GET: Producto/Create
        public IActionResult Create()
        {
            //llamando a la pagina create
            return View();
        }








        // POST: Insertando Datos a la base de datos 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto mProducto, IFormFile upload)
        {



            //Condicion que valida que el formulario se halla introducido al modelo correctamente
            if (ModelState.IsValid)
            {

                //Condicion que valida si la imagen insertada no esta vacia
                if (upload != null && upload.Length > 0)
                {
                    //Convirtiendola imagen a Byte para que se pueda guardar en la base de datos
                    byte[] ImageData = null;

                    using (var memoryStream = new MemoryStream())
                    {
                        await upload.CopyToAsync(memoryStream);
                        ImageData = memoryStream.ToArray();
                    }

                    // Reasignando el valor de la propiedad Imagen del modelo Producto con el valor de byte
                    mProducto.ImageFile = ImageData;
                    _context.Add(mProducto);
                    // Guardando cambios
                    await _context.SaveChangesAsync();
                    //Retornando vista
                    return RedirectToAction(nameof(Index2));
                }
                //Esta condicion llama al metodo de la clase uploadimage y solo se cumplira si no se inserto ninguna imagen en el formulario
                else
                {
                    mProducto.ImageFile = await _uploadimage.ByteAvatarImageAsync(_environment, "imagenes/imagen/default.jpg");
                    _context.Add(mProducto);
                    // Guardando cambios
                    await _context.SaveChangesAsync();
                    //Retornando vista
                    return RedirectToAction(nameof(Index2));
                }

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarritoC(Carrito carrito)
        {



            //Condicion que valida que el formulario se halla introducido al modelo correctamente
            if (ModelState.IsValid)
            {

                //Condicion que valida si la imagen insertada no esta vacia

                //Esta condicion llama al metodo de la clase uploadimage y solo se cumplira si no se inserto ninguna imagen en el formulario
                _context.Add(carrito);
                // Guardando cambios
                await _context.SaveChangesAsync();
                //Retornando vista
                return RedirectToAction(nameof(Productos));

            }
            return View();
        }



        // GET: Producto/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProducto = await _context.producto.FirstOrDefaultAsync(m => m.Id == id);
            if (mProducto == null)
            {
                return NotFound();
            }
            return View(mProducto);
        }
        public async Task<IActionResult> DeleteC(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mcarrito = await _context.carrito
                .FirstOrDefaultAsync(m => m.Id == id);
            _context.Remove(mcarrito);
            await _context.SaveChangesAsync();
            if (mcarrito == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Carrito));
        }

        public async Task<IActionResult> Comprar()
        {

            var mcarrito = await _context.carrito.ToListAsync();
            foreach (var item in mcarrito)
            {

                _context.carrito.Remove(item);

            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Productos));

        }
        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int id, Producto mProducto, IFormFile upload)
        {
            if (id != mProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (upload != null && upload.Length > 0)
                    {
                        //Convirtiendola imagen a Byte para que se pueda guardar en la base de datos
                        byte[] ImageData = null;

                        using (var memoryStream = new MemoryStream())
                        {
                            await upload.CopyToAsync(memoryStream);
                            ImageData = memoryStream.ToArray();
                        }

                        // Reasignando el valor de la propiedad Imagen del modelo Producto con el valor de byte
                        mProducto.ImageFile = ImageData;
                    }
                    //Esta condicion llama al metodo de la clase uploadimage y solo se cumplira si no se inserto ninguna imagen en el formulario
                    else
                    {
                        mProducto.ImageFile = await _uploadimage.ByteAvatarImageAsync(_environment, "imagenes/imagen/default.jpg");

                    }

                    _context.Update(mProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MProductoExists(mProducto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index2));
            }
            return View(mProducto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProducto = await _context.producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mProducto == null)
            {
                return NotFound();
            }

            return View(mProducto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mProducto = await _context.producto.FindAsync(id);

            _context.producto.Remove(mProducto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index2));
        }

        private bool MProductoExists(int id)
        {
            return _context.producto.Any(e => e.Id == id);
        }

        public ActionResult ConvertirImagen(int id)
        {
            using (var context = _context)
            {
                var imagen = (from Producto in context.producto
                              where Producto.Id == id
                              select Producto.ImageFile).FirstOrDefault();
                return File(imagen, "images/png");
            };
        }
    }
}
