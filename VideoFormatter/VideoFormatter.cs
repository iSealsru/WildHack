using System;
using System.Drawing;
using Emgu.CV;
namespace VideoFormatter
{
    public class VideoFormatter
    {
        public static Bitmap GetMiddleFrame(string path)
        {
            VideoCapture capture = new VideoCapture(path);
            Mat m = new Mat();
            capture.Read(m);
            var framesCount = capture.Get(Emgu.CV.CvEnum.CapProp.FrameCount);
            capture.Set(Emgu.CV.CvEnum.CapProp.PosFrames, framesCount / 2);
            return m.ToBitmap();
        }
    }
}
