using System;

namespace HW3
{
    public class LinkedQueue<T> : IQueueInterface<T>
    {
        private Node<T> front;
        private Node<T> rear;

        public LinkedQueue()
        {
            front = null;
            rear = null;
        }

        public T push(T element)
        {
            if (element == null)
            {
                throw new NullReferenceException();
            }
            if (isEmpty())
            {
                Node<T> tmp = new Node<T>(element, null);
                front = tmp;
                rear = tmp;
            }
            else
            {
                Node<T> tmp = new Node<T>(element, null);
                rear.next = tmp;
                rear = tmp;
            }
            return element;
        }

        public T pop()
        {
            T tmp;
            if (isEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (front == rear)
            {
                tmp = front.data;
                front = null;
                rear = null;
            }
            else
            {
                tmp = front.data;
                front = front.next;
            }
            return tmp;
        }

        public T peek()
        {
            if (isEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when peek was invoked.");
            }
            return front.data;
        }

        public Boolean isEmpty()
        {
            if (front == null && rear == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}