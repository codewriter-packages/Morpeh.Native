using System.Runtime.CompilerServices;
using Morpeh.Collections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Morpeh.Native
{
    public struct NativeIntHashMapWrapper<TNative> where TNative : unmanaged
    {
        [NativeDisableUnsafePtrRestriction] public unsafe int* capacityMinusOnePtr;
        [NativeDisableUnsafePtrRestriction] public unsafe int* buckets;
        [NativeDisableUnsafePtrRestriction] public unsafe IntHashMapSlot* slots;

        [NativeDisableParallelForRestriction]
        [NativeDisableUnsafePtrRestriction]
        public unsafe TNative* data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe ref TNative GetValueRefByKey(int key)
        {
            var rem = key & *capacityMinusOnePtr;

            int next;
            for (var i = buckets[rem] - 1; i >= 0; i = next)
            {
                ref var slot = ref UnsafeUtility.ArrayElementAsRef<IntHashMapSlot>(slots, i);
                if (slot.key - 1 == key)
                {
                    return ref UnsafeUtility.ArrayElementAsRef<TNative>(data, i);
                }

                next = slot.next;
            }

            return ref UnsafeUtility.ArrayElementAsRef<TNative>(data, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe int TryGetIndex(in int key)
        {
            var rem = key & *capacityMinusOnePtr;

            int next;
            for (var i = buckets[rem] - 1; i >= 0; i = next)
            {
                ref var slot = ref UnsafeUtility.ArrayElementAsRef<IntHashMapSlot>(slots, i);
                if (slot.key - 1 == key)
                {
                    return i;
                }

                next = slot.next;
            }

            return -1;
        }
    }
}