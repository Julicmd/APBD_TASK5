using System.Runtime.InteropServices.JavaScript;

namespace Application6.Models;

public class Reservation
{
    public int Id {get; set;}
    public int RoomId {get; set;}
    public string OrganizerName {get; set;} = string.Empty;
    public string Topic {get; set;} = string.Empty;
    public DateTime Date {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public string Status {get; set;} = string.Empty;
   
}