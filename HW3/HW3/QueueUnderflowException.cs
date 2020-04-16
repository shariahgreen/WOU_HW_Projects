using System;
using System.Runtime.Serialization;

namespace HW3
{
    public class QueueUnderflowException : SystemException
    {
        public QueueUnderflowException() : base()
        {
        }

        public QueueUnderflowException(string message) : base(message)
        {           
        }

    }
}