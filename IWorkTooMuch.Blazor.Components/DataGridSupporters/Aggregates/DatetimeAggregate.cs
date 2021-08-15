using System;
using System.Collections.Generic;
using System.Data;

namespace SEG.Components.Blazor.DataGridSupporters.Aggregates
{
    public class DatetimeAggregate : Aggregate
    {
        public override AggregateOption SelectedAggregate { get; set; } = AggregateOption.Earliest;

        public DatetimeAggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter) : base(getView, column, formatter)
        {
        }

        public override string AggregateValue() => SelectedAggregate switch
        {
            AggregateOption.None => base.GetNone(),
            AggregateOption.Count => base.GetCount(),
            AggregateOption.Earliest => GetEarliest(),
            AggregateOption.Latest => GetLatest(),
            AggregateOption.Timespan => GetTimespan(),
            _ => throw new NotImplementedException($"{SelectedAggregate} not implemented for DatetimeAggregate")
        };

        public override List<AggregateOption> GetAggregateOptions() =>
            new()
            {
                AggregateOption.None,
                AggregateOption.Count,
                AggregateOption.Earliest,
                AggregateOption.Latest,
                AggregateOption.Timespan
            };

        private string GetLatest()
        {
            DateTime? dt = null;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisDt = Convert.ToDateTime(row[_dataColumn.ColumnName]);
                if (dt == null || thisDt > dt)
                    dt = thisDt;
            }

            string output;

            if (dt == null)
                output = $"-";
            else
                output = _formatter.Invoke(_dataColumn, dt);

            return $"Latest: {output}";
        }

        private string GetEarliest()
        {
            DateTime? dt = null;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisDt = Convert.ToDateTime(row[_dataColumn.ColumnName]);
                if (dt == null || thisDt < dt)
                    dt = thisDt;
            }

            string output;

            if (dt == null)
                output = $"-";
            else
                output = _formatter.Invoke(_dataColumn, dt);

            return $"Earliest: {output}";
        }

        private string GetTimespan()
        {
            DateTime? earliest = null;
            DateTime? latest = null;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisDt = Convert.ToDateTime(row[_dataColumn.ColumnName]);
                if (earliest == null || thisDt < earliest)
                    earliest = thisDt;

                if (latest == null || thisDt > latest)
                    latest = thisDt;
            }

            string output;

            if (earliest == null)
                output = $"-";
            else
                output = ((TimeSpan)(latest - earliest)).ToString(@"d\.hh\:mm\:ss");

            return $"Timespan: {output}";
        }
    }
}
