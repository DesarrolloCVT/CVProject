using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Behaviors
{
    public class AdvancedCurrencyEntryBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty CultureCodeProperty =
            BindableProperty.Create(nameof(CultureCode), typeof(string), typeof(AdvancedCurrencyEntryBehavior), CultureInfo.CurrentCulture.Name);

        public static readonly BindableProperty DecimalPlacesProperty =
            BindableProperty.Create(nameof(DecimalPlaces), typeof(int), typeof(AdvancedCurrencyEntryBehavior), 2);

        public static readonly BindableProperty UseSymbolProperty =
            BindableProperty.Create(nameof(UseSymbol), typeof(bool), typeof(AdvancedCurrencyEntryBehavior), true);

        public string CultureCode
        {
            get => (string)GetValue(CultureCodeProperty);
            set => SetValue(CultureCodeProperty, value);
        }

        public int DecimalPlaces
        {
            get => (int)GetValue(DecimalPlacesProperty);
            set => SetValue(DecimalPlacesProperty, value);
        }

        public bool UseSymbol
        {
            get => (bool)GetValue(UseSymbolProperty);
            set => SetValue(UseSymbolProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.Unfocused += OnUnfocused;
            entry.Focused += OnFocused;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.Unfocused -= OnUnfocused;
            entry.Focused -= OnFocused;
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                if (decimal.TryParse(entry.Text, NumberStyles.Currency, GetCulture(), out var value))
                {
                    entry.Text = value.ToString("0.##");
                }
            }
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                if (decimal.TryParse(entry.Text, NumberStyles.Any, GetCulture(), out var value))
                {
                    var format = UseSymbol ? "C" + DecimalPlaces : "N" + DecimalPlaces;
                    entry.Text = value.ToString(format, GetCulture());
                }
            }
        }

        private CultureInfo GetCulture()
        {
            return new CultureInfo(CultureCode ?? CultureInfo.CurrentCulture.Name);
        }
    }
}
