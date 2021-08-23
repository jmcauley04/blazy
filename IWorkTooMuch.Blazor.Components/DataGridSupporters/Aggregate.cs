using IWorkTooMuch.Blazor.Components.DataGridSupporters.Aggregates;
using System;
using System.Collections.Generic;
using System.Data;


namespace IWorkTooMuch.Blazor.Components.DataGridSupporters
{
    public enum AggregateOption
    {
        None,
        Sum,
        Count,
        Mean,
        Mode,
        Median,
        First,
        Last,
        Min,
        Max,
        Earliest,
        Latest,
        Timespan,
        Trueness,
        Falseness
    }

    public abstract class Aggregate
    {
        public static Aggregate TryGetAggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter)
        {
            var type = column.DataType;

            if (type == typeof(DateTime))
                return new DatetimeAggregate(getView, column, formatter);

            if (type == typeof(int))
                return new IntegerAggregate(getView, column, formatter);

            if (type == typeof(double))
                return new DoubleAggregate(getView, column, formatter);

            if (type == typeof(bool))
                return new BoolAggregate(getView, column, formatter);

            if (type == typeof(string))
                return new StringAggregate(getView, column, formatter);

            return null;
        }

        protected Aggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter)
        {
            _getView = getView;
            _dataColumn = column;
            _formatter = formatter;
        }

        protected Func<DataView> _getView;

        protected DataColumn _dataColumn;

        protected Func<DataColumn, object, string> _formatter;

        public virtual AggregateOption SelectedAggregate { get; set; } = AggregateOption.None;

        public abstract List<AggregateOption> GetAggregateOptions();

        public abstract string AggregateValue();

        protected string GetNone() => string.Empty;

        protected string GetCount()
        {
            int count = 0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
                if (!string.IsNullOrEmpty(Convert.ToString(row[_dataColumn.ColumnName])))
                    count++;

            return $"Count: {count}";
        }
    }
}
