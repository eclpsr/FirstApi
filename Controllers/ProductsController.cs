using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>();

    [HttpGet]
    public IEnumerable<Product> GetAll() => _products;

    [HttpPost]
    public IActionResult Add(Product product)
    {
        product.Id = _products.Count + 1;
        _products.Add(product);
        if(!PriceCheck(product)) return BadRequest();
        return Ok(product);
    }

    [HttpPost("bulk")]
    public IActionResult AddMany(List<Product> products)
    {
        foreach (var product in products)
        {
            if (!PriceCheckBulk(products)) return BadRequest();
            product.Id = _products.Count + 1;
            _products.Add(product);
        }
        return Ok(products);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updated)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        product.Name = updated.Name;
        product.Price = updated.Price;
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        _products.Remove(product);
        return NoContent();
    }

    private bool PriceCheckBulk(List<Product> products)
    {
        foreach (var product in products)
        {
            if (product.Price < 0)
            {
                return false;
            }
        }
        return true;
    }
    private bool PriceCheck(Product product)
    {
        if (product.Price < 0)
        {
            return false;
        }
        return true;
    }
}