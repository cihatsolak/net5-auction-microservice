using System;

namespace ESourcing.UI.Infrastructure.Extensions
{
    internal static class EnumHelper
    {
        internal static int ToInt(this Enum @enum)
        {
            if (@enum is null)
                return 0;

            return Convert.ToInt32(@enum);
        }
    }
}
