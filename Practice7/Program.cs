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
        ///  00, 01, 010 и тд
        /// </summary>
        /// <param name="args"></param>
        /// 

        class Node
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
        static void MyShellSort(ref Node[] arr)
        {
            int i = 0;
            int j = 0;
            Node swap = new Node(-1, -1);

            for (int distance = arr.Length / 2; distance > 0; distance /= 2)    //Задаём шаг
                for (i = distance; i < arr.Length; i++)                         //Задаём группу для сортировки вставками:
                {
                    swap = arr[i];                                              //Сортировка встаавками
                    for (j = i; j >= distance; j -= distance)
                    {
                        if (swap < arr[j - distance])
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

            MyShellSort(ref nodes);


            return head;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число символов исходного алфавита");
            int N = int.Parse(Console.ReadLine());
            int[] words = new int[N];
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Введите сколько раз встретилось символ {0}", i + 1);
                words[i] = int.Parse(Console.ReadLine());
            }
        }
    }
}
