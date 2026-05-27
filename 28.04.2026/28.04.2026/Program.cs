using System;

class Program
{
    static void Main()
    {
       
        int[,] array = {
            { 5, 2, 8 },
            { 1, 9, 4 },
            { 7, 3, 6 }
        };

        Console.WriteLine("Исходный массив:");
        PrintArray(array);
        
        long product = MultiplyAllElements(array);
        Console.WriteLine($"\n1. Произведение всех элементов: {product}");
        
        int[,] sortedArray = SortArray(array);
        Console.WriteLine("\n2. Отсортированный массив:");
        PrintArray(sortedArray);

        int diagonalSum = SumDiagonal(array);
        Console.WriteLine($"\n3. Сумма элементов главной диагонали: {diagonalSum}");
    }

  
    static long MultiplyAllElements(int[,] array)
    {
        long product = 1;
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                product *= array[i, j];
            }
        }

        return product;
    }

   
    static int[,] SortArray(int[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        int[,] result = new int[rows, cols];

       
        int[] flatArray = new int[rows * cols];
        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                flatArray[index++] = array[i, j];
            }
        }

     
        Array.Sort(flatArray);
        
        index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = flatArray[index++];
            }
        }

        return result;
    }
    
    static int SumDiagonal(int[,] array)
    {
        int sum = 0;
        int size = Math.Min(array.GetLength(0), array.GetLength(1)); // Для неквадратных матриц

        for (int i = 0; i < size; i++)
        {
            sum += array[i, i];
        }

        return sum;
    }

   
    static void PrintArray(int[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{array[i, j],4}");
            }
            Console.WriteLine();
        }
    }
}