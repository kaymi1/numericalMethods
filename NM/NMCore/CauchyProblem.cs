using System;
using System.Collections.Generic;
using System.Drawing;

namespace IvanovAC.NM
{
    /// <summary>
    /// Содержит методы решения задачи Коши для обыкновенного дифференциального уравнения
    /// </summary>
    public static class CauchyProblem
    {
        /// <summary>
        /// Решает задачу Коши для обыкновенного дифференциального уравнения
        /// y'(x)=F(x,y)
        /// простейшим методом Эйлера на отрезке [A, B].
        /// </summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> SimpleEuler(double A, double B, double Y, Func<double, double, double> F, int N)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)(points[i - 1].Y + step * F(points[i - 1].X, points[i - 1].Y));
            }

            return new List<PointF>(points);
        }
        /// <summary>
        /// Разбивает отрезок [A, B] на N равных частей и возвращает массив узлов.
        /// </summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="N">Количество отрезков разбиения (количество узлов равно N + 1).</param> 
        /// <returns>Массив узлов.</returns>
        public static double[] Discretization(double A, double B, int N)
        {
            double[] x = new double[N + 1];

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            for (int i = 0; i < x.Length; i++)
                x[i] = A + i * step;

            return x;
        }


        /// <summary>
        /// Решает задачу Коши для обыкновенного дифференциального уравнения
        /// y'(x)=F(x,y)
        /// исправленным методом Эйлера на отрезке [A, B].
        /// </summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> CorrectedEuler(double A, double B, double Y, Func<double, double, double> F, int N)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)(points[i - 1].Y + (step / 2) * (F(points[i - 1].X, points[i - 1].Y) + F(points[i - 1].X + step, points[i - 1].Y + step * F(points[i - 1].X, points[i - 1].Y))));
            }

            return new List<PointF>(points);
        }


        /// <summary>
        /// Решает задачу Коши для обыкновенного дифференциального уравнения
        /// y'(x)=F(x,y)
        /// модифицированным методом Эйлера на отрезке [A, B].
        /// </summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> ModifiedEuler(double A, double B, double Y, Func<double, double, double> F, int N)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)(points[i - 1].Y + step * F(points[i - 1].X + step / 2, points[i - 1].Y + (step / 2) * F(points[i - 1].X, points[i - 1].Y)));
            }

            return new List<PointF>(points);
        }

        /// <summary>
        /// Вывод аналитического решения заданного уравнения
        /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> AnalyticSolve(double A, double B, double Y, Func<double, double, double> F, int N)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;


            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)F(points[i].X, 0);
            }

            return new List<PointF>(points);
        }

        /// <summary>
        /// Метод Рунге-Кутты третьего порядка точности
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> Simpson(double A, double B, double Y, Func<double, double, double> F, int N)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            float dy, f0, f1, f2;
            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                f0 = (float)(step * F(points[i - 1].X, points[i - 1].Y));
                f1 = (float)(step * F(points[i - 1].X + step / 2, points[i - 1].Y + 0.5 * f0));
                f2 = (float)(step * F(points[i - 1].X + step, points[i - 1].Y - f0 + f1));
                dy = (f0 + 4 * f1 + f2) * 1 / 6;
                points[i].Y = (float)(points[i - 1].Y + dy);
            }

            return new List<PointF>(points);
        }


        /// <summary>
        /// Метод Рунге-Кутты четвертого порядка точности
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> RungeKutta4(double A, double B, double Y, Func<double, double, double> F, int N)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            float dy, f0, f1, f2, f3;
            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                f0 = (float)(step * F(points[i - 1].X, points[i - 1].Y));
                f1 = (float)(step * F(points[i - 1].X + step / 2, points[i - 1].Y + 0.5 * f0));
                f2 = (float)(step * F(points[i - 1].X + step / 2, points[i - 1].Y + 0.5 * f1));
                f3 = (float)(step * F(points[i - 1].X + step, points[i - 1].Y + f2));
                dy = (f0 + 2 * f1 + 2 * f2 + f3) * 1 / 6;
                points[i].Y = (float)(points[i - 1].Y + dy);
            }

            return new List<PointF>(points);
        }

        /// <summary>
        /// Метод Рунге-Кутты четвертого порядка точности
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="E">Заданная точность.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> IterativeEuler(double A, double B, double Y, Func<double, double, double> F, int N, double E)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            double y0, y1;
            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                y1 = points[i - 1].Y + step * F(points[i - 1].X, points[i - 1].Y);

                do
                {
                    y0 = y1;
                    y1 = points[i - 1].Y + step / 2 * (F(points[i - 1].X, points[i - 1].Y) + F(points[i].X, y0));
                } while (!(Math.Abs(y1 - y0) <= E));

                points[i].Y = (float)(y1);
            }

            return new List<PointF>(points);
        }


        /// <summary>
        /// Двухшаговый метод прогноза и коррекции
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="E">Заданная точность.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> PredictorCorrector2(double A, double B, double Y, Func<double, double, double> F, int N, double E)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;
            double y0, y1;

            points[1].X = (float)x[1];
            y1 = points[0].Y + step * F(points[0].X, points[0].Y);

            do
            {
                y0 = y1;
                y1 = points[0].Y + step / 2 * (F(points[0].X, points[0].Y) + F(points[1].X, y0));
            } while (!(Math.Abs(y1 - y0) <= E));

            points[1].Y = (float)(y1);


            for (int i = 2; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                y1 = points[i - 2].Y + step * F(points[i - 2].X, points[i - 2].Y);

                do
                {
                    y0 = y1;
                    y1 = points[i - 1].Y + step / 2 * (F(points[i - 1].X, points[i - 1].Y) + F(points[i].X, y0));
                } while (!(Math.Abs(y1 - y0) <= E));

                points[i].Y = (float)(y1);
            }

            return new List<PointF>(points);
        }

        /// <summary>
        /// Метод Милна
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="E">Заданная точность.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> Milne(double A, double B, double Y, Func<double, double, double> F, int N, double E)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];

            points = ModifiedEulerNum(A, B, Y, F, N, 4);

            // Расстояние между соседними узлами
            double step = (B - A) / N;
            double y0, y1;

            for (int i = 4; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                y1 = points[i - 4].Y + step * (2 * F(points[i - 3].X, points[i - 3].Y) - F(points[i - 2].X, points[i - 2].Y) + 2 * F(points[i - 1].X, points[i - 1].Y)) * 4 / 3;

                do
                {
                    y0 = y1;
                    y1 = points[i - 2].Y + step / 3 * (F(points[i - 2].X, points[i - 2].Y) + 4 * F(points[i - 1].X, points[i - 1].Y) + F(points[i].X, y0));
                } while (!(Math.Abs(y1 - y0) <= E));

                points[i].Y = (float)(y1);
            }

            return new List<PointF>(points);
        }

        /// <summary>
        /// Модифицированный метод Эйлера с определенным количеством точек в результате
        /// y'(x)=F(x,y)
        /// модифицированным методом Эйлера на отрезке [A, B].
        /// </summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="count">Количество точек в конечном массиве</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static PointF[] ModifiedEulerNum(double A, double B, double Y, Func<double, double, double> F, int N, int count)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;

            // Расстояние между соседними узлами
            double step = (B - A) / N;

            for (int i = 1; i < count; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)(points[i - 1].Y + step * F(points[i - 1].X + step / 2, points[i - 1].Y + (step / 2) * F(points[i - 1].X, points[i - 1].Y)));
            }

            return points;
        }

        /// <summary>
        /// Разн. метод 1-ого пор. аппрок.
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="Z">Значение произвоодной искомой функции y в начальной точке A.</param>
        /// <param name="F">Правая часть уравнения.</param>
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> FiniteDiffMethod1(double A, double B, double Y, double Z, Func<double, double, double> F, int N)
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
            PointF startpoint2 = new PointF
            {
                X = (float)x[1],
                Y = (float)(points[0].Y + step * Z)
            };
            points[1] = startpoint2;


            for (int i = 2; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)((step * step * F(points[i - 1].X, points[i - 1].Y) + (2 - 1 * step) * points[i - 1].Y - points[i - 2].Y) * (1 / (1 - 1 * step)));               
            }

            return new List<PointF>(points);
        }

        /// <summary>
        /// Вывод аналитического решения заданного дифф. уравнения 2-го порядка
        /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>    
        /// <param name="N">Количество отрезков сетки (количество узлов равно N + 1).</param>
        /// <returns>Коллекция точек, задающих решение.</returns>
        public static List<PointF> AnalyticSolveDiff2(double A, double B, double Y, int N)
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
            // Алгоритм простейшего метода Эйлера
            PointF[] points = new PointF[N + 1];
            PointF startpoint = new PointF
            {
                X = (float)A,
                Y = (float)Y
            };
            points[0] = startpoint;


            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                //points[i].Y = (float)(Math.Log(Math.Exp(3 * points[i].X) + 1,Math.E) - 3 * points[i].X + Math.Log(2, Math.E) + Math.Exp(3 * points[i].X)* Math.Log(Math.Exp(3 * points[i].X) + 1, Math.E) - Math.Exp(3 * points[i].X) * 3 * points[i].X + 5 * Math.Log(2, Math.E) * Math.Exp(3 * points[i].X));
                //points[i].Y = (float)(Math.Log(4, Math.E) + 1 + 3 * Math.Exp(3 * points[i].X) * Math.Log(4, Math.E) - Math.Exp(3 * points[i].X));
                //points[i].Y = (float)((Math.Log(Math.Exp(3 * points[i].X) + 1, Math.E) - 3 * points[i].X) * (1 + Math.Exp(3 * points[i].X)) + Math.Log(2, Math.E) * (1 + 5 * Math.Exp(3 * points[i].X)));
                //points[i].Y = (float)((Math.Log(2 * Math.Exp(points[i].X) + 1) - points[i].X) * (1 + 2 * Math.Exp(points[i].X)));
                points[i].Y = (float)((-1.027 * Math.Sin((Math.Sqrt(11) / 2) * points[i].X) - 0.5 * Math.Cos((Math.Sqrt(11) / 2) * points[i].X)) * Math.Exp(-0.5 * points[i].X));
            }

            return new List<PointF>(points);
        }

        /// <summary>
        /// Разн. метод 2-ого пор. аппрок.
        ///  /// <summary>
        /// <param name="A">Левый конец отрезка.</param>
        /// <param name="B">Правый конец отрезка.</param>
        /// <param name="Y">Значение искомой функции y в начальной точке A.</param>
        /// <param name="Z">Значение произвоодной искомой функции y в начальной точке A.</param>
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
            PointF startpoint2 = new PointF
            {
                X = (float)x[1],
                Y = (float)(points[0].Y + step * Z + (F(points[0].X, points[0].Y) + 1 * Z) * step * step * 0.5),
            };
            points[1] = startpoint2;


            for (int i = 2; i < points.Length; i++)
            {
                points[i].X = (float)x[i];
                points[i].Y = (float)((step * step * F(points[i - 1].X, points[0].Y) + 2 * points[i - 1].Y - (1 + 0.5 * step) * points[i - 2].Y) * (1 / (1 - 0.5 * step)));
            }

            return new List<PointF>(points);
        }

    }
}
