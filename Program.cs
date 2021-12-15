using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace PAATA_GVICHIA_SDInterTask
{
    class Program
    {

        //task 1
        static bool IsPolimdrom(string word)
        {
            string newWord = word.ToLower();
            char[] reverseWord = newWord.ToCharArray();
            Array.Reverse(reverseWord);

            if(newWord == new string(reverseWord))
            {
                return true;
            }
            return false;
        }

        //task2
        static int MinSplit(int amount)
        {
            int[] coinVAlues = {1,5,10,20,50 };
            int coinCount = 0;

            List<int> ordersValues = coinVAlues.Distinct().OrderByDescending(value => value).ToList();

            foreach (int value in ordersValues)
            {
                coinCount += amount / value;
                amount = amount % value;
                if(amount == 0)
                {
                    return coinCount;
                }
            }

            return default;
        }

        //task3
        static int NotContains(int[] numbers)
        {
            // nums >0
            int[] posNumber = numbers.Where(x=>x>0).ToArray();

            List<int> orderNumber = posNumber.Distinct().OrderByDescending(number=>number).ToList();

            int minNumberInExistingList = orderNumber[(orderNumber.Count()-1)];

            int[] valuesNotInRange = new int[minNumberInExistingList];

            
            for(int i =0; i < minNumberInExistingList; i++)
            {          
                valuesNotInRange[i] = i;
            }


            List<int> orderValues = valuesNotInRange.Distinct().OrderByDescending(x => x).ToList();
            int minNumber = orderValues[orderValues.Count()-1 ];
       
            foreach( int item in posNumber)
            {
                if (posNumber.Contains(minNumber) || minNumber == 0)
                {
                    minNumber++;
                }
            }
            
            return minNumber;
        }

        //Task4
        static bool IsProperly(string expression)
        {
            int openParentasis = 0;
            int closingParentasis = 0;

            foreach(char item in expression)
            {
                if (item == '(')
                {
                    openParentasis++;
                }
                if (item == ')')
                {
                    closingParentasis++;
                }
            }


            if (openParentasis == closingParentasis)
            {
                return true;
            }

            return false;

        }

        //Task5
        static int CountVariants(int stears)
        {
            int maxStep  = 2 ;
            int currentStear = 0;
            int variants = 0;

            if (maxStep == stears) return ++variants;
            if (stears < maxStep) return ++variants;
            if (stears == 0) return variants;

            variants = stears / maxStep;
            currentStear = variants * maxStep;
            if ((stears - currentStear) == 1)
            {
                return ++variants;
                
            }
            return variants;
            

        }

        //Task 6
        class CustomQueue<T>
        {
            public int Count { get; set; }
            T[] data;

            public CustomQueue(IEnumerable<T> data)
            {
                Count = data.Count();
                this.data = data.Skip(1).ToArray();
                
            }

            public T[] Remove() => data;
          
            
        }

        //Task 8
         class XmlReciever
        {
            private static XmlReciever Instance = null;
            public string NodeData { get; private set; }
            private XmlReciever()
            {
                string url = @"http://www.nbg.ge/rss.php";

                XmlDocument doc = RequestData(url);
                XmlNode node = doc.SelectSingleNode("rss/channel/item/description/text()");

                string noTags = Regex.Replace(node.Value, @"<[^>]+>|&nbsp;", "").Trim();
                NodeData = noTags;
            }

            public static XmlReciever GetInstance()
            {
                if(Instance is null)
                {
                    Instance = new XmlReciever();
                }

                return Instance;
            }

            private XmlDocument RequestData(string url)
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var responce = (HttpWebResponse)request.GetResponse();

                var stream = responce.GetResponseStream();
                var document = new XmlDocument();

                document.Load(url);

                stream.Close();

                return document;
            }

       
        }

        static void Main(string[] args)

        
        {
            int[] nums = { -3,-2,-1,0,1,4,2,3,25};
            string mathExpression = "(a+b)+((b+a))";


           Console.WriteLine("Task 1");
           Console.WriteLine( IsPolimdrom("Anna"));
            Console.WriteLine();

            Console.WriteLine("Task2");
            Console.WriteLine(MinSplit(86));
            Console.WriteLine();

            Console.WriteLine("Task3");
            Console.WriteLine(NotContains(nums));
            Console.WriteLine();

            Console.WriteLine("Task4");
            Console.WriteLine(IsProperly(mathExpression));
            Console.WriteLine();

            Console.WriteLine("Task5");
            Console.WriteLine(CountVariants(100));
            Console.WriteLine();

            Console.WriteLine("Task6");
            int[] numberToCheck = { 1, 2, 3, 4, 5 };
            CustomQueue< int> customQueue = new CustomQueue<int>  (numberToCheck);      
            int[] newNumbers= customQueue.Remove();
            Console.WriteLine("First Array:");
            foreach(int item in numberToCheck)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Result Array:");
            foreach (int item in newNumbers)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();


            Console.WriteLine("Task8");
            XmlReciever xmlReciever = XmlReciever.GetInstance();
            string data= xmlReciever.NodeData;
           
            Console.WriteLine(data);

            Console.ReadLine();
        }
    }
}
