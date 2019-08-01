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
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        APIDbContext _context;

        public CountryController(APIDbContext context)
        {
            _context = context;
        }


        [HttpGet("All")]
        public async Task<ActionResult> All()
        {
            return Ok(new { data = _context.Countries.ToList() });
        }   

        [HttpGet("Filter")]
        public async Task<ActionResult> Filter([FromQuery] string Name, [FromQuery] string Alpha2Code)
        {
            if(String.IsNullOrEmpty(Name) && String.IsNullOrEmpty(Alpha2Code))
                return BadRequest("Must provide parameter \"Name\" or \"Alpha-2 Code\"");
            IEnumerable<Country> filtered = _context.Countries.ToList();
            if (!String.IsNullOrEmpty(Name))
                filtered = filtered.Where(x => x.Name == Name);
            if(!String.IsNullOrEmpty(Alpha2Code))
                filtered = filtered.Where(x => x.Alpha2Code == Alpha2Code);
            Country result = filtered.FirstOrDefault();
            if(result == null)
                return NotFound(new { Errors = new[] { "Country not found" } });
            else
                return Ok(new { data =  result });
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CountryRequestModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values });

            Country country = new Country
            {
                Name = model.Name,
                Alpha2Code = model.Alpha2Code,
                Alpha3Code = model.Alpha3Code,
                NumericCode = model.NumericCode,
                ISOReference = model.ISOReference,
                Independent = model.Independent
            };

            _context.Countries.Add(country);

            try
            {
                _context.SaveChanges();
                return Created(Url.Action("Filter", "Country", new { Name = country.Name }), new
                {
                    Data = country,
                    Link = new {
                        Self = Url.Action("Filter", "Country", new { Name = country.Name })
                    }
                });
            } catch(Exception ex)
            {
                return Ok(new {
                    Errors = "Error encountered, please try again later",
                    Meta = new { StackTrace = ex.Message }
                });
            }

        }

        [HttpPatch("Update")]
        public async Task<ActionResult> Update(CountryRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values });

            if(model.Id == 0)
                return BadRequest(new { Errors = new[] { "Country Id must be provided" } });

            Country country = _context.Countries.Find(model.Id);

            if (country == null)
                return NotFound(new { Errors = new[] { "Country not found" } });

            country.Name = model.Name;
            country.Alpha2Code = model.Alpha2Code;
            country.Alpha3Code = model.Alpha3Code;
            country.NumericCode = model.NumericCode;
            country.ISOReference = model.ISOReference;
            country.Independent = model.Independent;

            _context.Countries.Update(country);

            try
            {
                _context.SaveChanges();
                return Accepted(Url.Action("Filter", "Country", new { Name = country.Name }), new
                {
                    Data = country,
                    Link = new
                    {
                        Self = Url.Action("Filter", "Country", new { Name = country.Name })
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

        [HttpDelete("Delete/{countryId}")]
        public async Task<ActionResult> Destroy(int countryId)
        {
            if (countryId == 0)
                return BadRequest(new { Errors = new[] { "Country Id must be provided" } });

            Country country = _context.Countries.Find(countryId);
            if (country == null)
                return NotFound(new { Errors = new[] { "Country not found" } });
            _context.Countries.Remove(country);

            try
            {
                _context.SaveChanges();
                return Accepted(Url.Action("Filter", "Country", new { Name = country.Name }), new {Data = ""});
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