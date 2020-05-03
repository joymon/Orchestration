using JoymonOnline.Orchestration.Core;
using System.Linq;

namespace JoymonOnline.Orchestration.Tests
{
    internal class FindSumOperation : IOperation<CalculationContext>
    {
        void IOperation<CalculationContext>.Execute(CalculationContext context)
        {
            context.Sum = context.Numbers.Sum();
        }
    }
}
