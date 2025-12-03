CREATE DATABASE qlkhachsan
USE qlkhachsan

CREATE TABLE TaiKhoan (
    username VARCHAR(15) PRIMARY KEY,
    pass VARCHAR(20) NOT NULL
);

CREATE TABLE phong (
    maph NVARCHAR(5) PRIMARY KEY,
    loaiph NVARCHAR(15),
    dientich FLOAT,
    tinhtrang NVARCHAR(5),
    giaph INT
);
INSERT INTO phong (maph, loaiph, dientich, tinhtrang, giaph) VALUES
(N'P101', N'Đơn', 25, N'còn', 250000),
(N'P102', N'Đơn', 25, N'còn', 250000),
(N'P103', N'Đôi', 40, N'hết', 400000),
(N'P104', N'Đôi', 40, N'còn', 400000),
(N'P105', N'VIP', 50, N'còn', 600000);

CREATE TABLE dichvu (
    madv NVARCHAR(5) PRIMARY KEY,
    tendv NVARCHAR(15),
    donvitinh NVARCHAR(20),
    dongia INT
);
-- Thêm dữ liệu cho các dịch vụ đồ ăn và đồ uống
INSERT INTO dichvu (madv, tendv, donvitinh, dongia) VALUES
('DV01', N'KFC', N'suất', 80000),
('DV02', N'Bia', N'lon', 25000),
('DV03', N'Rượu vang', N'chai', 4000000),
('DV04', N'Humbeger', N'cái', 25000),
('DV05', N'Massage', N'lần', 1000000);


CREATE TABLE khachhang (
    makh NVARCHAR(15) PRIMARY KEY,
    tenkh NVARCHAR(25),
    namsinh DATE,
    gioitinh NVARCHAR(5),
    diachi NVARCHAR(50),
    sdt NVARCHAR(15)
);

INSERT INTO khachhang (makh, tenkh, namsinh, gioitinh, diachi, sdt) VALUES
('KH01', N'Nguyễn Văn An', '1990-05-15', N'Nam', N'Hà Nội', '0901234567'),
('KH02', N'Trần Thị Bình', '1992-08-20', N'Nữ', N' TP.HCM', '0912345678'),
('KH03', N'Lê Văn Cường', '1988-11-30', N'Nam', N'Ham Định', '0987654321'),
('KH04', N'Phạm Thị Dung', '1995-02-10', N'Nữ', N'Hà Nam', '0978123456'),
('KH05', N'Hoàng Thanh Tùng', '1985-07-25', N'Nam', N'Hải Phòng', '0934567890');

CREATE TABLE datphong (
    mahd NVARCHAR(10) PRIMARY KEY,
    makh NVARCHAR(15) FOREIGN KEY (makh) REFERENCES khachhang(makh),
    ngaybatdau DATE,
    ngayketthuc DATE
);

CREATE TABLE ctdatphong (
    mahd NVARCHAR(10) FOREIGN KEY(mahd) REFERENCES datphong(mahd),
    maph NVARCHAR(5) FOREIGN KEY(maph) REFERENCES phong(maph),
    PRIMARY KEY(mahd, maph),
    songaythue INT,
    thanhtoan INT
)

CREATE TABLE ctdichvu (
    mahd NVARCHAR(10),
    madv NVARCHAR(5) FOREIGN KEY(madv) REFERENCES dichvu(madv),
    maph NVARCHAR(5),
    FOREIGN KEY (mahd, maph) REFERENCES ctdatphong(mahd, maph),
    soluong INT,
    PRIMARY KEY(mahd, maph, madv),
    thanhtien INT
);

DROP TABLE ctdatphong
DROP TABLE ctdichvu
drop table datphong
drop table phong
drop table khachhang
drop table dichvu


delete from ctdatphong
delete from ctdichvu
delete from datphong
delete from phong
delete from khachhang
delete from dichvu

