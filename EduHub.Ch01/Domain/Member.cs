namespace EduHub.Ch01.Domain;

/// <summary>
/// Thanh vien sinh vien cua CLB.
/// </summary>
public sealed class Member
{
    /// <summary>
    /// Khoa chinh cua thanh vien.
    /// </summary>
    public Guid Id { get; private init; }

    /// <summary>
    /// Ho ten thanh vien.
    /// </summary>
    public string Name { get; private init; } = string.Empty;

    /// <summary>
    /// Email cua thanh vien.
    /// </summary>
    public string Email { get; private init; } = string.Empty;

    /// <summary>
    /// Ma so sinh vien.
    /// </summary>
    public string StudentCode { get; private init; } = string.Empty;

    /// <summary>
    /// Ngay tham gia CLB.
    /// </summary>
    public DateTime JoinedAt { get; private init; }

    private Member()
    {
    }

    /// <summary>
    /// Tao thanh vien moi sau khi kiem tra du lieu.
    /// </summary>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Ho ten khong duoc de trong.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new ArgumentException("Email khong hop le.", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(studentCode))
        {
            throw new ArgumentException("Ma sinh vien khong duoc de trong.", nameof(studentCode));
        }

        return new Member
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.Trim(),
            StudentCode = studentCode.Trim(),
            JoinedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Kiem tra thanh vien da duoc tao hop le hay chua.
    /// </summary>
    public bool IsActive()
    {
        return JoinedAt <= DateTime.UtcNow;
    }
}