using System;
using System.Collections.Generic;
using System.Data;

namespace SEG.Components.Blazor.DataGridSupporters.Aggregates
{
    public abstract class NumericalAggregate : Aggregate
    {
        public override AggregateOption SelectedAggregate { get; set; } = AggregateOption.Sum;

        public NumericalAggregate(Func<DataView> getView, DataColumn column, Func<DataColumn, object, string> formatter) : base(getView, column, formatter)
        {
        }

        public override string AggregateValue() => SelectedAggregate switch
        {
            AggregateOption.None => base.GetNone(),
            AggregateOption.Count => base.GetCount(),
            AggregateOption.Min => GetMin(),
            AggregateOption.Max => GetMax(),
            AggregateOption.Mean => GetMean(),
            AggregateOption.Mode => GetMode(),
            AggregateOption.Sum => GetSum(),
            _ => throw new NotImplementedException($"{SelectedAggregate} not implemented for NumericalAggregate")
        };

        public override List<AggregateOption> GetAggregateOptions() =>
            new()
            {
                AggregateOption.None,
                AggregateOption.Count,
                AggregateOption.Min,
                AggregateOption.Max,
                AggregateOption.Mean,
                AggregateOption.Mode,
                AggregateOption.Sum,
            };

        protected abstract string GetMin();

        protected abstract string GetMax();

        protected abstract string GetMean();

        protected abstract string GetMode();

        protected abstract string GetSum();
    }
}
