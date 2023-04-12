using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.RegularExpressions;
using WebApplication2.Helper;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpPost("compare")]
        public IActionResult CompareStrings(CompareStrings compareStringsRequest)
        {
            if (string.IsNullOrEmpty(compareStringsRequest.FirstString) || string.IsNullOrEmpty(compareStringsRequest.SecondString))
            {
                return BadRequest("Input strings cannot be null");
            }

            if (compareStringsRequest.FirstString == compareStringsRequest.SecondString)
            {
                return Ok("Strings are identical");
            }

            var differences = Enumerable.Range(0, Math.Max(compareStringsRequest.FirstString.Length, compareStringsRequest.SecondString.Length))
                .Where(i => i >= compareStringsRequest.FirstString.Length || i >= compareStringsRequest.SecondString.Length ||
                                 compareStringsRequest.FirstString[i] != compareStringsRequest.SecondString[i])
                .Select(i => $"{compareStringsRequest.FirstString.ElementAtOrDefault(i)} {compareStringsRequest.SecondString.ElementAtOrDefault(i)}");

            return Ok(string.Join(Environment.NewLine, differences));
        }
    }
}
