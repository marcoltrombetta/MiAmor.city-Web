using MiAmor.Core;
using MiAmor.Services;
using System.Globalization;


namespace MiAmor.Services
{

    public interface ILanguageService : IEntityService<Language>
    {
        Language GetById(int Id);
        string GetValue(string resourceKey, CultureInfo culture);
    }
}
