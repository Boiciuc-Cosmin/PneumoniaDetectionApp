// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML;
using System;
using System.IO;

namespace ObjectDetectionWPFML.Model {
    public class ConsumeModel {
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        public static string MLNetModelPath = Path.GetFullPath("MLModel.zip");

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static ModelOutput Predict(ModelInput input) {
            ModelOutput result = PredictionEngine.Value.Predict(input);
            Console.WriteLine($"\n\nPredicted Label value {result.Prediction} \nPredicted Label scores: [{String.Join(",", result.Score)}]\n\n");
            return result;
        }

        public static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine() {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }
    }
}
