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
    static void Day1()
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
        Console.WriteLine("Part 1 or 2?");
        string part = Console.ReadLine()!;
        if(part=="1")
            D1p1(column1, column2);
        else if(part=="2")
            //D1p2(column1, column2);
            Console.WriteLine("not yet implemented");
        else
            Console.WriteLine("Invalid input");
    }
    static void D1p1(uint[] column1, uint[] column2)
    {
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
    private static void Main()
    {
        Console.WriteLine("Select the day of the puzzle");
        string day = Console.ReadLine()!;
        if(day=="1")
            Day1();
        else
            Console.WriteLine("Invalid input");
    }
}