using HeatExchangeApp.Models;
using System.Text.Json;

namespace HeatExchangeApp.Services;

public class HeatExchangeService
{
    public List<LayerPoint> Calculate(InputData input)
    {
        // Геометрия
        double area = Math.PI * Math.Pow(input.ApparatusDiameter / 2.0, 2); // м²
        double Qg = input.GasVelocity * area; // м³/с — объёмный расход
        double Gg = Qg * input.GasHeatCapacity * 1000.0; // Вт/К — *1000 → Дж/(м³·К) → Вт/К
        double Gs = input.MaterialFlowRate * input.MaterialHeatCapacity * 1000.0; // Вт/К

        double alphaV = input.HeatTransferCoeff; // Вт/(м³·К)
        double H = input.LayerHeight;
        int n = Math.Max(2, input.NumLayers - 1); // кол-во шагов интегрирования
        double dy = -H / n; // от y = H к y = 0 (шаг отрицательный)


        double k = alphaV * area / (Gg + Gs); // 1/м
        double a = Gg / (Gg + Gs);
        double b = Gs / (Gg + Gs);
        double dT0 = input.GasTempInitial - input.MaterialTempInitial;

        var points = new List<LayerPoint>();

        var yValues = Enumerable.Range(0, input.NumLayers)
            .Select(i => i * H / (input.NumLayers - 1))
            .ToList();

        foreach (double y in yValues)
        {
            double phi = 1.0 - Math.Exp(-k * y);
            double Ts = input.MaterialTempInitial + dT0 * a * phi;
            double Tg = input.GasTempInitial - dT0 * b * phi;

            points.Add(new LayerPoint
            {
                Y = Math.Round(y, 2),
                MaterialTemp = Math.Round(Ts, 2),
                GasTemp = Math.Round(Tg, 2),
                RelativeHeight = Math.Round(y / H, 3)
            });
        }


        return points;
    }
}
