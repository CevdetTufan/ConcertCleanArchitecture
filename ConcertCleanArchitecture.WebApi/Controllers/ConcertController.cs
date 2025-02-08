using ConcertCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ConcertCleanArchitecture.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ConcertController : ODataController
{
	private readonly IConcertService _concertService;

	public ConcertController(IConcertService concertService)
	{
		_concertService = concertService;
	}

	[HttpGet("GetConcerts")]
	[EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
	public IActionResult GetConcerts()
	{
		var concerts = _concertService.GetConcerts();
		return Ok(concerts);
	}
}
