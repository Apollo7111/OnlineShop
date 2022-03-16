using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace OnlineShop.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //Изваждаме стойноста от bindingContext
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            //Проверка дали съществува и дали е празен
            if (valueResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                decimal actualValue = 0;
                bool success = false;

                try
                {
                    string decValue = valueResult.FirstValue;
                    //Replace . със сегашния формат
                    decValue = decValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    //Replace , със сегашния формат
                    decValue = decValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    actualValue = Convert.ToDecimal(decValue, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch (FormatException fe)
                {
                    //Добавя грешка в модела
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }
                if (success)
                {
                    //Успешно с стойност actualValue
                    bindingContext.Result = ModelBindingResult.Success(actualValue);
                }

            }
                return Task.CompletedTask;
        }
    }
}
