using System.Collections.Generic;

namespace playNET
{
    public interface IFileLocator
    {
        IEnumerable<string> FindAll();
    }
}