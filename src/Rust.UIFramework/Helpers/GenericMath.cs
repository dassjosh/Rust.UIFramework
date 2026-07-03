using System;
using System.Linq.Expressions;

namespace Oxide.Ext.UiFramework.Helpers;

/// <summary>
/// Provides generic mathematical operations for numeric types with minimal boxing.
/// </summary>
public static class GenericMath
{
    /// <summary>
    /// Adds two numeric values.
    /// </summary>
    /// <typeparam name="T">The numeric type of the values.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>The sum of the two values.</returns>
    public static T Add<T>(T a, T b) where T : struct => MathOperations<T>.Add(a, b);

    /// <summary>
    /// Subtracts one numeric value from another.
    /// </summary>
    /// <typeparam name="T">The numeric type of the values.</typeparam>
    /// <param name="a">The value to subtract from.</param>
    /// <param name="b">The value to subtract.</param>
    /// <returns>The result of the subtraction.</returns>
    public static T Subtract<T>(T a, T b) where T : struct => MathOperations<T>.Subtract(a, b);

    /// <summary>
    /// Multiplies two numeric values.
    /// </summary>
    /// <typeparam name="T">The numeric type of the values.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>The product of the two values.</returns>
    public static T Multiply<T>(T a, T b) where T : struct => MathOperations<T>.Multiply(a, b);

    /// <summary>
    /// Divides one numeric value by another.
    /// </summary>
    /// <typeparam name="T">The numeric type of the values.</typeparam>
    /// <param name="a">The dividend.</param>
    /// <param name="b">The divisor.</param>
    /// <returns>The result of the division.</returns>
    /// <exception cref="DivideByZeroException">Thrown when divisor is zero.</exception>
    public static T Divide<T>(T a, T b) where T : struct
    {
        // Check for division by zero
        if (MathOperations<T>.IsZero(b))
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
                
        return MathOperations<T>.Divide(a, b);
    }
    
    /// <summary>
    /// Checks if a value is zero.
    /// </summary>
    /// <typeparam name="T">The numeric type of the value.</typeparam>
    /// <param name="a">The value to check.</param>
    /// <returns>True if the value is zero, false otherwise.</returns>
    public static bool IsZero<T>(T a) where T : struct => MathOperations<T>.IsZero(a);

    /// <summary>
    /// Checks if a value has a mask.
    /// </summary>
    /// <typeparam name="T">The numeric type of the value.</typeparam>
    /// <param name="a">The value to check.</param>
    /// <param name="b">The mask to check for.</param>
    /// <returns>True if the value has a mask, false otherwise.</returns>
    public static bool HasMask<T>(T a, T b) where T : struct => MathOperations<T>.HasMask(a, b);
    
    /// <summary>
    /// Performs a bitwise AND operation on two values.
    /// </summary>
    /// <typeparam name="T">The numeric type of the values.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>The result of the bitwise AND operation.</returns>
    public static T And<T>(T a, T b) where T : struct => MathOperations<T>.And(a, b);
    
    /// <summary>
    /// Performs a bitwise OR operation on two values.
    /// </summary>
    /// <typeparam name="T">The numeric type of the values.</typeparam>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>The result of the bitwise OR operation.</returns>
    public static T Or<T>(T a, T b) where T : struct => MathOperations<T>.Or(a, b);
    
    /// <summary>
    /// Lerps between two generic numeric values
    /// </summary>
    /// <param name="start">Start value</param>
    /// <param name="end">End value</param>
    /// <param name="t">Duration</param>
    /// <typeparam name="T">Generic numeric type</typeparam>
    /// <returns>Lerped value</returns>
    public static T Lerp<T>(T start, T end, float t) where T : struct => MathOperations<T>.Lerp(start, end, t);

    /// <summary>
    /// Helper class that caches the compiled expression trees for each operation.
    /// </summary>
    private static class MathOperations<T> where T : struct
    {
        // Cached delegates for operations on type T
        private static readonly Func<T, T, T> _add;
        private static readonly Func<T, T, T> _subtract;
        private static readonly Func<T, T, T> _multiply;
        private static readonly Func<T, T, T> _divide;
        private static readonly Func<T, T, T> _and;
        private static readonly Func<T, T, T> _or;
        private static readonly Func<T, T, bool> _hasMask;
        private static readonly Func<T, bool> _isZero;
        private static readonly Func<T, T, float, T> _lerp;

