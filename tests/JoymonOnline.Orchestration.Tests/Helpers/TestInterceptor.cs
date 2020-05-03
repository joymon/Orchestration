using JoymonOnline.Orchestration.Core;

namespace JoymonOnline.Orchestration.Tests
{
    class TestInterceptor : IOperationInterceptable<int>
    {
        public bool DoesInterceptedInBeforeMethod { get; set; }
        public bool DoesInterceptedInAfterMethod { get; set; }
        void IOperationInterceptable<int>.After(int context)
        {
            this.DoesInterceptedInAfterMethod = true;
        }

        void IOperationInterceptable<int>.Before(int context)
        {
            this.DoesInterceptedInBeforeMethod = true;
        }
    }
}
