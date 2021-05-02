#region

using System;
using collections.src;
using Unity.Collections;
using Unity.Jobs;
using Unity.Profiling;

#endregion

namespace collections.native.src
{
    public static class SafeNative
    {
        private const string _PRF_PFX = nameof(SafeNative) + ".";

#region Safe Dispose

        private static readonly ProfilerMarker _PRF_IsSafe = new ProfilerMarker(_PRF_PFX + nameof(IsSafe));
        
        public static bool IsSafe<T>(this NativeList<T> native)
            where T : struct
        {
            using (_PRF_IsSafe.Auto())
            {
                if (!native.IsCreated)
                {
                    return false;
                }

                try
                {
                    if (native.Length > 0)
                    {
                        var n = native[0];
                    }
                }
                catch (NullReferenceException)
                {
                    return false;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }

                return true;
            }
        }

        public static bool IsSafe<T>(this NativeArray<T> native)
            where T : struct
        {
            using (_PRF_IsSafe.Auto())
            {
                if (!native.IsCreated)
                {
                    return false;
                }

                try
                {
                    if (native.Length > 0)
                    {
                        var n = native[0];
                    }
                }
                catch (NullReferenceException)
                {
                    return false;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }

                return true;
            }
        }

        private static readonly ProfilerMarker _PRF_SafeDispose = new ProfilerMarker(_PRF_PFX + nameof(SafeDisposeAll));
        
