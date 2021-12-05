using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;

namespace VideoCreating
{

    public class VideoCreate
    {
        public static bool IsBussy = false;
        public static string Path;
        public static string PathToModel;
        public string Extension { get => new FileInfo(Path).Extension; }
        public VideoCreate(string path, string pathToModel)
        {
            Path = path;
            PathToModel = pathToModel;
        }
        /// <summary>
        /// Возвращает словарь с таймодами для обрезки видео
        /// </summary>
        /// <param name="Path">Путь к видео для обработки</param>
        /// <returns></returns>
        public Dictionary<string, string> GetKeyValues(string Path)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            VideoCapture capture = new VideoCapture(Path);
            Mat m = new Mat();
            string start = "";

            uint FrameRate = (uint)Math.Truncate(capture.Get(Emgu.CV.CvEnum.CapProp.Fps));

            for (uint i = 0; i < capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount); i += FrameRate)
            {
                try
                {
                    capture.Set(Emgu.CV.CvEnum.CapProp.PosFrames, i);
                    capture.Read(m);
                    Bitmap bitmap = new Bitmap(m.ToBitmap());
                    if (GetBool(bitmap))
                    {
                        if (!IsBussy)
                        {
                            start = $"{(i / FrameRate)}";
                            IsBussy = true;
                        }
                    }
                    else
                    {
                        if (IsBussy)
                        {
                            keyValues.Add(start, $"{(i / FrameRate)}");
                            IsBussy = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
            if (IsBussy)
            {
                keyValues.Add(start, (capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount) / FrameRate).ToString());
            }
            return keyValues;
        }

        public void CreateNewVideo(string outputPath)
        {
            try
            {
                int i = 1;
                Dictionary<string, string> keyValues = GetKeyValues(Path);
                foreach (var item in keyValues)
                {
                    string[] getname = Path.Split('\\');
                    string arguments = $"main.py \"{Path}\" \"{outputPath}\\{getname[getname.Length - 1]}_{i}{Extension}\" {item.Key} {item.Value}";
                    Process.Start("cmd.exe", "/C " + arguments);
                    i++;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public bool GetBool(Bitmap bitmap)
        {
            try
            {
                using var scorer = new YoloScorer<YoloCocoP5Model>(PathToModel);

                List<YoloPrediction> predictions = scorer.Predict(bitmap);

                if (predictions.Count != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
    }

}
