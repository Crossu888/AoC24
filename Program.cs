using System.Formats.Asn1;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
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
            D1p2(column1, column2);
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
    static void D1p2(uint[] column1, uint[] column2)
    {
        uint simScore=0;
        uint dupes;
        for(int i=0;i<1000;i++)
        {
            dupes=0;
            for(int n=0;n<1000;n++)
            {
                if(column2[n]==column1[i])
                {
                    dupes++;
                }
            }
            simScore+=column1[i]*dupes;
        }
        Console.WriteLine(simScore);
    }
    static void Day2()
    {
        StreamReader file = new StreamReader("inputs/input2test");
        int safe=0;
        Console.WriteLine("Part 1 or 2?");
        string part = Console.ReadLine()!;
        if(!(part=="1"||part=="2")) {Console.WriteLine("Invalid input"); return;}
        for(int i=0;i<10;i++)
        {
            string[] report = file.ReadLine()!.Split(" ");
            int[] levels = new int[report.Length];
            for(int n=0;n<report.Length;n++)
            {
                levels[n]=Convert.ToInt32(report[n]);
            }
            // Check #1 - check if the report is ascending, descending or neither
            // -1 = unsafe report | 0 = descending | 1 = ascending
            int reportOrder = Day2check1(levels,part).order;
            Console.WriteLine(reportOrder);
            Console.WriteLine(Day2check1(levels,part).ignore);
            Console.WriteLine();
            if(reportOrder==-1) {continue;}
            // Check #2 - check if each number is less than 4 off the previous number
            if(Day2check2(reportOrder,levels,part,Day2check1(levels,part).ignore))
                safe++;
            //Console.WriteLine(Day2check2(reportOrder,levels,part,Day2check1(levels,part).ignore));
        }
        Console.WriteLine(safe);
    }
    static (int order, int ignore) Day2check1(int[] report, string part)
    {
        int order=0;
        int ignoreIndex=-1;
        bool ignored=false;
        if(part=="1") {ignored=true;}
        for(int i=0;i<report.Length-1;i++)
        {
            if(report[i]>=report[i+1] && order>0 && ignored==false) {ignoreIndex=i+1; ignored=true;}
            else if(report[i]<=report[i+1] && order<0 && ignored==false) {ignoreIndex=i+1; ignored=true;}
            else if(report[i]>report[i+1]) {order--;}
            else if(report[i]<report[i+1]) {order++;}
        }
        //Console.WriteLine(order);
        //Console.WriteLine(report.Length-2);
        //Console.WriteLine();
        if(part=="1")
        {
            if(order==report.Length-1)
                return (1,-1);
            else if(-order==report.Length-1)
                return (0,-1);
            else
                return (-1,-1);
        }
        else if(part=="2")
        {
            if(order==report.Length-2||order==report.Length-1)
                return (1,ignoreIndex);
            else if(-order==report.Length-2||-order==report.Length-1)
                return (0,ignoreIndex);
            else
                return (-1,ignoreIndex);
        }
        else
            return (-1,-1);
    }
    static bool Day2check2(int reportOrder, int[] report, string part, int ignoreIndex)
    {
        if(ignoreIndex==-1 && part=="2")
        {
            int ignored=-1;
            if(reportOrder==0)
            {
                for(int i=0;i<report.Length-1;i++)
                {
                    if(!(report[i+1]>=report[i]-3) && ignored!=-1)
                        return false;
                    else if(!(report[i+1]>=report[i]-3))
                        ignored=i+1;
                }
            }
            else if(reportOrder==1)
            {
                for(int i=0;i<report.Length-1;i++)
                {
                    //Console.WriteLine(report[i]);
                    if(!(report[i+1]<=report[i]+3) && ignored!=-1)
                    {
                        return false;
                    }
                    else if(!(report[i+1]<=report[i]+3))
                        ignored=i+1;
                }
            }
            return true;
        }
        else if(reportOrder==0)
        {
            for(int i=0;i<report.Length-1;i++)
            {
                if(i!=ignoreIndex)
                {
                    if(!(report[i+1]>=report[i]-3))
                        return false;
                }
            }
        }
        else if(reportOrder==1)
        {
            for(int i=0;i<report.Length-1;i++)
            {
                if(i!=ignoreIndex)
                {
                    if(!(report[i+1]<=report[i]+3))
                        return false;
                }
            }
        }
        return true;
    }
    private static void Main()
    {
        Console.WriteLine("Select the day of the puzzle");
        string day = Console.ReadLine()!;
        if(day=="1")
            Day1();
        else if(day=="2")
            Day2();
        else
            Console.WriteLine("Invalid input");
    }
}