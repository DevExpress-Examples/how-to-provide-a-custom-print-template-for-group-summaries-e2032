using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;

namespace Printing_GroupSummaryAlignment {

    public partial class Window1 : Window {

        public Window1() {

            InitializeComponent();

            List<TestData> list = new List<TestData>();
            for (int i = 0; i < 100; i++) {
                list.Add(new TestData() {
                    Number1 = i,
                    Number2 = (i + 10) / 10,
                    Text1 = "row " + i,
                    Text2 = "ROW " + i
                });
            }
            DataContext = list;
        }

        private void showPreviewButton_Click(object sender, RoutedEventArgs e) {
            view.ShowPrintPreviewDialog(this);
        }
    }

    public class TestData {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
    }

    public class CustomGroupRowContentControl : GroupRowContentControl {
        GroupRowData GroupRowData { get { return RowData.GetRowData(this) as GroupRowData; } }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            TextEdit edit = GetTemplateChild("PART_Edit") as TextEdit;
            Panel panel = GetTemplateChild("PART_Panel") as Panel;
            TextEdit summary = GetTemplateChild("PART_Summary") as TextEdit;

            panel.Width = edit.Width;

            edit.ClearValue(FrameworkElement.WidthProperty);
            edit.BorderThickness = new Thickness(edit.BorderThickness.Left,
                edit.BorderThickness.Top, 0, edit.BorderThickness.Bottom);

            summary.Style = View.PrintGroupRowStyle;
            summary.BorderThickness = new Thickness(0, edit.BorderThickness.Top,
                summary.BorderThickness.Right, edit.BorderThickness.Bottom);
            summary.EditValue = GetSummaryText();
        }

        protected override string GetGroupRowText() {
            return string.Format("{0}: {1}", GroupRowData.GroupValue.Column.HeaderCaption,
                GroupRowData.GroupValue.Value);
        }

        string GetSummaryText() {
            string res = string.Empty;
            foreach (GridGroupSummaryData item in GroupRowData.GroupSummaryData) {
                res += item.Text;
                if (!item.IsLast)
                    res += ", ";
            }
            return res;
        }
    }
}
