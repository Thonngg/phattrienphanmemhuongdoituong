namespace EduHub.Ch01.Domain;

/// <summary>
/// Ghi nhận điểm danh tại sự kiện.
/// </summary>
public sealed class CheckIn
{
    /// <summary>Khoá chính của bản ghi điểm danh.</summary>
    public Guid Id { get; private init; }

    /// <summary>Khoá ngoại liên kết tới Phiếu đăng ký.</summary>
    public Guid RegistrationId { get; private init; }

    /// <summary>Thời điểm thực hiện điểm danh.</summary>
    public DateTime CheckedInAt { get; private init; }

    /// <summary>Phương thức điểm danh (ví dụ: QR Code, Manual).</summary>
    public string Method { get; private init; } = string.Empty;

    private CheckIn() { }

    /// <summary>Tạo bản ghi điểm danh mới sau khi kiểm tra dữ liệu.</summary>
    public static CheckIn Create(Guid registrationId, string method)
    {
        if (registrationId == Guid.Empty)
        {
            throw new ArgumentException("Mã phiếu đăng ký không hợp lệ.", nameof(registrationId));
        }

        if (string.IsNullOrWhiteSpace(method))
        {
            throw new ArgumentException("Phương thức điểm danh không được để trống.", nameof(method));
        }

        return new CheckIn
        {
            Id = Guid.NewGuid(),
            RegistrationId = registrationId,
            CheckedInAt = DateTime.UtcNow,
            Method = method.Trim()
        };
    }

    /// <summary>Trả về chuỗi mô tả tóm tắt thông tin điểm danh.</summary>
    public string Summary()
    {
        return $"CheckIn ID: {Id} | Registration ID: {RegistrationId} | Method: {Method} | Time: {CheckedInAt:yyyy-MM-dd HH:mm:ss} UTC";
    }
}