using Endpoints;

namespace Extensions;

public static class MapEndpointsExtension
{
    public static void MapCustomEndpoints(this IEndpointRouteBuilder app){
        app.MapGameEndpoints();
        app.MapProductEndpoints();
        app.MapAuthEndpoints();
        app.MapShopEndpoints();
        app.MapCategoryEndpoints();
    }
}