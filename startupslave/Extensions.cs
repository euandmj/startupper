using System.IO;

namespace core
{
    public static class Extensions
    {

        public static string GetParent(this string path)
            => new DirectoryInfo(path)?.Parent.FullName;

    }
}
