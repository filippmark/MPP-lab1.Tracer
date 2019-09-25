using System.Collections.Generic;

namespace TracerClasses.Serializer
{
    interface ISerialize
    {
        void SerializeResult(List<ThreadDetails> threadsResult);
    }
}
