namespace Habr.Tests
{
    /// <summary>
    /// Configures Test Classes to run independently
    /// </summary>
    [CollectionDefinition("Sequential", DisableParallelization = true)]
    public class SequentialCollection { }
}
