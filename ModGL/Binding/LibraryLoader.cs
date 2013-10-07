using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModGL.Binding
{
    public interface ILibrary : IExtensionSupport
    {
        string Name { get; }
    }
    public interface ILibraryLoader
    {
        ILibrary Load(string moduleName);
    }
}
