namespace RealEstateAnalysis.Endpoints
{
    public static class ApiEndpoints
    {
        public static WebApplication UseApiEndpoints(this WebApplication app)
        {
            app.AddUserEndpoints();
            app.AddAgentEndpoints();
            app.AddPropertyEndpoints();
            app.AddOfferEndpoints();
            app.AddImageEndpoints();
            app.AddUsersFavouriteEndpoints();
            
            return app;
        }
    }
}