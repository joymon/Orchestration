using JoymonOnline.Orchestration.Core;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    abstract internal class StatePersistedAsyncOperation: IAsyncOperation<ExeContext>
    {

        protected virtual Task<bool> CanExecuteInternal(ExeContext context)
        {
            return Task.FromResult(true);
        }
        abstract protected Task<ExeContext> ExecuteInternal(ExeContext context);
        #region Interface implementation
        async Task<bool> IAsyncOperation<ExeContext>.CanExecute(ExeContext context)
        {
            return await CanExecuteInternal(context);
        }

        async Task<ExeContext> IAsyncOperation<ExeContext>.Execute(ExeContext context)
        {
            ExeContext resultContext = await ExecuteInternal(context);
            Persist(resultContext);
            return resultContext;
        }
        #endregion
        private void Persist(ExeContext context)
        {
            string serializedContext= JsonConvert.SerializeObject(context);
            File.WriteAllText("AsyncOpState.json",serializedContext);
        }

    }
}
