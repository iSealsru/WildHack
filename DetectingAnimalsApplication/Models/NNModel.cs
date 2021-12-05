using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;

namespace DetectingAnimalsApplication.Models
{
    public class NNModel
    {
        private static string savePath = "";
        public static void Predict(string imagePath, string absolutePath)
        {
            savePath = absolutePath;
            var image = Image.FromFile(imagePath);
            using var scorer = new YoloScorer<YoloCocoP5Model>( Environment.CurrentDirectory + "\\Weights\\best.onnx");
            try
            {
                List<YoloPrediction> predictions = scorer.Predict(image);
                if(predictions.Count == 0)
                {
                    var str = savePath + "\\bad\\empty";
                    if (!Directory.Exists(savePath + "\\bad"))
                    {
                        Directory.CreateDirectory(savePath + "\\bad");
                    }
                    File.Copy(imagePath, NameNumerator(str));
                }
                else
                {
                    foreach (var prediction in predictions)
                    {
                        string predictQuality = "";
                        if (prediction.Score > 0.6)
                            predictQuality = "good";
                        else if (prediction.Score > 0.3)
                            predictQuality = "middle";
                        else
                            predictQuality = "bad";

                        string animal = prediction.Label.Name;
                        string fullPath = Path.Combine(savePath, predictQuality, animal);
                        if (animal == "person")
                        {
                            if (!Directory.Exists(fullPath))
                            {
                                Directory.CreateDirectory(fullPath);
                                fullPath += "\\person";
                            }
                        }
                        if (!Directory.Exists(Path.Combine(savePath, predictQuality)))
                        {
                            Directory.CreateDirectory(Path.Combine(savePath, predictQuality));
                        }
                        File.Copy(imagePath,NameNumerator(fullPath));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private static string NameNumerator(string path)
        {
            var i = 1;
            string? str;
            do
            {
                str = path + $"_{i}.jpg";
                i++;
            } while (File.Exists(str));
            return str;  
        }
    }
}
