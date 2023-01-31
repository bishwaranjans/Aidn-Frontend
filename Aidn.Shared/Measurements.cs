using System.ComponentModel.DataAnnotations;

namespace Aidn.Shared;

public class Measurements
{  
    [Required]
    public double TempValue { get; set; }
   
    [Required]
    public double HrValue { get; set; }
  
    [Required]
    public double RrValue { get; set; }
}

