using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace IWorkTooMuch.Blazor.Components.DataGridSupporters.Filters
{
    public partial class StringFilterMenu
    {
        [Parameter]
        public Filter Filter { get; set; }

        [Parameter]
        public EventCallback<Filter> OnApplyPushed { get; set; }

        [Parameter]
        public EventCallback<Filter> OnClearPushed { get; set; }

        #region Selections

        private Qualifier SelectedQualifier { get; set; }

        private string SelectedOption_0 { get; set; }

        #endregion

        #region Options and clause converters

        private List<Qualifier> Qualifiers => new()
        {
            Qualifier.Ignore,
            Qualifier.Equals,
            Qualifier.NotEquals,
            Qualifier.Contains,
            Qualifier.NotContains,
            Qualifier.StartsWith,
            Qualifier.EndsWith
        };

        #endregion

        protected override void OnInitialized()
        {
            SelectedQualifier = Filter.Qualifier;

            SelectedOption_0 = Filter.Option ?? string.Empty;

            base.OnInitialized();
        }

        private void ApplyFilters()
        {
            string clause = SelectedQualifier switch
            {
                Qualifier.Equals => $"{Filter.ColumnName} = '{SelectedOption_0}'",
                Qualifier.NotEquals => $"{Filter.ColumnName} <> '{SelectedOption_0}'",
                Qualifier.Contains => $"{Filter.ColumnName} LIKE '%{SelectedOption_0}%'",
                Qualifier.NotContains => $"{Filter.ColumnName} NOT LIKE '%{SelectedOption_0}%'",
                Qualifier.StartsWith => $"{Filter.ColumnName} LIKE '{SelectedOption_0}%'",
                Qualifier.EndsWith => $"{Filter.ColumnName} LIKE '%{SelectedOption_0}'",
                _ => string.Empty
            };

            Filter = Filter.UpdateFilter(SelectedQualifier, SelectedOption_0, clause, Filter);

            OnApplyPushed.InvokeAsync(Filter);
        }

        private void ClearFilters()
        {
            OnClearPushed.InvokeAsync(Filter);
        }
    }
}
