namespace EduHub.Ch01.Domain;

/// <summary>
/// Phiếu đăng ký tham gia sự kiện của thành viên.
/// </summary>
public sealed class Registration
{
    /// <summary>Khoá chính của phiếu đăng ký.</summary>
    public Guid Id { get; private init; }

    /// <summary>Khoá ngoại liên kết tới Thành viên đăng ký.</summary>
    public Guid MemberId { get; private init; }

    /// <summary>Khoá ngoại liên kết tới Sự kiện đăng ký.</summary>
    public Guid EventId { get; private init; }

    /// <summary>Thời gian đăng ký.</summary>
    public DateTime RegisteredAt { get; private init; }

    /// <summary>Trạng thái của phiếu đăng ký (Pending, Confirmed, Cancelled).</summary>
    public string Status { get; private set; } = "Pending";

    private Registration() { }

    /// <summary>Tạo phiếu đăng ký mới sau khi kiểm tra dữ liệu.</summary>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
        {
            throw new ArgumentException("Mã thành viên không hợp lệ.", nameof(memberId));
        }

        if (eventId == Guid.Empty)
        {
            throw new ArgumentException("Mã sự kiện không hợp lệ.", nameof(eventId));
        }

        return new Registration
        {
            Id = Guid.NewGuid(),
            MemberId = memberId,
            EventId = eventId,
            RegisteredAt = DateTime.UtcNow,
            Status = "Pending"
        };
    }

    /// <summary>Xác nhận phiếu đăng ký.</summary>
    public void Confirm()
    {
        if (Status == "Cancelled")
        {
            throw new InvalidOperationException("Không thể xác nhận phiếu đăng ký đã bị hủy.");
        }
        Status = "Confirmed";
    }

    /// <summary>Hủy phiếu đăng ký.</summary>
    public void Cancel()
    {
        Status = "Cancelled";
    }
}