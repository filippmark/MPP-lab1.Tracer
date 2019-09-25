﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TracerClasses
{
    interface ITracer
    {
        // вызывается в начале замеряемого метода
        void StartTrace();

        // вызывается в конце замеряемого метода
        void StopTrace();

        // получить результаты измерений
        List<ThreadDetails> GetTraceResult();
    }
}
