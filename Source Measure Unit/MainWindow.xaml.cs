using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Axes;
using MahApps.Metro.Controls;
using OxyPlot.Series;

namespace Source_Measure_Unit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isDarkMode = false;

        private TabControl NewMeasureTab;
        private TabItem MeasureConfig;
        private TabItem RealTimeGraph;
        private StackPanel RealTimeGraphPanel;
        private PlotView GraphSMUChannel1;
        private PlotView GraphSMUChannel2;

        private TabControl CreateTechnique;
        private TabItem TechniqueEditor;
        public MainWindow()
        {
            InitializeComponent();
            ApplyTheme(); // Apply default theme (light)
        }
        private void ApplyTheme()
        {
            var theme = _isDarkMode ? "DarkTheme" : "LightTheme";
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add((ResourceDictionary)Application.LoadComponent(new Uri($"/Themes/{theme}.xaml", UriKind.Relative)));
        }

        private void ToggleDarkMode_Click(object sender, RoutedEventArgs e)
        {
            _isDarkMode = true;
            ApplyTheme();
        }

        private void ToggleLightMode_Click(object sender, RoutedEventArgs e)
        {
            _isDarkMode = false;
            ApplyTheme();
        }

        private void LoadMeasurement_Click(object sender, RoutedEventArgs e)
        {
            // Logic to load a selected measurement
            MessageBox.Show("Load Measurement Clicked");
        }

        private void DeleteMeasurement_Click(object sender, RoutedEventArgs e)
        {
            // Logic to delete a selected measurement
            MessageBox.Show("Delete Measurement Clicked");
        }

        private void ConfigureTechnique_Click(object sender, RoutedEventArgs e)
        {
            // Logic to configure the selected technique
            MessageBox.Show("Configure Technique Clicked");
        }

        private void StartMeasurement_Click(object sender, RoutedEventArgs e)
        {
            // Logic to start the measurement
            MessageBox.Show("Start Measurement Clicked");
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            // Logic to add a step to the custom technique
            MessageBox.Show("Add Step Clicked");
        }

        private void SaveTechnique_Click(object sender, RoutedEventArgs e)
        {
            // Logic to save the custom technique
            MessageBox.Show("Save Technique Clicked");
        }

        private void HistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void NewMeasure_Click(object sender, RoutedEventArgs e)
        {
            ClearEveryTabControl();
            NewMeasureTab = new TabControl
            {
                Margin = new Thickness(5)
            };
            Grid.SetRow(NewMeasureTab, 2);
            Grid.SetColumn(NewMeasureTab, 1);
            UserGrid.Children.Add(NewMeasureTab);

            MeasureConfig = new TabItem
            {
                Header = "Measure Configuration"
            };
            NewMeasureTab.Items.Add(MeasureConfig);

            RealTimeGraph = new TabItem
            {
                Header = "Real-Time Graph"
            };

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = RealTimeGraphPanel
            };

            RealTimeGraphPanel = new StackPanel 
            { 
                CanHorizontallyScroll = true,
                CanVerticallyScroll = true,
            };
            RealTimeGraphPanel.Orientation = Orientation.Horizontal;
            RealTimeGraphPanel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            RealTimeGraphPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            GraphSMUChannel1 = new PlotView
            {
                Height = 500,
                Width = 600
            };
            GraphSMUChannel2 = new PlotView
            {
                Height = 500,
                Width = 600
            };

            // Creating a plot model
            var plotModel = new PlotModel { Title = "Channel 1" };
            var plotModel2 = new PlotModel { Title = "Channel 2" };

            // Axes x and y
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Voltage (V)" });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Current (mA)" });
            plotModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Voltage (V)" });
            plotModel2.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Current (mA)" });

            LineSeries series = new LineSeries
            {
                StrokeThickness = 2
            };
            LineSeries series2 = new LineSeries
            {
                StrokeThickness = 2
            };

            /*series.Points.Add(new DataPoint(0, 0));
            series.Points.Add(new DataPoint(1, 2));
            series.Points.Add(new DataPoint(2, 4));
            series.Points.Add(new DataPoint(3, 6));
            series2.Points.Add(new DataPoint(0, 0));
            series2.Points.Add(new DataPoint(1, 2));
            */
            plotModel.Series.Add(series);
            plotModel2.Series.Add(series2);

            GraphSMUChannel1.Model = plotModel;
            GraphSMUChannel2.Model = plotModel2;

            RealTimeGraphPanel.Children.Add(GraphSMUChannel1);
            RealTimeGraphPanel.Children.Add(GraphSMUChannel2);

            scrollViewer.Content = RealTimeGraphPanel;
            RealTimeGraph.Content = scrollViewer;

            NewMeasureTab.Items.Add(RealTimeGraph);
        }

        private void CreateTechnique_Click(object sender, RoutedEventArgs e)
        {
            ClearEveryTabControl();
            CreateTechnique = new TabControl
            {
                    Margin = new Thickness(5)
            };
            Grid.SetRow(CreateTechnique, 2);
            Grid.SetColumn(CreateTechnique, 1);
            UserGrid.Children.Add(CreateTechnique);

            TechniqueEditor = new TabItem
            {
                Header = "Custom Technique Editor"
            };
            CreateTechnique.Items.Add(TechniqueEditor);           
        }

        private void ClearEveryTabControl()
        {
            UserGrid.Children.Remove(CreateTechnique);
            UserGrid.Children.Remove(NewMeasureTab);
        }

        private void SaveMeasurement_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save Measurement Clicked");
        }

        private void ExportGraph_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export Graph Clicked");
        }
    }
}
