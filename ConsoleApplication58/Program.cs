using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication49
{
    class Program
    {
        static int Smezh(int[,] matrix, int a, int n)
        {
            int k = 0;
            for (int j = 0; j < n; j++)
            {
                if (matrix[a, j] == 1) k++;
            }
            return k;
        }

        static void Main()
        {
            int nechet = 0; //показывает в графе кол-во нечетных вершин
            var stack = new Stack<int>();
            string[] Mass = File.ReadAllLines(@"D:\DEBUG.txt", System.Text.Encoding.Default); //
            int n = Convert.ToInt32(Mass[0]);
            int[,] a = new int[Mass.Length - 1, 3];
            for (int i = 0; i < Mass.Length; i++)
            {

                Console.WriteLine("{0,3}", Mass[i]);

            }
            int[,] matrix = new int[n, n];
            int[,] matrix_old = new int[Mass.Length - 1, 2];
            for (int p = 0; p < Mass.Length - 1; p++)
            {
                //удаляю пробелы и заножу символ как число в массив m
                int[] m = Mass[p + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
                for (int i = 0; i < m.Length; i++)
                {
                    matrix_old[p, i] = m[i]; //добавляю это число в двумерный массив

                }

            }
            for (int p = 0; p < Mass.Length - 1; p++)
            {

                matrix[matrix_old[p, 0] - 1, matrix_old[p, 1] - 1] = 1;

            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] != 1)
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
            Console.WriteLine("Матрица:");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0} ", matrix[i, j]);
                }
                Console.WriteLine(" ");
            }
            int otvet_k = 0; //счетчик для добавления цепи
            var otvet = new Stack<int>();
            int[] v = new int[n]; //вершина-степень
            int[,] v_i = new int[n, n]; // вершина - другие вершины (проверка на смежность)
            int smezhnost = 0; // параметр для проверки на смежность вершин, т.е. если нарушена матрица, то =1 и вывод ошибки
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == matrix[j, i]) //проверяю матрицу на адекватность
                    {
                        if (matrix[i, j] == 1)
                        {
                            v[i] += 1;
                        }
                    }
                    else
                    {
                        smezhnost = 1;
                    }
                }
                if (v[i] % 2 == 1) nechet++;
                if (v[i] == 0 || smezhnost == 1 || matrix[i, i] != 0) { Console.WriteLine("Граф несвязный. Или ошибка в матрице."); break; }

            }
            if (smezhnost != 1) Console.WriteLine($"Нечетных вершин - {nechet}");
            if (nechet > 2 || nechet == 1) Console.WriteLine("Нет ни цикла, ни цепи");
            if (nechet == 0) Console.WriteLine("Есть цикл, но нет цепи");


            int dlya_stacka = 0;

            if (nechet == 2) // пишем для поиска цепи.
            {
                //выберем стартовую вершину, например, будет 0.
                stack.Push(0);
                while (stack.Count > 0)
                {
                    if (v[dlya_stacka] == 0) { otvet.Push(stack.Pop()); } //когда пришли в вершину, где степень 0 - остается лишь вывести ответ
                    else
                    {//open
                        for (int j = 0; j < n; j++) //иначе будем проходить по смежным вершинам всегда
                        {
                            if (matrix[dlya_stacka, j] == 1 && !(v[j] == 1 && Smezh(matrix, dlya_stacka, n) > 1)) //Smezh - ищет кол-во смежных вершин
                            {
                                matrix[dlya_stacka, j] = 0; v[dlya_stacka] -= 1; v[j] -= 1; matrix[j, dlya_stacka] = 0; stack.Push(j); dlya_stacka = j;
                                break;
                            }
                        }//выше мы вычитаем степени у вершин ребра, убираем в матрице смежности "смежности".

                    }//close

                }

                foreach (int number in otvet)
                {
                    Console.WriteLine(number + 1); //вывод цикла.
                }
            }

            Console.ReadKey();
        }
    }
}


