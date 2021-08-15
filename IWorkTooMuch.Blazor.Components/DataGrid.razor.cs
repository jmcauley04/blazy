using Microsoft.AspNetCore.Components;
using SEG.Components.Blazor.DataGridSupporters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SEG.Components.Blazor
{
    public enum SelectStyle
    {
        Row,
        Cell
    }

    public partial class DataGrid
    {
        #region parameters

        private DataTable dataSource;

        private DataView dataView;

        private DataView DataView
        {
            get
            {
                if (dataView == null)
                    dataView = DataSource.AsDataView();

                return dataView;
            }
        }

        [Parameter]
        public DataTable DataSource { get => dataSource; set { dataSource = value; RowCount = dataSource?.Rows.Count ?? 0; ColumnCount = dataSource?.Columns.Count ?? 0; } }

        [Parameter]
        public SelectStyle SelectStyle { get; set; } = SelectStyle.Row;

        [Parameter]
        public bool CanEditRow { get; set; }

        [Parameter]
        public bool CanDeleteRow { get; set; }

        [Parameter]
        public bool CanAddRow { get; set; }

        [Parameter]
        public bool CanDownload { get; set; }

        [Parameter]
        public bool CanImport { get; set; }

        /// <summary>
        /// Enables download template button
        /// </summary>
        [Parameter]
        public string TemplatePath { get; set; }

        [Parameter]
        public bool CanFilterColumns { get; set; }

        [Parameter]
        public bool ShowAggregateRow { get; set; }

        [Parameter]
        public RenderFragment CustomToolbarButtons { get; set; }

        /// <summary>
        /// A RowsPerPage value of 0 disables paging
        /// </summary>
        [Parameter]
        public int RowsPerPage { get; set; }

        [Parameter]
        public Dictionary<string, string> Formatters { get; set; }

        #endregion

        #region private helpers

        // dynamic data grid template style
        private string DataGridColumnStyle => $"grid-template-columns: repeat({ColumnCount}, auto);";

        private bool NeedHeader => CanEditRow || CanDeleteRow || CanAddRow || CanDownload || CanImport || !string.IsNullOrEmpty(TemplatePath) || CustomToolbarButtons != null;

        private bool NeedFooter => RowsPerPage > 0;

        private int RowCount { get; set; }

        private int ViewRowCount { get; set; }

        private int ColumnCount { get; set; }

        private int ActiveMenuColumnNum = -1;

        private DataGridAction ActiveDataGridAction;

        private int SortColumn { get; set; } = -1;

        private SortOrder SortOrder { get; set; } = SortOrder.None;

        private List<Filter> Filters { get; set; } = new();

        #endregion

        #region data grid cell/header definition logic

        private IEnumerable<HeaderDefinition> ColumnHeaderDefinitions()
        {
            var colNum = 0;

            foreach (DataColumn col in DataView.ToTable().Columns)
                yield return new HeaderDefinition()
                {
                    cssClass = $"dg-col-{colNum} {(ColumnCount - 1 == colNum ? "dg-last-col" : "")}{(0 == colNum ? "dg-first-col" : "")}",
                    value = col.ColumnName,
                    colNum = colNum++,
                    DataColumn = col,
                    columnName = col.ColumnName
                };
        }

        private IEnumerable<CellDefinition> CellValueDefinitions()
        {
            var rowNum = 0;
            var colNum = 0;

            var dv = DataSource.AsDataView();

            foreach (var filter in Filters)
                dv.RowFilter = Filters.Select(f => f.Clause).Aggregate((a, b) => $"{a} and {b}");

            if (SortColumn >= 0 && SortOrder >= 0)
            {
                var colName = DataSource.Columns[SortColumn].ColumnName;
                dv.Sort = SortOrder switch
                {
                    SortOrder.Ascending => colName,
                    SortOrder.Descending => $"{colName} desc",
                    _ => ""
                };
            }

            dataView = dv;

            ViewRowCount = dv.ToTable().Rows.Count;
            var startRow = RowsPerPage == 0 ? 0 : activePage * RowsPerPage;
            var endRow = RowsPerPage == 0 ? ViewRowCount : Math.Min(ViewRowCount, activePage * RowsPerPage + RowsPerPage);

            foreach (DataRow row in dataView.ToTable().Rows)
            {
                if (startRow <= rowNum && rowNum < endRow)
                    foreach (var cellValue in row.ItemArray)
                    {
                        string displayValue = TryApplyFormat(DataSource.Columns[colNum], cellValue);

                        yield return new CellDefinition()
                        {
                            cssClass = $"dg-{rowNum}-{colNum} {(rowNum % 2 == 0 ? "dg-even-row" : "dg-odd-row")} {(ColumnCount - 1 == colNum ? "dg-last-col" : "")}{(0 == colNum ? "dg-first-col" : "")}",
                            value = displayValue,
                            rowNum = rowNum,
                            colNum = colNum++
                        };
                    }

                colNum = 0;
                rowNum++;
            }
        }

        private List<FooterDefinition> footerDefinitions = new();

        private IEnumerable<FooterDefinition> ColumnFooterDefinitions()
        {
            var colNum = 0;

            List<FooterDefinition> activeDefinitions = new();

            foreach (DataColumn col in DataView.ToTable().Columns)
            {
                var footerDefinition = footerDefinitions.SingleOrDefault(f => f.columnName == col.ColumnName);

                var aggregate = Aggregate.TryGetAggregate(() => DataView, col, TryApplyFormat);

                if (footerDefinition == null)
                {
                    footerDefinition = new FooterDefinition()
                    {
                        cssClass = $"dg-col-{colNum} {(ColumnCount - 1 == colNum ? "dg-last-col" : "")}{(0 == colNum ? "dg-first-col" : "")}",
                        value = aggregate == null ? string.Empty : aggregate.AggregateValue(),
                        colNum = colNum++,
                        columnName = col.ColumnName,
                        aggregate = aggregate
                    };

                    footerDefinitions.Add(footerDefinition);
                }

                activeDefinitions.Add(footerDefinition);

                yield return footerDefinition;
            }

            footerDefinitions = activeDefinitions;
        }

        #endregion

        #region highlight logic

        private Tuple<int, int> hoveredCell = new(-1, -1);
        private Tuple<int, int> clickedCell = new(-1, -1);

        private string hoverClass = "dg-hover";
        private string selectClass = "dg-select";

        private void SetHoverCell(bool applyHighlight, int rowNum, int colNum)
        {
            if (!applyHighlight && hoveredCell.Item1 == rowNum)
                hoveredCell = new(-1, hoveredCell.Item2);

            else if (applyHighlight)
                hoveredCell = new(rowNum, hoveredCell.Item2);

            if (!applyHighlight && hoveredCell.Item2 == colNum)
                hoveredCell = new(hoveredCell.Item1, -1);

            else if (applyHighlight)
                hoveredCell = new(hoveredCell.Item1, colNum);
        }

        private void SetClickedCell(int rowNum, int colNum) => clickedCell = new(rowNum, colNum);

        private string GetDynamicCellClass(int rowNum, int colNum) => ($"{GetHighlightClass(rowNum, colNum)} {GetSelectClass(rowNum, colNum)}").Trim();

        private string GetHighlightClass(int rowNum, int colNum) => SelectStyle switch
        {
            SelectStyle.Row => rowNum == hoveredCell.Item1 ? hoverClass : string.Empty,
            SelectStyle.Cell => rowNum == hoveredCell.Item1 && colNum == hoveredCell.Item2 ? hoverClass : string.Empty,
            _ => string.Empty
        };

        private string GetSelectClass(int rowNum, int colNum) => SelectStyle switch
        {
            SelectStyle.Row => rowNum == clickedCell.Item1 ? selectClass : string.Empty,
            SelectStyle.Cell => rowNum == clickedCell.Item1 && colNum == clickedCell.Item2 ? selectClass : string.Empty,
            _ => string.Empty
        };

        #endregion

        #region paging logic

        private struct PageBtnDefinition
        {
            public int PageNumber;
            public string cssClass;
        }

        private IEnumerable<PageBtnDefinition> PageBtnDefinitions()
        {
            var startPage = Math.Min(Math.Max(pageCount - 5, 0), Math.Max(activePage - 2, 0));
            var endPage = Math.Min(pageCount, startPage + 5);

            for (int i = startPage; i <= endPage; i++)
                yield return new PageBtnDefinition()
                {
                    PageNumber = i,
                    cssClass = i == activePage ? "page-btn active" : "page-btn "
                };
        }

        private int activePage { get; set; }

        private int pageCount => (int)Math.Ceiling(ViewRowCount * 1.0 / RowsPerPage) - 1;

        private void SetPage(int targetPage)
        {
            activePage = targetPage;
        }

        private void FastForward(int skipAmt)
        {
            activePage = Math.Max(0, Math.Min(activePage + skipAmt, pageCount));
        }

        private void SkipToEnd(bool toEnd)
        {
            activePage = toEnd ? pageCount : 0;
        }

        #endregion

        #region formatter logic

        private string TryApplyFormat(DataColumn col, object cellValue)
        {
            var displayValue = cellValue.ToString();

            if (Formatters != null && Formatters.ContainsKey(col.ColumnName))
                displayValue = ApplyFormat(cellValue, Formatters[col.ColumnName], col.DataType);


            return displayValue;
        }

        private string ApplyFormat(object cellValue, string format, Type dataType)
        {
            if (cellValue is DBNull)
                return string.Empty;

            if (dataType == typeof(int))
                return Convert.ToInt32(cellValue).ToString(format);

            else if (dataType == typeof(DateTime))
                return Convert.ToDateTime(cellValue).ToString(format);

            else if (dataType == typeof(double))
                return Convert.ToDouble(cellValue).ToString(format);

            else if (dataType == typeof(decimal))
                return Convert.ToDecimal(cellValue).ToString(format);

            else
                return string.Format(format, cellValue);
        }

        #endregion
    }
}
