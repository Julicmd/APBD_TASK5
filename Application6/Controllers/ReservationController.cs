using Application6.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Application6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController: ControllerBase
{
    [HttpGet]
    public ActionResult<List<Reservation>> GetAllReservations(
        [FromQuery]  string? status,
        [FromQuery]  int? roomId,
        [FromQuery]  DateOnly? date)
    {
        var reserve = Database.DataStore.Reservations.AsEnumerable();
        if (!string.IsNullOrEmpty(status))
        {
            reserve = reserve.Where(r => r.Status == status);
        }

        if (roomId.HasValue)
        {
            reserve = reserve.Where(r => r.RoomId == roomId);
        }

        if (date != null)
        {
            reserve = reserve.Where(re=> re.Date >= date.Value);
        }

        return Ok(reserve);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetReservationById(int id)
    {
        var reservation = Database.DataStore.Reservations.FirstOrDefault(re=> re.Id == id);
        if (reservation == null)
        {
            return NotFound("Reservation not found 404");
        }
        
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> AddReservation(Reservation reservation)
    {
        reservation.Id = Database.DataStore.NextReservationId;
        Database.DataStore.Reservations.Add(reservation);
        
        return Ok(reservation);
    }
    
}