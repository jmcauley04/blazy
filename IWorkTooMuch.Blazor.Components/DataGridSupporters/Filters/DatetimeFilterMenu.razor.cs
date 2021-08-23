using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace IWorkTooMuch.Blazor.Components.DataGridSupporters.Filters
{
    public partial class DatetimeFilterMenu
    {
        [Parameter]
        public Filter Filter { get; set; }

        [Parameter]
        public EventCallback<Filter> OnApplyPushed { get; set; }

        [Parameter]
        public EventCallback<Filter> OnClearPushed { get; set; }

        #region Selections

        private Qualifier SelectedQualifier { get; set; }

        private DateTime SelectedOption_0 { get; set; }

        private DateTime SelectedOption_1 { get; set; }

        #endregion

        #region Options

        private List<Qualifier> Qualifiers => new()
        {
            Qualifier.Ignore,
            Qualifier.On,
            Qualifier.NotOn,
            Qualifier.Before,
            Qualifier.After,
            Qualifier.Between,
            Qualifier.NotBetween
        };

        #endregion

        protected override void OnInitialized()
        {
            SelectedQualifier = Filter.Qualifier;

            SelectedOption_0 = Filter.Option?[0] ?? DateTime.Today;

            SelectedOption_1 = Filter.Option?[1] ?? DateTime.Today;

            base.OnInitialized();
        }

        private void ApplyFilters()
        {
            string clause = SelectedQualifier switch
            {
                Qualifier.On => $"{Filter.ColumnName} >= '{SelectedOption_0}' AND {Filter.ColumnName} < '{SelectedOption_0.AddDays(1)}'",
                Qualifier.NotOn => $"NOT ({Filter.ColumnName} >= '{SelectedOption_0}' AND {Filter.ColumnName} < '{SelectedOption_0.AddDays(1)}')",
                Qualifier.Before => $"{Filter.ColumnName} < '{SelectedOption_0}'",
                Qualifier.After => $"{Filter.ColumnName} >= '{SelectedOption_0.AddDays(1)}'",
                Qualifier.Between => $"{Filter.ColumnName} >= '{SelectedOption_0.AddDays(1)}' AND {Filter.ColumnName} < '{SelectedOption_1}'",
                Qualifier.NotBetween => $"NOT ({Filter.ColumnName} >= '{SelectedOption_0.AddDays(1)}' AND {Filter.ColumnName} < '{SelectedOption_1}')",
                _ => string.Empty
            };

            Filter = Filter.UpdateFilter(SelectedQualifier, new[] { SelectedOption_0, SelectedOption_1 }, clause, Filter);

            OnApplyPushed.InvokeAsync(Filter);
        }

        private void ClearFilters()
        {
            OnClearPushed.InvokeAsync(Filter);
        }
    }
}
