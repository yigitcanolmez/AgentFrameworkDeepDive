namespace AgenticUserForm.Services;

using AgenticUserForm.Models;
using System.Collections.Concurrent;

public class UserService : IUserService
{
    private static readonly ConcurrentDictionary<string, UserContact> _store = new(StringComparer.OrdinalIgnoreCase);

    public void AddOrUpdate(UserContact user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));

        _store.AddOrUpdate(user.Eposta.Trim(), user, (_, _) => user);
    }

    public UserContact? GetByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return null;
        _store.TryGetValue(email.Trim(), out var user);
        return user;
    }
}
