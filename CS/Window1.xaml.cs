using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System.Collections;
using DevExpress.Xpf.Core.Native;
using System.ComponentModel;

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

    public class CollectionConverterDecorator : Border, IWeakEventListener {


        public string SummaryText {
            get { return (string)GetValue(SummaryTextProperty); }
            set { SetValue(SummaryTextProperty, value); }
        }

        public static readonly DependencyProperty SummaryTextProperty =
            DependencyProperty.Register("SummaryText", typeof(string), typeof(CollectionConverterDecorator), new PropertyMetadata(null));

        public IList Collection {
            get { return (IList)GetValue(CollectionProperty); }
            set { SetValue(CollectionProperty, value); }
        }
        public static readonly DependencyProperty CollectionProperty =
            DependencyProperty.Register("Collection", typeof(IList), typeof(CollectionConverterDecorator), new PropertyMetadata(null, OnCollectionChanged));

        static void OnCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((CollectionConverterDecorator)d).OnCollectionChanged();
        }
        IBindingList bindingList;
        void OnCollectionChanged() {
            if(bindingList != null)
                ListChangedEventManager.RemoveListener(bindingList, this);
            bindingList = BindingListAdapter.CreateFromList(Collection, ItemPropertyNotificationMode.All);
            if(bindingList != null)
                ListChangedEventManager.AddListener(bindingList, this);
        }

        #region IWeakEventListener Members

        bool IWeakEventListener.ReceiveWeakEvent(System.Type managerType, object sender, System.EventArgs e) {
            if(managerType == typeof(ListChangedEventManager)) {
                InvalidateMeasure();
                return true;
            }
            return false;
        }

        #endregion
        protected override Size MeasureOverride(Size constraint) {
            if(bindingList != null) {
                SummaryText = GetSummaryText();
            }
            return base.MeasureOverride(constraint);
        }
        string GetSummaryText() {
            string res = string.Empty;
            foreach(GridGroupSummaryData item in bindingList) {
                res += item.Text;
                if(!item.IsLast)
                    res += ", ";
            }
            return res;
        }

    }
}
