using System;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Xml.Serialization;
using DevExpress.Xpf.PivotGrid;

namespace DXPivotGrid_CustomGroupIntervals {
    public partial class MainPage : UserControl {
        string dataFileName = "DXPivotGrid_CustomGroupIntervals.nwind.xml";
        public MainPage() {
            InitializeComponent();

            // Parses an XML file and creates a collection of data items.
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataFileName);
            XmlSerializer s = new XmlSerializer(typeof(OrderData));
            object dataSource = s.Deserialize(stream);

            // Binds a pivot grid to this collection.
            pivotGridControl1.DataSource = dataSource;
        }
        private void pivotGridControl1_CustomGroupInterval(object sender, 
                PivotCustomGroupIntervalEventArgs e) {
            if (!object.ReferenceEquals(e.Field, fieldCustomerGroup)) return;
            string customerName = Convert.ToString(e.Value);
            if (customerName[0] < 'F')
                e.GroupValue = "A-E";
            else if (customerName[0] > 'E' && customerName[0] < 'T')
                e.GroupValue = "F-S";
            else if (customerName[0] > 'S')
                e.GroupValue = "T-Z";
        }
    }
}