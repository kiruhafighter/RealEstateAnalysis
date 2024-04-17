using System.Linq.Expressions;

namespace Services.Extensions;

internal static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> CombineExpressions<T>(
        params Expression<Func<T, bool>>[] expressions)
    {
        if (expressions.Length == 0)
        {
            return _ => true;
        }

        var combinedExpression = expressions.First();
        var basicParameter = combinedExpression.Parameters.First();

        for (int i = 1; i < expressions.Length; i++)
        {
            var currentParameter = expressions[i].Parameters.First();
            var currentBody = expressions[i].Body;

            var parameterReplacer = new ExpressionParameterReplacer(currentParameter, basicParameter);
            var nextExpressionBody = parameterReplacer.Visit(currentBody);
            
            combinedExpression = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    combinedExpression.Body,
                    nextExpressionBody),
                combinedExpression.Parameters);
        }
        
        return combinedExpression;
    }
}

internal sealed class ExpressionParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _oldParameter;
    private readonly ParameterExpression _newParameter;
    
    public ExpressionParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
    {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == _oldParameter ? _newParameter : Visit(node);
    }
}