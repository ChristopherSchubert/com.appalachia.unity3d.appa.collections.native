using Unity.Jobs.LowLevel.Unsafe;

namespace collections.native.src
{
    [JobProducerType(typeof(IJobParallelFor3DExtensions.ParallelFor3DJobStruct<>))]
    public interface IJobParallelFor3D
    {
        /// <summary>
        ///     <para>Implement this method to perform work against a specific iteration index.</para>
        /// </summary>
        /// <param name="flatIndex">The index of the Parallel for loop at which to perform work.</param>
        void Execute(int flatIndex);
    }
}