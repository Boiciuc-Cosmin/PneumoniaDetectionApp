using Microsoft.Win32;
using ObjectDetectionWPFML.Model;
using System;
using System.Windows;

namespace PneumoniaDetection {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void AddImage_Clicked(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string fileName = string.Empty;
            if (openFileDialog.ShowDialog() == true) {
                fileName = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                //Image.Source = new BitmapImage(fileUri);
            }
            Predict(fileName);
        }

        private void Predict(string path) {
            ModelInput sampleData = new ModelInput() {
                ImageSource = path
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);
            //PredictionBox.Text = "Rezultat: " + predictionResult.Prediction;
            //Score.Text = "Prediction Score:\nNormal [ " + predictionResult.Score[0].ToString() + " ] \nPneumonia [ " + predictionResult.Score[1].ToString() + " ]";
        }

        private void Canvas_Drop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && e.Data.GetData(DataFormats.FileDrop) is string[] files) {

            }
        }

        #region ToolBarButtons
        private void ButtonMinimizeWindow_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void Header_MouseDown(object sender, RoutedEventArgs e) {
            DragMove();
        }

        private void ButtonCloseWindow_Click(object sender, RoutedEventArgs e) {
            Close();
        }
        #endregion
    }
}
