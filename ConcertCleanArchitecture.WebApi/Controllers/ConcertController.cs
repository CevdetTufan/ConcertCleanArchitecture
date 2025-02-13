using ConcertCleanArchitecture.Application.Dtos;
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
	[EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
	public IActionResult GetConcerts()
	{
		var concerts = _concertService.GetConcerts();
		return Ok(concerts);
	}

	[HttpGet("GetSeatsByConcertId")]
	public async Task<IActionResult> GetSeatsByConcertId(Guid concertId)
	{
		var seats = await _concertService.GetSeatsByConcertId(concertId);

		if (seats is [])
		{
			return NotFound();
		}

		return Ok(seats);
	}

	[HttpPost("MakeReservation")]
	public async Task<IActionResult> MakeReservation(ReservationMakeDto reservation)
	{
		await _concertService.MakeReservation(reservation);
		return Created();
	}
}
