using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class EntityLocalizer<T> : IStringLocalizer<ILocalize> where T : ILocalize
    {
        private readonly CultureInfo _Culture;
        private readonly ILocalize _Entity;
        public T Entity => (T)_Entity;
        public EntityLocalizer(T Entity, CultureInfo culture = null)
        {
            _Entity = Entity;
            _Culture = culture ?? CultureInfo.CurrentCulture;
        }
        public LocalizedString this[string name] => _Culture != null && _Culture.Name == "fr-CA" ? new LocalizedString(_Entity.Value, _Entity.ValueCA) : new LocalizedString(_Entity.Value, _Entity.Value);

        public LocalizedString this[string name, params object[] arguments] =>
           _Culture != null && _Culture.Name == "fr-CA" ? new LocalizedString(_Entity.Value, _Entity.ValueCA) : new LocalizedString(_Entity.Value, _Entity.Value);

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return new LocalizedString[] { new LocalizedString(_Entity.Value, _Entity.Value), new LocalizedString(_Entity.Value, _Entity.ValueCA) };
        }

    }
}
