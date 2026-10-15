namespace EduHub.Ch01.Domain;

/// <summary>
/// Câu lạc bộ sinh viên.
/// </summary>
public sealed class Club
{
    /// <summary>Khoá chính của câu lạc bộ.</summary>
    public Guid Id { get; private init; }

    /// <summary>Tên câu lạc bộ.</summary>
    public string Name { get; private init; } = string.Empty;

    /// <summary>Mô tả câu lạc bộ.</summary>
    public string Description { get; private init; } = string.Empty;

    /// <summary>Ngày thành lập câu lạc bộ.</summary>
    public DateTime CreatedAt { get; private init; }

    private readonly List<Member> _members = new();

    /// <summary>Danh sách thành viên của câu lạc bộ.</summary>
    public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

    private Club() { }

    /// <summary>Tạo câu lạc bộ mới sau khi kiểm tra dữ liệu.</summary>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tên câu lạc bộ không được để trống.", nameof(name));
        }

        return new Club
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>Thêm thành viên vào câu lạc bộ.</summary>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);
        
        if (!_members.Any(m => m.Id == member.Id))
        {
            _members.Add(member);
        }
    }

    /// <summary>Trả về số lượng thành viên hiện tại trong câu lạc bộ.</summary>
    public int MemberCount() => _members.Count;
}
