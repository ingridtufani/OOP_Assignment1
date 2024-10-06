
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

        // Handles multiplication and division
        private double ParseTerm()
        {
            double result = ParseFactor();

            while (currentIndex < tokens.Count && (tokens[currentIndex].Value == "*" || tokens[currentIndex].Value == "/"))
            {
                string op = tokens[currentIndex].Value;
                currentIndex++;
                double right = ParseFactor();

                if (op == "*")
                {
                    result *= right;
                }
                else
                {
                    result /= right;
                }
            }

            return result;
        }

        // Handles parentheses, numbers, and unary operators
        private double ParseFactor()
        {
            // Handle parentheses
            if (tokens[currentIndex].Value == "(")
            {
                currentIndex++;
                double result = ParseExpression();
                currentIndex++;  // Skip the closing parenthesis
                return result;
            }

            // Handle unary operators
            if (tokens[currentIndex].Type == "unary")
            {
                currentIndex++;
                return -ParseFactor();
            }

            // Handle numbers
            if (tokens[currentIndex].Type == "number")
            {
                double number = double.Parse(tokens[currentIndex].Value, CultureInfo.InvariantCulture);
                currentIndex++;
                return number;
            }

            throw new Exception("Unexpected token in the expression");
        }
    }
     public class Calculator
{
    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Enter a mathematical expression (or type 'exit' to quit):");
            string input = Console.ReadLine();

            // Check if the input is a command (like 'exit')
            if (input.Trim().ToLower() == "exit")
            {
                ProcessCommand(input);
                break;  // Exit the loop after processing the command
            }

            try
            {
                Parser parser = new Parser(input);
                List<Token> tokens = parser.ParseTokens();

                ExpressionEvaluator evaluator = new ExpressionEvaluator(tokens);
                double result = evaluator.Evaluate();

                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // Process user commands like 'exit'
    private void ProcessCommand(string command)
    {
        if (command.Trim().ToLower() == "exit")
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Unknown command: " + command);
        }
    }
}

// Main program entry point
class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        calculator.Start();
    }
}

}

