using System.Collections.Generic;

namespace playNET
{
    public interface ISinger
    {
        void Sing(IEnumerable<string> tracks);
        void ShutUp();
    }
}