        public static void SafeDispose<T>(this NativeArray<T> native, JobHandle handle)
            where T : struct
        {

            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    var l = native.Length;

                    if (l > 0)
                    {
                        var x = native[0];
                    }

                    native.Dispose(handle);
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose<T>(this AppaList<NativeList<T>> native)
            where T : struct
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    if (native == null)
                    {
                        return;
                    }

                    var l = native.Count;

                    if (l > 0)
                    {
                        var x = native[0];
                    }

                    for (var i = 0; i < native.Count; i++)
                    {
                        native[i].SafeDispose();
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose<T>(this AppaList<NativeArray<T>> native)
            where T : struct
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    if (native == null)
                    {
                        return;
                    }

                    var l = native.Count;

                    if (l > 0)
                    {
                        var x = native[0];
                    }

                    for (var i = 0; i < native.Count; i++)
                    {
                        native[i].SafeDispose();
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose<T>(this NativeList<T>[] native)
            where T : struct
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    if (native == null)
                    {
                        return;
                    }

                    var l = native.Length;

                    if (l > 0)
                    {
                        var x = native[0];
                    }

                    for (var i = 0; i < native.Length; i++)
                    {
                        native[i].SafeDispose();
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose<T>(this NativeList<T>[] native, JobHandle handle)
            where T : struct
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    var l = native.Length;

                    if (l > 0)
                    {
                        var x = native[0];
                    }

                    for (var i = 0; i < native.Length; i++)
                    {
                        native[i].Dispose(handle);
                    }
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose<T>(this NativeList<T> native, JobHandle handle)
            where T : struct
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    var l = native.Length;

                    if (l > 0)
                    {
                        var x = native[0];
                    }

                    native.Dispose(handle);
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        private static readonly ProfilerMarker _PRF_SafeDisposeAll = new ProfilerMarker(_PRF_PFX + nameof(SafeDisposeAll));
        public static void SafeDisposeAll(this NativeList<JobHandle> native)
        {
            using (_PRF_SafeDisposeAll.Auto())
            {
                try
                {
                    JobHandle.CompleteAll(native);
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }

                try
                {
                    native.Dispose();
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose(this IDisposable native)
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    native.Dispose();
                }
                catch (NullReferenceException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        private static readonly ProfilerMarker _PRF_IsDisposed = new ProfilerMarker(_PRF_PFX + nameof(IsDisposed));
        public static bool IsDisposed<T>(this INativeList<T> native)
            where T : struct
        {
            using (_PRF_IsDisposed.Auto())
            {
                try
                {
                    var x = native.Capacity;

                    return false;
                }

                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }

        public static void SafeDispose<T>(ref T disposable)
            where T : IDisposable
        {
            using (_PRF_SafeDispose.Auto())
            {
                try
                {
                    disposable?.Dispose();
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static void SafeDispose<T0, T1>(ref T0 d0, ref T1 d1)
            where T0 : IDisposable
            where T1 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
        }

        public static void SafeDispose<T0, T1, T2>(ref T0 d0, ref T1 d1, ref T2 d2)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
        }

        public static void SafeDispose<T0, T1, T2, T3>(ref T0 d0, ref T1 d1, ref T2 d2, ref T3 d3)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4>(ref T0 d0, ref T1 d1, ref T2 d2, ref T3 d3, ref T4 d4)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8,
            ref T9 d9)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
            where T9 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
            SafeDispose(ref d9);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8,
            ref T9 d9,
            ref T10 d10)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
            where T9 : IDisposable
            where T10 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
            SafeDispose(ref d9);
            SafeDispose(ref d10);
        }

        public static void SafeDispose<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            ref T0 d0,
            ref T1 d1,
            ref T2 d2,
            ref T3 d3,
            ref T4 d4,
            ref T5 d5,
            ref T6 d6,
            ref T7 d7,
            ref T8 d8,
            ref T9 d9,
            ref T10 d10,
            ref T11 d11)
            where T0 : IDisposable
            where T1 : IDisposable
            where T2 : IDisposable
            where T3 : IDisposable
            where T4 : IDisposable
            where T5 : IDisposable
            where T6 : IDisposable
            where T7 : IDisposable
            where T8 : IDisposable
            where T9 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
        {
            SafeDispose(ref d0);
            SafeDispose(ref d1);
            SafeDispose(ref d2);
            SafeDispose(ref d3);
            SafeDispose(ref d4);
            SafeDispose(ref d5);
            SafeDispose(ref d6);
            SafeDispose(ref d7);
            SafeDispose(ref d8);
            SafeDispose(ref d9);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
        }

        public static void SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
        }

        public static void
            SafeDispose<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(
                ref T00 d00,
                ref T01 d01,
                ref T02 d02,
                ref T03 d03,
                ref T04 d04,
                ref T05 d05,
                ref T06 d06,
                ref T07 d07,
                ref T08 d08,
                ref T09 d09,
                ref T10 d10,
                ref T11 d11,
                ref T12 d12,
                ref T13 d13,
                ref T14 d14,
                ref T15 d15,
                ref T16 d16,
                ref T17 d17,
                ref T18 d18,
                ref T19 d19,
                ref T20 d20,
                ref T21 d21,
                ref T22 d22)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23,
            ref T24 d24)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23,
            ref T24 d24,
            ref T25 d25)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23,
            ref T24 d24,
            ref T25 d25,
            ref T26 d26)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26,
            T27>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23,
            ref T24 d24,
            ref T25 d25,
            ref T26 d26,
            ref T27 d27)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
            where T27 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
            SafeDispose(ref d27);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26,
            T27, T28>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23,
            ref T24 d24,
            ref T25 d25,
            ref T26 d26,
            ref T27 d27,
            ref T28 d28)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
            where T27 : IDisposable
            where T28 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
            SafeDispose(ref d27);
            SafeDispose(ref d28);
        }

        public static void SafeDispose<
            T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26,
            T27, T28, T29>(
            ref T00 d00,
            ref T01 d01,
            ref T02 d02,
            ref T03 d03,
            ref T04 d04,
            ref T05 d05,
            ref T06 d06,
            ref T07 d07,
            ref T08 d08,
            ref T09 d09,
            ref T10 d10,
            ref T11 d11,
            ref T12 d12,
            ref T13 d13,
            ref T14 d14,
            ref T15 d15,
            ref T16 d16,
            ref T17 d17,
            ref T18 d18,
            ref T19 d19,
            ref T20 d20,
            ref T21 d21,
            ref T22 d22,
            ref T23 d23,
            ref T24 d24,
            ref T25 d25,
            ref T26 d26,
            ref T27 d27,
            ref T28 d28,
            ref T29 d29)
            where T00 : IDisposable
            where T01 : IDisposable
            where T02 : IDisposable
            where T03 : IDisposable
            where T04 : IDisposable
            where T05 : IDisposable
            where T06 : IDisposable
            where T07 : IDisposable
            where T08 : IDisposable
            where T09 : IDisposable
            where T10 : IDisposable
            where T11 : IDisposable
            where T12 : IDisposable
            where T13 : IDisposable
            where T14 : IDisposable
            where T15 : IDisposable
            where T16 : IDisposable
            where T17 : IDisposable
            where T18 : IDisposable
            where T19 : IDisposable
            where T20 : IDisposable
            where T21 : IDisposable
            where T22 : IDisposable
            where T23 : IDisposable
            where T24 : IDisposable
            where T25 : IDisposable
            where T26 : IDisposable
            where T27 : IDisposable
            where T28 : IDisposable
            where T29 : IDisposable
        {
            SafeDispose(ref d00);
            SafeDispose(ref d01);
            SafeDispose(ref d02);
            SafeDispose(ref d03);
            SafeDispose(ref d04);
            SafeDispose(ref d05);
            SafeDispose(ref d06);
            SafeDispose(ref d07);
            SafeDispose(ref d08);
            SafeDispose(ref d09);
            SafeDispose(ref d10);
            SafeDispose(ref d11);
            SafeDispose(ref d12);
            SafeDispose(ref d13);
            SafeDispose(ref d14);
            SafeDispose(ref d15);
            SafeDispose(ref d16);
            SafeDispose(ref d17);
            SafeDispose(ref d18);
            SafeDispose(ref d19);
            SafeDispose(ref d20);
            SafeDispose(ref d21);
            SafeDispose(ref d22);
            SafeDispose(ref d23);
            SafeDispose(ref d24);
            SafeDispose(ref d25);
            SafeDispose(ref d26);
            SafeDispose(ref d27);
            SafeDispose(ref d28);
            SafeDispose(ref d29);
        }

