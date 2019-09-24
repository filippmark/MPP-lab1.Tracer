using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {

        public void StartTrace()
        {
            StackTrace st = new StackTrace(true);
            MethodBase methodBase = st.GetFrame(1).GetMethod();
            string methodName = methodBase.Name;
            string className = methodBase.ReflectedType.Name;
            Method method = new Method(methodName, className);
        }

        public void StopTrace()
        {

        }

        public List<string> GetTraceResult()
        {

        }



    }
}
