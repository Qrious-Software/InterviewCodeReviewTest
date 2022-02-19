namespace InterviewCodeReviewTest
{
    public interface IConfiguration
    {
        /// <summary>
        /// Gets a value from config based on a value type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>The read value of the default for the type</returns>
        bool GetConfigurationValueType<T>(string key, out T value, T defaultValue = default(T)) where T : struct;
        /// <summary>
        /// Gets a string value within config based on the key 
        /// </summary>
        /// <param name="key">key in config</param>
        /// <returns>the ead value or null</returns>
        string GetConfigurationStringValue(string key);
        /// <summary>
        /// Populates an instance of the type 'T' with config data
        /// </summary>
        /// <typeparam name="T">A POCO that maps to the config</typeparam>
        /// <param name="sectionName"></param>
        /// <returns>an instance of the type populated with read values</returns>
        T GetConfigurationSection<T>(string sectionName) where T : new();
    }
}