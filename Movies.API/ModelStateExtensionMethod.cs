using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API
{
    public static class ModelStateExtensionMethod
    {
        public static object FieldErrorsToJson(this ModelStateDictionary modelState)
        {
            return modelState.Keys.Select(key => {

                return new
                {
                    campo = key,
                    erros = modelState[key].Errors.Select(error => error.ErrorMessage).ToArray()
                };

            }).Where(x => x.erros.Length > 0);
        }
    }
}
