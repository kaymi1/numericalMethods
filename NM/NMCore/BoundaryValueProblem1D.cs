using System;
using System.Collections.Generic;
using System.Drawing;

namespace IvanovAC.NM
{
    /// <summary>
    /// Содержит методы решения краевой задачи для обыкновенного дифференциального уравнения
    /// </summary>
    public static class BoundaryValueProblem1D
    {

        /// <summary>
        /// Алгоритм метода левой прогонки
        /// </summary>
        /// <param name="A">Заданный массив</param>
        /// <param name="B">Заданный массив</param>
        /// <param name="C">Заданный массив</param>
        /// <param name="F">Заданный массив</param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="u1"></param>
        /// <param name="u2"></param>
        /// <returns>масиив решений для дифференциального уравнения</returns>
        public static double[] ThomasAlgoritm(double[] A, double[] B, double[] C, double[] F, double t, double step)
        {
            int n = F.Length;

            double[] y = new double[n];
            double[] a = new double[n];
            double[] b = new double[n];
            /* Тестовая задача
            a[0] = 1;
            b[0] = -8*Math.Exp(-4*t)*step;*/
           
            /* Освновная задача*/
            a[0] = 1;
            b[0] = 0;

            for (int i = 1; i < n; i++)
            {
                a[i] = (-C[i]) / (A[i] * a[i - 1] + B[i]);
                b[i] = (F[i] - A[i] * b[i - 1]) / (A[i] * a[i - 1] + B[i]);
            }
            /* Тестовая задача
            y[n - 1] = 4 * Math.Exp(-4 * t)*(Math.Cos(2) + Math.Sin(2)) + 4;*/
            
            /* Основная задача*/
            y[n - 1] = 20;
            

            for (int i = n - 2; i >= 0; i--)
            {
                y[i] = a[i] * y[i + 1] + b[i];
            }

            return y;
        }
        /// <summary>
        /// Алгоритм метода левой прогонки
        /// </summary>
        /// <param name="A">Заданный массив</param>
        /// <param name="B">Заданный массив</param>
        /// <param name="C">Заданный массив</param>
        /// <param name="F">Заданный массив</param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="u1"></param>
        /// <param name="u2"></param>
        /// <returns>масиив решений для дифференциального уравнения</returns>
        public static double[] ThomasAlgoritm(double[] A, double[] B, double[] C, double[] F, double v1, double v2, double u1, double u2)
      {
            int n = F.Length;           
                        
            double[] y = new double[n];
            double[] a = new double[n];
            double[] b = new double[n];
            a[0] = v1;
            b[0] = u1;

            for (int i = 0; i < n - 1; i++)
            {
                a[i + 1] = B[i] / (C[i] - a[i] * A[i]);
                b[i + 1] = (b[i] * A[i] + F[i]) / (C[i] - a[i] * A[i]);
            }

            y[n - 1] = (u2 + b[n-1] * v2) / (1 - a[n-1] * v2); 
            
            for (int i = n - 2; i > 0; i--)
            {
                y[i] = a[i + 1] * y[i + 1] + b[i + 1];
            }

            return y;
        }

        /// <summary>
        /// Проверка метода левой прогонки
        /// </summary>
        public static double[] CheckThomasAlgorithm()
        {
            double v1 = 0.3;
            double v2 = 0.5;
            double u1 = 13;
            double u2 = u1;

            double[] A = { 0.3, 3, 1, 6, 11, 7, 4, 2, 13, v2 };
            double[] B = { v1, 1, 0.5, 7, 10, 15, 5, 3, 15, 0.2 };
            double[] C = { 1, 4, 2, 15, 25, 31, 11, 9, 31, 1 };
            double[] F = { u1, 2.5, 3, 7.1, 8, 17, 21, 34, 45.6, 2 };

            return ThomasAlgoritm(A, B, C, F, v1, v2, u1, u2);
        }


