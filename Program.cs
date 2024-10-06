
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExpressionCalculator
{
    // Represent a token in the expression (number, operator, parentheses)
    public class Token
    {
        public string Type { get; private set; }
        public string Value { get; private set; }

        public Token(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }

    // Class for parsing the expression
    public class Parser
    {
        private string expression;
        private int index;

        public Parser(string expression)
        {
            this.expression = expression;
            this.index = 0;
        }

        // Reads numbers from the expression
        private Token ParseNumber()
        {
            int startIndex = index;
            while (index < expression.Length && (char.IsDigit(expression[index]) || expression[index] == '.'))
            {
                index++;
            }

            string number = expression.Substring(startIndex, index - startIndex);
            return new Token("number", number);
        }
    }

    // This class is for evaluates the expression
    public class ExpressionEvaluator
    {
        private List<Token> tokens;
        private int currentIndex;

        public ExpressionEvaluator(List<Token> tokens)
        {
            this.tokens = tokens;
            currentIndex = 0;
        }

        // Evaluates the expression
        public double Evaluate()
        {
            return ParseExpression();
        }

        // Handles the precedence of operations (addition and subtraction)
        private double ParseExpression()
        {
            double result = ParseTerm();

            while (currentIndex < tokens.Count && (tokens[currentIndex].Value == "+" || tokens[currentIndex].Value == "-"))
            {
                string op = tokens[currentIndex].Value;
                currentIndex++;
                double right = ParseTerm();

                if (op == "+")
                {
                    result += right;
                }
                else
                {
                    result -= right;
                }
            }

            return result;
        }

    }