#endregion

#region Safe Check

        private static readonly ProfilerMarker _PRF_ShouldAllocate = new ProfilerMarker(_PRF_PFX + nameof(ShouldAllocate));
        public static bool ShouldAllocate<T>(this NativeList<T> native)
            where T : struct
        {
            using (_PRF_ShouldAllocate.Auto())
            {
                try
                {
                    if (!native.IsCreated)
                    {
                        return true;
                    }

                    var x = native.Length;
                    return false;
                }
                catch (NullReferenceException)
                {
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }

        public static bool ShouldAllocate<T>(this NativeArray<T> native)
            where T : struct
        {
            using (_PRF_ShouldAllocate.Auto())
            {
                try
                {
                    if (!native.IsCreated)
                    {
                        return true;
                    }

                    if (native.Length > 0)
                    {
                        var item = native[0];

                        return false;
                    }

                    return false;
                }
                catch (NullReferenceException)
                {
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }

        public static bool ShouldAllocate<T>(this NativeArray2D<T> native)
            where T : struct
        {
            using (_PRF_ShouldAllocate.Auto())
            {
                try
                {
                    if (!native.IsCreated)
                    {
                        return true;
                    }

                    if (native.TotalLength > 0)
                    {
                        var item = native[0, 0];

                        return false;
                    }

                    return false;
                }
                catch (NullReferenceException)
                {
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }

        public static bool ShouldAllocate<TK, TV>(this NativeHashMap<TK, TV> native)
            where TK : struct, IEquatable<TK>
            where TV : struct
        {
            using (_PRF_ShouldAllocate.Auto())
            {
                try
                {
                    if (!native.IsCreated)
                    {
                        return true;
                    }

                    var x = native.Capacity;
                    return false;
                }
                catch (NullReferenceException)
                {
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            }
        }

#endregion

#region Safe Clear

        private static readonly ProfilerMarker _PRF_SafeClear = new ProfilerMarker(_PRF_PFX + nameof(SafeClear));
        public static void SafeClear<T>(ref NativeList<T> list)
            where T : struct
        {
            using (_PRF_SafeClear.Auto())
            {
                try
                {
                    list.Clear();
                }
                catch (InvalidOperationException)
                {
                    list = new NativeList<T>(Allocator.Persistent);
                }
            }
        }

        public static void SafeClear<T0, T1>(ref NativeList<T0> d0, ref NativeList<T1> d1)
            where T0 : struct
            where T1 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
        }

        public static void SafeClear<T0, T1, T2>(ref NativeList<T0> d0, ref NativeList<T1> d1, ref NativeList<T2> d2)
            where T0 : struct
            where T1 : struct
            where T2 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
        }

        public static void SafeClear<T0, T1, T2, T3>(ref NativeList<T0> d0, ref NativeList<T1> d1, ref NativeList<T2> d2, ref NativeList<T3> d3)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
        }

        public static void SafeClear<T0, T1, T2, T3, T4>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
        }

        public static void SafeClear<T0, T1, T2, T3, T4, T5>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4,
            ref NativeList<T5> d5)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
            SafeClear(ref d5);
        }

        public static void SafeClear<T0, T1, T2, T3, T4, T5, T6>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4,
            ref NativeList<T5> d5,
            ref NativeList<T6> d6)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
            SafeClear(ref d5);
            SafeClear(ref d6);
        }

        public static void SafeClear<T0, T1, T2, T3, T4, T5, T6, T7>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4,
            ref NativeList<T5> d5,
            ref NativeList<T6> d6,
            ref NativeList<T7> d7)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
            SafeClear(ref d5);
            SafeClear(ref d6);
            SafeClear(ref d7);
        }

        public static void SafeClear<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4,
            ref NativeList<T5> d5,
            ref NativeList<T6> d6,
            ref NativeList<T7> d7,
            ref NativeList<T8> d8)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct
            where T8 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
            SafeClear(ref d5);
            SafeClear(ref d6);
            SafeClear(ref d7);
            SafeClear(ref d8);
        }

        public static void SafeClear<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4,
            ref NativeList<T5> d5,
            ref NativeList<T6> d6,
            ref NativeList<T7> d7,
            ref NativeList<T8> d8,
            ref NativeList<T9> d9)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct
            where T8 : struct
            where T9 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
            SafeClear(ref d5);
            SafeClear(ref d6);
            SafeClear(ref d7);
            SafeClear(ref d8);
            SafeClear(ref d9);
        }

        public static void SafeClear<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4,
            ref NativeList<T5> d5,
            ref NativeList<T6> d6,
            ref NativeList<T7> d7,
            ref NativeList<T8> d8,
            ref NativeList<T9> d9,
            ref NativeList<T10> d10)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct
            where T8 : struct
            where T9 : struct
            where T10 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
            SafeClear(ref d5);
            SafeClear(ref d6);
            SafeClear(ref d7);
            SafeClear(ref d8);
            SafeClear(ref d9);
            SafeClear(ref d10);
        }

        public static void SafeClear<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            ref NativeList<T0> d0,
            ref NativeList<T1> d1,
            ref NativeList<T2> d2,
            ref NativeList<T3> d3,
            ref NativeList<T4> d4,
            ref NativeList<T5> d5,
            ref NativeList<T6> d6,
            ref NativeList<T7> d7,
            ref NativeList<T8> d8,
            ref NativeList<T9> d9,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11)
            where T0 : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct
            where T8 : struct
            where T9 : struct
            where T10 : struct
            where T11 : struct
        {
            SafeClear(ref d0);
            SafeClear(ref d1);
            SafeClear(ref d2);
            SafeClear(ref d3);
            SafeClear(ref d4);
            SafeClear(ref d5);
            SafeClear(ref d6);
            SafeClear(ref d7);
            SafeClear(ref d8);
            SafeClear(ref d9);
            SafeClear(ref d10);
            SafeClear(ref d11);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14,
            ref NativeList<T15> d15)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14,
            ref NativeList<T15> d15,
            ref NativeList<T16> d16)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14,
            ref NativeList<T15> d15,
            ref NativeList<T16> d16,
            ref NativeList<T17> d17)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14,
            ref NativeList<T15> d15,
            ref NativeList<T16> d16,
            ref NativeList<T17> d17,
            ref NativeList<T18> d18)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14,
            ref NativeList<T15> d15,
            ref NativeList<T16> d16,
            ref NativeList<T17> d17,
            ref NativeList<T18> d18,
            ref NativeList<T19> d19)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14,
            ref NativeList<T15> d15,
            ref NativeList<T16> d16,
            ref NativeList<T17> d17,
            ref NativeList<T18> d18,
            ref NativeList<T19> d19,
            ref NativeList<T20> d20)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
        }

        public static void SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(
            ref NativeList<T00> d00,
            ref NativeList<T01> d01,
            ref NativeList<T02> d02,
            ref NativeList<T03> d03,
            ref NativeList<T04> d04,
            ref NativeList<T05> d05,
            ref NativeList<T06> d06,
            ref NativeList<T07> d07,
            ref NativeList<T08> d08,
            ref NativeList<T09> d09,
            ref NativeList<T10> d10,
            ref NativeList<T11> d11,
            ref NativeList<T12> d12,
            ref NativeList<T13> d13,
            ref NativeList<T14> d14,
            ref NativeList<T15> d15,
            ref NativeList<T16> d16,
            ref NativeList<T17> d17,
            ref NativeList<T18> d18,
            ref NativeList<T19> d19,
            ref NativeList<T20> d20,
            ref NativeList<T21> d21)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22,
                ref NativeList<T23> d23)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
            where T23 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
            SafeClear(ref d23);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22,
                ref NativeList<T23> d23,
                ref NativeList<T24> d24)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
            where T23 : struct
            where T24 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
            SafeClear(ref d23);
            SafeClear(ref d24);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24,
                      T25>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22,
                ref NativeList<T23> d23,
                ref NativeList<T24> d24,
                ref NativeList<T25> d25)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
            where T23 : struct
            where T24 : struct
            where T25 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
            SafeClear(ref d23);
            SafeClear(ref d24);
            SafeClear(ref d25);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24,
                      T25, T26>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22,
                ref NativeList<T23> d23,
                ref NativeList<T24> d24,
                ref NativeList<T25> d25,
                ref NativeList<T26> d26)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
            where T23 : struct
            where T24 : struct
            where T25 : struct
            where T26 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
            SafeClear(ref d23);
            SafeClear(ref d24);
            SafeClear(ref d25);
            SafeClear(ref d26);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24,
                      T25, T26, T27>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22,
                ref NativeList<T23> d23,
                ref NativeList<T24> d24,
                ref NativeList<T25> d25,
                ref NativeList<T26> d26,
                ref NativeList<T27> d27)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
            where T23 : struct
            where T24 : struct
            where T25 : struct
            where T26 : struct
            where T27 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
            SafeClear(ref d23);
            SafeClear(ref d24);
            SafeClear(ref d25);
            SafeClear(ref d26);
            SafeClear(ref d27);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24,
                      T25, T26, T27, T28>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22,
                ref NativeList<T23> d23,
                ref NativeList<T24> d24,
                ref NativeList<T25> d25,
                ref NativeList<T26> d26,
                ref NativeList<T27> d27,
                ref NativeList<T28> d28)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
            where T23 : struct
            where T24 : struct
            where T25 : struct
            where T26 : struct
            where T27 : struct
            where T28 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
            SafeClear(ref d23);
            SafeClear(ref d24);
            SafeClear(ref d25);
            SafeClear(ref d26);
            SafeClear(ref d27);
            SafeClear(ref d28);
        }

        public static void
            SafeClear<T00, T01, T02, T03, T04, T05, T06, T07, T08, T09, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24,
                      T25, T26, T27, T28, T29>(
                ref NativeList<T00> d00,
                ref NativeList<T01> d01,
                ref NativeList<T02> d02,
                ref NativeList<T03> d03,
                ref NativeList<T04> d04,
                ref NativeList<T05> d05,
                ref NativeList<T06> d06,
                ref NativeList<T07> d07,
                ref NativeList<T08> d08,
                ref NativeList<T09> d09,
                ref NativeList<T10> d10,
                ref NativeList<T11> d11,
                ref NativeList<T12> d12,
                ref NativeList<T13> d13,
                ref NativeList<T14> d14,
                ref NativeList<T15> d15,
                ref NativeList<T16> d16,
                ref NativeList<T17> d17,
                ref NativeList<T18> d18,
                ref NativeList<T19> d19,
                ref NativeList<T20> d20,
                ref NativeList<T21> d21,
                ref NativeList<T22> d22,
                ref NativeList<T23> d23,
                ref NativeList<T24> d24,
                ref NativeList<T25> d25,
                ref NativeList<T26> d26,
                ref NativeList<T27> d27,
                ref NativeList<T28> d28,
                ref NativeList<T29> d29)
            where T00 : struct
            where T01 : struct
            where T02 : struct
            where T03 : struct
            where T04 : struct
            where T05 : struct
            where T06 : struct
            where T07 : struct
            where T08 : struct
            where T09 : struct
            where T10 : struct
            where T11 : struct
            where T12 : struct
            where T13 : struct
            where T14 : struct
            where T15 : struct
            where T16 : struct
            where T17 : struct
            where T18 : struct
            where T19 : struct
            where T20 : struct
            where T21 : struct
            where T22 : struct
            where T23 : struct
            where T24 : struct
            where T25 : struct
            where T26 : struct
            where T27 : struct
            where T28 : struct
            where T29 : struct
        {
            SafeClear(ref d00);
            SafeClear(ref d01);
            SafeClear(ref d02);
            SafeClear(ref d03);
            SafeClear(ref d04);
            SafeClear(ref d05);
            SafeClear(ref d06);
            SafeClear(ref d07);
            SafeClear(ref d08);
            SafeClear(ref d09);
            SafeClear(ref d10);
            SafeClear(ref d11);
            SafeClear(ref d12);
            SafeClear(ref d13);
            SafeClear(ref d14);
            SafeClear(ref d15);
            SafeClear(ref d16);
            SafeClear(ref d17);
            SafeClear(ref d18);
            SafeClear(ref d19);
            SafeClear(ref d20);
            SafeClear(ref d21);
            SafeClear(ref d22);
            SafeClear(ref d23);
            SafeClear(ref d24);
            SafeClear(ref d25);
            SafeClear(ref d26);
            SafeClear(ref d27);
            SafeClear(ref d28);
            SafeClear(ref d29);
        }

#endregion
    }
}