        static MathOperations()
        {
            Type type = typeof(T);

            if (!IsNumericType(type))
            {
                throw new NotSupportedException($"Type {type.Name} is not a supported numeric type.");
            }

            // Create parameter expressions
            ParameterExpression paramA = Expression.Parameter(type, "a");
            ParameterExpression paramB = Expression.Parameter(type, "b");
            ParameterExpression paramProgress = Expression.Parameter(typeof(float), "t");
                
            // Create operation expressions
            _add = CompileOperation(Expression.Add(paramA, paramB));
            _subtract = CompileOperation(Expression.Subtract(paramA, paramB));
            _multiply = CompileOperation(Expression.Multiply(paramA, paramB));
            _divide = CompileOperation(Expression.Divide(paramA, paramB));
            if (IsBitwiseType(type))
            {
                _and = CompileOperation(Expression.And(paramA, paramB));
                _or = CompileOperation(Expression.Or(paramA, paramB));
                _hasMask = Expression.Lambda<Func<T, T, bool>>(Expression.Equal(Expression.And(paramA, paramB), paramB), paramA, paramB).Compile();
            }

            _lerp = CreateLerp(type, paramA, paramB, paramProgress);
                
            // Create zero comparison
            ConstantExpression zero = Expression.Constant(GetZeroValue(type), type);
            _isZero = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(paramA, zero), paramA).Compile();
        }

        /// <summary>
        /// Compiles a binary operation into a strongly typed delegate.
        /// </summary>
        private static Func<T, T, T> CompileOperation(BinaryExpression operation)
        {
            ParameterExpression paramA = operation.Left as ParameterExpression;
            ParameterExpression paramB = operation.Right as ParameterExpression;
                
            return Expression.Lambda<Func<T, T, T>>(
                operation, paramA, paramB).Compile();
        }

        private static Func<T, T, float, T> CreateLerp(Type type, ParameterExpression paramA, ParameterExpression paramB, ParameterExpression paramProgress)
        {
            UnaryExpression startAsFloat = Expression.Convert(paramA, typeof(float));
            UnaryExpression endAsFloat = Expression.Convert(paramB, typeof(float));

            BinaryExpression lerpAsFloat = Expression.Add(startAsFloat, Expression.Multiply(Expression.Subtract(endAsFloat, startAsFloat), paramProgress));

            return Expression.Lambda<Func<T, T, float, T>>(
                    Expression.Convert(lerpAsFloat, type),
                    paramA,
                    paramB,
                    paramProgress)
                .Compile();
        }

        /// <summary>
        /// Gets a zero value for the specified type.
        /// </summary>
        private static object GetZeroValue(Type type)
        {
            return Convert.ChangeType(0, type);
        }

        /// <summary>
        /// Determines if a type is a supported numeric type.
        /// </summary>
        private static bool IsNumericType(Type type)
        {
            return type == typeof(int) ||
                   type == typeof(double) ||
                   type == typeof(float) ||
                   type == typeof(decimal) ||
                   type == typeof(long) ||
                   type == typeof(ulong) ||
                   type == typeof(uint) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type.IsEnum;
        }

        private static bool IsBitwiseType(Type type)
        {
            return type == typeof(int) ||
                   type == typeof(long) ||
                   type == typeof(uint) ||
                   type == typeof(ulong) ||
                   type.IsEnum;
        }

        // Public methods that use the cached delegates
        public static T Add(T a, T b) => _add(a, b);
        public static T Subtract(T a, T b) => _subtract(a, b);
        public static T Multiply(T a, T b) => _multiply(a, b);
        public static T Divide(T a, T b) => _divide(a, b);
        public static bool IsZero(T value) => _isZero(value);
        public static T And(T a, T b) => _and?.Invoke(a, b) ?? throw new NotSupportedException($"Type {typeof(T).Name} is not a supported bitwise type.");
        public static T Or(T a, T b) => _or?.Invoke(a, b) ?? throw new NotSupportedException($"Type {typeof(T).Name} is not a supported bitwise type.");
        public static bool HasMask(T a, T b) => _hasMask?.Invoke(a, b) ?? throw new NotSupportedException($"Type {typeof(T).Name} is not a supported bitwise type.");
        public static T Lerp(T a, T b, float t) => _lerp(a, b, t);
    }
}