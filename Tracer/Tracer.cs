using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace TracerClasses
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, ThreadDetails> threads;

        public Tracer()
        {
            threads = new ConcurrentDictionary<int, ThreadDetails>();
        }

        public void StartTrace()
        {
            StackTrace st = new StackTrace(true);
            MethodBase methodBase = st.GetFrame(1).GetMethod();
            string methodName = methodBase.Name;
            string className = methodBase.ReflectedType.Name;
            Method method = new Method(methodName, className);
            int id = Thread.CurrentThread.ManagedThreadId;
            ThreadDetails thread = new ThreadDetails(id);
            AddMethodToTracerDictionary(id, thread, method);
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
            RemoveMethodFromTracerDictionary(id, thread);
        }

        private void RemoveMethodFromTracerDictionary(int id, ThreadDetails thread)
        {
            if (threads.ContainsKey(id))
            {
                threads.TryGetValue(id, out thread);
                thread.StopTraceMethod();
            }
        }


        public List<ThreadDetails> GetTraceResult()
        {
            List<ThreadDetails> result = new List<ThreadDetails>();
            try
            {
                foreach (var thread in threads)
                {
                    result.Add(thread.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return result;
        }



    }
}
