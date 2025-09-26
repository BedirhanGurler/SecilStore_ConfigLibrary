using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SecilStore.Common.Validations.Abstract
{
    public interface IModelStateResponseService
    {
        string HandleErrorMessage(ModelStateDictionary modelState);
    }
}
