using System.IO;
internal class AoC
{
    static uint[] Sort(uint[] array)
    {
        uint temp;
        bool swapThisRound=true;
        while(swapThisRound)
        {
            swapThisRound=false;
            for(int i=0;i<array.Length-1;i++)
            {
                if(array[i]>array[i+1])
                {
                    temp=array[i+1];
                    array[i+1]=array[i];
                    array[i]=temp;
                    swapThisRound=true;
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