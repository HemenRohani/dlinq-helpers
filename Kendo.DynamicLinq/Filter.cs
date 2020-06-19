using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Kendo.DynamicLinq
{
    public class Filter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public string Logic { get; set; }
        public IEnumerable<Filter> Filters { get; set; }

        /// <summary>
        /// Mapping of Kendo DataSource filtering operators to Dynamic Linq
        /// </summary>
        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "Contains"}
        };

        /// <summary>
        /// Get a flattened list of all child filter expressions.
        /// </summary>
        public IList<Filter> All()
        {
            var filters = new List<Filter>();

            Collect(filters);

            return filters;
        }

        private void Collect(IList<Filter> filters)
        {
            filters.Add(this);
            if (Filters != null && Filters.Any())
            {
                foreach (Filter filter in Filters)
                {
                    filters.Add(filter);

                    filter.Collect(filters);
                }
            }
        }

        /// <summary>
        /// Converts the filter expression to a predicate suitable for Dynamic Linq e.g. "Field1 = @1 and Field2.Contains(@2)"
        /// </summary>
        /// <param name="filters">A list of flattened filters.</param>
        public string ToExpression(IList<Filter> filters)
        {
           
            int index = filters.IndexOf(this);

            string comparison = operators[Operator];
            StringBuilder sb = new StringBuilder();
            
            if (Operator == "doesnotcontain")
            {
                sb.Append(String.Format("!{0}.{1}(@{2})", Field, comparison, index));
            }
            else if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Contains")
            {
                sb.Append(String.Format("{0}.{1}(@{2})", Field, comparison, index));
            }
            else
                sb.Append(String.Format("{0} {1} @{2}", Field, comparison, index));


            if (Filters != null && Filters.Any())
            {
                sb.Insert(0,"(");
                sb.Append($" {Logic} ");
                sb.Append( String.Join(" " + Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) + ")");
            }
            return sb.ToString();
        }
    }
}
