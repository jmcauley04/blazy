using System;
using System.Collections.Generic;
using System.Data;


namespace IWorkTooMuch.Blazor.Components.DataGridSupporters.Aggregates
{
    public class DoubleAggregate : NumericalAggregate
    {
        public DoubleAggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter) : base(getView, column, formatter)
        {
        }

        protected override string GetMin()
        {
            double? min = null;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisVal = Convert.ToDouble(row[_dataColumn.ColumnName]);
                if (min == null || thisVal < min)
                    min = thisVal;
            }

            string output;

            if (min == null)
                output = $"-";
            else
                output = _formatter.Invoke(_dataColumn, min);

            return $"Min: {output}";
        }

        protected override string GetMax()
        {
            double? max = null;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisVal = Convert.ToDouble(row[_dataColumn.ColumnName]);
                if (max == null || thisVal > max)
                    max = thisVal;
            }

            string output;

            if (max == null)
                output = $"-";
            else
                output = _formatter.Invoke(_dataColumn, max);

            return $"Max: {output}";
        }

        protected override string GetMean()
        {
            var sum = 0.0;
            var count = 0.0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                count++;
                sum += Convert.ToDouble(row[_dataColumn.ColumnName]);
            }

            string output;

            if (count == 0)
                output = $"-";
            else
                output = _formatter.Invoke(_dataColumn, sum / count);

            return $"Mean: {output}";
        }

        protected override string GetMode()
        {
            Dictionary<string, int> kvp = new();

            var maxValue = string.Empty;
            var maxQty = 0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisVal = _formatter.Invoke(_dataColumn, Convert.ToDouble(row[_dataColumn.ColumnName]));
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

        protected override string GetSum()
        {
            var sum = 0.0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                sum += Convert.ToDouble(row[_dataColumn.ColumnName]);
            }

            var output = _formatter.Invoke(_dataColumn, sum);

            return $"Sum: {output}";
        }
    }
}
