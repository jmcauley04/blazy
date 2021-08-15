using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace SEG.Components.Blazor.DataGridSupporters.Filters
{
    public partial class NumericalFilterMenu<T>
    {
        [Parameter]
        public Filter Filter { get; set; }

        [Parameter]
        public EventCallback<Filter> OnApplyPushed { get; set; }

        [Parameter]
        public EventCallback<Filter> OnClearPushed { get; set; }

        #region Selections

        private Qualifier SelectedQualifier { get; set; }

        private T SelectedOption_0 { get; set; }

        private T SelectedOption_1 { get; set; }

        #endregion

        #region Options and clause converters

        private List<Qualifier> Qualifiers => new()
        {
            Qualifier.Ignore,
            Qualifier.Equals,
            Qualifier.NotEquals,
            Qualifier.GreaterThan,
            Qualifier.LessThan,
            Qualifier.GreaterThanOrEqualTo,
            Qualifier.LessThanOrEqualTo,
            Qualifier.Between,
            Qualifier.NotBetween
        };

        #endregion

        protected override void OnInitialized()
        {
            SelectedQualifier = Filter.Qualifier;

            SelectedOption_0 = Filter.Option?[0] ?? 0;

            SelectedOption_1 = Filter.Option?[1] ?? 0;

            base.OnInitialized();
        }

        private void ApplyFilters()
        {
            string clause = SelectedQualifier switch
            {
                Qualifier.Equals => $"{Filter.ColumnName} = {SelectedOption_0}",
                Qualifier.NotEquals => $"{Filter.ColumnName} <> {SelectedOption_0}",
                Qualifier.GreaterThan => $"{Filter.ColumnName} > {SelectedOption_0}",
                Qualifier.LessThan => $"{Filter.ColumnName} < {SelectedOption_0}",
                Qualifier.GreaterThanOrEqualTo => $"{Filter.ColumnName} >= {SelectedOption_0}",
                Qualifier.LessThanOrEqualTo => $"{Filter.ColumnName} <= {SelectedOption_0}",
                Qualifier.Between => $"{Filter.ColumnName} > {SelectedOption_0} AND {Filter.ColumnName} < {SelectedOption_0}",
                Qualifier.NotBetween => $"NOT ({Filter.ColumnName} > {SelectedOption_0} AND {Filter.ColumnName} < {SelectedOption_0})",
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