namespace ProjectRed.Core.Interfaces.Services.Validators
{
    public interface IPasswordValidator
    {
        bool IsValid(string password);
    }
}
