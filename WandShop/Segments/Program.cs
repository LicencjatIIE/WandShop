using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segments
{
    public class Program
    {
        static void Main(string[] args)
        {
            GenerateSegments(new SegmentsInfo());
            Console.ReadKey();
        }

        public static void GenerateSegments(SegmentsInfo segmentsInfo)
        {
            List<Segment> segments = new List<Segment>();
            Gaussian g = new Gaussian(0.2, 0.04);
            List<double> numbers = new List<double>();

            for (int j = 0; j < 1; j++)
            {
                double sum = 0;
                for (int i = 0; i < segmentsInfo.SegmentsAmount; i++)
                {
                    double d = Math.Round(Math.Abs(g.NextGaussian()), 2);
                    if (i == segmentsInfo.SegmentsAmount - 1)
                    {
                        d = 1 - sum;
                    }
                    numbers.Add(d);
                    sum += d;

                    if (d <= 0)
                    {
                        j--;
                        numbers = new List<double>();
                        break;
                    }
                }
            }

            for (int i = 0; i < numbers.Count; i++)
                Console.WriteLine(numbers[i]);
        }
    }
    public class Gaussian
    {
        private Random r;
        private double mean;
        private double sigma;

        public Gaussian(double mean, double sigma)
        {
            r = new Random(Guid.NewGuid().GetHashCode());
            //r = new Random(0);
            this.mean = mean;
            this.sigma = sigma;
        }
        public double NextGaussian()
        {
            double u1 = r.NextDouble();
            double u2 = r.NextDouble();
            double left = Math.Cos(2.0 * Math.PI * u1);
            double right = Math.Sqrt(-2.0 * Math.Log(u2));
            double z = left * right;
            return this.mean + (z * this.sigma);
        }
    }
    public class SegmentsInfo
    {
        public int TotalPopulation { get; set; } = 1000;
        public int SegmentsAmount { get; set; } = 5;
    }
    public class Segment
    {
        public int SegmentId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; } 

        public double WandPriceRatio { get; set; }
        public double WandAmountRatio { get; set; }
        public double AdsRatio { get; set; }
        public double QualityRatio { get; set; }
        public double NothingRatio { get; set; }

        public Dictionary<int, double> NothingRatioPlayers { get; set; } = new Dictionary<int, double>();
        public Dictionary<int, double> QualityRatioPlayers { get; set; } = new Dictionary<int, double>();
        public Dictionary<int, double> AdsRatioPlayers { get; set; } = new Dictionary<int, double>();
        public Dictionary<int, double> WandAmountRatioPlayers { get; set; } = new Dictionary<int, double>();
        public Dictionary<int, double> WandPriceRatioPlayers { get; set; } = new Dictionary<int, double>();
    }
}
