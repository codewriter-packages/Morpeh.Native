using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Morpeh.Native
{
    public struct NativeComponentsCache<TNative> where TNative : unmanaged, IComponent
    {
        [NativeDisableUnsafePtrRestriction]
        public NativeIntHashMapWrapper<TNative> components;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent(int entityId)
        {
            return entityId != -1 && components.TryGetIndex(entityId) != -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TNative GetComponent(int entityId, out bool exists)
        {
            exists = HasComponent(entityId);
            return ref components.GetValueRefByKey(entityId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TNative GetComponent(int entityId)
        {
            return ref components.GetValueRefByKey(entityId);
        }
    }
}