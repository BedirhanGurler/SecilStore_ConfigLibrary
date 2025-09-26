using Microsoft.AspNetCore.Mvc.ModelBinding;
using SecilStore.Common.Validations.Abstract;

namespace SecilStore.Common.Validations.Concrete
{
    public class ModelStateResponseManager : IModelStateResponseService
    {
        public string HandleErrorMessage(ModelStateDictionary modelState)
        {
            List<string> errorList = modelState.Values.SelectMany(x => x.Errors).Select(v => v.ErrorMessage).ToList();
            string errorMessage = string.Join(",", errorList);

            return errorMessage;
        }
    }
}
