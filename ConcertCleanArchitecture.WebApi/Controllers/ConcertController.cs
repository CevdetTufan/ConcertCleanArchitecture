using ConcertCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ConcertCleanArchitecture.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ConcertController(IConcertService concertService) : ODataController
{
	private readonly IConcertService _concertService = concertService;

	[HttpGet("GetConcerts")]
	[EnableQuery]
	public IActionResult GetConcerts()
	{
		var concerts = _concertService.GetConcerts();
		return Ok(concerts);
	}
}
