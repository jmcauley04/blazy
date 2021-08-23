using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IWorkTooMuch.Blazor.Components.DataGridSupporters
{
    public enum Qualifier
    {
        [Display(Name = " ")]
        Ignore,

        Equals,

        [Display(Name = "Not equals")]
        NotEquals,

        On,

        [Display(Name = "Not on")]
        NotOn,

        Between,

        [Display(Name = "Not between")]
        NotBetween,

        Contains,

        [Display(Name = "Not contains")]
        NotContains,

        [Display(Name = "Starts with")]
        StartsWith,

        [Display(Name = "Ends with")]
        EndsWith,

        Before,

        After,

        [Display(Name = "Greater than")]
        GreaterThan,

        [Display(Name = "Less than")]
        LessThan,

        [Display(Name = "Greater than / equals")]
        GreaterThanOrEqualTo,

        [Display(Name = "Less than / equals")]
        LessThanOrEqualTo,
    }

    public class Filter : IEquatable<Filter>
    {
        public string ColumnName { get; set; }

        public string Clause { get; set; } = string.Empty;

        public Qualifier Qualifier { get; set; }

        public dynamic Option;

        public bool IsValid => !string.IsNullOrEmpty(Clause);

        public bool Equals(Filter other) => ColumnName == other.ColumnName && Qualifier == other.Qualifier && Clause == other.Clause;

        public override int GetHashCode() => $"{ColumnName}-{Clause}".GetHashCode();

        public static Qualifier SetQualifier(Filter filter, IEnumerable<Qualifier> qualifiers, Qualifier selectedQualifier, Func<Qualifier, string> getClauseString)
        {
            foreach (var qualifier in qualifiers.Where(x => x != Qualifier.Ignore))
                if (filter.Clause.Contains(getClauseString.Invoke(qualifier)))
                    return qualifier;

            return selectedQualifier;
        }

        public static T SetOption<T>(Filter filter, List<T> options, T selectedOption, Func<T, string> getClauseString)
        {
            foreach (var option in options)
                if (filter.Clause.Contains(getClauseString.Invoke(option)))
                    return option;

            return selectedOption;
        }

        public static Filter GetFilter(List<Filter> filters, string colName)
        {
            var filter = filters.SingleOrDefault(f => f.ColumnName == colName);

            if (filter == null)
                filter = new() { ColumnName = colName };

            return filter;
        }

        public static Filter UpdateFilter(Qualifier qualifier, dynamic option, string clause, Filter filter)
        {
            filter.Qualifier = qualifier;
            filter.Option = option;
            filter.Clause = clause;

            return filter;
        }
    }
}
