using Application6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application6.Controllers;


    [ApiController]
    [Route("api/[controller]")] 
    public class RoomsController: ControllerBase 
    {
        [HttpGet]
        public ActionResult<List<Room>> GetAll(
            [FromQuery] int?  minCapacity,
            [FromQuery] bool? hasProjector,
            [FromQuery] bool? isActive)
        {
            var rooms = Database.DataStore.Rooms.AsEnumerable();
            if (minCapacity.HasValue)
            {
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
            }

            if (hasProjector.HasValue)
            {
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
            }

            if (isActive.HasValue)
            {
                rooms = rooms.Where(r => r.IsActive == isActive.Value);
            }

            return Ok(rooms.ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Room> GetById(int id)
        {
            var room = Database.DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound($"Room with this {id} id was not found");
            }
            return Ok(room);
        }

        [HttpPost]
        public ActionResult<Room> CreateRoom(Room room)
        {
            room.Id = Database.DataStore.NextRoomId;
            Database.DataStore.Rooms.Add(room);
            
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpGet("building/{buildingCode}")]
        public ActionResult<Room> GetBuilding(string buildingCode)
        {
            var room = Database.DataStore.Rooms
                .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(room);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Room> UpdateRoom(int id, Room newRoom)
        {
            var room = Database.DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound($"Room with this {id} id was not found");
            }
            
            room.Name = newRoom.Name;
            room.Capacity = newRoom.Capacity;
            room.Floor = newRoom.Floor;
            room.BuildingCode = newRoom.BuildingCode;
            room.HasProjector = newRoom.HasProjector;
            room.IsActive = newRoom.IsActive;
            
            return  Ok(room);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Room> DeleteRoom(int id)
        {
            var room =  Database.DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound($"Room with this {id} id was not found");
            }

            Database.DataStore.Rooms.Remove(room);
            
            return Ok(room);
        }
        
        
        
        
    }