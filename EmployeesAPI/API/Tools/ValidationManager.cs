using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Tools
{
    public static class ValidationManager
    {
        public static ICollection<string> ErrorMessages(ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(s => s.Errors.Select(s => s.ErrorMessage))
                .ToList();
        }
    }
}
