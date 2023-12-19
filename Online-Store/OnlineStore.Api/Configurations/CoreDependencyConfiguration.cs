namespace OnlineStore.Api.Configurations
{
    public static class CoreDependencyConfiguration
    {
        public static void AddCoreDependencies(this IServiceCollection collection, ConfigurationManager config)
        {
            collection.AddHttpContextAccessor();
            collection.AddServices();
        }

        private static void AddServices(this IServiceCollection collection)
        {
        }
    }
}
