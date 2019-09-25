using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace TracerClasses.Serializer
{
    public class JSONSerializer : ISerialize
    {
        private List<ThreadDetails> threads;
        private List<JObject> jsonThreads;
        private void SerializeResult(List<ThreadDetails> threadsResult)
        {
            threads = threadsResult;
            jsonThreads = new List<JObject>();
            foreach (var thread in threads)
            {
                JObject jsonThread = new JObject();
                jsonThread["id"] = thread.Id;
                jsonThread["time"] = thread.ExecutionTime;
                JArray jsonMethods = new JArray();
                foreach (var rootMethod in thread.RootMethods)
                {
                    JObject jsonMethod = SerializeMethod(rootMethod);
                    jsonMethods.Add(jsonMethod);
                }
                jsonThread["methods"] = jsonMethods;
                jsonThreads.Add(jsonThread);
            }
        }

        private JObject SerializeMethod(Method method)
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

        public void SerializeResultAndPutToFile(List<ThreadDetails> threadsResult)
        {
            SerializeResult(threadsResult);
            using (StreamWriter file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"\" + "json.txt"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                MakeRoot().WriteTo(writer);
            }
        }

        public void SerializeResultAndPutToConsole(List<ThreadDetails> threadsResult)
        {
            SerializeResult(threadsResult);
            Console.WriteLine(MakeRoot());
        }

        private JObject MakeRoot()
        {
            JObject root = new JObject();
            JArray threads = new JArray();
            foreach (var thread in jsonThreads)
            {
                threads.Add(thread);
            }
            root["threads"] = threads;
            return root;
        }

    }
}
