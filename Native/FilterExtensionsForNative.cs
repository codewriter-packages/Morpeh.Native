using Unity.Collections;

namespace Morpeh.Native
{
    public static class FilterExtensionsForNative
    {
        public static NativeArray<int> ToNativeArray(this Filter filter, Allocator allocator)
        {
            var array = new NativeArray<int>(filter.Length, allocator, NativeArrayOptions.UninitializedMemory);

            var index = 0;
            for (int archetypeId = 0, archetypesCount = filter.archetypes.length;
                archetypeId < archetypesCount;
                archetypeId++)
            {
                foreach (var entityId in filter.archetypes.data[archetypeId].entitiesBitMap)
                {
                    array[index++] = entityId;
                }
            }

            return array;
        }

#if MORPEH_UNITY_COLLECTIONS_ENABLED
        public static void CopyTo(this Filter filter, NativeList<int> entityIds)
        {
            entityIds.ResizeUninitialized(filter.Length);

            for (int archetypeId = 0, archetypesCount = filter.archetypes.length;
                archetypeId < archetypesCount;
                archetypeId++)
            {
                foreach (var entityId in filter.archetypes.data[archetypeId].entitiesBitMap)
                {
                    entityIds.Add(entityId);
                }
            }
        }
#endif
    }
}