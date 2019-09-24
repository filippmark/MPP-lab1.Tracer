using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Concurrent;
using System.Threading;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, ThreadDetails> threads;

        public void StartTrace()
        {
            StackTrace st = new StackTrace(true);
            MethodBase methodBase = st.GetFrame(1).GetMethod();
            string methodName = methodBase.Name;
            string className = methodBase.ReflectedType.Name;
            Method method = new Method(methodName, className);
            int id = Thread.CurrentThread.ManagedThreadId;
            ThreadDetails thread = new ThreadDetails(id);
        }

        private void AddMethodToTracerDictionary(int id, ThreadDetails thread, Method method)
        {
            if (threads.ContainsKey(id))
            {
                threads.TryGetValue(id, out thread);
                thread.StartTraceMethod(method);
            }
            else
            {
                thread.StartTraceMethod(method);
                threads.TryAdd(id, thread);
            }
        }

        public void StopTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            ThreadDetails thread = new ThreadDetails(id);
        }

        public void RemoveMethodFromTracerDictionary(int id, ThreadDetails thread)
        {
            if (threads.ContainsKey(id))
            {
                threads.TryGetValue(id, out thread);
                thread.StopTraceMethod();     
            }
        }


        public List<string> GetTraceResult()
        {

        }



    }
}
