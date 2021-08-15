using System;
using System.Collections.Generic;
using System.Data;

namespace SEG.Components.Blazor.DataGridSupporters.Aggregates
{
    public class BoolAggregate : Aggregate
    {
        public override AggregateOption SelectedAggregate { get; set; } = AggregateOption.Trueness;

        public BoolAggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter) : base(getView, column, formatter)
        {
        }

        public override string AggregateValue() => SelectedAggregate switch
        {
            AggregateOption.None => base.GetNone(),
            AggregateOption.Count => base.GetCount(),
            AggregateOption.Trueness => GetTrueness(),
            AggregateOption.Falseness => GetFalseness(),
            _ => throw new NotImplementedException($"{SelectedAggregate} not implemented for BoolAggregate")
        };

        public override List<AggregateOption> GetAggregateOptions() =>
            new()
            {
                AggregateOption.None,
                AggregateOption.Count,
                AggregateOption.Trueness,
                AggregateOption.Falseness,
            };

        private string GetTrueness() => $"True: {percent(true):P1}";

        private string GetFalseness() => $"False: {percent(false):P1}";

        private double percent(bool state)
        {
            var stateCount = 0;
            var count = 0.0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                count++;
                if (Convert.ToBoolean(row[_dataColumn.ColumnName]) == state)
                    stateCount++;
            }

            return stateCount / Math.Max(1.0, count);
        }
    }
}
