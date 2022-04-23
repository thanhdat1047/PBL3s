
USE QuanLyThuVienSach;
GO
    
INSERT INTO dbo.Sach VALUES ( 'Dac Nhan Tam', 'Cuoc Song', 'Dale Carnegie', '80', '1936', 76000, 80000 )

INSERT INTO dbo.Sach VALUES ( 'Nha Gia Kim', 'Phieu Luu', 'Paulo Coelho', '100', '1988', 59000, 65000 )

INSERT INTO dbo.Sach VALUES ( 'De Men Phieu Luu Ky', 'Thieu Nhi', 'To Hoai', '100', '1941', 39000, 45000 )


INSERT INTO dbo.Position VALUES ( 1, 'Admin' )

INSERT INTO dbo.Position VALUES( 2, 'Thu Kho' )

INSERT INTO dbo.Position VALUES( 3, 'Nhan Vien Ban Sach' )

INSERT INTO dbo.Position VALUES( 4, 'Khach Hang' )


INSERT INTO dbo.Account VALUES( 'Admin1', '12345ad', 1  )

INSERT INTO dbo.Account VALUES( 'ThuKho1', '12345ad', 2  )

INSERT INTO dbo.Account VALUES( 'NhanVienBanSach1', '12345ad', 3  )

INSERT INTO dbo.Account VALUES( 'KhachHang1', '12345ad', 4  )

INSERT INTO dbo.Account VALUES( 'KhachHang2', '12345ad', 4 )

INSERT INTO dbo.Account VALUES( 'KhachHang3', '12345ad', 4 )



INSERT INTO dbo.Person VALUES ( 'Vu Tien Hung', 1, '2002-05-03', 'Ninh Binh', 'Tienhung0305@gmail.com', '0867166915', 1)

INSERT INTO dbo.Person VALUES ( 'Nguyen Hieu', 0, '2002-03-22', 'Da Nang', 'Hieupiro124@gmail.com', '0929230458', 2)

INSERT INTO dbo.Person VALUES ( 'Nguyen Tri Hau', 0, '2002-10-19', 'Quang Nam', 'Nguyentrihauqna@gmail.com', '0365288052', 3)

INSERT INTO dbo.Person VALUES ( 'Truong Thanh Dat', 0, '2002-05-10', 'Quang Nam', 'Thanhdat1047t@gmail.com', '0848056800', 4)

INSERT INTO dbo.Person VALUES ( 'Phan Thanh Duc', 0, '2002-02-10', 'Quang Nam', 'Thanhduc1047t@gmail.com', '0848056811', 5)

INSERT INTO dbo.Person VALUES ( 'Nguyen Trong Hoang', 0, '2002-03-10', 'Ha Noi', 'TTronghoang2002t@gmail.com', '0848056811', 6)



INSERT INTO dbo.Kho VALUES ( 1, 10, 5)

INSERT INTO dbo.Kho VALUES ( 2, 20, 15)

INSERT INTO dbo.Kho VALUES ( 3, 15, 10)


			
INSERT INTO dbo.LichSuNhapSach VALUES( 1, 10, GETDATE(), 2 )

INSERT INTO dbo.LichSuNhapSach VALUES( 2, 20, GETDATE(), 2 )

INSERT INTO dbo.LichSuNhapSach VALUES( 3, 15, GETDATE(), 2 )
					

INSERT INTO dbo.SachKhuyenMai VALUES( 1, 0.5, GETDATE(), '2022-6-1' )

INSERT INTO dbo.SachKhuyenMai VALUES( 2, 0.2, GETDATE(), '2022-6-1' )

		
		
INSERT INTO dbo.HoaDon VALUES ( GETDATE(), 80000, 4)

			
INSERT INTO dbo.ChiTietHoaDon VALUES ( 1, 1, 2, 0.5 )

