
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


    }