        /// <summary>
        /// Метод левой прогонки
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="ya">Значение искомой функции y в начальной точке A.</param>
        /// <param name="yb">Значение функции в конечной точке B.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> FiniteDiffMethod(double A, double B, double ya, double yb, Func<double, double, double> p, Func<double, double, double> q, Func<double, double, double> F, int N)
        {
            // Проверка корректности исходных данных
            if (A >= B)
            {
                throw new Exception("Координата правой точки должна быть больше координаты левой");
            }
            if (N <= 0)
            {
                throw new Exception("Количество элементов разбиения должно быть положительным");
            }
            // Получение регулярной сетки
            double[] x = CauchyProblem.Discretization(A, B, N);
            // Расстояние между соседними узлами
            double step = (B - A) / N;
            
            PointF[] points = new PointF[N + 1];            

            double[] a = new double[N + 1];
            double[] b = new double[N + 1];
            double[] c = new double[N + 1];
            double[] f = new double[N + 1];
            c[0] = 1;
            f[0] = ya;
            b[0] = 0;
            a[N] = 0;
            f[N] = yb;
            c[N] = 1;

            for (int i = 1; i < N; i++)
            {
                a[i] = (1 / (step * step)) - (p(x[i],0) / (2 * step));
                c[i] = (2 / (step * step)) - q(x[i], 0);
                b[i] = (1 / (step * step)) + (p(x[i], 0) / (2 * step));
                f[i] = -F(x[i], 0);
            }

            double[] y = ThomasAlgoritm(a, b, c, f, 0, 0, ya, yb);
            y[0] = ya;
            y[y.Length - 1] = yb;

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)y[i];
            }            

