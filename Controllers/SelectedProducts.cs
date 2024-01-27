using SelectedProductsModel = mongodb_dotnet_example.Models.SelectedProducts;
using SelectedProductsService = mongodb_dotnet_example.Services.SelectedProductsService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;
using mongodb_dotnet_example.Models;

namespace mongodb_dotnet_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectedProductsController : ControllerBase
    {
        private readonly SelectedProductsService _selectedProductsService;

        public SelectedProductsController(SelectedProductsService selectedProductsService)
        {
            _selectedProductsService = selectedProductsService;
        }

        [HttpGet]
        public ActionResult<List<SelectedProductsModel>> Get()
        {
            var selectedProducts = _selectedProductsService.Get();
            return Ok(selectedProducts);
        }

        [HttpGet("{id:length(24)}", Name = "GetSelectedProductsById")]
        public ActionResult<selectedProductsModel> Get(string id)
        {
            var selectedProducts = _selectedProductsService.Get(id);

            if (selectedProducts == null)
            {
                return NotFound();
            }

            return Ok(selectedProducts);
        }

[HttpPut("{id:length(24)}")]
public IActionResult UpdateSelectedProductsList(string id, [FromBody] SelectedProducts updatedData)
{
    var existingSelectedProducts = _selectedProductsService.Get(id);

    if (existingSelectedProducts == null)
    {
        return NotFound();
    }

    try
    {
        // Оновіть поле SelectedProductsList у існуючого об'єкта SelectedProducts
        existingSelectedProducts.SelectedProductsList = updatedData.SelectedProductsList;
        
        _selectedProductsService.Update(id, existingSelectedProducts);

        // Після оновлення отримайте оновлені дані з бази даних
        var updatedSelectedProducts = _selectedProductsService.Get(id);

        if (updatedSelectedProducts == null)
        {
            return NotFound();
        }

        return Ok(updatedSelectedProducts); // Повернути оновлені дані у відповіді
    }
    catch (Exception)
    {
        return BadRequest("Invalid JSON format or missing SelectedProducts field.");
    }
}






        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var selectedProducts = _selectedProductsService.Get(id);

            if (selectedProducts == null)
            {
                return NotFound();
            }

            _selectedProductsService.Delete(id);

            return NoContent();
        }

    }

    public class selectedProductsModel
    {
    }
}
