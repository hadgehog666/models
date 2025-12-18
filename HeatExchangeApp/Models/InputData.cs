using System.ComponentModel.DataAnnotations;

namespace HeatExchangeApp.Models;

public class InputData
{
    [Display(Name = "Высота слоя, м")]
    [Range(0.1, 50, ErrorMessage = "Высота должна быть от 0.1 до 50 м")]
    public double LayerHeight { get; set; } = 6.0;

    [Display(Name = "Нач. т-ра материала, °C")]
    [Range(-50, 1500)]
    public double MaterialTempInitial { get; set; } = 15.0;

    [Display(Name = "Нач. т-ра газа, °C")]
    [Range(-50, 1500)]
    public double GasTempInitial { get; set; } = 600.0;

    [Display(Name = "Скорость газа, м/с")]
    [Range(0.01, 10)]
    public double GasVelocity { get; set; } = 0.73;

    [Display(Name = "Теплоёмкость газа, кДж/(м³·К)")]
    [Range(0.1, 5)]
    public double GasHeatCapacity { get; set; } = 1.09;

    [Display(Name = "Расход материала, кг/с")]
    [Range(0.01, 100)]
    public double MaterialFlowRate { get; set; } = 1.68;

    [Display(Name = "Теплоёмкость материала, кДж/(кг·К)")]
    [Range(0.1, 5)]
    public double MaterialHeatCapacity { get; set; } = 1.49;

    [Display(Name = "αV, Вт/(м³·К)")]
    [Range(100, 20000)]
    public double HeatTransferCoeff { get; set; } = 2440.0;

    [Display(Name = "Диаметр аппарата, м")]
    [Range(0.1, 10)]
    public double ApparatusDiameter { get; set; } = 2.2;

    [Display(Name = "Число узлов по высоте")]
    [Range(3, 50)]
    public int NumLayers { get; set; } = 7; // → y = 0,1,2,3,4,5,6 м — как в отчёте
}