            return new List<PointF>(points);
        }

        /// <summary>
        /// Разностный метод для уравнения теплопроводности 
        /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="ya">Значение искомой функции y в начальной точке A.</param>
        /// <param name="yb">Значение функции в конечной точке B.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF[]> FiniteDiffMethodForHeatEquation(double A, double B, int N, int u1, int u2, double l1)
        {
            if (A >= B)
            {
                throw new Exception("Координата правой точки должна быть больше координаты левой");
            }
            if (N <= 0)
            {
                throw new Exception("Количество элементов разбиения должно быть положительным");
            }
            
            double[] x = CauchyProblem.Discretization(A, B, N);
            double step = (B - A) / N;
            
            double dt = 0.01;
            double r = dt / (step * step);
            double[] t = new double[N + 1];           

            for(int i = 0; i < x.Length; i++)           
                t[i] = i * dt;


            
            double[] ustart = new double[N + 1];
            /*Начальные условия для основной задачи*/
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] <= l1)
                    ustart[i] = u1;
                else if (x[i] > 0.1)
                    ustart[i] = u2;
            }
            /* Начальные условия тестовой задачи t = 0
            for (int i = 0; i < x.Length; i++)
            {
                ustart[i] = 4 * Math.Cos(2 * x[i]) + 4 * Math.Sin(2 * x[i]) + 4;
            }*/     

            double[] a = new double[N + 1];
            double[] b = new double[N + 1];
            double[] c = new double[N + 1];
            double[] f = new double[N + 1];

            for (int j = 0; j < c.Length; j++)
            {
                a[j] = r;
                c[j] = r;
                b[j] = - 1 - 2 * r;                
                f[j] = -ustart[j];
            }

            List<PointF[]> pointsList = new List<PointF[]>();
            PointF[] points = new PointF[N + 1];

            for (int j = 0; j < points.Length; j++)
            {
                points[j].X = (float)x[j];
                points[j].Y = (float)ustart[j];
            }
            pointsList.Add(points);
            double[] yn = ustart;


            for (int i = 1; i < t.Length; i++)
            {                            
                double[] y = ThomasAlgoritm(a, b, c, f, t[i], step);                             
                points = new PointF[N + 1];

                if((Math.Abs(MaxValueMassive(y) - MaxValueMassive(yn)) / Math.Abs(MaxValueMassive(y))) <= 0.001)
                {
                    break;
                }

                for (int j = 0; j < points.Length; j++)
                {
                    yn[j] = -f[j];
                    f[j] = -y[j];
                    points[j].X = (float)x[j];
                    points[j].Y = (float)y[j];
                }
                pointsList.Add(points);
            }
                       
            return pointsList;
        }

        /// <summary>
        /// Вывод аналитического решения заданного дифф. уравнения 2-го порядка
        /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>    
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF[]> AnalyticSolveHeatEquation(double A, double B, int N)
        {
            if (A >= B)
            {
                throw new Exception("Координата правой точки должна быть больше координаты левой");
            }
            if (N <= 0)
            {
                throw new Exception("Количество элементов разбиения должно быть положительным");
            }
            double[] x = CauchyProblem.Discretization(A, B, N);
            double step = (B - A) / N;
            double dt = 0.01;
            double[] t = new double[N + 1];
            double[] yn = new double[N + 1];
            double[] yn1 = new double[N + 1];
            yn1[1] = 1;

            for (int i = 0; i < x.Length; i++)
                t[i] = i * dt;

            List<PointF[]> pointsList = new List<PointF[]>();

            for (int i = 0; i < t.Length; i++)
            {
                PointF[] points = new PointF[N + 1];

                for (int j = 0; j < points.Length; j++)
                {
                    points[j].X = (float)x[j];
                    points[j].Y = (float)(4 * (Math.Cos(2 * points[j].X) + Math.Sin(2 * points[j].X)) * Math.Exp(-4 * t[i]) + 4);
                    yn[j] = points[j].Y;
                }
                
                if ((Math.Abs(MaxValueMassive(yn1) - MaxValueMassive(yn)) / Math.Abs(MaxValueMassive(yn1))) <= 0.001)
                {
                    break;
                }
                for (int j = 0; j < yn.Length; j++)
                    yn1[j] = yn[j];

                pointsList.Add(points);
            }

            return pointsList;
        }

        public static double MaxValueMassive(double[] y)
        {
            double max = double.MinValue;

            foreach(double temp in y)
            {
                if(temp > max)
                {
                    max = temp;
                }
            }
            return max;
        }

        /// <summary>
        /// Метод левой прогонки
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="Z">Значение функции в конечной точке B.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> FiniteDiffMethod2(double A, double B, double Y, double Z, Func<double, double, double> F, int N)
        {
            // Проверка корректности исходных данных
            if (A >= B)
            {
                throw new Exception("Координата правой точки должна быть больше координаты левой");
            }
            if (N <= 0)
            {
                throw new Exception("Количество элементов разбиения должно быть положительным");
            }
            // Получение регулярной сетки
            double[] x = CauchyProblem.Discretization(A, B, N);
            // Расстояние между соседними узлами
            double step = (B - A) / N;
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint1 = new PointF
            {
                X = (float)x[0],
                Y = (float)Y
            };
            points[0] = startpoint1;

            double[] a = new double[N + 1];
            double[] b = new double[N + 1];
            double[] c = new double[N + 1];
            double[] f = new double[N + 1];

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = -1 / (step * step);
                c[i] = (-2 / (step * step) - 2 * Math.Pow(Math.E, x[i]));
                b[i] = -1 / (step * step);
                f[i] = F(x[i], 0);
            }

            double[] y = ThomasAlgoritm(a, b, c, f, 0, 0, 0, 0);

            for (int i = 1; i < points.Length - 1; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)(y[i]);
            }

            PointF startpoint2 = new PointF
            {
                X = (float)x[x.Length - 1],
                Y = (float)Z,
            };
            points[points.Length - 1] = startpoint2;

            return new List<PointF>(points);
        }

        /// <summary>
        /// Вычисляет точки коллакации
        /// </summary>
        /// <param name="a">Левый конец отрезка.</param>
        /// <param name="b">Правый конец отрезка.</param>
        /// <param name="n">Количество отрезков разбиения (количество узлов равно N + 1).</param> 
        /// <returns>Массив узлов.</returns>
        public static double[] GetCollacationPoints(double a, double b, int n)
        {
            double[] x = new double[n];

            for (int i = 0; i < x.Length; i++)
                x[i] = a + (i + 0.5) / n * (b - a);

            return x;
        }

        /// <summary>
        /// Возвращает значение левой части ОДУ
        /// y''+p(x)y'+q(x)y=f(x) при подстановке функции y(x).
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <param name="p">Функция при производной y'.</param>
        /// <param name="q">Функция при y.</param> 
        /// <param name="y">Функция, подставляемая в левую часть ОДУ.</param>
        /// <returns>Значение левой части.</returns>
        public static double Ly(double x, Func<double, double, double> p, Func<double, double, double> q, IBasisFunction y)
        {
            return y.SecondDerivative(x) + p(x, 0) * y.FirstDerivative(x) + q(x, 0) * y.Function(x);
        }

        /// <summary>
        /// Скалярное произведение для диск. мет. наим. квад.       
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <param name="p">Функция при производной y'.</param>
        /// <param name="q">Функция при y.</param> 
        /// <param name="y">Функция, подставляемая в левую часть ОДУ.</param>
        /// <returns>Значение левой части.</returns>
        public static double ScalarProduct(double[] x, Func<double, double, double> p, Func<double, double, double> q, IBasisFunction[] y, int i, int j)
        {
            double result = 0;
            double check = 0;

            for (int iterator = 0; iterator < x.Length; iterator++)
            {
                check = Ly(x[iterator], p, q, y[i]);
                check = Ly(x[iterator], p, q, y[j]);
                result += (Ly(x[iterator], p, q, y[i]) * Ly(x[iterator], p, q, y[j]));
            }      

            return result;
        }

        /// <summary>
        /// Скалярное произведение для правой части диск. мет. наим. квад.       
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <param name="p">Функция при производной y'.</param>
        /// <param name="q">Функция при y.</param> 
        /// <param name="y">Функция, подставляемая в левую часть ОДУ.</param>
        /// <returns>Значение левой части.</returns>
        public static double ScalarProductRight(double[] x, Func<double, double, double> p, Func<double, double, double> q, Func<double, double, double> f, IBasisFunction[] y, int i)
        {
            double result = 0;

            for (int iterator = 0; iterator < x.Length; iterator++)
            {
                result += (f(x[iterator],0) - Ly(x[iterator], p, q, y[0]) )* Ly(x[iterator], p, q, y[i]);
            }

            return result;
        }

        /// <summary>
        /// Метод коллакации
        /// </summary>
        /// <param name="A">Левый конец отрезка</param>
        /// <param name="B">Правый конец отрезка</param>
        /// <param name="ya">Значение функции в точке А</param>
        /// <param name="yb">Значение функции в точке В</param>
        /// <param name="p">Функция в уравнении</param>
        /// <param name="q">Функция в уравнении</param>
        /// <param name="f">Правая часть уравнения</param>
        /// <param name="y">Масиив базисных функций</param>
        /// <param name="slausolver">Объект решения СЛАУ</param>
        /// <returns>Массив коэффициентов</returns>
        public static double[] CollocationMethod(double A, double B, double ya, double yb,
            Func<double, double, double> p, Func<double, double, double> q,Func<double, double, double> f, 
            IBasisFunction[] y, SlauSolver slausolver)
        {
            int n = y.Length - 1;
            double[] x = GetCollacationPoints(A, B, n);           
            double[,] basismatrix = new double[n, n];
            double[] basisvector = new double[n];
            double[] result = new double[n];            

            for (int i = 0; i < n; i++)
            {
                for(int j = 1; j < n + 1; j++)
                {                    
                    basismatrix[i, j - 1] = Ly(x[i], p, q, y[j]);                    
                }                            
                basisvector[i] = f(x[i], 0) - Ly(x[i], p, q, y[0]);
            }

            result = slausolver.Solve(basismatrix, basisvector);

            return result;
        }

        /// <summary>
        /// Дискретный метод наименьших квадратов
        /// </summary>
        /// <param name="A">Левый конец отрезка</param>
        /// <param name="B">Правый конец отрезка</param>
        /// <param name="ya">Значение функции в точке А</param>
        /// <param name="yb">Значение функции в точке В</param>
        /// <param name="p">Функция в уравнении</param>
        /// <param name="q">Функция в уравнении</param>
        /// <param name="f">Правая часть уравнения</param>
        /// <param name="y">Масиив базисных функций</param>
        /// <param name="slausolver">Объект решения СЛАУ</param>
        /// <returns>Массив коэффициентов</returns>
        public static double[] LeastSquaresMethod(double A, double B, double ya, double yb,
            Func<double, double, double> p, Func<double, double, double> q, Func<double, double, double> f,
            IBasisFunction[] y, SlauSolver slausolver)
        {
            int n = y.Length - 1;
            double[] x = GetCollacationPoints(A, B, n);            
            double[,] basismatrix = new double[n, n];
            double[] basisvector = new double[n];
            double[] result = new double[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    basismatrix[i, j - 1] = ScalarProduct(x, p, q, y, j, i + 1);
                }
                basisvector[i] = ScalarProductRight(x, p, q, f, y, i + 1);
            }

            result = slausolver.Solve(basismatrix, basisvector);

            return result;
        }

    }
}
