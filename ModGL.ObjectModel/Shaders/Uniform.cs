using ModGL.NativeGL;

namespace ModGL.ObjectModel.Shaders
{
    public abstract class Uniform<TValueType>
    {
        protected Uniform(IOpenGL30 gl, string name, int location)
        {
            Name = name;
            Location = location;
            _gl = gl;
        }


        private readonly IOpenGL30 _gl;

        private TValueType _value;

        public int Location { get; private set; }

        public string Name { get; private set; }

        public abstract void SetData(IOpenGL30 gl);

        public TValueType Value
        {
            get { return _value; }
            set
            {
                _value = value;
                SetData(_gl);
            }
        }
    }
}
