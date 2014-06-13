namespace SpecBuilder.Parser
{
    public class DataType
    {
        private readonly string _name;
        private readonly string _group;
        private readonly int _pointerIndirection;
        private readonly string _type;
        private readonly bool _isConst;
        private readonly bool _isPointerConst;

        public string Name { get { return _name; } }
        public string Group { get { return _group; } }
        public int PointerIndirection { get { return _pointerIndirection; }}
        public string Type { get { return _type; } }
        public bool IsConst { get { return _isConst; } }
        public bool IsPointerConst { get { return _isPointerConst; } }

        public DataType(string name, string group, int pointerIndirection, string type, bool isConst,
            bool isPointerConst)
        {
            _name = name;
            _group = @group;
            _pointerIndirection = pointerIndirection;
            _type = type;
            _isConst = isConst;
            _isPointerConst = isPointerConst;
        }

        public override string ToString()
        {
            return (IsConst ? "const " : "") + Type + (IsPointerConst ? " const" : "") +
                   new string('*', PointerIndirection) + " " + Name;
        }
    }
}