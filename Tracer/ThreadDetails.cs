using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tracer
{
    class ThreadDetails
    {
        public int Id { get; private set; }
        public long ExecutionTime { get; private set; }
        public Stack<Method> RunningMethods { get; private set; }
        public List<Method> RootMethods { get; private set; }
        
        public ThreadDetails(int id)
        {
            Id = id;
            ExecutionTime = 0;
            RunningMethods = new Stack<Method>();
            RootMethods = new List<Method>();
        }

        public void StartTraceMethod(Method method)
        {
            if (RunningMethods.Count() != 0 )
            {
                Method topMethod = RunningMethods.Peek();
                topMethod.AddNestedMethod(method);
            }
            RunningMethods.Push(method);
        }
          
        public void StopTraceMethod()
        {
            if (RunningMethods.Count > 0)
            {
                Method executedMethod = RunningMethods.Pop();
                executedMethod.StopTrace();
                ExecutionTime += executedMethod.ExecutionTime;
                if (RunningMethods.Count == 1)
                {
                    RootMethods.Add(executedMethod);
                }
            }
        }
    }
}
