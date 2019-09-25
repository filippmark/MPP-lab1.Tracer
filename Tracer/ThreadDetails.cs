﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TracerClasses
{
    public class ThreadDetails
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
            if (RunningMethods.Count() != 0)
            {
                Method topMethod = RunningMethods.Peek();
                topMethod.AddNestedMethod(method);
            }
            RunningMethods.Push(method);
            method.StartTrace();
        }

        public void StopTraceMethod()
        {
            if (RunningMethods.Count > 0)
            {
                Method executedMethod = RunningMethods.Pop();
                executedMethod.StopTrace();
                ExecutionTime += executedMethod.ExecutionTime;
                Console.WriteLine("{0} ms, {1} id", executedMethod.ExecutionTime, Id);
                if (RunningMethods.Count == 0)
                {
                    RootMethods.Add(executedMethod);
                }
            }
        }
    }
}
