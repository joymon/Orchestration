# Orchestration
.Net library to orchestrate operations and commands. Helps to follow Single Responsibility Pattern
## When to use this
- You have some data/context and needs to perform series of operations on it.
- Where operations are independent.
- If you are ok on dealing with a state. Here in this case the context. 
## How to use
### Simple usage
<pre>
<code>
IOperationOrchestrator<int> orchestrator = new OperationOrchestrator<int>(new List<IOperation<int>>() { new FindSquare() });
orchestrator.Start(10);       
public class FindSquare : IOperation<int>
{
        void IOperation<int>.Execute(int context)
    {
        Console.WriteLine("Square of {0} is {1}", context, context * context);
    }
}
    </code>
    </pre>
#### Output
Square of 10 is 100
