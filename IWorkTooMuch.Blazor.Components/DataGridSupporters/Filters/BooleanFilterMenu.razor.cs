using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace SEG.Components.Blazor.DataGridSupporters.Filters
{
    public partial class BooleanFilterMenu
    {
        [Parameter]
        public Filter Filter { get; set; }

        [Parameter]
        public EventCallback<Filter> OnApplyPushed { get; set; }

        [Parameter]
        public EventCallback<Filter> OnClearPushed { get; set; }

        #region Selections

        private Qualifier SelectedQualifier { get; set; }

        private bool SelectedOption_0 { get; set; }

        #endregion

        #region Options

        private List<Qualifier> Qualifiers => new()
        {
            Qualifier.Ignore,
            Qualifier.Equals,
            Qualifier.NotEquals
        };

        private List<bool> Options => new()
        {
            false,
            true
        };

        #endregion

        protected override void OnInitialized()
        {
            SelectedQualifier = Filter.Qualifier;

            SelectedOption_0 = Filter.Option ?? false;

            base.OnInitialized();
        }

        private void ApplyFilters()
        {
            string clause = SelectedQualifier switch
            {
                Qualifier.Equals => $"{Filter.ColumnName} = {(SelectedOption_0 ? 1 : 0)}",
                Qualifier.NotEquals => $"{Filter.ColumnName} <> {(SelectedOption_0 ? 1 : 0)}",
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
