using System.Threading;
using TracerClasses;
using TracerClasses.Serializer;

namespace ExampleOfUse
{
    public class Example
    {
        private static Tracer tracer = new Tracer();
        private static XMLSerializer serializer = new XMLSerializer();

        public static void Main()
        {
            Thread thread1 = new Thread(new ThreadStart(NestedMethod));
            Thread thread2 = new Thread(new ThreadStart(NestedMethod));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            serializer.SerializeResult(tracer.GetTraceResult());
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
            ThirdNestedMethod();
            tracer.StopTrace();
        }

        public static void SecondNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        public static void ThirdNestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            tracer.StopTrace();
        }
    }
}
