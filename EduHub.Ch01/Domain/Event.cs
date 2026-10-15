namespace EduHub.Ch01.Domain;

/// <summary>
/// Sự kiện do câu lạc bộ tổ chức.
/// </summary>
public sealed class Event
{
    /// <summary>Khoá chính của sự kiện.</summary>
    public Guid Id { get; private init; }

    /// <summary>Khoá ngoại liên kết tới Câu lạc bộ tổ chức.</summary>
    public Guid ClubId { get; private init; }

    /// <summary>Tiêu đề sự kiện.</summary>
    public string Title { get; private init; } = string.Empty;

    /// <summary>Thời gian diễn ra sự kiện.</summary>
    public DateTime Date { get; private init; }

    /// <summary>Sức chứa tối đa của sự kiện.</summary>
    public int Capacity { get; private init; }

    /// <summary>Hình thức tổ chức (Online hoặc Offline).</summary>
    public bool IsOnline { get; private init; }

    private Event() { }

    /// <summary>Tạo sự kiện mới sau khi kiểm tra dữ liệu.</summary>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        if (clubId == Guid.Empty)
        {
            throw new ArgumentException("Mã câu lạc bộ không hợp lệ.", nameof(clubId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Tiêu đề sự kiện không được để trống.", nameof(title));
        }

        if (capacity <= 0)
        {
            throw new ArgumentException("Sức chứa sự kiện phải lớn hơn 0.", nameof(capacity));
        }

        return new Event
        {
            Id = Guid.NewGuid(),
            ClubId = clubId,
            Title = title.Trim(),
            Date = date,
            Capacity = capacity,
            IsOnline = isOnline
        };
    }

    /// <summary>Kiểm tra xem sự kiện còn chỗ trống hay không dựa trên số lượng đã đăng ký.</summary>
    public bool HasCapacity(int registeredCount)
    {
        return registeredCount < Capacity;
    }
}