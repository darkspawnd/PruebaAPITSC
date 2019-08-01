using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.Data;
using PruebaAPI.Models;
using PruebaAPI.RequestModels;

namespace PruebaAPI.Controllers
{
    [Route("api/Country/{alpha2Code}/Subdivisions")]
    [ApiController]
    public class CountrySubdivisionController : ControllerBase
    {
        APIDbContext _context;

        public CountrySubdivisionController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult> All(string alpha2Code)
        {
            Country country = _context.Countries
                                .Include("Subdivisions")
                                .Where(x => x.Alpha2Code == alpha2Code).FirstOrDefault();
            if (country == null)
                return NotFound(new { Errors = new[] { "Country not found" } });

            return Ok(new
            {
                Data = country.Subdivisions
            });
        }

        [HttpGet("{subdivisionCode}")]
        public async Task<ActionResult> Filter(string alpha2Code, string subdivisionCode)
        {
            Country country = _context.Countries
                                .Include("Subdivisions")
                                .Where(x => x.Alpha2Code == alpha2Code).FirstOrDefault();
            if (country == null)
                return NotFound(new { Errors = new[] { "Country not found" } });

            CountrySubdivision subdivision = country.Subdivisions.Where(x => x.Code == subdivisionCode).FirstOrDefault();

            if (subdivision == null)
                return NotFound(new { Errors = new[] { "Subdivision not found" } });

            return Ok(new
            {
                Data = subdivision
            });
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(string alpha2Code, CountrySubdivisionRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values });

            Country country = _context.Countries
                                .Where(x => x.Alpha2Code == alpha2Code).FirstOrDefault();
            if (country == null)
                return NotFound(new { Errors = new[] { "Country not found" } });

            CountrySubdivision countrySubdivision = new CountrySubdivision
            {
                Name = model.Name,
                Code = model.Code,
                LocalizedName = model.LocalizedName,
                CountryId = country.Id
            };

            _context.CountrySubdivisions.Add(countrySubdivision);

            try
            {
                _context.SaveChanges();
                return Created(Url.Action("Filter", "CountrySubdivision", new { subdivisionCode = countrySubdivision.Code }), new
                {
                    Data = countrySubdivision,
                    Link = new
                    {
                        Self = Url.Action("Filter", "CountrySubdivision", new { subdivisionCode = countrySubdivision.Code })
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Errors = "Error encountered, please try again later",
                    Meta = new { StackTrace = ex.Message }
                });
            }

        }

        [HttpPatch("Update")]
        public async Task<ActionResult> Update(CountrySubdivisionRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values });

            if (model.Id == 0)
                return BadRequest(new { Errors = new[] { "Country Subdivision Id must be provided" } });

            CountrySubdivision countrySubdivisions = _context.CountrySubdivisions.Find(model.Id);

            if (countrySubdivisions == null)
                return NotFound(new { Errors = new[] { "Country Subdivision not found" } });

            countrySubdivisions.Name = model.Name;
            countrySubdivisions.Code = model.Code;
            countrySubdivisions.LocalizedName = model.LocalizedName;

            _context.CountrySubdivisions.Update(countrySubdivisions);

            try
            {
                _context.SaveChanges();
                return Accepted(Url.Action("Filter", "CountrySubdivision", new { subdivisionCode = countrySubdivisions.Code }), 
                new {
                    Data = countrySubdivisions,
                    Link = new
                    {
                        Self = Url.Action("Filter", "CountrySubdivision", new { subdivisionCode = countrySubdivisions.Code })
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Errors = "Error encountered, please try again later",
                    Meta = new { StackTrace = ex.Message }
                });
            }

        }

        [HttpDelete("Delete/{subdivisionId}")]
        public async Task<ActionResult> Destroy(int subdivisionId)
        {
            if (subdivisionId == 0)
                return BadRequest(new { Errors = new[] { "Country Subdivision Id must be provided" } });

            CountrySubdivision countrySubdivision = _context.CountrySubdivisions.Find(subdivisionId);
            if (countrySubdivision == null)
                return NotFound(new { Errors = new[] { "Country Subdivision not found" } });
            _context.CountrySubdivisions.Remove(countrySubdivision);

            try
            {
                _context.SaveChanges();
                return Accepted(Url.Action("Filter", "Country", new { Name = countrySubdivision.Name }), new { Data = "" });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Errors = "Error encountered, please try again later",
                    Meta = new { StackTrace = ex.Message }
                });
            }
        }

    }
}