--hien thi ten khach hang trong hop dong 01:thong tin hien thi gom co makh,tenkh
select thuephong.makh,tenkh
from thuephong,khachhang
where thuephong.makh =khachhang.makh and thuephong.mahd='hd01'
--Hiển thị tên khách hàng đã thuê phòng có từ 2 hợp đồng trở lên
select khachhang.makh,tenkh,COUNT(thuephong.mahd) as tongsophong
from khachhang,thuephong
where khachhang.makh =thuephong.makh 
group by khachhang.makh,tenkh
having COUNT(*) >=2
--Hiển thị tên khách hàng, điện thoại các khách hàng đã thuê phòng trong ngày YEAR-MONTH-DAY
select khachhang.makh,tenkh,sdt,ngaybatdau
from khachhang,thuephong
where khachhang.makh =thuephong.makh and ngaybatdau ='2024-11-01'

--Hiển thị diện tích và giá của loại phòng ‘Phòng đơn’
select * from phong
where phong.loaiph =N'đơn'
-- Hiển thị tổng số phòng mỗi loại trong khách sạn
select phong.loaiph,count(loaiph) as tongsophong
from phong
group by loaiph
--Hiển thị giá phòng mỗi loại theo thứ tự giảm dần
select * from phong
order by giaph desc
--Tạo view hiển thị tên khách hàng, điện thoại khách thuê phòng loại ‘phòng đôi’
create view vie21
as
select khachhang.makh,tenkh,sdt,loaiph
from khachhang,thuephong,ctthuephong,phong
where khachhang.makh =thuephong.makh and thuephong.mahd =ctthuephong.mahd 
and phong.maph =ctthuephong.maph and loaiph ='don'
drop view vie21
--Hiển thị tên khách hàng, điện thoại những khách hàng có ngày kết thúc thuê
--phòng là YEAR-MONTH-DAY
select khachhang.makh,tenkh,sdt,ngayketthuc
from khachhang,thuephong
where khachhang.makh =thuephong.makh  and  ngayketthuc ='2024-11-04'

--2. Hiển thị thông tin các phòng trong khách sạn theo giá thuê tăng dần
select * from phong
order by giaph 
--2. Hiển thị tên, điện thoại những khách hàng thuê phòng có ngày thuê bắt đầu trong
--tháng x năm x
select khachhang.makh,tenkh,sdt,ngaybatdau
from khachhang,thuephong
where khachhang.makh =thuephong.makh and MONTH(ngaybatdau) =11 and YEAR(ngaybatdau) =2024
--2. Loại phòng là ‘Phòng đơn’ trong khách sạn đã có bao nhiêu lượt người thuê.
select phong.maph,loaiph,count(*) as tongsoluotthue
from phong,ctthuephong
where phong.maph =ctthuephong.maph and loaiph='don thuong'
group by phong.maph,loaiph
--2. Hiển thị Giá phòng cao nhất trong khách sạn
select * from phong
where giaph =(select max(giaph) from phong)
--2. Những phòng nào có giá thấp nhất trong khách sạn
select * from phong
where giaph =(select min(giaph) from phong)

create rule gt
as
@gt in(N'Nam',N'Nữ')
exec sp_bindrule gt,'khachhang.gioitinh'
exec sp_unbindrule 'khachhang.gioitinh'
drop rule gt
--3.Tạo bổ sung ràng buộc Rule cho cột diện tích phòng chỉ nhận một trong các giá
--trị 20,25,35,50
create rule dientich
as
@dt in(20,30,40,60)
exec sp_bindrule dientich,'phong.dientich'
exec sp_unbindrule 'phong.dientich'
--3. Tạo bổ sung ràng buộc Default cho cột ngày bắt đầu bằng ngày hiện tại
create default ngaybd
as
getdate()

exec sp_bindefault ngaybd,'thuephong.ngaybatdau'
exec sp_unbindefault 'thuephong.ngaybatdau'
drop default def3

CREATE DEFAULT ngaykt 
as
DATEADD(DAY, 1, GETDATE());

