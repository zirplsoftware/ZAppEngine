namespace Zirpl.AppEngine.Service.Membership
{
    public enum ChangeUserNameError : byte
    {
        Unknown = 0,
        UserNotFound = 1,
        NewUserNameAlreadyTaken = 2
    }
}
