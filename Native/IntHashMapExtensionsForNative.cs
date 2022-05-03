using System.Runtime.CompilerServices;
using Morpeh.Collections;

namespace Morpeh.Native
{
    public static class IntHashMapExtensionsForNative
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe NativeIntHashMapWrapper<TNative> AsNative<TNative>(this IntHashMap<TNative> hashMap)
            where TNative : unmanaged
        {
            var nativeIntHashMap = new NativeIntHashMapWrapper<TNative>();

            fixed (int* capacityMinusOnePtr = &hashMap.capacityMinusOne)
            fixed (TNative* dataPtr = &hashMap.data[0])
            fixed (int* bucketsPtr = &hashMap.buckets[0])
            fixed (IntHashMapSlot* slotsPtr = &hashMap.slots[0])
            {
                nativeIntHashMap.capacityMinusOnePtr = capacityMinusOnePtr;
                nativeIntHashMap.data = dataPtr;
                nativeIntHashMap.buckets = bucketsPtr;
                nativeIntHashMap.slots = slotsPtr;
            }

            return nativeIntHashMap;
        }
    }
}