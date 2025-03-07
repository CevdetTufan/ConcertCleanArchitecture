using ConcertCleanArchitecture.Application.Dtos.Concert;
using ConcertCleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ConcertCleanArchitecture.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ConcertController(IConcertService concertService, ILogger<ConcertController> logger) : ODataController
{
	private readonly IConcertService _concertService = concertService;
	private readonly ILogger<ConcertController> _logger = logger;

	[HttpGet("GetConcerts")]
	[EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
	public IActionResult GetConcerts()
	{
		var concerts = _concertService.GetConcerts();
		_logger.LogInformation("Concerts are listed");
		return Ok(concerts);
	}

	[HttpGet("GetSeatsByConcertId")]
	public async Task<IActionResult> GetSeatsByConcertId(Guid concertId)
	{
		var seats = await _concertService.GetSeatsByConcertId(concertId);

		if (seats is [])
		{
			_logger.LogWarning("Seats not found for concert {ConcertId}", concertId);
			return NotFound();
		}

		return Ok(seats);
	}

	[HttpPost("MakeReservation")]
	public async Task<IActionResult> MakeReservation(ReservationMakeDto reservation)
	{
		await _concertService.MakeReservation(reservation);

		_logger.LogInformation("Reservation is made for concert {ConcertId} and seat {SeatId}", reservation.ConcertId, reservation.SeatId);

		return Created();
	}
}
