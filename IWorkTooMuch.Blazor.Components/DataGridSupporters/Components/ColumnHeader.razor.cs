using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace IWorkTooMuch.Blazor.Components.DataGridSupporters.Components
{
    public partial class ColumnHeader
    {
        [Parameter]
        public bool CanFilterColumns { get; set; }

        [Parameter]
        public HeaderDefinition HeaderDefinition { get; set; }

        [Parameter]
        public int SelectedColumn { get; set; }

        [Parameter]
        public EventCallback<int> SelectedColumnChanged { get; set; }

        [Parameter]
        public DataGridAction DataGridAction { get; set; }

        [Parameter]
        public EventCallback<DataGridAction> DataGridActionChanged { get; set; }

        [Parameter]
        public int SortColumn { get; set; }

        [Parameter]
        public EventCallback<int> SortColumnChanged { get; set; }

        [Parameter]
        public SortOrder SortOrder { get; set; }

        [Parameter]
        public EventCallback<SortOrder> SortOrderChanged { get; set; }

        [Parameter]
        public List<Filter> Filters { get; set; }

        [Parameter]
        public EventCallback<List<Filter>> FiltersChanged { get; set; }

        private Filter filter;

        private bool FilterMenuOpen => SelectedColumn == HeaderDefinition.colNum && DataGridAction == DataGridAction.Filter;

        protected override void OnInitialized()
        {
            filter = Filter.GetFilter(Filters, HeaderDefinition.columnName);

            base.OnInitialized();
        }

        private void TrySetSelectedColumn(int colNum)
        {
            if (colNum != SelectedColumn)
            {
                SelectedColumn = colNum;
                SelectedColumnChanged.InvokeAsync(SelectedColumn);
            }
        }

        private void TrySetDataGridAction(DataGridAction action)
        {
            if (DataGridAction != action)
            {
                DataGridAction = action;
                DataGridActionChanged.InvokeAsync(DataGridAction);
            }
        }

        private void ToggleFilterMenu()
        {
            if (DataGridAction == DataGridAction.Filter && SelectedColumn == HeaderDefinition.colNum)
                TrySetSelectedColumn(-1);
            else
                TrySetSelectedColumn(HeaderDefinition.colNum);

            TrySetDataGridAction(DataGridAction.Filter);
        }

        private void Sort()
        {
            if (SortColumn == HeaderDefinition.colNum)
                SortOrder = (SortOrder)(((int)(SortOrder + 1)) % Enum.GetNames(typeof(SortOrder)).Length);
            else
                SortOrder = SortOrder.Ascending;

            if (HeaderDefinition.colNum != SortColumn)
            {
                SortColumn = HeaderDefinition.colNum;
                SortColumnChanged.InvokeAsync(SortColumn);
            }

            TrySetDataGridAction(DataGridAction.Sort);

            SortOrderChanged.InvokeAsync(SortOrder);
        }

        private void SetFilter(Filter filter)
        {
            if (!Filters.Contains(filter) && filter.Qualifier != Qualifier.Ignore)
                Filters.Add(filter);

            if (Filters.Contains(filter) && filter.Qualifier == Qualifier.Ignore)
                Filters.Remove(filter);

            TrySetSelectedColumn(-1);
        }

        private void ClearFilter(Filter filter)
        {
            Filters.RemoveAll(x => x.ColumnName == filter.ColumnName);

            TrySetSelectedColumn(-1);
        }
    }
}
