using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice7
{
    class Program
    {
        /// <summary>
        /// 21.	
        /// Заданы частоты символов входного алфавита. 
        ///        -на вход - N - число символов
        ///        -N частот words символов, частоты задаём целым числом - количество встреченных символов в тексте
        ///     
        /// Построить двоичный суффиксный код Хаффмана. 
        ///     -любое слово не суффикс другого
        ///     -строим дерево для префиксного кода и собираем снизу 
        ///     
        /// Кодовые слова выписать в лексикографическом порядке.
        ///  00, 000, 001, 01, 11 и тд
        /// </summary>
        /// <param name="args"></param>
        /// 

        public class Node
        {
            public Node L;         //Левая вершина
            public Node R;         //Правая вершина
            public Node Root;      //Вершина "отец"
            public int Index;      //Индекс в массиве, если вершина конечная
            public int Sum;        //Сумма частот левой и правой вершины
            public Node()
            {
                L = null;
                R = null;
                Index = -1;
                Sum = 0;
            }
            public Node(Node l, Node r)
            {
                L = l;
                R = r;
                l.Root = this;
                r.Root = this;
                Index = -1;
                Sum = l.Sum + r.Sum;
            }
            public Node(int index, int sum)
            {
                L = null;
                R = null;
                Index = index;
                Sum = sum;
            }

            static public implicit operator int(Node node)
            {
                return node.Sum;
            }
        }

        static public bool CompareStrings(string s1, string s2)//s1 < s2 = true
        {
            for (int i = 0; i < s1.Length; i++)
                if (i == s2.Length)
                    return false;
                else
                if (s1[i] < s2[i])
                    return true;

            return false;
        }
        static void MyShellSort(ref string[] arr)
        {
            int i = 0;
            int j = 0;
            string swap = "";

            for (int distance = arr.Length / 2; distance > 0; distance /= 2)    //Задаём шаг
                for (i = distance; i < arr.Length; i++)                         //Задаём группу для сортировки вставками:
                {
                    swap = arr[i];                                              //Сортировка встаавками
                    for (j = i; j >= distance; j -= distance)
                    {
                        if (CompareStrings(swap, arr[j - distance]))
                            arr[j] = arr[j - distance];
                        else
                            break;
                    }
                    arr[j] = swap;
                }
        }

        static public Node HaffTree(int[] words)
        {
            Node head;

            Node[] nodes = new Node[words.Length];
            
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new Node(i, words[i]);

            while (nodes.Length != 1)
            {
                int l = 0, r = 1;

                for (int i = 0; i < nodes.Length; i++)
                    if (nodes[l] >= nodes[i] && r != l && l != i && r != i)
                        l = i;

                for (int i = 0; i < nodes.Length; i++)
                    if (nodes[r] >= nodes[i] && r != l && r != i && l != i)
                        r = i;
            
                Node[] temp = new Node[nodes.Length - 1];


                for (int j = 0, i = 0; i < temp.Length - 1; i++)
                {

                    if (j == l || j == r)
                        j++;
                    if (j == l || j == r)
                        j++;
                    temp[i] = nodes[j];
                    j++;
                }
                temp[temp.Length - 1] = new Node(nodes[l], nodes[r]);



                nodes = temp;
            }
            head = nodes[0];

            return head;
        }

        //Сбор словаря обходом дерева в глубину
        static public string[] GetAlphabet(Node head, int N)
        {
            string[] result = new string[N];
            char[] word;

            int wordLength = 0;
            Node mark = head;

            //Пока все слова не убраны
            while (head != 0)
            {

                if (mark.L != null && mark.L != 0)
                {
                    mark = mark.L;
                    wordLength++;
                }
                else
                    if (mark.R != null && mark.R != 0)
                {
                    mark = mark.R;
                    wordLength++;
                }
                else
                {
                    if (mark.Root != null)
                    {
                        mark = mark.Root;
                    }
                    wordLength--;
                }

                if (mark != null && mark.Index != -1)
                {
                    //Убираем найденное слово из дерева

                    Node wordBuilder = mark;
                    word = new char[wordLength];
                    int i = 0;
                    while (wordBuilder != head)
                    {
                        if (wordBuilder.Root.L == wordBuilder)
                            word[i] = '0';
                        else
                            word[i] = '1';
                        i++;
                        wordBuilder = wordBuilder.Root;
                        wordBuilder.Sum -= mark;
                    }
                    result[mark.Index] = "";

                    for (int j = 0; j < word.Length; j++)
                        result[mark.Index] += word[j];

                    word = null;
                    mark.Sum = 0;
                    mark = mark.Root;
                    wordLength--;
                }
            }
            return result;
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число символов исходного алфавита");
            int N = int.Parse(Console.ReadLine());
            while (N <= 2)
            {
                Console.WriteLine("Кодирование не имеет смысла. Используйте больше двух символов в исходном алфавите");
                Console.WriteLine("Введите число символов исходного алфавита");
                N = int.Parse(Console.ReadLine());
            }

            int[] words = new int[N];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Введите сколько раз встретился символ {0}", i + 1);
                words[i] = int.Parse(Console.ReadLine());
            }
            Node tree = HaffTree(words);

            string[] result = GetAlphabet(tree, N);
            for (int i = 0; i < N; i++)
                Console.WriteLine("Слово №{0, -10} Частота:{1, -10} Зашифровано: {2, -10}", i + 1, words[i], result[i]);
            

           // string[] result2 = (string[])result.Clone();
            //Array.Sort(result);
            MyShellSort(ref result);


            Console.WriteLine("");
            Console.WriteLine("В лексикографическом порядке");

            for (int i = 0; i < N; i++)
                Console.WriteLine(result[i]);
            //Слова не требуют переворта, так как уже были собраны с листьев дерева

        }
    }
}
