using System;



    public class HistogramArray
    {
        private readonly double[] _sortedData;
        private readonly double _min;
        private readonly double _max;
        private readonly double _binWidth;
        private readonly int _binCount;

        public HistogramArray(double[] numbers, int intervalCount)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("Числа не могут быть равны 0");
            if (intervalCount <= 0)
                throw new ArgumentException("Интервал не может быть меньше 0");

            _binCount = intervalCount;
            _sortedData = new double[numbers.Length];
            Array.Copy(numbers, _sortedData, numbers.Length);
            Array.Sort(_sortedData);

            _min = _sortedData[0];
            _max = _sortedData[_sortedData.Length - 1];


            double range = _max - _min;
            if (range == 0)
                range = 1;
            _binWidth = range / _binCount;
        }


        public int Gett(double value)
        {
            if (value < _min)
                return 0;
            if (value >= _max)
                return _binCount - 1;

            int binIndex = (int)((value - _min) / _binWidth);


            if (binIndex >= _binCount)
                binIndex = _binCount - 1;

            return binIndex;
        }


        public double Get(double percentile)
        {
            if (percentile < 0 || percentile > 100)
                throw new ArgumentException("Значение может быть только в пределах от 0 до 100");

            if (_sortedData.Length == 1)
                return _sortedData[0];


            double position = (percentile / 100.0) * (_sortedData.Length - 1);
            int lowerIndex = (int)position;
            double fraction = position - lowerIndex;

            if (lowerIndex >= _sortedData.Length - 1)
                return _sortedData[_sortedData.Length - 1];


            return _sortedData[lowerIndex] +
                   fraction * (_sortedData[lowerIndex + 1] - _sortedData[lowerIndex]);
        }


        public double[] GetBinEdges()
        {
            double[] edges = new double[_binCount + 1];
            for (int i = 0; i <= _binCount; i++)
            {
                edges[i] = _min + i * _binWidth;
            }

            return edges;
        }

        public int[] GetBinCounts()
        {
            int[] counts = new int[_binCount];
            for (int i = 0; i < _sortedData.Length; i++)
            {
                int binIndex = Gett(_sortedData[i]);
                counts[binIndex]++;
            }

            return counts;
        }

    }
    class Program
    {
        static void Main()
        {
            double[] data = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };
            HistogramArray histogram = new HistogramArray(data, 5);

           
            Console.WriteLine($"Значение 3.5 в bin: {histogram.Get(3.5)}");  
            Console.WriteLine($"Значение 7.2 в bin: {histogram.Get(7.2)}");  

          
            Console.WriteLine($"50 значение : {histogram.Get(50.0)}");  
            Console.WriteLine($"25 значение: {histogram.Get(25.0)}");           
            Console.WriteLine($"75 значение: {histogram.Get(75.0)}");           

           
            double[] edges = histogram.GetBinEdges();
            Console.WriteLine("Bin edges:");
            for (int i = 0; i < edges.Length; i++)
                Console.Write($"{edges[i]} ");
            Console.WriteLine();

          
            int[] counts = histogram.GetBinCounts();
            Console.WriteLine("Bin counts:");
            for (int i = 0; i < counts.Length; i++)
                Console.Write($"{counts[i]} ");
        }
    }
