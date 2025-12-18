namespace HeatExchangeApp.Models;

public class LayerPoint
{
    public double Y { get; set; }                   // м
    public double MaterialTemp { get; set; }        // °C
    public double GasTemp { get; set; }             // °C
    public double DeltaTemp => GasTemp - MaterialTemp;
    public double RelativeHeight { get; set; }      // y / H
}