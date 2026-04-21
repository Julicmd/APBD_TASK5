using System.ComponentModel.DataAnnotations;

namespace Application6.Models;

public class Room
{
    public int Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name {get; set;} = string.Empty;
    public string BuildingCode {get; set;}
    public int Floor {get; set;}
    
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be positive number")]
    public int Capacity {get; set;}
    public bool HasProjector  {get; set;}
    public bool IsActive {get; set;}
}