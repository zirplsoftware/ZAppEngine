using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.AppEngine;
using Zirpl.AppEngine.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model
{
    public static class EntityExtensions
    {
        public static DateTime GetNextShipmentDate(this ISubscriptionPeriodFields entity, DateTime lastShipmentDate)
        {
            if (entity.ShipmentPeriod == 0
                || !entity.ShipmentPeriodType.EnumValue.HasValue)
            {
                throw new InvalidOperationException();
            }

            DateTime nextShipmentDate;
            switch (entity.ShipmentPeriodType.EnumValue.Value)
            {
                case SubscriptionPeriodTypeEnum.Day:
                    nextShipmentDate = lastShipmentDate.AddDays(entity.ShipmentPeriod);
                    break;
                case SubscriptionPeriodTypeEnum.Week:
                    nextShipmentDate = lastShipmentDate.AddDays(entity.ShipmentPeriod * 7);
                    break;
                case SubscriptionPeriodTypeEnum.Month:
                    nextShipmentDate = lastShipmentDate.AddMonths(entity.ShipmentPeriod);
                    break;
                case SubscriptionPeriodTypeEnum.Year:
                    nextShipmentDate = lastShipmentDate.AddYears(entity.ShipmentPeriod);
                    break;
                default:
                    throw new UnexpectedCaseException();
            }
            return nextShipmentDate;
        }
        public static DateTime GetNextChargeDate(this ISubscriptionPeriodFields entity, DateTime lastChargeDate)
        {
            if (entity.ChargePeriod == 0
                || !entity.ChargePeriodType.EnumValue.HasValue)
            {
                throw new InvalidOperationException();
            }

            DateTime nextChargeDate;
            switch (entity.ChargePeriodType.EnumValue.Value)
            {
                case SubscriptionPeriodTypeEnum.Day:
                    nextChargeDate = lastChargeDate.AddDays(entity.ChargePeriod);
                    break;
                case SubscriptionPeriodTypeEnum.Week:
                    nextChargeDate = lastChargeDate.AddDays(entity.ChargePeriod * 7);
                    break;
                case SubscriptionPeriodTypeEnum.Month:
                    nextChargeDate = lastChargeDate.AddMonths(entity.ChargePeriod);
                    break;
                case SubscriptionPeriodTypeEnum.Year:
                    nextChargeDate = lastChargeDate.AddYears(entity.ChargePeriod);
                    break;
                default:
                    throw new UnexpectedCaseException();
            }
            return nextChargeDate;
        }
    }
}
