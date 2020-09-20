using System;
using System.Collections.Generic;

namespace Expenses.API.Models
{
    public class ErrorModel
    {
        public bool Success => false;
        public List<string> Messages { get; private set; }

        public ErrorModel(List<string> messages)
        {
            Messages = messages ?? new List<string>();
        }

        public ErrorModel (string message)
        {
            Messages = new List<string>();
            if (!string.IsNullOrEmpty(message))
            {
                Messages.Add(message);
            }
        }
    }
}
