using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace collections.native.src
{
    /// <summary>
    ///     Supporting functionality for <see cref="IJobParallelForRanged" />
    /// </summary>
    public static class IJobParallelForRangedExtensions
    {
        /// <summary>
        ///     Run a job asynchronously
        /// </summary>
        /// <param name="jobData">
        ///     Job to run
        /// </param>
        /// <param name="valuesLength">
        ///     Length of the values to execute on.
        /// </param>
        /// <param name="innerloopBatchCount">
        ///     Number of job executions per batch
        /// </param>
        /// <param name="dependsOn">
        ///     Handle of the job that must be run before this job
        /// </param>
        /// <returns>
        ///     A handle to the created job
        /// </returns>
        /// <typeparam name="T">
        ///     Type of job to run
        /// </typeparam>
        public static unsafe JobHandle ScheduleRanged<T>(
            this T jobData,
            int valuesLength,
            int innerloopBatchCount,
            JobHandle dependsOn = new JobHandle())
            where T : struct, IJobParallelForRanged
        {
            var scheduleParams = new JobsUtility.JobScheduleParameters(
                UnsafeUtility.AddressOf(ref jobData),
                ParallelForJobStruct<T>.Initialize(),
                dependsOn,
                ScheduleMode.Batched
            );
            return JobsUtility.ScheduleParallelFor(ref scheduleParams, valuesLength, innerloopBatchCount);
        }

        /// <summary>
        ///     Run a job synchronously
        /// </summary>
        /// <param name="jobData">
        ///     Job to run
        /// </param>
        /// <param name="valuesLength">
        ///     Length of the values to execute on.
        /// </param>
        /// <typeparam name="T">
        ///     Type of job to run
        /// </typeparam>
        public static unsafe void RunRanged<T>(this T jobData, int valuesLength)
            where T : struct, IJobParallelForRanged
        {
            var scheduleParams = new JobsUtility.JobScheduleParameters(
                UnsafeUtility.AddressOf(ref jobData),
                ParallelForJobStruct<T>.Initialize(),
                new JobHandle(),
                ScheduleMode.Run
            );
            JobsUtility.ScheduleParallelFor(ref scheduleParams, valuesLength, valuesLength);
        }

        /// <summary>
        ///     Supporting functionality for <see cref="IJobParallelForRanged" />
        /// </summary>
        internal struct ParallelForJobStruct<TJob>
            where TJob : struct, IJobParallelForRanged
        {
            /// <summary>
            ///     Cached job type reflection data
            /// </summary>
            public static IntPtr jobReflectionData;

            /// <summary>
            ///     Initialize the job type
            /// </summary>
            /// <returns>
            ///     Reflection data for the job type
            /// </returns>
            public static IntPtr Initialize()
            {
                if (jobReflectionData == IntPtr.Zero)
                {
                    jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(TJob), JobType.ParallelFor, (ExecuteJobFunction) Execute);
                }

                return jobReflectionData;
            }

            /// <summary>
            ///     Delegate type for <see cref="Execute" />
            /// </summary>
            public delegate void ExecuteJobFunction(
                ref TJob data,
                IntPtr additionalPtr,
                IntPtr bufferRangePatchData,
                ref JobRanges ranges,
                int jobIndex);

            /// <summary>
            ///     Execute the job until there are no more work stealing ranges
            ///     available to execute
            /// </summary>
            /// <param name="jobData">
            ///     The job to execute
            /// </param>
            /// <param name="additionalPtr">
            ///     TBD. Unused.
            /// </param>
            /// <param name="bufferRangePatchData">
            ///     TBD. Unused.
            /// </param>
            /// <param name="ranges">
            ///     Work stealing ranges to execute from
            /// </param>
            /// <param name="jobIndex">
            ///     Index of this job
            /// </param>
            public static unsafe void Execute(ref TJob jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
            {
                int startIndex;
                int endIndex;
                while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out startIndex, out endIndex))
                {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    JobsUtility.PatchBufferMinMaxRanges(
                        bufferRangePatchData,
                        UnsafeUtility.AddressOf(ref jobData),
                        startIndex,
                        endIndex - startIndex
                    );
#endif
                    jobData.Execute(startIndex, endIndex);
                }
            }
        }
    }
}