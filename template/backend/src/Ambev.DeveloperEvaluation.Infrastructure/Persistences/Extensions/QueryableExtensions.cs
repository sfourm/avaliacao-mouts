using System.Linq.Expressions;
using System.Reflection;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.Persistences.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> IncludeWithFilter<TEntity>(this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>> filterExpression)
        where TEntity : class, IAggregateRoot
    {
        var includeProperties = GetIncludeProperties(filterExpression);

        query = includeProperties.OfType<string>()
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return query.Where(filterExpression);
    }

    private static List<string?> GetIncludeProperties<TEntity>(Expression<Func<TEntity, bool>> filterExpression)
    {
        List<string?> includeProperties = [];
        IncludeVisitor visitor = new();

        visitor.Visit(filterExpression);

        var properties = GetAllowedProperties(typeof(TEntity)).ToList();

        var activeProperties = GetActiveProperties(filterExpression);

        foreach (var property in visitor.IncludeProperties
                     .Where(property => properties.Any(x => x.Name == property?.Split('.')[0]) &&
                                        (activeProperties.Contains(property) ||
                                         activeProperties.Any(ap => property != null && property.StartsWith(ap + "."))))
                     .Where(property => !includeProperties.Contains(property)))
            includeProperties.Add(property);

        return includeProperties;
    }

    private static HashSet<string?> GetActiveProperties<TEntity>(Expression<Func<TEntity, bool>> expression)
    {
        var activeProperties = new HashSet<string?>();
        var binaryExpression = expression.Body as BinaryExpression;

        switch (binaryExpression)
        {
            case { NodeType: ExpressionType.AndAlso }:
            {
                var left = GetActiveProperties(
                    Expression.Lambda<Func<TEntity, bool>>(binaryExpression.Left, expression.Parameters));
                var right = GetActiveProperties(
                    Expression.Lambda<Func<TEntity, bool>>(binaryExpression.Right, expression.Parameters));

                foreach (var prop in left) activeProperties.Add(prop);
                foreach (var prop in right) activeProperties.Add(prop);
                break;
            }
            case { NodeType: ExpressionType.OrElse }:
            {
                if (!IsNullCheck(binaryExpression.Left, out var propertyName)) return activeProperties;

                var rightProperties = GetPropertyAccessPaths(binaryExpression.Right);
                if (rightProperties.Contains(propertyName)) activeProperties.Add(propertyName);

                break;
            }
            default:
            {
                var properties = GetPropertyAccessPaths(expression.Body);
                foreach (var prop in properties) activeProperties.Add(prop);

                break;
            }
        }

        return activeProperties;
    }

    private static bool IsNullCheck(Expression expression, out string? propertyName)
    {
        propertyName = null;

        if (expression is not BinaryExpression binary ||
            (binary.NodeType != ExpressionType.Equal && binary.NodeType != ExpressionType.NotEqual))
            return false;

        if (binary.Left is MemberExpression { Expression: ParameterExpression } leftMember &&
            IsNullConstant(binary.Right))
        {
            propertyName = leftMember.Member.Name;
            return true;
        }

        if (binary.Right is not MemberExpression { Expression: ParameterExpression } rightMember ||
            !IsNullConstant(binary.Left))
            return false;

        propertyName = rightMember.Member.Name;
        return true;
    }

    private static bool IsNullConstant(Expression expression)
    {
        return expression is ConstantExpression { Value: null };
    }

    private static List<string?> GetPropertyAccessPaths(Expression expression)
    {
        var paths = new List<string?>();
        var visitor = new PropertyAccessVisitor();
        visitor.Visit(expression);

        foreach (var access in visitor.AccessPaths.Where(access => !paths.Contains(access))) paths.Add(access);

        return paths;
    }

    private static IEnumerable<PropertyInfo> GetAllowedProperties(Type type)
    {
        return type.GetProperties().Where(prop => (prop.PropertyType.IsClass && prop.PropertyType != typeof(string)) ||
                                                  (prop.PropertyType.IsGenericType &&
                                                   (prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>) ||
                                                    prop.PropertyType.GetGenericTypeDefinition() ==
                                                    typeof(IReadOnlyCollection<>) ||
                                                    prop.PropertyType.GetGenericTypeDefinition() ==
                                                    typeof(IEnumerable<>))));
    }

    private class PropertyAccessVisitor : ExpressionVisitor
    {
        public List<string?> AccessPaths { get; } = [];

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ParameterExpression)
            {
                AccessPaths.Add(node.Member.Name);
            }
            else if (node.Expression != null)
            {
                var innerVisitor = new PropertyAccessVisitor();
                innerVisitor.Visit(node.Expression);

                foreach (var path in innerVisitor.AccessPaths) AccessPaths.Add($"{path}.{node.Member.Name}");
            }

            return base.VisitMember(node);
        }
    }

    private class IncludeVisitor : ExpressionVisitor
    {
        public List<string?> IncludeProperties { get; } = [];

        protected override Expression VisitMember(MemberExpression node)
        {
            switch (node.Expression)
            {
                case ParameterExpression:
                    IncludeProperties.Add(node.Member.Name);
                    break;
                case MemberExpression { Expression: ParameterExpression } memberExpression:
                    IncludeProperties.Add($"{memberExpression.Member.Name}.{node.Member.Name}");
                    break;
            }

            return base.VisitMember(node);
        }
    }
}