# EduHub - Chương 1: Phân tích yêu cầu và Domain Modeling

## 1. Giới thiệu Bối cảnh
EduHub là hệ thống quản lý Câu lạc bộ & Sự kiện sinh viên của Trường Đại học Đồng Tháp, hỗ trợ ban quản lý tổ chức sự kiện, theo dõi thành viên đăng ký và ghi nhận điểm danh[cite: 1].

## 2. Mô tả thiết kế Domain (Nhiệm vụ 3)
Hệ thống gồm 5 class chính tuân thủ chặt chẽ nguyên tắc encapsulation (đóng gói), sử dụng `private init` / `private set`, factory method `Create(...)` và XML doc comments (`/// <summary>`)[cite: 3, 5]:
* **Member**: Quản lý thông tin cá nhân sinh viên, mã số sinh viên, email và trạng thái hoạt động (`IsActive()`).
* **Club**: Quản lý thông tin CLB, danh sách thành viên an toàn qua `IReadOnlyCollection<Member>` và phương thức thêm thành viên (`AddMember`), đếm số lượng (`MemberCount()`).
* **Event**: Quản lý sự kiện do CLB tổ chức, kiểm tra sức chứa tối đa (`HasCapacity()`).
* **Registration**: Quản lý phiếu đăng ký tham gia sự kiện của thành viên với các trạng thái (`Pending`, `Confirmed`, `Cancelled`), hỗ trợ xác nhận (`Confirm()`) và hủy (`Cancel()`).
* **CheckIn**: Ghi nhận điểm danh thực tế tại sự kiện dựa trên phiếu đăng ký, cung cấp phương thức tóm tắt thông tin (`Summary()`).

## 3. Cách chạy ứng dụng (Console App Demo)
1. Mở terminal và di chuyển vào thư mục dự án:
   ```powershell
   cd EduHub.Ch01
