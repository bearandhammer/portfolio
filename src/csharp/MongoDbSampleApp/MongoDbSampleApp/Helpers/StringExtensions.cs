using System;

namespace MongoDbSampleApp.Helpers
{
    public static class StringExtensions
    {
        public static bool TryParseValueFromUser<T>(this string inputValue, out T parsedValue, Func<string, T> providedParser = null)
        {
            bool parsedSuccessful = false;
            parsedValue = default;

            try
            {
                if (!string.IsNullOrWhiteSpace(inputValue))
                {
                    inputValue = inputValue.Trim();
                }

                if (providedParser != null)
                {
                    parsedValue = providedParser(inputValue);
                }
                else
                {
                    Type type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                    parsedValue = (T)Convert.ChangeType(inputValue, type);
                }

                parsedSuccessful = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occurred parsing a value from the UI: { ex.Message }");
            }

            return parsedSuccessful;
        }
    }
}