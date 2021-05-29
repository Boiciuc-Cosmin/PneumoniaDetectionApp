using ObjectDetectionWPFML.Model;
using System;
using System.ComponentModel;
using System.IO;

namespace PneumoniaDetection.Api.Worker {
    public class BackgroundWorkerModel {
        private readonly BackgroundWorker backgroundWorker;
        private const string tsvFileName = "tsvFile.tsv";

        public BackgroundWorkerModel() {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            var rootPath = Directory.GetCurrentDirectory();
            ModelHandler(rootPath);
            var tsvFilePath = Path.Combine(rootPath, tsvFileName);
            ModelBuilder.CreateModel(tsvFilePath);
        }

        private void ModelHandler(string rootPath) {
            var modelPath = Path.Combine(rootPath, "MLModel.zip");
            if (File.Exists(modelPath)) {
                var nameOfBackupFolder = Path.Combine("ModelBackups", DateTime.Now.ToString("ddMMyyyyhhm"));
                var directoryInfo = Directory.CreateDirectory(nameOfBackupFolder);
                File.Copy(modelPath, Path.Combine(directoryInfo.FullName, "MLModel.zip"));
                //File.Delete(modelPath);
            }
        }
    }
}
