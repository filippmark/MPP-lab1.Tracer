using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tracer
{
    class Thread
    {
        public int Id { get; private set; }
        public List<Method> Methods { get; private set;} 
        public Stack<Method> RunningMethods { get; private set; }
        public List<Method> RootMethods { get; private set; }
        
        public Thread(int id)
        {
            Id = id;
            Methods = new List<Method>();

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
                if (RunningMethods.Count == 1)
                {
                    RootMethods.Add(executedMethod);
                }
            }
        }
    }
}
