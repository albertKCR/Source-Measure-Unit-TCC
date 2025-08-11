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
using System.IO.Ports;

namespace Source_Measure_Unit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isDarkMode = false;

        private String parametersString = "";

        private String selectedSerialPort;
        private SerialPort _serialPort;

        private TabControl NewMeasureTab;
        private TabItem MeasureConfig;
        private TabItem RealTimeGraph;
        private StackPanel RealTimeGraphPanel;
        private PlotView GraphSMUChannel1;
        private PlotView GraphSMUChannel2;
        private StackPanel MeasureConfigPanel;
        private ComboBox comboBoxCh1;
        private ComboBox comboBoxCh2;
        private ComboBox COMselect;
        private Button COMconnectButton;
        private StackPanel ChannelPanel;
        private CheckBox channel1Check;
        private CheckBox channel2Check;
        private StackPanel TechniqueSelectPanel;

        private StackPanel ParametersPanel;
        private StackPanel Ch1ParametersPanel;
        private StackPanel Ch2ParametersPanel;
        private StackPanel Ch1ParametersTextPanel;
        private StackPanel Ch1ParametersLabelPanel;
        private StackPanel Ch2ParametersTextPanel;
        private StackPanel Ch2ParametersLabelPanel;

        private Button SubmitButton;

        #region LSV
        private Button LSVsubmitButton;
        private TextBox LSVtimeStepBox;
        private TextBox LSVtimeStepBox2;
        private Label LSVtimeStepLabel;
        private TextBox LSVstepVBox;
        private TextBox LSVstepVBox2;
        private Label LSVstepVLabel;
        private TextBox LSVfinalVBox;
        private TextBox LSVfinalVBox2;
        private Label LSVfinalVLabel;
        private Label LSVstartVLabel;
        private TextBox LSVstartVBox;
        private TextBox LSVstartVBox2;
        #endregion
        #region CV
        private TextBox CVtimeStepBox;
        private TextBox CVtimeStepBox2;
        private Label CVtimeStepLabel;
        private TextBox CVstepVBox;
        private TextBox CVstepVBox2;
        private Label CVstepVLabel;
        private TextBox CVfinalVBox;
        private TextBox CVfinalVBox2;
        private Label CVfinalVLabel;
        private Label CVstartVLabel;
        private TextBox CVstartVBox;
        private TextBox CVstartVBox2;
        private Label CVcycleLabel;
        private TextBox CVcycleBox;
        private TextBox CVcycleBox2;
        private Label CVpeakV1Label;
        private TextBox CVpeakV1Box;
        private TextBox CVpeakV1Box2;
        private Label CVpeakV2Label;
        private TextBox CVpeakV2Box;
        private TextBox CVpeakV2Box2;
        #endregion
        #region SWV
        private Label SWVstartVLabel;
        private TextBox SWVstartVBox;
        private TextBox SWVstartVBox2;
        private Label SWVfinalVLabel;
        private TextBox SWVfinalVBox;
        private TextBox SWVfinalVBox2;
        private Label SWVstepVLabel;
        private TextBox SWVstepVBox;
        private TextBox SWVstepVBox2;
        private Label SWVtimeStepLabel;
        private TextBox SWVtimeStepBox;
        private TextBox SWVtimeStepBox2;
        private Label SWVAmpLabel;
        private TextBox SWVAmpBox;
        private TextBox SWVAmpBox2;
        #endregion
        #region DPV
        private Label DPVstartVLabel;
        private TextBox DPVstartVBox;
        private TextBox DPVstartVBox2;
        private Label DPVfinalVLabel;
        private TextBox DPVfinalVBox;
        private TextBox DPVfinalVBox2;
        private Label DPVstepVLabel;
        private TextBox DPVstepVBox;
        private TextBox DPVstepVBox2;
        private Label DPVpulseLabel;
        private TextBox DPVpulseBox;
        private TextBox DPVpulseBox2;
        private Label DPVpulseTimeLabel;
        private TextBox DPVpulseTimeBox;
        private TextBox DPVpulseTimeBox2;
        private Label DPVbaseTimeLabel;
        private TextBox DPVbaseTimeBox;
        private TextBox DPVbaseTimeBox2;
        #endregion
        #region CP
        private Label CPcurrentLabel;
        private TextBox CPcurrentBox2;
        private TextBox CPcurrentBox;
        private Label CPsampleTLabel;
        private TextBox CPsampleTBox2;
        private TextBox CPsampleTBox;
        private Label CPsamplePLabel;
        private TextBox CPsamplePBox2;
        private TextBox CPsamplePBox;
        #endregion
        #region LSP
        private Label LSPstartILabel;
        private TextBox LSPstartIBox;
        private TextBox LSPstartIBox2;
        private Label LSPfinalILabel;
        private TextBox LSPfinalIBox;
        private TextBox LSPfinalIBox2;
        private Label LSPstepILabel;
        private TextBox LSPstepIBox;
        private TextBox LSPstepIBox2;
        private Label LSPtimeStepLabel;
        private TextBox LSPtimeStepBox;
        private TextBox LSPtimeStepBox2;
        #endregion
        #region CyP
        private Label CyPstartILabel;
        private TextBox CyPstartIBox;
        private TextBox CyPstartIBox2;
        private Label CyPfinalILabel;
        private TextBox CyPfinalIBox;
        private TextBox CyPfinalIBox2;
        private Label CyPpeakI1Label;
        private TextBox CyPpeakI1Box;
        private TextBox CyPpeakI1Box2;
        private Label CyPpeakI2Label;
        private TextBox CyPpeakI2Box;
        private TextBox CyPpeakI2Box2;
        private Label CyPstepILabel;
        private TextBox CyPstepIBox;
        private TextBox CyPstepIBox2;
        private Label CyPtimeStepLabel;
        private TextBox CyPtimeStepBox;
        private TextBox CyPtimeStepBox2;
        private Label CyPcycleLabel;
        private TextBox CyPcycleBox;
        private TextBox CyPcycleBox2;
        #endregion

        private TabControl HomeTabControl;
        private TabItem StartTab;

        private TabControl CreateTechnique;
        private TabItem TechniqueEditor;
        public MainWindow()
        {
            InitializeComponent();
            ApplyTheme(); // Apply default theme (light)

            // Create the start tab
            HomeTabControl = new TabControl
            {
                Margin = new Thickness(5)
            };
            Grid.SetRow(HomeTabControl, 2);
            Grid.SetColumn(HomeTabControl, 1);
            UserGrid.Children.Add(HomeTabControl);

            StartTab = new TabItem
            {
                Header = "Start"
            };
            HomeTabControl.Items.Add(StartTab);
            // --
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

            MeasureConfigPanel = new StackPanel
            {
                CanHorizontallyScroll = true,
                CanVerticallyScroll = true,
            };

            COMselect = new ComboBox
            {
                Width = 200,
                Margin = new Thickness(0, 0, 0, 10)
            };

            string[] ports = SerialPort.GetPortNames();

            for (int i = 0; i < ports.Length; i++)
            {
                COMselect.Items.Add(ports[i]);
            }
            MeasureConfigPanel.Children.Add(COMselect);


            COMconnectButton = new Button
            {
                Content = "Connect",
                Width = 150,
                FontSize = 15,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            COMconnectButton.Click += ToggleConnection;
            MeasureConfigPanel.Children.Add(COMconnectButton);

            ChannelPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            channel1Check = new CheckBox() { Content = "Channel 1", Margin = new Thickness(5) };
            channel2Check = new CheckBox() { Content = "Channel 2", Margin = new Thickness(5) };

            ChannelPanel.Children.Add(channel1Check);
            ChannelPanel.Children.Add(channel2Check);

            channel1Check.Click += Channel1Check_Changed;
            channel2Check.Click += Channel2Check_Changed;

            TechniqueSelectPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };


            MeasureConfigPanel.Children.Add(ChannelPanel);
            MeasureConfigPanel.Children.Add(TechniqueSelectPanel);

            ParametersPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            comboBoxCh1 = new ComboBox
            {
                Width = 200,
                Margin = new Thickness(10, 10, 10, 10)
            };

            comboBoxCh1.Items.Add(null);
            comboBoxCh1.Items.Add("Linear Sweep Voltammetry");
            comboBoxCh1.Items.Add("Cyclic Voltammetry");
            comboBoxCh1.Items.Add("Differential Pulse Voltammetry");
            comboBoxCh1.Items.Add("Square Wave Voltammetry");
            comboBoxCh1.Items.Add("Chronopotentiometry");
            comboBoxCh1.Items.Add("Linear Sweep Potentiometry");
            comboBoxCh1.Items.Add("Cyclic Potentiometry");

            TechniqueSelectPanel.Children.Add(comboBoxCh1);
            comboBoxCh1.IsEnabled = false;

            comboBoxCh1.SelectionChanged += ComboBoxCh1_SelectionChanged;

            comboBoxCh2 = new ComboBox
            {
                Width = 200,
                Margin = new Thickness(10, 10, 10, 10)
            };

            comboBoxCh2.Items.Add(null);
            comboBoxCh2.Items.Add("Linear Sweep Voltammetry");
            comboBoxCh2.Items.Add("Cyclic Voltammetry");
            comboBoxCh2.Items.Add("Differential Pulse Voltammetry");
            comboBoxCh2.Items.Add("Square Wave Voltammetry");
            comboBoxCh2.Items.Add("Chronopotentiometry");
            comboBoxCh2.Items.Add("Linear Sweep Potentiometry");
            comboBoxCh2.Items.Add("Cyclic Potentiometry");

            TechniqueSelectPanel.Children.Add(comboBoxCh2);
            comboBoxCh2.IsEnabled = false;

            comboBoxCh2.SelectionChanged += ComboBoxCh2_SelectionChanged;

            Ch1ParametersPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            Ch2ParametersPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            ParametersPanel.Children.Add(Ch1ParametersPanel);
            ParametersPanel.Children.Add(Ch2ParametersPanel);

            MeasureConfigPanel.Children.Add(ParametersPanel);

            SubmitButton = new Button
            {
                Content = "Start",
                Width = 150,
                FontSize = 15,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };
            SubmitButton.IsEnabled = false;

            MeasureConfigPanel.Children.Add(SubmitButton);
            SubmitButton.Click += SendParameters;

            MeasureConfig.Content = MeasureConfigPanel;
            NewMeasureTab.Items.Add(MeasureConfig);
            //-------------------------------
            #region Real-Time Graph
            RealTimeGraph = new TabItem
            {
                Header = "Real-Time Graph"
            };

            RealTimeGraphPanel = new StackPanel
            {
                CanHorizontallyScroll = true,
                CanVerticallyScroll = true,
            };

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = RealTimeGraphPanel
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
            #endregion
        }
        private void SendParameters(object sender, RoutedEventArgs e)
        {
            parametersString = "";
            if (channel1Check.IsChecked == true && comboBoxCh1.SelectedItem != null) 
            {
                switch (comboBoxCh1.SelectedItem.ToString())
                {
                    case "Linear Sweep Voltammetry":
                        if (string.IsNullOrEmpty(LSVstartVBox.Text) ||
                            string.IsNullOrEmpty(LSVfinalVBox.Text) ||
                            string.IsNullOrEmpty(LSVstepVBox.Text) ||
                            string.IsNullOrEmpty(LSVtimeStepBox.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString = "LSV" + ";" + LSVstartVBox.Text + ";" + LSVfinalVBox.Text + ";" + LSVstepVBox.Text + ";" + LSVtimeStepBox.Text;
                            Console.WriteLine("LSV Channel 1 Parameters Sent");
                        }
                        break;

                    case "Cyclic Voltammetry":
                        if (string.IsNullOrEmpty(CVstartVBox.Text) ||
                            string.IsNullOrEmpty(CVpeakV1Box.Text) ||
                            string.IsNullOrEmpty(CVpeakV2Box.Text) ||
                            string.IsNullOrEmpty(CVfinalVBox.Text) ||
                            string.IsNullOrEmpty(CVstepVBox.Text) ||
                            string.IsNullOrEmpty(CVtimeStepBox.Text) ||
                            string.IsNullOrEmpty(CVcycleBox.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString = "CV" + ";" + CVstartVBox.Text + ";" + CVpeakV1Box.Text + ";" + CVpeakV2Box.Text + ";" + CVfinalVBox.Text + ";" + CVstepVBox.Text + ";" + CVtimeStepBox.Text + ";" + CVcycleBox.Text;
                            Console.WriteLine("CV Channel 1 Parameters Sent");
                        }
                        break;

                    case "Differential Pulse Voltammetry":
                        if (string.IsNullOrEmpty(DPVstartVBox.Text) ||
                            string.IsNullOrEmpty(DPVfinalVBox.Text) ||
                            string.IsNullOrEmpty(DPVstepVBox.Text) ||
                            string.IsNullOrEmpty(DPVpulseBox.Text) ||
                            string.IsNullOrEmpty(DPVpulseTimeBox.Text) ||
                            string.IsNullOrEmpty(DPVbaseTimeBox.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString = "DPV" + ";" + DPVstartVBox.Text + ";" + DPVfinalVBox.Text + ";" + DPVstepVBox.Text + ";" + DPVpulseBox.Text + ";" + DPVpulseTimeBox.Text + ";" + DPVbaseTimeBox.Text;
                            Console.WriteLine("DPV Channel 1 Parameters Sent");
                        }
                        break;

                    case "Square Wave Voltammetry":
                        if (string.IsNullOrEmpty(SWVstartVBox.Text) ||
                            string.IsNullOrEmpty(SWVfinalVBox.Text) ||
                            string.IsNullOrEmpty(SWVstepVBox.Text) ||
                            string.IsNullOrEmpty(SWVAmpBox.Text) ||
                            string.IsNullOrEmpty(SWVtimeStepBox.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString = "SWV" + ";" + SWVstartVBox.Text + ";" + SWVfinalVBox.Text + ";" + SWVstepVBox.Text + ";" + SWVAmpBox.Text + ";" + SWVtimeStepBox.Text;
                            Console.WriteLine("SWV Channel 1 Parameters Sent");
                        }
                        break;

                    case "Chronopotentiometry":
                        if (string.IsNullOrEmpty(CPcurrentBox.Text) ||
                            string.IsNullOrEmpty(CPsampleTBox.Text) ||
                            string.IsNullOrEmpty(CPsamplePBox.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString = "CP" + ";" + CPcurrentBox.Text + ";" + CPsampleTBox.Text + ";" + CPsamplePBox.Text;
                            Console.WriteLine("CP Channel 1 Parameters Sent");
                        }
                        break;

                    case "Linear Sweep Potentiometry":
                        if (string.IsNullOrEmpty(LSPstartIBox.Text) ||
                            string.IsNullOrEmpty(LSPfinalIBox.Text) ||
                            string.IsNullOrEmpty(LSPstepIBox.Text) ||
                            string.IsNullOrEmpty(LSPtimeStepBox.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString = "LSP" + ";" + LSPstartIBox.Text + ";" + LSPfinalIBox.Text + ";" + LSPstepIBox.Text + ";" + LSPtimeStepBox.Text;
                            Console.WriteLine("LSP Channel 1 Parameters Sent");
                        }
                        break;

                    case "Cyclic Potentiometry":
                        if (string.IsNullOrEmpty(CyPstartIBox.Text) ||
                            string.IsNullOrEmpty(CyPpeakI1Box.Text) ||
                            string.IsNullOrEmpty(CyPpeakI2Box.Text) ||
                            string.IsNullOrEmpty(CyPfinalIBox.Text) ||
                            string.IsNullOrEmpty(CyPstepIBox.Text) ||
                            string.IsNullOrEmpty(CyPtimeStepBox.Text) ||
                            string.IsNullOrEmpty(CyPcycleBox.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString = "CyP" + ";" + CyPstartIBox.Text + ";" + CyPpeakI1Box.Text + ";" + CyPpeakI2Box.Text + ";" + CyPfinalIBox.Text + ";" + CyPstepIBox.Text + ";" + CyPtimeStepBox.Text + ";" + CyPcycleBox.Text;
                            Console.WriteLine("CyP Channel 1 Parameters Sent");
                        }
                        break;
                    default:
                        MessageBox.Show("Please select a valid technique for Channel 1.");
                        break;
                }
                parametersString += ",";
            }
            if (channel2Check.IsChecked == true && comboBoxCh2.SelectedItem != null)
            {
                switch (comboBoxCh2.SelectedItem.ToString())
                {
                    case "Linear Sweep Voltammetry":
                        if (string.IsNullOrEmpty(LSVstartVBox2.Text) ||
                            string.IsNullOrEmpty(LSVfinalVBox2.Text) ||
                            string.IsNullOrEmpty(LSVstepVBox2.Text) ||
                            string.IsNullOrEmpty(LSVtimeStepBox2.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString += "LSV" + ";" + LSVstartVBox2.Text + ";" + LSVfinalVBox2.Text + ";" + LSVstepVBox2.Text + ";" + LSVtimeStepBox2.Text;
                            Console.WriteLine("LSV Channel 2 Parameters Sent");
                        }
                        break;

                    case "Cyclic Voltammetry":
                        if (string.IsNullOrEmpty(CVstartVBox2.Text) ||
                            string.IsNullOrEmpty(CVpeakV1Box2.Text) ||
                            string.IsNullOrEmpty(CVpeakV2Box2.Text) ||
                            string.IsNullOrEmpty(CVfinalVBox2.Text) ||
                            string.IsNullOrEmpty(CVstepVBox2.Text) ||
                            string.IsNullOrEmpty(CVtimeStepBox2.Text) ||
                            string.IsNullOrEmpty(CVcycleBox2.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else { 
                        parametersString += "CV" + ";" + CVstartVBox2.Text + ";" + CVpeakV1Box2.Text + ";" + CVpeakV2Box2.Text + ";" + CVfinalVBox2.Text + ";" + CVstepVBox2.Text + ";" + CVtimeStepBox2.Text + ";" + CVcycleBox2.Text;
                        Console.WriteLine("CV Channel 2 Parameters Sent");
                        }
                        break;

                    case "Differential Pulse Voltammetry":
                        if (string.IsNullOrEmpty(DPVstartVBox2.Text) ||
                            string.IsNullOrEmpty(DPVfinalVBox2.Text) ||
                            string.IsNullOrEmpty(DPVstepVBox2.Text) ||
                            string.IsNullOrEmpty(DPVpulseBox2.Text) ||
                            string.IsNullOrEmpty(DPVpulseTimeBox2.Text) ||
                            string.IsNullOrEmpty(DPVbaseTimeBox2.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString += "DPV" + ";" + DPVstartVBox2.Text + ";" + DPVfinalVBox2.Text + ";" + DPVstepVBox2.Text + ";" + DPVpulseBox2.Text + ";" + DPVpulseTimeBox2.Text + ";" + DPVbaseTimeBox2.Text;
                            Console.WriteLine("DPV Channel 2 Parameters Sent");
                        }
                        
                        break;

                    case "Square Wave Voltammetry":
                        if (string.IsNullOrEmpty(SWVstartVBox2.Text) ||
                            string.IsNullOrEmpty(SWVfinalVBox2.Text) ||
                            string.IsNullOrEmpty(SWVstepVBox2.Text) ||
                            string.IsNullOrEmpty(SWVAmpBox2.Text) ||
                            string.IsNullOrEmpty(SWVtimeStepBox2.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else 
                        {
                            parametersString += "SWV" + ";" + SWVstartVBox2.Text + ";" + SWVfinalVBox2.Text + ";" + SWVstepVBox2.Text + ";" + SWVAmpBox2.Text + ";" + SWVtimeStepBox2.Text;
                            Console.WriteLine("SWV Channel 2 Parameters Sent");
                        }
                        
                        break;

                    case "Chronopotentiometry":
                        if (string.IsNullOrEmpty(CPcurrentBox2.Text) ||
                            string.IsNullOrEmpty(CPsampleTBox2.Text) ||
                            string.IsNullOrEmpty(CPsamplePBox2.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else 
                        {
                            parametersString += "CP" + ";" + CPcurrentBox2.Text + ";" + CPsampleTBox2.Text + ";" + CPsamplePBox2.Text;
                            Console.WriteLine("CP Channel 2 Parameters Sent");
                        }
                        
                        break;

                    case "Linear Sweep Potentiometry":
                        if (string.IsNullOrEmpty(LSPstartIBox2.Text) ||
                            string.IsNullOrEmpty(LSPfinalIBox2.Text) ||
                            string.IsNullOrEmpty(LSPstepIBox2.Text) ||
                            string.IsNullOrEmpty(LSPtimeStepBox2.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else 
                        {
                            parametersString += "LSP" + ";" + LSPstartIBox2.Text + ";" + LSPfinalIBox2.Text + ";" + LSPstepIBox2.Text + ";" + LSPtimeStepBox2.Text;
                            Console.WriteLine("LSP Channel 2 Parameters Sent");
                        }
                        
                        break;

                    case "Cyclic Potentiometry":
                        if (string.IsNullOrEmpty(CyPstartIBox2.Text) ||
                            string.IsNullOrEmpty(CyPpeakI1Box2.Text) ||
                            string.IsNullOrEmpty(CyPpeakI2Box2.Text) ||
                            string.IsNullOrEmpty(CyPfinalIBox2.Text) ||
                            string.IsNullOrEmpty(CyPstepIBox2.Text) ||
                            string.IsNullOrEmpty(CyPtimeStepBox2.Text) ||
                            string.IsNullOrEmpty(CyPcycleBox2.Text))
                        {
                            MessageBox.Show("You must fill all the parameters.", "Error");
                            break;
                        }
                        else
                        {
                            parametersString += "CyP" + ";" + CyPstartIBox2.Text + ";" + CyPpeakI1Box2.Text + ";" + CyPpeakI2Box2.Text + ";" + CyPfinalIBox2.Text + ";" + CyPstepIBox2.Text + ";" + CyPtimeStepBox2.Text + ";" + CyPcycleBox2.Text;
                            Console.WriteLine("CyP Channel 2 Parameters Sent");
                        }
                        
                        break;
                    default:
                        MessageBox.Show("Please select a valid technique for Channel 2.");
                        break;
                }
            }
            Console.WriteLine("Parameters String: " + parametersString);
        }

        private void Channel1Check_Changed(object sender, RoutedEventArgs e)
        {
            if (channel1Check.IsChecked == true)
            {
                comboBoxCh1.IsEnabled = true;
            }
            else
            {
                comboBoxCh1.IsEnabled = false;
                if (Ch1ParametersPanel != null)
                {
                    Ch1ParametersPanel.Children.Clear();
                }
                comboBoxCh1.SelectedItem = null;
                if (channel2Check.IsChecked == false) SubmitButton.IsEnabled = false;
            }
        }
        private void Channel2Check_Changed(object sender, RoutedEventArgs e)
        {
            if (channel2Check.IsChecked == true)
            {
                comboBoxCh2.IsEnabled = true;
            }
            else
            {
                comboBoxCh2.IsEnabled = false;
                if (Ch2ParametersPanel != null)
                {
                    Ch2ParametersPanel.Children.Clear();
                }
                comboBoxCh2.SelectedItem = null;
                if (channel1Check.IsChecked == false) SubmitButton.IsEnabled = false;
            }
        }
        private void ComboBoxCh1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (channel1Check.IsChecked == true)
            {
                string selectedContent = comboBoxCh1.SelectedItem.ToString();
                Console.WriteLine("LSV Selected");

                switch (selectedContent)
                {
                    case "Linear Sweep Voltammetry":
                        if (Ch1ParametersPanel != null)
                        {
                            Ch1ParametersPanel.Children.Clear();
                        }
                        LSV_CreateConfigPanelItemsCh1();
                        break;
                    case "Cyclic Voltammetry":
                        if (Ch1ParametersPanel != null)
                        {
                            Ch1ParametersPanel.Children.Clear();
                        }
                        CV_CreateConfigPanelItemsCh1();
                        break;
                    case "Differential Pulse Voltammetry":
                        if (Ch1ParametersPanel != null)
                        {
                            Ch1ParametersPanel.Children.Clear();
                        }
                        DPV_CreateConfigPanelItemsCh1();
                        break;
                    case "Square Wave Voltammetry":
                        if (Ch1ParametersPanel != null)
                        {
                            Ch1ParametersPanel.Children.Clear();
                        }
                        SWV_CreateConfigPanelItemsCh1();
                        break;
                    case "Chronopotentiometry":
                        if (Ch1ParametersPanel != null)
                        {
                            Ch1ParametersPanel.Children.Clear();
                        }
                        CP_CreateConfigPanelItemsCh1();
                        break;
                    case "Linear Sweep Potentiometry":
                        if (Ch1ParametersPanel != null)
                        {
                            Ch1ParametersPanel.Children.Clear();
                        }
                        LSP_CreateConfigPanelItemsCh1();
                        break;
                    case "Cyclic Potentiometry":
                        if (Ch1ParametersPanel != null)
                        {
                            Ch1ParametersPanel.Children.Clear();
                        }
                        CyP_CreateConfigPanelItemsCh1();
                        break;
                    default:

                        break;
                }

            }

        }
        
        private void ComboBoxCh2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (channel2Check.IsChecked == true)
            {
                string selectedContent = comboBoxCh2.SelectedItem.ToString();
                Console.WriteLine("LSV Selected");

                switch (selectedContent)
                {
                    case "Linear Sweep Voltammetry":
                        //DestroyItems
                        if (Ch2ParametersPanel != null)
                        {
                            Ch2ParametersPanel.Children.Clear();
                        }
                        LSV_CreateConfigPanelItemsCh2();
                        break;
                    case "Cyclic Voltammetry":
                        if (Ch2ParametersPanel != null)
                        {
                            Ch2ParametersPanel.Children.Clear();
                        }
                        CV_CreateConfigPanelItemsCh2();
                        break;
                    case "Differential Pulse Voltammetry":
                        if (Ch2ParametersPanel != null)
                        {
                            Ch2ParametersPanel.Children.Clear();
                        }
                        DPV_CreateConfigPanelItemsCh2();
                        break;
                    case "Square Wave Voltammetry":
                        if (Ch2ParametersPanel != null)
                        {
                            Ch2ParametersPanel.Children.Clear();
                        }
                        SWV_CreateConfigPanelItemsCh2();
                        break;
                    case "Chronopotentiometry":
                        if (Ch2ParametersPanel != null)
                        {
                            Ch2ParametersPanel.Children.Clear();
                        }
                        CP_CreateConfigPanelItemsCh2();
                        break;
                    case "Linear Sweep Potentiometry":
                        if (Ch2ParametersPanel != null)
                        {
                            Ch2ParametersPanel.Children.Clear();
                        }
                        LSP_CreateConfigPanelItemsCh2();
                        break;
                    case "Cyclic Potentiometry":
                        if (Ch2ParametersPanel != null)
                        {
                            Ch2ParametersPanel.Children.Clear();
                        }
                        CyP_CreateConfigPanelItemsCh2();
                        break;
                    default:

                        break;

                }
            }
            
        }

        private void ToggleConnection(object sender, RoutedEventArgs e)
        {
            if (_serialPort != null)
            {
                Console.WriteLine("disconnect");
                SubmitButton.IsEnabled = false;
                _serialPort.Close();
                _serialPort = null;
                COMconnectButton.Content = "Connect";
            }
            else if (!String.IsNullOrEmpty(COMselect.Text))
            {
                try
                {
                    Console.WriteLine("connect");
                    selectedSerialPort = COMselect.Text;
                    SubmitButton.IsEnabled = true;
                    InitializeSerialPort();
                    COMconnectButton.Content = "Disconnect";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar: {ex.Message}");
                }
            }
        }

        private void InitializeSerialPort()
        {
            try
            {
                _serialPort = new SerialPort(selectedSerialPort, 9600);
                _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Open();
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();
            }
            catch
            {
                MessageBox.Show("Nonexistent COM or incomplete form.", "Error");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = _serialPort.ReadExisting();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            ClearEveryTabControl();
            HomeTabControl = new TabControl
            {
                Margin = new Thickness(5)
            };
            Grid.SetRow(HomeTabControl, 2);
            Grid.SetColumn(HomeTabControl, 1);
            UserGrid.Children.Add(HomeTabControl);

            StartTab = new TabItem
            {
                Header = "Start"
            };
            HomeTabControl.Items.Add(StartTab);
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
        private void LSV_CreateConfigPanelItemsCh1()
        {
            Ch1ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersPanel.Children.Add(Ch1ParametersLabelPanel);
            Ch1ParametersPanel.Children.Add(Ch1ParametersTextPanel);

            #region Initial Voltage
            LSVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVstartVBox = new TextBox
            {
                Text = "-0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(LSVstartVLabel);
            Ch1ParametersTextPanel.Children.Add(LSVstartVBox);
            #endregion
            #region Final Voltage
            LSVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVfinalVBox = new TextBox
            {
                Text = "1",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(LSVfinalVLabel);
            Ch1ParametersTextPanel.Children.Add(LSVfinalVBox);
            #endregion
            #region Step Voltage
            LSVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVstepVBox = new TextBox
            {
                Text = "15",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(LSVstepVLabel);
            Ch1ParametersTextPanel.Children.Add(LSVstepVBox);
            #endregion
            #region Time Step
            LSVtimeStepLabel = new Label
            {
                Content = "Scan rate (mV/s)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVtimeStepBox = new TextBox
            {
                Text = "100",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };

            Ch1ParametersLabelPanel.Children.Add(LSVtimeStepLabel);
            Ch1ParametersTextPanel.Children.Add(LSVtimeStepBox);
            #endregion

            LSVsubmitButton = new Button
            {
                Content = "Start",
                Width = 150,
                FontSize = 15,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };

            //ParametersPanel.Children.Add(Ch1ParametersPanel);
            //LSVsubmitButton.Click += LSV_SendMeasureParameters;
        }
        private void LSV_CreateConfigPanelItemsCh2()
        {
            Ch2ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersPanel.Children.Add(Ch2ParametersLabelPanel);
            Ch2ParametersPanel.Children.Add(Ch2ParametersTextPanel);

            #region Initial Voltage
            LSVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVstartVBox2 = new TextBox
            {
                Text = "-0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(LSVstartVLabel);
            Ch2ParametersTextPanel.Children.Add(LSVstartVBox2);
            #endregion
            #region Final Voltage
            LSVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVfinalVBox2 = new TextBox
            {
                Text = "1",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(LSVfinalVLabel);
            Ch2ParametersTextPanel.Children.Add(LSVfinalVBox2);
            #endregion
            #region Step Voltage
            LSVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVstepVBox2 = new TextBox
            {
                Text = "15",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(LSVstepVLabel);
            Ch2ParametersTextPanel.Children.Add(LSVstepVBox2);
            #endregion
            #region Time Step
            LSVtimeStepLabel = new Label
            {
                Content = "Scan rate (mV/s)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSVtimeStepBox2 = new TextBox
            {
                Text = "20",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };

            Ch2ParametersLabelPanel.Children.Add(LSVtimeStepLabel);
            Ch2ParametersTextPanel.Children.Add(LSVtimeStepBox2);
            #endregion

            LSVsubmitButton = new Button
            {
                Content = "Start",
                Width = 150,
                FontSize = 15,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };

            //ParametersPanel.Children.Add(Ch2ParametersPanel);
            //LSVsubmitButton.Click += LSV_SendMeasureParameters;
        }
        private void CV_CreateConfigPanelItemsCh1()
        {
            Ch1ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersPanel.Children.Add(Ch1ParametersLabelPanel);
            Ch1ParametersPanel.Children.Add(Ch1ParametersTextPanel);

            #region Initial Voltage
            CVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVstartVBox = new TextBox
            {
                Text = "-0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CVstartVLabel);
            Ch1ParametersTextPanel.Children.Add(CVstartVBox);

            #endregion
            #region Peak1 Voltage
            CVpeakV1Label = new Label
            {
                Content = "Peak1 E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVpeakV1Box = new TextBox
            {
                Text = "0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CVpeakV1Label);
            Ch1ParametersTextPanel.Children.Add(CVpeakV1Box);

            #endregion
            #region Peak2 Voltage
            CVpeakV2Label = new Label
            {
                Content = "Peak2 E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVpeakV2Box = new TextBox
            {
                Text = "-0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CVpeakV2Label);
            Ch1ParametersTextPanel.Children.Add(CVpeakV2Box);
            
            #endregion
            #region Final Voltage
            CVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVfinalVBox = new TextBox
            {
                Text = "0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CVfinalVLabel);
            Ch1ParametersTextPanel.Children.Add(CVfinalVBox);

            #endregion
            #region Step Voltage
            CVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVstepVBox = new TextBox
            {
                Text = "15",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CVstepVLabel);
            Ch1ParametersTextPanel.Children.Add(CVstepVBox);

            #endregion
            #region Time Step
            CVtimeStepLabel = new Label
            {
                Content = "Scan rate (mV/s):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVtimeStepBox = new TextBox
            {
                Text = "100",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CVtimeStepLabel);
            Ch1ParametersTextPanel.Children.Add(CVtimeStepBox);

            #endregion
            #region Cycle
            CVcycleLabel = new Label
            {
                Content = "Cycles:",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVcycleBox = new TextBox
            {
                Text = "1",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CVcycleLabel);
            Ch1ParametersTextPanel.Children.Add(CVcycleBox);

            #endregion

        }
        private void CV_CreateConfigPanelItemsCh2()
        {
            Ch2ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersPanel.Children.Add(Ch2ParametersLabelPanel);
            Ch2ParametersPanel.Children.Add(Ch2ParametersTextPanel);

            #region Initial Voltage
            CVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVstartVBox2 = new TextBox
            {
                Text = "-0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CVstartVLabel);
            Ch2ParametersTextPanel.Children.Add(CVstartVBox2);

            #endregion
            #region Peak1 Voltage
            CVpeakV1Label = new Label
            {
                Content = "Peak1 E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVpeakV1Box2 = new TextBox
            {
                Text = "0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CVpeakV1Label);
            Ch2ParametersTextPanel.Children.Add(CVpeakV1Box2);

            #endregion
            #region Peak2 Voltage
            CVpeakV2Label = new Label
            {
                Content = "Peak2 E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVpeakV2Box2 = new TextBox
            {
                Text = "-0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CVpeakV2Label);
            Ch2ParametersTextPanel.Children.Add(CVpeakV2Box2);

            #endregion
            #region Final Voltage
            CVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVfinalVBox2 = new TextBox
            {
                Text = "0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CVfinalVLabel);
            Ch2ParametersTextPanel.Children.Add(CVfinalVBox2);

            #endregion
            #region Step Voltage
            CVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVstepVBox2 = new TextBox
            {
                Text = "15",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CVstepVLabel);
            Ch2ParametersTextPanel.Children.Add(CVstepVBox2);

            #endregion
            #region Time Step
            CVtimeStepLabel = new Label
            {
                Content = "Scan rate (mV/s):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVtimeStepBox2 = new TextBox
            {
                Text = "100",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CVtimeStepLabel);
            Ch2ParametersTextPanel.Children.Add(CVtimeStepBox2);

            #endregion
            #region Cycle
            CVcycleLabel = new Label
            {
                Content = "Cycles:",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CVcycleBox2 = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CVcycleLabel);
            Ch2ParametersTextPanel.Children.Add(CVcycleBox2);

            #endregion

        }
        private void SWV_CreateConfigPanelItemsCh1()
        {
            Ch1ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersPanel.Children.Add(Ch1ParametersLabelPanel);
            Ch1ParametersPanel.Children.Add(Ch1ParametersTextPanel);
            #region Initial Voltage
            SWVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVstartVBox = new TextBox
            {
                Text = "-0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(SWVstartVLabel);
            Ch1ParametersTextPanel.Children.Add(SWVstartVBox);

            #endregion
            #region Final Voltage
            SWVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVfinalVBox = new TextBox
            {
                Text = "0.7",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(SWVfinalVLabel);
            Ch1ParametersTextPanel.Children.Add(SWVfinalVBox);

            #endregion
            #region Step Voltage
            SWVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVstepVBox = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(SWVstepVLabel);
            Ch1ParametersTextPanel.Children.Add(SWVstepVBox);

            #endregion
            #region Amplitude
            SWVAmpLabel = new Label
            {
                Content = "Amplitude (mV)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVAmpBox = new TextBox
            {
                Text = "20",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(SWVAmpLabel);
            Ch1ParametersTextPanel.Children.Add(SWVAmpBox);

            #endregion

            #region Time Step
            SWVtimeStepLabel = new Label
            {
                Content = "Frequency (Hz)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVtimeStepBox = new TextBox
            {
                Text = "25",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(SWVtimeStepLabel);
            Ch1ParametersTextPanel.Children.Add(SWVtimeStepBox);

            #endregion

        }
        private void SWV_CreateConfigPanelItemsCh2()
        {
            Ch2ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersPanel.Children.Add(Ch2ParametersLabelPanel);
            Ch2ParametersPanel.Children.Add(Ch2ParametersTextPanel);
            #region Initial Voltage
            SWVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVstartVBox2 = new TextBox
            {
                Text = "-0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(SWVstartVLabel);
            Ch2ParametersTextPanel.Children.Add(SWVstartVBox2);

            #endregion
            #region Final Voltage
            SWVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVfinalVBox2 = new TextBox
            {
                Text = "0.7",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(SWVfinalVLabel);
            Ch2ParametersTextPanel.Children.Add(SWVfinalVBox2);

            #endregion
            #region Step Voltage
            SWVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVstepVBox2 = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(SWVstepVLabel);
            Ch2ParametersTextPanel.Children.Add(SWVstepVBox2);

            #endregion
            #region Amplitude
            SWVAmpLabel = new Label
            {
                Content = "Amplitude (mV)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVAmpBox2 = new TextBox
            {
                Text = "20",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(SWVAmpLabel);
            Ch2ParametersTextPanel.Children.Add(SWVAmpBox2);

            #endregion

            #region Time Step
            SWVtimeStepLabel = new Label
            {
                Content = "Frequency (Hz)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            SWVtimeStepBox2 = new TextBox
            {
                Text = "15",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(SWVtimeStepLabel);
            Ch2ParametersTextPanel.Children.Add(SWVtimeStepBox2);

            #endregion

        }
        private void DPV_CreateConfigPanelItemsCh1()
        {
            Ch1ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersPanel.Children.Add(Ch1ParametersLabelPanel);
            Ch1ParametersPanel.Children.Add(Ch1ParametersTextPanel);
            #region Initial Voltage
            DPVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVstartVBox = new TextBox
            {
                Text = "-0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(DPVstartVLabel);
            Ch1ParametersTextPanel.Children.Add(DPVstartVBox);

            #endregion
            #region Final Voltage
            DPVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVfinalVBox = new TextBox
            {
                Text = "0.7",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(DPVfinalVLabel);
            Ch1ParametersTextPanel.Children.Add(DPVfinalVBox);

            #endregion
            #region Step Voltage
            DPVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVstepVBox = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(DPVstepVLabel);
            Ch1ParametersTextPanel.Children.Add(DPVstepVBox);

            #endregion
            #region Pulse
            DPVpulseLabel = new Label
            {
                Content = "Pulse E (mV)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVpulseBox = new TextBox
            {
                Text = "20",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(DPVpulseLabel);
            Ch1ParametersTextPanel.Children.Add(DPVpulseBox);

            #endregion

            #region Pulse time
            DPVpulseTimeLabel = new Label
            {
                Content = "Pulse Time (ms)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVpulseTimeBox = new TextBox
            {
                Text = "25",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(DPVpulseTimeLabel);
            Ch1ParametersTextPanel.Children.Add(DPVpulseTimeBox);

            #endregion
            #region Base time
            DPVbaseTimeLabel = new Label
            {
                Content = "Base Time (ms)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVbaseTimeBox = new TextBox
            {
                Text = "25",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(DPVbaseTimeLabel);
            Ch1ParametersTextPanel.Children.Add(DPVbaseTimeBox);

            #endregion

        }
        private void DPV_CreateConfigPanelItemsCh2()
        {
            Ch2ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersPanel.Children.Add(Ch2ParametersLabelPanel);
            Ch2ParametersPanel.Children.Add(Ch2ParametersTextPanel);
            #region Initial Voltage
            DPVstartVLabel = new Label
            {
                Content = "Initial E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVstartVBox2 = new TextBox
            {
                Text = "-0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(DPVstartVLabel);
            Ch2ParametersTextPanel.Children.Add(DPVstartVBox2);

            #endregion
            #region Final Voltage
            DPVfinalVLabel = new Label
            {
                Content = "Final E (V):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVfinalVBox2 = new TextBox
            {
                Text = "0.7",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(DPVfinalVLabel);
            Ch2ParametersTextPanel.Children.Add(DPVfinalVBox2);

            #endregion
            #region Step Voltage
            DPVstepVLabel = new Label
            {
                Content = "Step size (mV):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVstepVBox2 = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(DPVstepVLabel);
            Ch2ParametersTextPanel.Children.Add(DPVstepVBox2);

            #endregion
            #region Pulse
            DPVpulseLabel = new Label
            {
                Content = "Pulse E (mV)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVpulseBox2 = new TextBox
            {
                Text = "20",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(DPVpulseLabel);
            Ch2ParametersTextPanel.Children.Add(DPVpulseBox2);

            #endregion

            #region Pulse time
            DPVpulseTimeLabel = new Label
            {
                Content = "Pulse Time (ms)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVpulseTimeBox2 = new TextBox
            {
                Text = "25",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(DPVpulseTimeLabel);
            Ch2ParametersTextPanel.Children.Add(DPVpulseTimeBox2);

            #endregion
            #region Base time
            DPVbaseTimeLabel = new Label
            {
                Content = "Base Time (ms)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            DPVbaseTimeBox2 = new TextBox
            {
                Text = "15",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(DPVbaseTimeLabel);
            Ch2ParametersTextPanel.Children.Add(DPVbaseTimeBox2);

            #endregion

        }
        private void CP_CreateConfigPanelItemsCh1()
        {
            Ch1ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersPanel.Children.Add(Ch1ParametersLabelPanel);
            Ch1ParametersPanel.Children.Add(Ch1ParametersTextPanel);
            #region Initial Voltage
            CPcurrentLabel = new Label
            {
                Content = "Current (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CPcurrentBox = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CPcurrentLabel);
            Ch1ParametersTextPanel.Children.Add(CPcurrentBox);

            #endregion
            #region Final Voltage
            CPsampleTLabel = new Label
            {
                Content = "Sample time (ms):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CPsampleTBox = new TextBox
            {
                Text = "10",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CPsampleTLabel);
            Ch1ParametersTextPanel.Children.Add(CPsampleTBox);

            #endregion
            #region Step Voltage
            CPsamplePLabel = new Label
            {
                Content = "Sample Period (s):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CPsamplePBox = new TextBox
            {
                Text = "45",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CPsamplePLabel);
            Ch1ParametersTextPanel.Children.Add(CPsamplePBox);

            #endregion

        }
        private void CP_CreateConfigPanelItemsCh2()
        {
            Ch2ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersPanel.Children.Add(Ch2ParametersLabelPanel);
            Ch2ParametersPanel.Children.Add(Ch2ParametersTextPanel);
            #region Initial Voltage
            CPcurrentLabel = new Label
            {
                Content = "Current (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CPcurrentBox2 = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CPcurrentLabel);
            Ch2ParametersTextPanel.Children.Add(CPcurrentBox2);

            #endregion
            #region Final Voltage
            CPsampleTLabel = new Label
            {
                Content = "Sample time (ms):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CPsampleTBox2 = new TextBox
            {
                Text = "10",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CPsampleTLabel);
            Ch2ParametersTextPanel.Children.Add(CPsampleTBox2);

            #endregion
            #region Step Voltage
            CPsamplePLabel = new Label
            {
                Content = "Sample Period (s):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CPsamplePBox2 = new TextBox
            {
                Text = "30",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CPsamplePLabel);
            Ch2ParametersTextPanel.Children.Add(CPsamplePBox2);

            #endregion

        }
        private void LSP_CreateConfigPanelItemsCh1()
        {
            Ch1ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersPanel.Children.Add(Ch1ParametersLabelPanel);
            Ch1ParametersPanel.Children.Add(Ch1ParametersTextPanel);

            #region Initial Voltage
            LSPstartILabel = new Label
            {
                Content = "Initial I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPstartIBox = new TextBox
            {
                Text = "-3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(LSPstartILabel);
            Ch1ParametersTextPanel.Children.Add(LSPstartIBox);
            #endregion
            #region Final Voltage
            LSPfinalILabel = new Label
            {
                Content = "Final I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPfinalIBox = new TextBox
            {
                Text = "5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(LSPfinalILabel);
            Ch1ParametersTextPanel.Children.Add(LSPfinalIBox);
            #endregion
            #region Step Current
            LSPstepILabel = new Label
            {
                Content = "Step size (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPstepIBox = new TextBox
            {
                Text = "0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(LSPstepILabel);
            Ch1ParametersTextPanel.Children.Add(LSPstepIBox);
            #endregion
            #region Time Step
            LSPtimeStepLabel = new Label
            {
                Content = "Scan rate (mA/s)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPtimeStepBox = new TextBox
            {
                Text = "100",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };

            Ch1ParametersLabelPanel.Children.Add(LSPtimeStepLabel);
            Ch1ParametersTextPanel.Children.Add(LSPtimeStepBox);
            #endregion
        }
        private void LSP_CreateConfigPanelItemsCh2()
        {
            Ch2ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersPanel.Children.Add(Ch2ParametersLabelPanel);
            Ch2ParametersPanel.Children.Add(Ch2ParametersTextPanel);

            #region Initial Voltage
            LSPstartILabel = new Label
            {
                Content = "Initial I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPstartIBox2 = new TextBox
            {
                Text = "-1",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(LSPstartILabel);
            Ch2ParametersTextPanel.Children.Add(LSPstartIBox2);
            #endregion
            #region Final Voltage
            LSPfinalILabel = new Label
            {
                Content = "Final I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPfinalIBox2 = new TextBox
            {
                Text = "7",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(LSPfinalILabel);
            Ch2ParametersTextPanel.Children.Add(LSPfinalIBox2);
            #endregion
            #region Step Current
            LSPstepILabel = new Label
            {
                Content = "Step size (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPstepIBox2 = new TextBox
            {
                Text = "0.1",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(LSPstepILabel);
            Ch2ParametersTextPanel.Children.Add(LSPstepIBox2);
            #endregion
            #region Time Step
            LSPtimeStepLabel = new Label
            {
                Content = "Scan rate (mA/s)",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            LSPtimeStepBox2 = new TextBox
            {
                Text = "10",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };

            Ch2ParametersLabelPanel.Children.Add(LSPtimeStepLabel);
            Ch2ParametersTextPanel.Children.Add(LSPtimeStepBox2);
            #endregion
        }
        private void CyP_CreateConfigPanelItemsCh1()
        {
            Ch1ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch1ParametersPanel.Children.Add(Ch1ParametersLabelPanel);
            Ch1ParametersPanel.Children.Add(Ch1ParametersTextPanel);

            #region Initial Voltage
            CyPstartILabel = new Label
            {
                Content = "Initial I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPstartIBox = new TextBox
            {
                Text = "-0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CyPstartILabel);
            Ch1ParametersTextPanel.Children.Add(CyPstartIBox);

            #endregion
            #region Peak1 Current
            CyPpeakI1Label = new Label
            {
                Content = "Peak1 I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPpeakI1Box = new TextBox
            {
                Text = "0.5",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CyPpeakI1Label);
            Ch1ParametersTextPanel.Children.Add(CyPpeakI1Box);

            #endregion
            #region Peak2 Current
            CyPpeakI2Label = new Label
            {
                Content = "Peak2 I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPpeakI2Box = new TextBox
            {
                Text = "-0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CyPpeakI2Label);
            Ch1ParametersTextPanel.Children.Add(CyPpeakI2Box);

            #endregion
            #region Final Current
            CyPfinalILabel = new Label
            {
                Content = "Final I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPfinalIBox = new TextBox
            {
                Text = "0.3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CyPfinalILabel);
            Ch1ParametersTextPanel.Children.Add(CyPfinalIBox);

            #endregion
            #region Step Current
            CyPstepILabel = new Label
            {
                Content = "Step size (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPstepIBox = new TextBox
            {
                Text = "15",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CyPstepILabel);
            Ch1ParametersTextPanel.Children.Add(CyPstepIBox);

            #endregion
            #region Time Step
            CyPtimeStepLabel = new Label
            {
                Content = "Scan rate (mV/s):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPtimeStepBox = new TextBox
            {
                Text = "100",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CyPtimeStepLabel);
            Ch1ParametersTextPanel.Children.Add(CyPtimeStepBox);

            #endregion
            #region Cycle
            CyPcycleLabel = new Label
            {
                Content = "Cycles:",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPcycleBox = new TextBox
            {
                Text = "1",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch1ParametersLabelPanel.Children.Add(CyPcycleLabel);
            Ch1ParametersTextPanel.Children.Add(CyPcycleBox);

            #endregion

        }
        private void CyP_CreateConfigPanelItemsCh2()
        {
            Ch2ParametersLabelPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersTextPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };

            Ch2ParametersPanel.Children.Add(Ch2ParametersLabelPanel);
            Ch2ParametersPanel.Children.Add(Ch2ParametersTextPanel);

            #region Initial Voltage
            CyPstartILabel = new Label
            {
                Content = "Initial I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPstartIBox2 = new TextBox
            {
                Text = "-7",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CyPstartILabel);
            Ch2ParametersTextPanel.Children.Add(CyPstartIBox2);

            #endregion
            #region Peak1 Current
            CyPpeakI1Label = new Label
            {
                Content = "Peak1 I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPpeakI1Box2 = new TextBox
            {
                Text = "4",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CyPpeakI1Label);
            Ch2ParametersTextPanel.Children.Add(CyPpeakI1Box2);

            #endregion
            #region Peak2 Current
            CyPpeakI2Label = new Label
            {
                Content = "Peak2 I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPpeakI2Box2 = new TextBox
            {
                Text = "-2",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CyPpeakI2Label);
            Ch2ParametersTextPanel.Children.Add(CyPpeakI2Box2);

            #endregion
            #region Final Current
            CyPfinalILabel = new Label
            {
                Content = "Final I (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPfinalIBox2 = new TextBox
            {
                Text = "6",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CyPfinalILabel);
            Ch2ParametersTextPanel.Children.Add(CyPfinalIBox2);

            #endregion
            #region Step Current
            CyPstepILabel = new Label
            {
                Content = "Step size (mA):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPstepIBox2 = new TextBox
            {
                Text = "30",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CyPstepILabel);
            Ch2ParametersTextPanel.Children.Add(CyPstepIBox2);

            #endregion
            #region Time Step
            CyPtimeStepLabel = new Label
            {
                Content = "Scan rate (mV/s):",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPtimeStepBox2 = new TextBox
            {
                Text = "25",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CyPtimeStepLabel);
            Ch2ParametersTextPanel.Children.Add(CyPtimeStepBox2);

            #endregion
            #region Cycle
            CyPcycleLabel = new Label
            {
                Content = "Cycles:",
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };

            CyPcycleBox2 = new TextBox
            {
                Text = "3",
                Width = 100,
                Margin = new Thickness(5),
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                FontSize = 17,
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
            };
            Ch2ParametersLabelPanel.Children.Add(CyPcycleLabel);
            Ch2ParametersTextPanel.Children.Add(CyPcycleBox2);

            #endregion

        }
    }
}
