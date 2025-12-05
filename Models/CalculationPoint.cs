namespace HeatExchangeModeling.Models
{
    public class CalculationPoint
    {
        public double CoordinateY { get; set; } // Координата у, м
        public double Y { get; set; } // Безразмерная координата
        public double OneMinusExp { get; set; } // 1 - exp[(m-1)Y/m]
        public double OneMinusMExp { get; set; } // 1 - m*exp[(m-1)Y/m]
        public double Theta { get; set; } // ϑ
        public double Phi { get; set; } // θ
        public double MaterialTemperature { get; set; } // Температура материала, °C
        public double GasTemperature { get; set; } // Температура газа, °C
        public double TemperatureDifference { get; set; } // Разность температур, °C
    }
}
