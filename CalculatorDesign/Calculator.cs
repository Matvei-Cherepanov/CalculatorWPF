using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorDesign
{
    public class Calculator
    {
        public List<string> operators = new List<string> { "+", "-", "×", "÷", "%"};

        public double Calculate(string expression)
        {
            try
            {
                return ProcessExpression(expression);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }

        private bool IsOperator(char c)
        {
            return operators.Contains(c.ToString());
        }

        private double ProcessExpression(string expression)
        {
            expression = expression.Trim();

            // обработка минуса в начале
            if (expression.StartsWith("-"))
            {
                if (!expression.Contains("+") && !expression.Contains('-') &&
                    !expression.Contains("×") && !expression.Contains("÷"))
                {
                    return double.Parse(expression);
                }
                expression = "0" + expression; // 0-2-3
            }

            // поиск операторов сложения и вычитания
            int index = FindOperator(expression, new List<string> { "+", "-" });

            if (index != -1)
            {
                string op = expression[index].ToString();
                string left = expression.Substring(0, index);
                string right = expression.Substring(index + 1);

                double leftVal = ProcessExpression(left);
                double rightVal = ProcessExpression(right);

                if (op == "+")
                    return leftVal + rightVal;
                else if (op == "-")
                    return leftVal - rightVal;
            }

            // Поиск операторов умножения и деления
            index = FindOperator(expression, new List<string> { "×", "÷" });

            if (index != -1)
            {
                string op = expression[index].ToString();
                string left = expression.Substring(0, index);
                string right = expression.Substring(index + 1);

                double leftVal = ProcessExpression(left);
                double rightVal = ProcessExpression(right);

                if (op == "×")
                    return leftVal * rightVal;
                else if (op == "÷")
                {
                    if (rightVal == 0)
                        throw new DivideByZeroException("Деление на ноль!");
                    return leftVal / rightVal;
                }
            }

            // обработка процента
            index = FindOperator(expression, new List<string> { "%" });
            if (index != -1)
            {
                string left = expression.Substring(0, index);
                double leftVal = ProcessExpression(left);

                return leftVal / 100.0;
            }

            return double.Parse(expression);
        }

        private int FindOperator(string expr, List<string> targetOperators)
        {
            for (int i = expr.Length - 1; i >= 0; i--)
            {
                string currentChar = expr[i].ToString();
                if (targetOperators.Contains(currentChar))
                {
                    // скип знака в начале
                    if (operators.Contains(currentChar) && (i == 0 || IsOperator(expr[i - 1])))
                    {
                        continue;
                    }
                    return i;
                }
            }
            return -1;
        }
    }
}