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
            Node L;         //Левая вершина
            Node R;         //Правая вершина
            int Index;      //Индекс в массиве, если вершина конечная
            int Sum;        //Сумма частот левой и правой вершины
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
