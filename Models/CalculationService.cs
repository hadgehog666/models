namespace HeatExchangeModeling.Models
{
    public class CalculationService
    {
        public HeatExchangeModel Calculate(HeatExchangeModel model)
        {
            // Расчет основных параметров
            model.HeatCapacityRatio = CalculateHeatCapacityRatio(model);
            model.TotalRelativeHeight = CalculateTotalRelativeHeight(model);
            model.FullRelativeHeightDenominator = CalculateFullRelativeHeightDenominator(model);

            // Очищаем предыдущие результаты
            model.Results.Clear();

            // Определяем шаг координаты (разбиваем высоту на 6 частей)
            double step = model.LayerHeight / 6;

            // Расчет для каждой точки
            for (int i = 0; i <= 6; i++)
            {
                double coordinateY = i * step;
                var point = CalculatePoint(model, coordinateY);
                model.Results.Add(point);
            }

            return model;
        }

        private double CalculateHeatCapacityRatio(HeatExchangeModel model)
        {
            // m = Gм*См / (wг*S*Сг)
            double denominator = model.GasVelocity * model.CrossSectionArea * model.GasHeatCapacity;
            return (model.MaterialConsumption * model.MaterialHeatCapacity) / denominator;
        }

        private double CalculateTotalRelativeHeight(HeatExchangeModel model)
        {
            // Y0 = αV*H0 / (wг*Сг*1000)
            // 1000 для перевода кДж в Дж
            return model.HeatTransferCoefficient * model.LayerHeight /
                   (model.GasVelocity * model.GasHeatCapacity * 1000);
        }

        private double CalculateFullRelativeHeightDenominator(HeatExchangeModel model)
        {
            // 1 - m*exp[-(1-m)*Y0/m]
            double exponent = -(1 - model.HeatCapacityRatio) * model.TotalRelativeHeight /
                              model.HeatCapacityRatio;
            return 1 - model.HeatCapacityRatio * Math.Exp(exponent);
        }

        private CalculationPoint CalculatePoint(HeatExchangeModel model, double coordinateY)
        {
            var point = new CalculationPoint { CoordinateY = coordinateY };

            // Y = αV*y / (wг*Сг*1000)
            point.Y = model.HeatTransferCoefficient * coordinateY /
                     (model.GasVelocity * model.GasHeatCapacity * 1000);

            // 1 - exp[(m-1)*Y/m]
            double exponent1 = (model.HeatCapacityRatio - 1) * point.Y / model.HeatCapacityRatio;
            point.OneMinusExp = 1 - Math.Exp(exponent1);

            // 1 - m*exp[(m-1)*Y/m]
            point.OneMinusMExp = 1 - model.HeatCapacityRatio * Math.Exp(exponent1);

            // ϑ = (1 - exp[(m-1)Y/m]) / (1 - m*exp[(m-1)Y0/m])
            point.Theta = point.OneMinusExp / model.FullRelativeHeightDenominator;

            // θ = (1 - m*exp[(m-1)Y/m]) / (1 - m*exp[(m-1)Y0/m])
            point.Phi = point.OneMinusMExp / model.FullRelativeHeightDenominator;

            // t = t' + (T' - t')*ϑ
            point.MaterialTemperature = model.MaterialInitialTemperature +
                (model.GasInitialTemperature - model.MaterialInitialTemperature) * point.Theta;

            // T = t' + (T' - t')*θ
            point.GasTemperature = model.MaterialInitialTemperature +
                (model.GasInitialTemperature - model.MaterialInitialTemperature) * point.Phi;

            // ΔT = T - t
            point.TemperatureDifference = point.GasTemperature - point.MaterialTemperature;

            return point;
        }
    }
}