EXEC sp_bindefault 'ngaykt', 'thuephong.ngayketthuc';
exec sp_unbindefault 'thuephong.ngayketthuc'
drop default ngaykt

--3.Tạo bổ sung ràng buộc Rule cho cột thành tiền có giá trị >0
create rule thanhtien
as
@tt > 0
exec sp_bindrule rul34,'cthuephong.thanhtien'
exec sp_unbindrule 'cthuephong.thanhtien'
-- Tạo thủ tục hiển thị thông tin của phòng trong hợp đồng khi biết mã hợp đồng

create proc thongtinphong @mahd varchar(10) 
as
select phong.maph,mahd
from phong,ctthuephong
where phong.maph =ctthuephong.maph and mahd =@mahd
exec thongtinphong 'hd01'
drop proc thongtinphong

--4. Tạo thủ tục hiển thị thông tin thuê phòng của khách hàng trong ngày 20/10/2024
--(thông tin gồm mã hợp đồng, mã phòng, ngày bắt đầu thuê, ngày kết thúc thuê khi
--biết mã khách hàng
create proc pro42 @makh varchar(10)
as
select cthuephong.mahd,maph,ngaybd,ngaykt
from cthuephong,thuephong
where cthuephong.mahd =thuephong.mahd and makh=@makh
exec pro42 'kh01'
--4. Tạo thủ tục hiển thị thông tin của phòng trong hợp đồng khi biết ngày bắt đầu
--thuê.
create proc pro43 @ngaybd date
as
select phong.maph,loaiph,dientich,giaph
from phong,cthuephong,thuephong
where phong.maph =cthuephong.maph and thuephong.mahd =cthuephong.mahd and ngaybd =@ngaybd
exec pro43 '2024-01-01'
--4. Tạo thủ tục hiển thị thông tin của khách hàng trong hợp đồng khi biết ngày bắt
--đầu thuê phòng trong hợp đồng thuê phòng.
--4. Tạo thủ tục hiển thị thông tin của khách hàng đã thuê nhiều phòng nhất
create proc pro45
as
select tenkh,count(mahd) as tongsophong
from khachhang,thuephong
where khachhang.makh =thuephong.makh
group by tenkh
having count(mahd) >=all(select count(mahd) 
from khachhang,thuephong
where khachhang.makh =thuephong.makh
group by tenkh)

exec pro45

--4.Tạo thủ tục hiển thị thông tin tổng số phòng thuê trong hợp đồng khi biết mã hợp
--đồng thuê phòng.
create proc proc46 @mahd varchar(10)
as
select mahd,count(*) tongsophongthue
from phong,cthuephong
where phong.maph =cthuephong.maph and mahd=@mahd
group by mahd
exec proc46 'hd01'
drop proc proc46
--4. Tạo thủ tục hiển thị số lượng phòng mỗi loại trong khách sạn


--4.Tạo thủ tục hiển thị loại phòng có giá thấp nhất trong khách sạn
create proc minph as
select * from phong
where giaph =(select min(giaph) from phong)
exec minph
--4.Tạo thủ tục hiển thị thông tin của những phòng có giá cao nhất trong khách sạn
create proc maxph as
select * from phong
where giaph =(select max(giaph) from phong)
exec maxph

CREATE TRIGGER CapNhatTinhTrangPhong
ON ctthuephong
FOR insert
AS
UPDATE phong
SET tinhtrang = 'het'
WHERE maph IN (
    SELECT maph
    FROM inserted
);
drop trigger CapNhatTinhTrangPhong


--5.Tạo trigger cập nhật lại số tiền mà khách phải trả (Thành tiền) khi ngày kết thúc
--thuê có sự thay đổi biết Thành tiền= (ngày kết thúc- ngày bắt đầu)*Giá phòng
 create trigger trig5 on thuephong
	 for update
	 as
	 update ctthuephong
	 set songaythue = datediff(DAY,(select ngaybatdau 
	                              from thuephong 
	                              where thuephong.mahd =ctthuephong.mahd),(select ngayketthuc
	                              from thuephong 
	                              where thuephong.mahd =ctthuephong.mahd))
	 drop trigger trig5
--5. Tạo trigger cập nhật lại số tiền mà khách phải trả (Thành tiền) khi Giá phòng có
--sự thay đổi biết Thành tiền= (ngày kết thúc- ngày bắt đầu)*Giá phòng
 create trigger trig52 on thuephong
	 for update
	 as
	 update ctthuephong
	 set thanhtien = datediff(DAY,(select ngaybatdau 
	                              from thuephong 
	                              where thuephong.mahd =ctthuephong.mahd),(select ngayketthuc
	                              from thuephong 
	                              where thuephong.mahd =ctthuephong.mahd))
	 *(select giaph from phong where phong.maph =ctthuephong.maph)
	 
	 drop trigger trig52

--3.tao thu tuc khi biet ma hop dong thue  phong
--hien thi ma phong,loai phong,dien tich,gia phong,madh
create proc proc3 @mahopdong varchar(10)
as
select mahd,phong.maph,loaiph,gia
from phong,cthuephong
where phong.maph =cthuephong.maph and mahd =@mahopdong
exec proc3 'hd01'
exec proc3 'hd02'

--4.hien thi so luong phong co khach hang mai hoe thue
select khachhang.makh,tenkh,count(*) as tongsophong
from khachhang,thuephong,cthuephong
where khachhang.makh =thuephong.makh  and thuephong.mahd=cthuephong.mahd and tenkh ='mai hoe'
group by tenkh,khachhang.makh

--5.hien thi phong co gia phong dat nhat

select *
from phong
where giaph =(select max(giaph) from phong)
--6 tao thu tuc hien thi loai phong co gia thap nhat :,maph,loạiphong
create proc proc6
as
select *from phong
where giaph =(select min(giaph) from phong)
exec proc6
--tao trugger kiem tra ngay bat dau < ngay ket thuc
CREATE TRIGGER trig_check_ngay
ON thuephong
FOR INSERT,UPDATE
AS
    IF ((SELECT COUNT(*) 
         FROM inserted 
         WHERE ngaybatdau >= ngayketthuc) > 0)
    BEGIN
        PRINT N'Ngày bắt đầu phải nhỏ hơn ngày kết thúc.'
        ROLLBACK TRAN
    END
	--9 hien thi gia phong theo thu tu giam dan
	select loaiph,giaph
	from phong
	order by  giaph desc
	--10 loai phong la phong don trong khach san
	-- da co boa nhieu luot nguoi thue hien thi loai phong,tong so luot thue
	 select loaiph,count(*) as tongsoluotthue
	 from phong,cthuephong
	 where phong.maph =cthuephong.maph and loaiph ='don'
	 group by loaiph
	 --11 backup co so du lieu sang o dia khac
	 --backup database ten db to dick ='o dia:\đường dẫn.tên file.bak'
	 backup database qlkhachsan_hual to disk ='D:\qlkhachsan_hual.bak'
	 --12 tao trigger tinh tien nha bang cong thuc thanhtien =giaph * songay
	-- datediff(mouth,ngaybd,ngaykt)
CREATE TRIGGER trig12 ON thuephong
FOR INSERT
AS
    -- Cập nhật thành tiền cho tất cả chi tiết thuê phòng của hợp đồng vừa thêm
    UPDATE ctthuephong
    SET thanhtien = DATEDIFF(DAY, 
                            (SELECT ngaybatdau 
                             FROM thuephong 
                             WHERE thuephong.mahd = ctthuephong.mahd),
                            (SELECT ngayketthuc
                             FROM thuephong 
                             WHERE thuephong.mahd = ctthuephong.mahd))
                   * (SELECT giaph 
                      FROM phong 
                      WHERE phong.maph = ctthuephong.maph)
    WHERE ctthuephong.mahd IN (SELECT mahd FROM inserted);


	 drop trigger trig12
