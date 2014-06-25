using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModGL.VertexInfo;

namespace ModGL.Shaders.Linq
{
    public static class VertexDescriptorExtensions
    {
        public static VertexShaderQueryable<TVertex> CreateVertexShader<TVertex>(
            this IVertexDescriptor<TVertex> descriptor)
        {
            return new VertexShaderQueryable<TVertex>();
        }

        public static IQueryable CreateFragmentShader<TVertex>(this VertexShaderQueryable<TVertex> vertexShader)
        {
            throw new NotImplementedException();
        }

        public static string ToShaderString<TVertex>(this IQueryable<TVertex> query)
        {
            if(!(query is VertexShaderQueryable<TVertex>))
                throw new InvalidOperationException("Can only create a shader from VertexShaderQueryable.");
            var visitor = new VertexShaderQueryVisitor();
            visitor.Visit(query.Expression);
            return visitor.ToString();
        }
    }
}
