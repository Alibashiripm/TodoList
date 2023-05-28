namespace TodoList.Utilities
{
    public static class GetEnumText
    {
        public static string GetText<T>(int index) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (index < 0 || index >= Enum.GetValues(typeof(T)).Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return Enum.GetName(typeof(T), Enum.GetValues(typeof(T)).GetValue(index));
        }

    }
}
