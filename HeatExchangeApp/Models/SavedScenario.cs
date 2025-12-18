using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeatExchangeApp.Models;

public class SavedScenario
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = "Без названия";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column(TypeName = "TEXT")]
    public string InputJson { get; set; } = "";

    [Required]
    [Column(TypeName = "TEXT")]
    public string ResultsJson { get; set; } = "";
}
