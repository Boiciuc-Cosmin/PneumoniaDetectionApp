namespace PneumoniaDetection.Api.Dtos {
    public class PredictionResultDto {
        public string Prediction { get; set; }
        public float NormalScore { get; set; }
        public float PneumoniaScore { get; set; }
    }
}
