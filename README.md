| Area      |      Badges  |
|:----------|:-------------|
| Build | [![Build status](https://ci.appveyor.com/api/projects/status/6qx7d8dpnx8o5d4n?svg=true)](https://ci.appveyor.com/project/joymon/orchestration) |
| Code | ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/joymon/Orchestration) ![GitHub repo size](https://img.shields.io/github/repo-size/joymon/Orchestration) [![](https://tokei.rs/b1/github/joymon/Orchestration)](https://github.com/joymon/Orchestration) |
| Code Quality | [![Total alerts](https://img.shields.io/lgtm/alerts/g/joymon/Orchestration.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/joymon/Orchestration/alerts/) |
| Security | [![Known Vulnerabilities](https://snyk.io/test/github/joymon/Orchestration/badge.svg?targetFile=src/JoymonOnline.Orchestration/JoymonOnline.Orchestration.csproj)](https://snyk.io/test/github/joymon/Orchestration?targetFile=src/JoymonOnline.Orchestration/JoymonOnline.Orchestration.csproj) |
| Test | [![Test](https://img.shields.io/appveyor/tests/joymon/orchestration.svg)](https://ci.appveyor.com/project/joymon/orchestration)[![codecov](https://codecov.io/gh/joymon/Orchestration/branch/master/graph/badge.svg)](https://codecov.io/gh/joymon/Orchestration) |
| Issues | [![Average time to resolve an issue](http://isitmaintained.com/badge/resolution/joymon/Orchestration.svg)](http://isitmaintained.com/project/joymon/Orchestration "Average time to resolve an issue") [![Percentage of issues still open](http://isitmaintained.com/badge/open/joymon/Orchestration.svg)](http://isitmaintained.com/project/joymon/Orchestration "Percentage of issues still open") |
| Deployment | [![Build status](https://img.shields.io/nuget/v/Orchestration.svg)](https://www.nuget.org/packages/Orchestration)[![Build status](https://img.shields.io/nuget/dt/Orchestration.svg)](https://img.shields.io/nuget/dt/Orchestration.svg) |


# Orchestration
.Net library to orchestrate operations and commands. Helps to follow Single Responsibility Pattern
## When to use this
- You have some data/context and needs to perform series of operations on it.
- Where operations are independent.
- If you are ok on dealing with a state. Here in this case the context.
- If you are struck with .Net 4.0. On 4.5 there is [Banzai](https://github.com/eswann/Banzai) [nuget library](https://www.nuget.org/packages/Banzai/) which can do things in async way.

## How to use

Goto nuget package manager console and run the below command to get the package added to the project.

``` 
install-package Orchestration
```

### Simple usage
```cs
IOperationOrchestrator<int> orchestrator = new OperationOrchestrator<int>(new List<IOperation<int>>() { new FindSquare() });
orchestrator.Start(10);       
public class FindSquare : IOperation<int>
{
        void IOperation<int>.Execute(int context)
        {
                Console.WriteLine("Square of {0} is {1}", context, context * context);
        }
}
```
Output: Square of 10 is 100
### Working with 2 operations
```cs
IOperationOrchestrator<int> orchestrator = new OperationOrchestrator<int>(new List<IOperation<int>>()
        {
                new FindSquare(),
                new FindSquareRoot()
        });
orchestrator.Start(16);
internal class FindSquareRoot : IOperation<int>
{
    void IOperation<int>.Execute(int context)
    {
        Console.WriteLine("SquareRoot of {0} is {1}", context, Math.Sqrt(context));
    }
}
public class FindSquare : IOperation<int>
{
    void IOperation<int>.Execute(int context)
    {
        Console.WriteLine("Square of {0} is {1}", context, context * context);
    }
}
```

Output : Square of 9 is 81

SquareRoot of 9 is 3
### Using context to communicate between operation steps

Instead of feeding the output from one step to another step, its kept in the context object / state.

```cs
internal class CalculationContext
{
    public int[] Numbers { get; set; }
    public int Sum { get; set; }
    public int Average { get; set; }
}
IOperationOrchestrator<CalculationContext> orchestrator = new OperationOrchestrator<CalculationContext>(
    new List<IOperation<CalculationContext>>() {
        new FindSumOperation(),
        new FindAverageOperation()
    });
CalculationContext context = new CalculationContext() { Numbers = new int[] { 1, 2, 3, 6 } };
orchestrator.Start(context);
Console.WriteLine("Sum={0},Average={1}", context.Sum, context.Average);
internal class FindSumOperation : IOperation<CalculationContext>
{
    void IOperation<CalculationContext>.Execute(CalculationContext context)
    {
        context.Sum = context.Numbers.Sum();
    }
}
internal class FindAverageOperation : IOperation<CalculationContext>
{
    void IOperation<CalculationContext>.Execute(CalculationContext context)
    {
        context.Average = context.Sum / context.Numbers.Length;
    }
}
```
Output : Sum=12,Average=3
