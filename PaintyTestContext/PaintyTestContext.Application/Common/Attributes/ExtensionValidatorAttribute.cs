using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PaintyTestContext.Application.Common.Exceptions;

namespace PaintyTestContext.Application.Common.Attributes
{
    /// <summary>
    /// Атирибут валидации расширения файла
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ExtensionValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Допустимые расширения
        /// </summary>
        public string Extensions { get; set; } = string.Empty;

        public override bool IsValid(object? value)
        {
            var extensions = Extensions.Trim('.').Split(",");

            switch (value)
            {
                case IFormFile file:
                    Validation(file, extensions);
                    return true;
                case List<IFormFile> files:
                {
                    foreach (var arrayFile in files)
                    {
                        Validation(arrayFile, extensions);
                    }
                    return true;
                }
                case null:
                    return true;
                default:
                    throw new BadRequestException("Разрешён только IFormFile тип данных");
            }
        }

        private bool Validation(IFormFile file, IEnumerable<string> extensions)
        {
            var fileExtension = Path.GetExtension(file.FileName).Trim('.');
            if (extensions.Contains(fileExtension))
                return true;
            else
                throw new BadRequestException(ErrorMessage == string.Empty
                    ? "Неподдерживаемый формат файла"
                    : ErrorMessage!);
        }
    }
}
