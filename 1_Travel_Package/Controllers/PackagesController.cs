using Microsoft.AspNetCore.Mvc;
using TravelPackageApi.Models;

namespace TravelPackageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagesController : ControllerBase
    {
        private static readonly List<TravelPackage> Packages = new();
        private static int nextId = 1;

        private bool IsValid(TravelPackage pkg, out List<string> errors)
        {
            errors = new();
            if (string.IsNullOrWhiteSpace(pkg.Destination))
                errors.Add("Destination cannot be empty.");
            if (pkg.Price < 0)
                errors.Add("Price must be non-negative.");
            if (pkg.EndDate < pkg.StartDate)
                errors.Add("EndDate must be on or after StartDate.");
            return !errors.Any();
        }

        [HttpGet]
        public ActionResult<IEnumerable<TravelPackage>> GetAll() => Ok(Packages);

        [HttpGet("{id}")]
        public ActionResult<TravelPackage> Get(int id)
        {
            var pkg = Packages.FirstOrDefault(p => p.PackageId == id);
            return pkg == null ? NotFound() : Ok(pkg);
        }

        [HttpPost]
        public ActionResult<TravelPackage> Create([FromBody] TravelPackageInput input)
        {
            var pkg = new TravelPackage(
                PackageId: nextId++,
                Destination: input.Destination,
                StartDate: input.StartDate,
                EndDate: input.EndDate,
                Price: input.Price,
                Description: input.Description
            );

            if (!IsValid(pkg, out var errs))
                return BadRequest(errs);

            Packages.Add(pkg);
            return CreatedAtAction(nameof(Get), new { id = pkg.PackageId }, pkg);
        }

        [HttpPut("{id}")]
        public ActionResult<TravelPackage> Update(int id, [FromBody] TravelPackageInput input)
        {
            var existing = Packages.FirstOrDefault(p => p.PackageId == id);
            if (existing == null)
                return NotFound();

            var updated = new TravelPackage(
                PackageId: existing.PackageId,
                Destination: input.Destination,
                StartDate: input.StartDate,
                EndDate: input.EndDate,
                Price: input.Price,
                Description: input.Description
            );

            if (!IsValid(updated, out var errs))
                return BadRequest(errs);

            var index = Packages.IndexOf(existing);
            if (index >= 0)
                Packages[index] = updated;

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pkg = Packages.FirstOrDefault(p => p.PackageId == id);
            if (pkg == null)
                return NotFound();
            Packages.Remove(pkg);
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<TravelPackage>> Search(
            [FromQuery] string destination,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var result = Packages.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(destination))
                result = result.Where(p => p.Destination.Contains(destination, StringComparison.OrdinalIgnoreCase));
            if (minPrice.HasValue)
                result = result.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                result = result.Where(p => p.Price <= maxPrice.Value);
            return Ok(result);
        }
    }
}
