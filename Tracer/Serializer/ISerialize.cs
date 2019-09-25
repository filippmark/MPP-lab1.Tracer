using System.Collections.Generic;

namespace TracerClasses.Serializer
{
    public interface ISerialize
    {
        void SerializeResultAndPutToFile(List<ThreadDetails> threadsResult);
        void SerializeResultAndPutToConsole(List<ThreadDetails> threadsResult);

    }
}
