using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TracerClasses.Serializer
{
    public class XMLSerializer : ISerialize
    {
        private List<ThreadDetails> threads;
        private List<XElement> xmlThreads;


        public void SerializeResult(List<ThreadDetails> threadsResult)
        {
            threads = threadsResult;
            xmlThreads = new List<XElement>();
            foreach (var thread in threads)
            {
                XElement xmlThread = new XElement("thread",
                    new XAttribute("id", thread.Id),
                    new XAttribute("time", thread.ExecutionTime + "ms"));
                foreach (var rootMethod in thread.RootMethods)
                {
                    XElement xmlMethod = SerializeMethod(rootMethod);
                    xmlThread.Add(xmlMethod);
                }
                xmlThreads.Add(xmlThread);
            }
        }
        
        public XElement SerializeMethod(Method method)
        {
            XElement xmlMethod = new XElement("method", 
                new XAttribute("name", method.Name),
                new XAttribute("class", method.ClassName),
                new XAttribute("time", method.ExecutionTime));
            foreach (var nestedMethod in method.NestedMethods)
            {
                xmlMethod.Add(SerializeMethod(nestedMethod));
            }
            return xmlMethod;
        }

        public void SerializeResultAndPutToFile(List<ThreadDetails> threadsResult)
        {
            SerializeResult(threadsResult);
        }

        public void SerializeResultAndPutToConsole(List<ThreadDetails> threadsResult)
        {
            SerializeResult(threadsResult);
            XElement root = new XElement("root");
            foreach(var thread in xmlThreads)
            {
                root.Add(thread);
            }
            Console.WriteLine(root);
        }
    }
}
