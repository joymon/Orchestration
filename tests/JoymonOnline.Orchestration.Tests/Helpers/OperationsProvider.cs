using JoymonOnline.Orchestration.Core;
using System.Collections.Generic;

namespace JoymonOnline.Orchestration.Tests
{
    public class OperationsProvider : IOperationsProvider<int>
    {
        IEnumerable<IOperation<int>> IOperationsProvider<int>.GetOperations()
        {
            return new List<IOperation<int>>() { new FindSquare() };
        }
    }
}
