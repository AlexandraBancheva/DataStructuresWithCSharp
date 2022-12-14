namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (parentheses.Length % 2 == 1)
            {
                return false;
            }
            var stackParentheses = new Stack<char>(parentheses.Length / 2); 
            foreach (var currentElement in parentheses)
            {
                char expectedElement = default;
                switch (currentElement)
                {
                    case '}':
                        expectedElement = '{';
                        break;
                    case ']':
                        expectedElement = '[';
                        break;
                    case ')':
                        expectedElement = '(';
                        break;
                    default:
                        stackParentheses.Push(currentElement);
                        break;
                }

                if (expectedElement == default)
                {
                    continue;
                }

                if (stackParentheses.Pop() != expectedElement)
                {
                    return false;
                }

            }
            return stackParentheses.Count == 0;
        }
    }
}
