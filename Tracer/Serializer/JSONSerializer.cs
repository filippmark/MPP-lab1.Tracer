using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TracerClasses.Serializer
{
    public class JSONSerializer : ISerialize
    {
        private List<ThreadDetails> threads;
        private List<JObject> jsonThreads;
        public void SerializeResult(List<ThreadDetails> threadsResult)
        {
            threads = threadsResult;
            jsonThreads = new List<JObject>();
            foreach (var thread in threads)
            {
                JObject jsonThread = new JObject();
                jsonThread["id"] = thread.Id;
                JArray jsonMethods = new JArray();
                foreach (var rootMethod in thread.RootMethods)
                {
                    JObject jsonMethod = SerializeMethod(rootMethod);
                    jsonMethods.Add(jsonMethod);
                }
                jsonThread["methods"] = jsonMethods;
                Console.WriteLine(jsonThread.ToString());
                jsonThreads.Add(jsonThread);
            }
        }

        public JObject SerializeMethod(Method method)
        {
            JObject jsonMethod = new JObject();
            jsonMethod["Name"] = method.Name;
            jsonMethod["Class"] = method.ClassName;
            jsonMethod["Time"] = method.ExecutionTime + " ms";
            JArray methods = new JArray();
            foreach (var nestedMethod in method.NestedMethods)
            {
                methods.Add(SerializeMethod(nestedMethod));
            }
            jsonMethod["Methods"] = methods;
            return jsonMethod;
        }


    }
}
