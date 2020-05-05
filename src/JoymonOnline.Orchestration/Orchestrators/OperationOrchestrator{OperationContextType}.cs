using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoymonOnline.Orchestration.Orchestrators
{

    public class OperationOrchestrator<OperationContextType> : IOperationOrchestrator<OperationContextType>
    {
        IEnumerable<IOperation<OperationContextType>> _operations;
        readonly IOperationsProvider<OperationContextType> _provider;
        readonly IOrchestrationStrategy<OperationContextType> _strategy = new SequentialOrchestrationStrategy<OperationContextType>();

        protected virtual IEnumerable<IOperation<OperationContextType>> Operations
        {
            get
            {
                if (_operations == null)
                {
                    _operations = _provider.GetOperations();
                }
                return _operations;
            }
        }

        #region "Constructors"
        public OperationOrchestrator()
        {
            _operations = new List<IOperation<OperationContextType>>();
        }
        public OperationOrchestrator(IOperationsProvider<OperationContextType> provider)
            : this(provider, new SequentialOrchestrationStrategy<OperationContextType>())
        {

        }
        public OperationOrchestrator(IOperationsProvider<OperationContextType> provider,
            IOrchestrationStrategy<OperationContextType> strategy)
        {
            _provider = provider;
            _strategy = strategy;
        }
        public OperationOrchestrator(IEnumerable<IOperation<OperationContextType>> operations)
        {
            _operations = operations;
        }
        #endregion

        #region Methods
        protected virtual void InternalStart(OperationContextType context)
        {
            _strategy.Start(Operations, context);
        }
        protected virtual void InternalStop()
        {
            _strategy.Stop();
        }
        #endregion

        #region IOperationOrchestrator
        void IOperationOrchestrator.Start()
        {
            _strategy.Start(this.Operations, default(OperationContextType));
        }
        void IOperationOrchestrator<OperationContextType>.Start(OperationContextType context)
        {
            InternalStart(context);
        }

        void IOperationOrchestrator.Stop()
        {
            InternalStop();
        }
        #endregion
    }


}
