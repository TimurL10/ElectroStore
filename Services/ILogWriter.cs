using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Services
{
    public interface ILogWriter
    {
        public void LogWrite(string logMessage);

    }
}
