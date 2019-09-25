using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TracerClasses.Serializer
{
    public class JSONSerializer : ISerialize
    {
        private List<ThreadDetails> threads;

        public void SerializeResult(List<ThreadDetails> threadsResult)
        {
            threads = threadsResult;
            foreach (var thread in threads)
            {
                foreach (var rootMethod in thread.RootMethods)
                {
                    Console.WriteLine(rootMethod.Name);
                    JObject jsonMethod = SerializeMethod(rootMethod);
                    Console.WriteLine(jsonMethod.ToString());
                }
            }
        }

        public JObject SerializeMethod(Method method)
        {
            JObject jsonMethod = new JObject();
            jsonMethod["Name"] = method.Name;
            jsonMethod["Class"] = method.ClassName;
            jsonMethod["Time"] = method.ExecutionTime;
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
