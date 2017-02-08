//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------
using System;
using System.Windows;
using System.ComponentModel;
using System.IO;
using EyeXFramework.Wpf;

namespace UserPresenceWpf
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class GazeData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _gazeX;
        private int _gazeY;
        private int _time;
        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public int gazeX
        {
            get
            {
                return _gazeX;
            }
            set
            {
                _gazeX = value;
                OnPropertyChanged("gazeX");
            }
        }
        public int gazeY
        {
            get
            {
                return _gazeY;
            }
            set
            {
                _gazeY = value;
                OnPropertyChanged("gazeY");
            }
        }
        public int time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("time");
            }
        }
    }

    public partial class MainWindow : Window
    {
        private WpfEyeXHost _eyeXHost;
        public GazeData publicGazeData = new GazeData();
        public bool userPresent;
        public string initialTime;
        public string outputFileDirectory;
        public MainWindow()
        {
            _eyeXHost = new WpfEyeXHost();
            _eyeXHost.Start();
            InitializeComponent();

            gazeDataTextX.DataContext = publicGazeData;
            gazeDataTextY.DataContext = publicGazeData;
            gazeDataTextTime.DataContext = publicGazeData;

            var stream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);

            string exeRuntimeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            outputFileDirectory =
            Path.Combine(exeRuntimeDirectory, "Output");
                    if (!System.IO.Directory.Exists(outputFileDirectory))
                    {
                        // Output directory does not exist, so create it.
                        System.IO.Directory.CreateDirectory(outputFileDirectory);
                    }
            Console.WriteLine(exeRuntimeDirectory);
            Console.WriteLine(outputFileDirectory);
            stream.Next += (s, e) => updateGazeData((int)e.X, (int)e.Y, (int)e.Timestamp);
            File.WriteAllText(outputFileDirectory + @"\gazeDataOutput.csv", "X Gaze Data, Y Gaze Data, Time \r\n");
        }

        private void updateGazeData(int x, int y, int time)
        {
            publicGazeData.gazeX = x;
            publicGazeData.gazeY = y;
            publicGazeData.time = time;

            string csvFormattedGazeData = x.ToString() + "," + y.ToString() + "," + time.ToString();
            writeDataToFile(csvFormattedGazeData);
        }

        private void writeDataToFile(string text)
        {
            using (StreamWriter sw = File.AppendText(outputFileDirectory + @"\gazeDataOutput.csv"))
            {
                sw.WriteLine(text);
            }
        }
    }
}
