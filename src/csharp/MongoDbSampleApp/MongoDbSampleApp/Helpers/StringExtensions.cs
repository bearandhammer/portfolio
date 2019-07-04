using System;
using System.ComponentModel;

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

                    if (type == typeof(Guid))
                    {
                        parsedValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(inputValue);
                    }
                    else
                    {
                        parsedValue = (T)Convert.ChangeType(inputValue, type);
                    }
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