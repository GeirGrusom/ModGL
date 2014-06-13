using System.Collections.Generic;
using System.Linq;

namespace SpecBuilder.Parser
{
    public class Command
    {
        private readonly DataType _dataType;
        private readonly DataType[] _arguments;
        public DataType ReturnType { get { return _dataType; } }
        public IList<DataType> Arguments { get { return _arguments; } }

        public Command(DataType dataType, IEnumerable<DataType> arguments)
        {
            _dataType = dataType;
            _arguments = arguments.ToArray();
        }

        public override string ToString()
        {
            return _dataType + "(" + string.Join(", ", Arguments) + ")";
        }
    }
}