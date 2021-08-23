using Microsoft.AspNetCore.Components;

namespace IWorkTooMuch.Blazor.Components.DataGridSupporters.Components
{
    public partial class ColumnFooter
    {
        [Parameter]
        public FooterDefinition FooterDefinition { get; set; }

        [Parameter]
        public int SelectedColumn { get; set; }

        [Parameter]
        public EventCallback<int> SelectedColumnChanged { get; set; }

        [Parameter]
        public DataGridAction DataGridAction { get; set; }

        [Parameter]
        public EventCallback<DataGridAction> DataGridActionChanged { get; set; }

        private string _value => FooterDefinition.aggregate?.AggregateValue() ?? FooterDefinition.value;

        private void SetSelectedColumn(int colNum)
        {
            if (SelectedColumn == colNum && DataGridAction == DataGridAction.Aggregate)
                SelectedColumn = -1;
            else
                SelectedColumn = colNum;

            if (DataGridAction != DataGridAction.Aggregate)
            {
                DataGridAction = DataGridAction.Aggregate;
                DataGridActionChanged.InvokeAsync(DataGridAction);
            }

            SelectedColumnChanged.InvokeAsync(SelectedColumn);
        }

        private void SetAggregation(AggregateOption aggregate)
        {
            SetSelectedColumn(-1);

            FooterDefinition.aggregate.SelectedAggregate = aggregate;
        }
    }
}
