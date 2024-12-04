using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
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
        StreamReader file = new StreamReader("inputs/input2");
        int safe=0;
        Console.WriteLine("Part 1 or 2?");
        string part = Console.ReadLine()!;
        if(!(part=="1"||part=="2")) {Console.WriteLine("Invalid input"); return;}
        for(int i=0;i<1000;i++)
        {
            string[] report = file.ReadLine()!.Split(" ");
            int[] levels = new int[report.Length];
            for(int n=0;n<report.Length;n++)
            {
                levels[n]=Convert.ToInt32(report[n]);
            }
			if(part=="2")
			{
				ArrayList reportCut = new ArrayList();
				for(int ignore=0;ignore<levels.Length;ignore++)
				{
                    reportCut.Clear();
					reportCut.AddRange(levels);
                    reportCut.RemoveAt(ignore);
                    int[] newReport = (int[])reportCut.ToArray(typeof(int));
					// Check #1 - check if the report is ascending, descending or neither
	            	// -1 = unsafe report | 0 = descending | 1 = ascending
					int reportOrder = Day2check1(newReport);
					if(reportOrder==-1) {continue;}
					// Check #2 - check if each number is less than 4 off the previous number
					if(Day2check2(reportOrder,newReport)) {safe++; break;}
				}
			}
			else
			{
				// Check #1 - check if the report is ascending, descending or neither
            	// -1 = unsafe report | 0 = descending | 1 = ascending
				int reportOrder = Day2check1(levels);
				if(reportOrder==-1) {continue;}
				// Check #2 - check if each number is less than 4 off the previous number
				if(Day2check2(reportOrder,levels))
                	safe++;
			}
        }
        Console.WriteLine(safe);
    }
    static int Day2check1(int[] report)
    {
        int order=0;
        for(int i=0;i<report.Length-1;i++)
        {
            if(report[i]>report[i+1]) {order--;}
            else if(report[i]<report[i+1]) {order++;}
        }
        //Console.WriteLine(order);
        //Console.WriteLine(report.Length-2);
        //Console.WriteLine();
        if(order==report.Length-1)
            return 1;
        else if(-order==report.Length-1)
            return 0;
        else
            return -1;
    }
    static bool Day2check2(int reportOrder, int[] report)
    {
        if(reportOrder==0)
        {
            for(int i=0;i<report.Length-1;i++)
            {
                if(!(report[i+1]>=report[i]-3))
                    return false;
            }
        }
        else if(reportOrder==1)
        {
            for(int i=0;i<report.Length-1;i++)
            {
                if(!(report[i+1]<=report[i]+3))
                    return false;
            }
        }
        return true;
    }
static void Day3()
{
    StreamReader file = new StreamReader("inputs/input3");
    int sum=0;
    Console.WriteLine("Part 1 or 2?");
    string part = Console.ReadLine()!;
    if(part=="1")
    {
        string mulPatt = @"(mul\([0-9]+,[0-9]+\))"; //finds every mul(x,y) instruction
        string input = file.ReadToEnd();
        MatchCollection matches = Regex.Matches(input,mulPatt);
        foreach(Match match in matches)
        {
            MatchCollection numbers = Regex.Matches(match.ToString(),@"[0-9]+");
            sum+=Convert.ToInt32(numbers[0].ToString()) * Convert.ToInt32(numbers[1].ToString());
            
        }
    }
    else if(part=="2")
    {
        string pattern=@"(mul\([0-9]+,[0-9]+\))|(do\(\)|(don't\(\)))";
        string input=file.ReadToEnd();
        MatchCollection matches = Regex.Matches(input,pattern);
        bool exec=true;
        foreach(Match match in matches)
        {
            if(match.ToString()=="do()")
                exec=true;
            else if(match.ToString()=="don't()")
                exec=false;
            else if(exec==true)
            {
                MatchCollection numbers = Regex.Matches(match.ToString(),@"[0-9]+");
                sum+=Convert.ToInt32(numbers[0].ToString()) * Convert.ToInt32(numbers[1].ToString());
            }
        }
    }
    Console.WriteLine(sum);
    /*string[] subLines = file.ReadToEnd()!.Split("mul("); //Non-Regex part 1 solution
    for(int i=0;i<subLines.Length;i++)
    {
        if(subLines[i].Contains(")"))
        {
            subLines[i]=subLines[i].Remove(subLines[i].IndexOf(")"));
            if(subLines[i].ToCharArray().Length<=7 && subLines[i].Contains(","))
            {
                string[] pairStr = subLines[i].Split(",");
                sum+=Convert.ToInt32(pairStr[0])*Convert.ToInt32(pairStr[1]);
                //Console.WriteLine(subLines[i]);
            }
        }
    }*/
    //Console.WriteLine(sum);
}
    private static void Main()
    {
        Console.WriteLine("Select the day of the puzzle");
        string day = Console.ReadLine()!;
        if(day=="1")
            Day1();
        else if(day=="2")
            Day2();
        else if(day=="3")
            Day3();
        else
            Console.WriteLine("Invalid input");
    }
}
