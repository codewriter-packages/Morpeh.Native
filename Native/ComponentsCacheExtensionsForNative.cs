using System.Runtime.CompilerServices;

namespace Morpeh.Native
{
    public static class ComponentsCacheExtensionsForNative
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NativeComponentsCache<TNative> AsNative<TNative>(this ComponentsCache<TNative> cache)
            where TNative : unmanaged, IComponent
        {
            var nativeCache = new NativeComponentsCache<TNative>
            {
                components = cache.components.AsNative(),
            };

            return nativeCache;
        }
    }
}