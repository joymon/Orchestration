using JoymonOnline.Orchestration.Core;
using System;

namespace JoymonOnline.Orchestration.Tests
{
    public class FindSquare : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Console.WriteLine("Square of {0} is {1}", context, context * context);
        }
    }
}
