using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
internal class AoC
{
    static uint[] Sort(uint[] array)
    {
        uint temp;
        int arrayLength = array.Length;
        for(int n=(int)(arrayLength / 1.3);n>=1;n=(int)(n / 1.3))
        {
            for(int i=0;i<arrayLength-1;i++)
            {
                if(i+n<arrayLength)
                {
                    if(array[i]>array[i+n])
                    {
                        temp=array[i];
                        array[i]=array[i+n];
                        array[i+n]=temp;
                    }
                }
            }
        }
        return array;
    }
    private static void Main()
    {
        StreamReader file = new StreamReader("inputs/input1");
        uint[] column1 = new uint[1000];
        uint[] column2 = new uint[1000];
        for(int i=0;i<1000;i++)
        {
            string[] numbers = file.ReadLine()!.Split("   ");
            column1[i] = Convert.ToUInt32(numbers[0]);
            column2[i] = Convert.ToUInt32(numbers[1]);
        }
        file.Close();
        column1 = Sort(column1);
        column2 = Sort(column2);
        //write array to file to check if it's sorted ok
        string[] column1sorted = new string[1000];
        for(int i=0;i<1000;i++)
        {
            column1sorted[i] = Convert.ToString(column1[i]);
        }
        File.WriteAllLines("testsorted", column1sorted);
        uint sum=0;
        for(int i=0;i<1000;i++)
        {
            if(column1[i]>column2[i])
                sum+=column1[i]-column2[i];
            else if(column1[i]<column2[i])
                sum+=column2[i]-column1[i];
        }
        Console.WriteLine(sum);
    }
}