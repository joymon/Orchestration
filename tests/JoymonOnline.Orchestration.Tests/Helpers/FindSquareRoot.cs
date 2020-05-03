using JoymonOnline.Orchestration.Core;
using System;

namespace JoymonOnline.Orchestration.Tests
{
    internal class FindSquareRoot : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Console.WriteLine("SquareRoot of {0} is {1}", context, Math.Sqrt(context));
        }
    }
}
