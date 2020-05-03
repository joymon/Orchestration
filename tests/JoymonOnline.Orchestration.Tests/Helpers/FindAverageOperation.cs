using JoymonOnline.Orchestration.Core;

namespace JoymonOnline.Orchestration.Tests
{
    internal class FindAverageOperation : IOperation<CalculationContext>
    {
        void IOperation<CalculationContext>.Execute(CalculationContext context)
        {
            context.Average = context.Sum / context.Numbers.Length;
        }
    }
}
