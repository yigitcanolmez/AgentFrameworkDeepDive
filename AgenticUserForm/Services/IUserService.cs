using AgenticUserForm.Models;

namespace AgenticUserForm.Services;

public interface IUserService
{
    /// <summary>
    /// Yeni kullanıcı iletişim bilgisini ekler. Aynı e-posta varsa günceller.
    /// </summary>
    /// <param name="user">Eklenecek kullanıcı</param>
    void AddOrUpdate(UserContact user);

    /// <summary>
    /// E-posta adresine göre kullanıcı getirir; yoksa null döner.
    /// </summary>
    /// <param name="email">E-posta</param>
    UserContact? GetByEmail(string email);
}
