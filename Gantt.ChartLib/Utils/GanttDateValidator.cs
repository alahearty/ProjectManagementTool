using Gantt.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Gantt.ChartLib.Utils
{
    public class GanttDateValidator : ValidationRule
    {
        private BindingExpression expression;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || expression == null)
                return new ValidationResult(false, null);
            var taskSchedule = expression.DataItem as ITaskSchedule;

            if (expression.ResolvedSourcePropertyName == "StartDate")
            {
                var startDate = (DateTime)value;
                if (startDate.IsLessThan(taskSchedule.EndDate))
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "Start-date must be less than End-date.");
            }

            if (expression.ResolvedSourcePropertyName == "EndDate")
            {
                var endDate = (DateTime)value;
                if (endDate.IsGreaterThan(taskSchedule.StartDate))
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "Start-date must be less than End-date.");
            }

            if (expression.ResolvedSourcePropertyName == "Duration")
            {
                if (!float.TryParse(value.ToString(), out float duration))
                    return new ValidationResult(false, "Invalid input");

                if (duration > 0)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "Invalid input");
            }
            return null;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            expression = owner as BindingExpression;
            return base.Validate(value, cultureInfo, owner);
        }
    }
}
