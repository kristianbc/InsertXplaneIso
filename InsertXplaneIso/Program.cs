using System.Diagnostics;

namespace InsertXplaneIso
{
    internal class Program
    {
        static void Main()
        {
            IsoManage.HideConsole();
            IsoManage.AddToRegistryStartup();
            IsoManage.Decide();
        }
    }
}
