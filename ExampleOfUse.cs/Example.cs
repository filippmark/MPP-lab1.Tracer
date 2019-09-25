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

        public static void Main()
        {
            Thread thread1 = new Thread( new ThreadStart(NestedMethod));
            Thread thread2 = new Thread(new ThreadStart(NestedMethod));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
        }
        public static void NestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            FirstNestedMethod();
            tracer.StopTrace();
        }

        public static void FirstNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            SecondNestedMethod();
            tracer.StopTrace();
        }

        public static void SecondNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

    }
}
