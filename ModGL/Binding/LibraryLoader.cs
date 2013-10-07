using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        [Pure]
        ILibrary Load(string moduleName);
    }
}
