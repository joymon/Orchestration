using JoymonOnline.Orchestration.Core;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    public class AsyncStateManagementOperationInterceptor : IAsyncOperationInterceptable<ExeContext>
    {
        internal const string stateFilePath = "AsyncOpStateFromInterceptable.json";
        public int BeforeExecuteCallCount { get; set; }
        public int AfterExecuteCallCount { get; set; }
        async Task IAsyncOperationInterceptable<ExeContext>.AfterExecute(IAsyncOperation<ExeContext> op, ExeContext context)
        {
            await Task.FromResult(2); //Dummy to avoid warning
            AfterExecuteCallCount += 1;
            string serializedContext = JsonConvert.SerializeObject(context);
            File.WriteAllText(stateFilePath, serializedContext);
        }

        Task<bool> IAsyncOperationInterceptable<ExeContext>.BeforeExecute(IAsyncOperation<ExeContext> op, ExeContext context)
        {
            this.BeforeExecuteCallCount += 1;
            return Task.FromResult(true);
        }
    }
}