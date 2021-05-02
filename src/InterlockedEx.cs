#region

using System.Threading;

#endregion

namespace collections.native.src
{
    public static class InterlockedEx
    {
        public static int OptimisticDivide(ref int location1, int value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue / value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static int OptimisticMultiply(ref int location1, int value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue * value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static int OptimisticIncrement(ref int location1)
        {
            return OptimisticAdd(ref location1, 1);
        }

        public static int OptimisticDecrement(ref int location1)
        {
            return OptimisticAdd(ref location1, -1);
        }

        public static int OptimisticAdd(ref int location1, int value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue + value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static int OptimisticSubtract(ref int location1, int value)
        {
            return OptimisticAdd(ref location1, -value);
        }

        public static int OptimisticMax(ref int location1, int value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = math.max(currentValue, value);
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static int OptimisticMin(ref int location1, int value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = math.min(currentValue, value);
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static double Divide(ref double location1, double value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue / value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static double Multiply(ref double location1, double value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue * value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static double Increment(ref double location1)
        {
            return Add(ref location1, 1.0f);
        }

        public static double Decrement(ref double location1)
        {
            return Add(ref location1, -1.0f);
        }

        public static double Add(ref double location1, double value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue + value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static double Subtract(ref double location1, double value)
        {
            return Add(ref location1, -value);
        }

        public static double Max(ref double location1, double value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = math.max(currentValue, value);
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static double Min(ref double location1, double value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = math.min(currentValue, value);
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static float Divide(ref float location1, float value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue / value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static float Multiply(ref float location1, float value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue * value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static float Increment(ref float location1)
        {
            return Add(ref location1, 1.0f);
        }

        public static float Decrement(ref float location1)
        {
            return Add(ref location1, -1.0f);
        }

        public static float Add(ref float location1, float value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = currentValue + value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static float Subtract(ref float location1, float value)
        {
            return Add(ref location1, -value);
        }

        public static float Max(ref float location1, float value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = math.max(currentValue, value);
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static float Min(ref float location1, float value)
        {
            var newCurrentValue = location1; // non-volatile read, so may be stale
            while (true)
            {
                var currentValue = newCurrentValue;
                var newValue = math.min(currentValue, value);
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                {
                    return newValue;
                }
            }
        }

        public static float2 Divide(ref float2 location1, float value)
        {
            return Divide(ref location1, new float2(value, value));
        }

        public static float2 Divide(ref float2 location1, float2 value)
        {
            

            for (var i = 0; i < 2; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue / stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float2 Multiply(ref float2 location1, float value)
        {
            return Multiply(ref location1, new float2(value, value));
        }

        public static float2 Multiply(ref float2 location1, float2 value)
        {
            for (var i = 0; i < 2; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue * stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float2 Add(ref float2 location1, float value)
        {
            return Add(ref location1, new float2(value, value));
        }

        public static float2 Add(ref float2 location1, float2 value)
        {
            

            for (var i = 0; i < 2; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue + stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float2 Subtract(ref float2 location1, float value)
        {
            return Subtract(ref location1, new float2(value, value));
        }

        public static float2 Subtract(ref float2 location1, float2 value)
        {
            return Add(ref location1, -value);
        }

        public static float3 Divide(ref float3 location1, float value)
        {
            return Divide(ref location1, new float3(value, value, value));
        }

        public static float3 Divide(ref float3 location1, float3 value)
        {
            

            for (var i = 0; i < 3; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue / stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float3 Multiply(ref float3 location1, float value)
        {
            return Multiply(ref location1, new float3(value, value, value));
        }

        public static float3 Multiply(ref float3 location1, float3 value)
        {
            

            for (var i = 0; i < 3; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue * stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float3 Add(ref float3 location1, float value)
        {
            return Add(ref location1, new float3(value, value, value));
        }

        public static float3 Add(ref float3 location1, float3 value)
        {
            

            for (var i = 0; i < 3; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue + stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float3 Subtract(ref float3 location1, float value)
        {
            return Subtract(ref location1, new float3(value, value, value));
        }

        public static float3 Subtract(ref float3 location1, float3 value)
        {
            return Add(ref location1, -value);
        }

        public static float4 Divide(ref float4 location1, float value)
        {
            return Divide(ref location1, new float4(value, value, value, value));
        }

        public static float4 Divide(ref float4 location1, float4 value)
        {
            

            for (var i = 0; i < 4; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue / stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float4 Multiply(ref float4 location1, float value)
        {
            return Multiply(ref location1, new float4(value, value, value, value));
        }

        public static float4 Multiply(ref float4 location1, float4 value)
        {
            

            for (var i = 0; i < 4; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue * stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float4 Add(ref float4 location1, float value)
        {
            return Add(ref location1, new float4(value, value, value, value));
        }

        public static float4 Add(ref float4 location1, float4 value)
        {
            

            for (var i = 0; i < 4; i++)
            {
                var newCurrentValue = location1[i];
                var stepVal = value[i];

                while (true)
                {
                    var currentValue = newCurrentValue;
                    var newValue = currentValue + stepVal;

                    newCurrentValue = Interlocked.CompareExchange(ref newCurrentValue, newValue, currentValue);

                    if (newCurrentValue == currentValue)
                    {
                        location1[i] = newValue;
                        break;
                    }
                }
            }

            return location1;
        }

        public static float4 Subtract(ref float4 location1, float value)
        {
            return Subtract(ref location1, new float4(value, value, value, value));
        }

        public static float4 Subtract(ref float4 location1, float4 value)
        {
            return Add(ref location1, -value);
        }
    }
}
