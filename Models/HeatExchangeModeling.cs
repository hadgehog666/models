namespace HeatExchangeModeling.Models
{
    public class HeatExchangeModel
    {
        // Исходные данные (ввод пользователя)
        public double LayerHeight { get; set; } = 6; // Высота слоя, м
        public double MaterialInitialTemperature { get; set; } = 15; // Начальная температура материала, °C
        public double GasInitialTemperature { get; set; } = 600; // Начальная температура газа, °C
        public double GasVelocity { get; set; } = 0.73; // Скорость газа, м/с
        public double GasHeatCapacity { get; set; } = 1.09; // Теплоемкость газа, кДж/(м³·К)
        public double MaterialConsumption { get; set; } = 1.68; // Расход материалов, кг/с
        public double MaterialHeatCapacity { get; set; } = 1.49; // Теплоемкость материалов, кДж/(кг·К)
        public double HeatTransferCoefficient { get; set; } = 2440; // Объемный коэффициент теплоотдачи, Вт/(м³·К)
        public double ApparatusDiameter { get; set; } = 2.2; // Диаметр аппарата, м

        // Расчетные параметры
        public double CrossSectionArea => Math.PI * Math.Pow(ApparatusDiameter / 2, 2);
        public double HeatCapacityRatio { get; set; } // Отношение теплоемкостей (m)
        public double TotalRelativeHeight { get; set; } // Y0
        public double FullRelativeHeightDenominator { get; set; } // Знаменатель для полной относительной высоты

        // Результаты расчета по координатам
        public List<CalculationPoint> Results { get; set; } = new List<CalculationPoint>();
    }
}