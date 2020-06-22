using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUtilTool.NLog.Connection
{
    public interface ILogConnection
    {
        ExceptionslessOptions exceptionslessOptions { get; set; }
    }
}
