using Data;
using Microsoft.EntityFrameworkCore;

namespace Extensions;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider){
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        await dbContext.Database.MigrateAsync();
    }

}