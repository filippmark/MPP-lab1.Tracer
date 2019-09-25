using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TracerClasses;
using System.Threading;

namespace ExampleOfUse
{
    public class Example
    {
        private static Tracer tracer = new Tracer();


        public static void Main(string[] args)
        {
            tracer.StartTrace();
            Thread.Sleep(11);
            FirstNestedMethod();
            tracer.StopTrace();
        }

        public static void FirstNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(12);
            SecondNestedMethod();
            tracer.StopTrace();
        }

        public static void SecondNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(13);
            tracer.StopTrace();
        }

    }
}
