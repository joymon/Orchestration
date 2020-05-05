using JoymonOnline.Orchestration.Core;
using System;

namespace JoymonOnline.Orchestration.Tests
{
    internal class FindAverageOperation : IOperation<CalculationContext>
    {
        void IOperation<CalculationContext>.Execute(CalculationContext context)
        {
            Console.WriteLine($"{nameof(FindAverageOperation)}.Execute - Start {DateTime.UtcNow}");
            context.Average = context.Sum / context.Numbers.Length;
        }
    }
}
