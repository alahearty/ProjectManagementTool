using System.Windows;
using System.Windows.Data;

namespace Gantt.ChartLib.Utils
{
    public class BindingBuilder
    {
        private readonly Binding binding;
        private readonly DependencyObject _dpObj;
        private readonly DependencyProperty _dp;

        public BindingBuilder(DependencyObject dpObj, DependencyProperty dp, object source, string path)
        {
            _dpObj = dpObj;
            _dp = dp;
            binding = new Binding(path)
            {
                Source = source
            };
        }

        public BindingBuilder Append(BindingMode mode)
        {
            binding.Mode = mode;
            return this;
        }

        public BindingBuilder Append(bool validateOnDataError)
        {
            binding.ValidatesOnDataErrors = validateOnDataError;
            return this;
        }

        public BindingBuilder Append(UpdateSourceTrigger updateSourceTrigger)
        {
            binding.UpdateSourceTrigger = updateSourceTrigger;
            return this;
        }

        public BindingBuilder Append(string stringFormat)
        {
            binding.StringFormat = stringFormat;
            return this;
        }

        public BindingBuilder Append(IValueConverter converter)
        {
            binding.Converter = converter;
            return this;
        }

        public BindingBuilder Append(object converterParameter)
        {
            binding.ConverterParameter = converterParameter;
            return this;
        }

        public void Bind()
        {
            BindingOperations.SetBinding(_dpObj, _dp, binding);
        }
    }
}
