using System.Transactions;
using Application.Core.AspectOrientedProgramming.AOP.Interceptor;
using Castle.DynamicProxy;

namespace Application.Core.AspectOrientedProgramming.Aspects.Transaction;

    /// <summary>
    /// UnitOfWorkAspect works when method did not work successfully. Throws expected exception after transaction.
    /// </summary>
    public class UnitOfWorkAspect : MethodInterceptor
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();

                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();

                    throw;
                }
            }
        }
    }

