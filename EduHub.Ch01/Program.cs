using System;
using System.Collections.Generic;
using EduHub.Ch01.Domain;

namespace EduHub.Ch01;

/// <summary>
/// Chương trình demo luồng nghiệp vụ hệ thống EduHub.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        // Đảm bảo hiển thị tiếng Việt đúng trên terminal
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        const string Separator = "==================================================";

        Console.WriteLine(Separator);
        Console.WriteLine("      DEMO LUỒNG NGHIỆP VỤ EDUHUB - CHƯƠNG 1        ");
        Console.WriteLine(Separator);

        try
        {
            // 1. Tạo CLB "Tech Club"
            Console.WriteLine("\n[1] Tạo Câu lạc bộ...");
            var techClub = Club.Create("Tech Club", "Câu lạc bộ Công nghệ sinh viên Trường Đại học Đồng Tháp");
            Console.WriteLine($"- Đã tạo CLB: {techClub.Name}");
            Console.WriteLine($"- Mô tả: {techClub.Description}");

            // 2. Thêm 3 thành viên vào CLB
            Console.WriteLine("\n[2] Thêm thành viên vào CLB...");
            var member1 = Member.Create("Nguyễn Văn An", "an.nv@dthu.edu.vn", "B2101234");
            var member2 = Member.Create("Trần Thị Bình", "binh.tt@dthu.edu.vn", "B2105678");
            var member3 = Member.Create("Lê Hoàng Cường", "cuong.lh@dthu.edu.vn", "B2109012");

            techClub.AddMember(member1);
            techClub.AddMember(member2);
            techClub.AddMember(member3);

            Console.WriteLine($"- Đã thêm {techClub.MemberCount()} thành viên vào {techClub.Name}:");
            foreach (var m in techClub.Members)
            {
                Console.WriteLine($"  + {m.Name} (MSSV: {m.StudentCode}) - Email: {m.Email}");
            }

            // 3. Tạo 1 sự kiện cho CLB
            Console.WriteLine("\n[3] Tạo sự kiện cho CLB...");
            var workshopEvent = Event.Create(techClub.Id, "Workshop AI & .NET 10", DateTime.UtcNow.AddDays(7), 50, true);
            Console.WriteLine($"- Tên sự kiện: {workshopEvent.Title}");
            Console.WriteLine($"- Sức chứa: {workshopEvent.Capacity} | Hình thức: {(workshopEvent.IsOnline ? "Online" : "Offline")}");

            // 4. 2 thành viên đăng ký tham dự sự kiện -> Status = "Pending" -> gọi Confirm()
            Console.WriteLine("\n[4] Thành viên đăng ký và xác nhận tham dự sự kiện...");
            var reg1 = Registration.Create(member1.Id, workshopEvent.Id);
            var reg2 = Registration.Create(member2.Id, workshopEvent.Id);

            Console.WriteLine($"- {member1.Name} đã đăng ký. Trạng thái ban đầu: {reg1.Status}");
            Console.WriteLine($"- {member2.Name} đã đăng ký. Trạng thái ban đầu: {reg2.Status}");

            reg1.Confirm();
            reg2.Confirm();
            Console.WriteLine($"- Sau khi gọi Confirm():");
            Console.WriteLine($"  + Phiếu của {member1.Name}: {reg1.Status}");
            Console.WriteLine($"  + Phiếu của {member2.Name}: {reg2.Status}");

            // 5. 1 thành viên thực hiện check-in -> tạo CheckIn record
            Console.WriteLine("\n[5] Điểm danh tham dự sự kiện...");
            var checkIn1 = CheckIn.Create(reg1.Id, "QR Code");
            Console.WriteLine($"- {member1.Name} đã thực hiện điểm danh thành công!");
            Console.WriteLine($"  {checkIn1.Summary()}");

            // 6. In báo cáo tham dự
            Console.WriteLine("\n[6] Báo cáo tham dự sự kiện:");
            Console.WriteLine(Separator);
            Console.WriteLine($"Sự kiện: {workshopEvent.Title}");
            Console.WriteLine($"Tổng số đăng ký: 2");
            Console.WriteLine($"Tổng số check-in: 1");
            Console.WriteLine("Danh sách thành viên tham dự:");
            Console.WriteLine($"1. {member1.Name} ({member1.StudentCode}) - Trạng thái: {reg1.Status} [Đã Check-in]");
            Console.WriteLine($"2. {member2.Name} ({member2.StudentCode}) - Trạng thái: {reg2.Status} [Chưa Check-in]");
            Console.WriteLine(Separator);

            // 7. Thử một thao tác vi phạm luật nghiệp vụ và bắt InvalidOperationException
            Console.WriteLine("\n[7] Kiểm tra xử lý ngoại lệ (Business Rule Violation)...");
            var reg3 = Registration.Create(member3.Id, workshopEvent.Id);
            reg3.Cancel(); // Hủy phiếu đăng ký
            Console.WriteLine($"- {member3.Name} đã hủy phiếu đăng ký. Trạng thái: {reg3.Status}");

            Console.WriteLine("- Thử gọi Confirm() trên phiếu đã bị hủy...");
            reg3.Confirm(); // Sẽ ném ra InvalidOperationException
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[BẮT NGOẠI LỆ THÀNH CÔNG] Phát hiện vi phạm quy tắc nghiệp vụ: {ex.Message}");
        }

        Console.WriteLine("\n" + Separator);
        Console.WriteLine("       KẾT THÚC CHƯƠNG TRÌNH DEMO EDUHUB         ");
        Console.WriteLine(Separator);
    }
}