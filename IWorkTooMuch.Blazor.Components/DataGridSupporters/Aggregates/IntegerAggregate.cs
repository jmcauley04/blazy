using System;
using System.Collections.Generic;
using System.Data;

namespace SEG.Components.Blazor.DataGridSupporters.Aggregates
{
    public class IntegerAggregate : NumericalAggregate
    {
        public IntegerAggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter) : base(getView, column, formatter)
        {
        }

        protected override string GetMin()
        {
            int? min = null;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisVal = Convert.ToInt32(row[_dataColumn.ColumnName]);
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
            int? max = null;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisVal = Convert.ToInt32(row[_dataColumn.ColumnName]);
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
            var sum = 0;
            var count = 0.0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                count++;
                sum += Convert.ToInt32(row[_dataColumn.ColumnName]);
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
            Dictionary<int, int> kvp = new();

            var maxValue = 0;
            var maxQty = 0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                var thisVal = Convert.ToInt32(row[_dataColumn.ColumnName]);
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
                output = _formatter.Invoke(_dataColumn, maxValue);

            return $"Mode: [{maxQty}] {output}";
        }

        protected override string GetSum()
        {
            var sum = 0;

            foreach (DataRow row in _getView.Invoke().ToTable().Rows)
            {
                if (row[_dataColumn.ColumnName] is DBNull)
                    continue;

                sum += Convert.ToInt32(row[_dataColumn.ColumnName]);
            }

            var output = _formatter.Invoke(_dataColumn, sum);

            return $"Sum: {output}";
        }
    }
}
