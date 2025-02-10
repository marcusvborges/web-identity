namespace WebIdentity.Services
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRoleAsync();
        Task SeedUserAsync();
    }
}
