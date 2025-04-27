using Flowspire.Application.Exceptions;

namespace Flowspire.Application.Common
{
    public static class ObjectExtensions
    {
        public static T ThrowIfNull<T>(this T? obj, string errorMessage) where T : class
        {
            if (obj == null)
                throw new NotFoundException(errorMessage);

            return obj;
        }
    }
}