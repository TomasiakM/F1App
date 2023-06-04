namespace Infrastructure.Interfaces;
internal interface IHashService
{
    string Hash(string value);
    bool Validate(string value, string hash);
}
