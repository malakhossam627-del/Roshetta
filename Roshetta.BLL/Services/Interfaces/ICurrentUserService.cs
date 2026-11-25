namespace Roshetta.BLL.Services.Interfaces
{
    public interface ICurrentUserService
    {
        int GetCurrentUserId();
        string GetCurrentUserName();
    }
}
