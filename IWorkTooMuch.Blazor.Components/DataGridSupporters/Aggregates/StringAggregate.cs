using System;
using System.Collections.Generic;
using System.Data;

namespace IWorkTooMuch.Blazor.Components.DataGridSupporters.Aggregates
{
    public class StringAggregate : Aggregate
    {
        public StringAggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter) : base(getView, column, formatter)
        {
        }

        public override string AggregateValue() => SelectedAggregate switch
        {
            AggregateOption.None => base.GetNone(),
            AggregateOption.Count => base.GetCount(),
            AggregateOption.First => GetFirst(),
            AggregateOption.Last => GetLast(),
            AggregateOption.Mode => GetMode(),
            _ => throw new NotImplementedException($"{SelectedAggregate} not implemented for BoolAggregate")
        };

        public override List<AggregateOption> GetAggregateOptions() =>
            new()
            {
                AggregateOption.None,
                AggregateOption.Count,
                AggregateOption.First,
                AggregateOption.Last,
                AggregateOption.Mode,
            };

        private string GetFirst() => $"First: {getEndValue(true)}";

        private string GetLast() => $"Last: {getEndValue(false)}";

        private string getEndValue(bool first)
        {
            int index = 0;

            if (!first)
                index = _getView.Invoke().ToTable().Rows.Count - 1;

            return Convert.ToString(_getView.Invoke().ToTable().Rows[index][_dataColumn.ColumnName]);
        }

        private string GetMode()
        {
            Dictionary<string, int> kvp = new();

            var maxValue = string.Empty;
            var maxQty = 0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisVal = Convert.ToString(row[_dataColumn.ColumnName]);

                if (kvp.ContainsKey(thisVal))
                    kvp[thisVal]++;
                else
                    kvp[thisVal] = 1;

                if (kvp[thisVal] > maxQty)
                {
                    maxValue = thisVal;
                    maxQty = kvp[thisVal];
                }
            }

            string output;

            if (kvp.Count == 0)
                output = $"-";
            else
                output = maxValue;

            return $"Mode: {output} [{maxQty}]";
        }
    }
}
