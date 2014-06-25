using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ModGL.Shaders.Linq
{
    public class VertexShaderQueryable<TVertex> : IQueryable<TVertex>
    {

        public VertexShaderQueryable()
        {
            Provider = new VertexShaderQueryProvider();
            ElementType = typeof(TVertex);
            Expression = Expression.Constant(this);
        }

        public VertexShaderQueryable(Expression expression, IQueryProvider provider)
        {
            Provider = provider;
            ElementType = typeof(TVertex);
            Expression = expression;

        }

        public IEnumerator<TVertex> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression { get; private set; }
        public Type ElementType { get; private set; }
        public IQueryProvider Provider { get; private set; }
    }
}
