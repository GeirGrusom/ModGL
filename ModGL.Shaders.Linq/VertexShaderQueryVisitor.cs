using System.Linq.Expressions;
using System.Text;
using ModGL.NativeGL;

namespace ModGL.Shaders.Linq
{

    public class UniformDeclarativeVisitor : ExpressionVisitor
    {
        private readonly StringBuilder builder;

        public UniformDeclarativeVisitor()
        {
            builder = new StringBuilder();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if(node.NodeType != ExpressionType.Assign)
                throw new InvalidOperationException("Cannot perform anything but an assignment on uniform declarations.");

            

            return base.VisitBinary(node);
        }
    }

    public class VertexShaderQueryVisitor : ExpressionVisitor
    {
        private readonly StringBuilder builder;

        public VertexShaderQueryVisitor()
        {
            builder = new StringBuilder();
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Select") // Assume select expression    
            {
                if (IsSelectExpressionUniformDef(node))
                {
                    var visitor = new UniformDeclarativeVisitor();
                    visitor.Visit(node.Arguments[1]);
                }
            }
            return base.VisitMethodCall(node);
        }

        private static bool IsSelectExpressionUniformDef(MethodCallExpression node)
        {
            
        }
    }
}
