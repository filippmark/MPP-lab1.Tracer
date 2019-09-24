﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Tracer
{
    class Method
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public long ExecutionTime { get; private set; }
        public List<Method> NestedMethods { get; set; }

        private Stopwatch stopwatch;

        public void MethodDetails(string name, string className)
        {
            Name = name;
            ClassName = className;
            stopwatch = new Stopwatch();
            NestedMethods = new List<Method>();
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            ExecutionTime = stopwatch.ElapsedMilliseconds;
        }

        public void AddNestedMethod(Method method)
        {
            NestedMethods.Add(method);
        }

    }
}
