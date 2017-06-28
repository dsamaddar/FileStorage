Insert into tblAppSettings(PropertyName,PropertyValue)
Values('CurrentFileStoragePermID',0)

GO

if object_id('tblFileStoragePermission') is not null
	drop table tblFileStoragePermission

GO

Create table tblFileStoragePermission(
FileStoragePermID nvarchar(50) primary key,
DepartmentID nvarchar(50) foreign key references tblDepartment(DepartmentID),
FileCategoryID nvarchar(50) foreign key references tblFileCategory(FileCategoryID),
CanUpload bit default 0,
CanView bit default 0,
IsActive bit default 1,
EntryBy nvarchar(50),
EntryDate datetime default getdate()
)

GO

if object_id('spInsertFileStoragePermission') is not null
	drop proc spInsertFileStoragePermission

GO

Create proc spInsertFileStoragePermission
@DepartmentID nvarchar(50),
@FileCategoryID nvarchar(50),
@CanUpload bit,
@CanView bit,
@EntryBy nvarchar(50)
as
begin
	Declare @FileStoragePermID nvarchar(50)=''
	Declare @CurrentFileStoragePermID numeric(18,0)=0
	Declare @FileStoragePermIDPrefix as nvarchar(4)=''

	set @FileStoragePermIDPrefix='FSP-'
begin tran

	If Exists(
	Select * from tblFileStoragePermission Where DepartmentID=@DepartmentID And FileCategoryID=@FileCategoryID And IsActive=1
	)
	begin
		GOTO ERR_HANDLER
	end

	select @CurrentFileStoragePermID = cast(PropertyValue as numeric(18,0)) from tblAppSettings where  PropertyName='CurrentFileStoragePermID'
	
	set @CurrentFileStoragePermID=isnull(@CurrentFileStoragePermID,0)+1
	Select @FileStoragePermID=dbo.generateID(@FileStoragePermIDPrefix,@CurrentFileStoragePermID,8)		
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	Insert Into tblFileStoragePermission(FileStoragePermID,DepartmentID,FileCategoryID,CanUpload,CanView,EntryBy)
	Values(@FileStoragePermID,@DepartmentID,@FileCategoryID,@CanUpload,@CanView,@EntryBy)

	update tblAppSettings set PropertyValue=@CurrentFileStoragePermID where PropertyName='CurrentFileStoragePermID'
	IF (@@ERROR <> 0) GOTO ERR_HANDLER
	
COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

If object_id('spRemoveFileStoragePermission') is not null
	drop proc spRemoveFileStoragePermission

GO

Create proc spRemoveFileStoragePermission
@FileStoragePermID nvarchar(50)
as
begin
	Update tblFileStoragePermission Set IsActive=0 Where FileStoragePermID = @FileStoragePermID
end

GO

if object_id('spListPermStorageFileCatByDept') is not null
	drop proc spListPermStorageFileCatByDept

GO

Create proc spListPermStorageFileCatByDept
@DepartmentID nvarchar(50)
as
begin
	Select FileStoragePermID,dbo.fnGetFileCategoryNameByID(FileCategoryID) as 'FileCategory'
	from tblFileStoragePermission Where DepartmentID = @DepartmentID And IsActive=1
end

GO

if object_id('spGetCanUploadListByDept') is not null
	drop proc spGetCanUploadListByDept

GO

Create proc spGetCanUploadListByDept
@DepartmentID nvarchar(50)
as
begin
	Declare @CanUploadList as nvarchar(4000) = ''

	Select @CanUploadList= @CanUploadList + FileCategoryID +',' from tblFileStoragePermission 
	Where DepartmentID = @DepartmentID And IsActive=1 And CanUpload = 1

	Select ISNULL(@CanUploadList,'') as 'CanUploadList'
end

GO

if object_id('spGetCanViewListByDept') is not null
	drop proc spGetCanViewListByDept

GO

Create proc spGetCanViewListByDept
@DepartmentID nvarchar(50)
as
begin
	Declare @CanViewList as nvarchar(4000) = ''

	Select @CanViewList= @CanViewList + FileCategoryID +',' from tblFileStoragePermission 
	Where DepartmentID = @DepartmentID And IsActive=1 And CanView = 1

	Select ISNULL(@CanViewList,'') as 'CanViewList'
end

GO

if object_id('spBulkInsertCanUploadFileStoPerm') is not null
	drop proc spBulkInsertCanUploadFileStoPerm

GO
--exec spBulkInsertCanUploadFileStoPerm 'DEPT-00000009','FC-00000011','dsamaddar'
GO
Create proc spBulkInsertCanUploadFileStoPerm
@DepartmentID nvarchar(50),
@CanUploadList nvarchar(4000),
@EntryBy nvarchar(50)
as
begin

	Declare @FileCategoryID as nvarchar(50) = ''

	Declare @CanUploadTbl table(
	FileCategoryID nvarchar(50),
	Taken bit default 0
	);
	
begin tran

	Insert Into @CanUploadTbl(FileCategoryID)
	Select Value from dbo.Split('~',@CanUploadList)
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	Update tblFileStoragePermission Set CanUpload=0 
	Where DepartmentID = @DepartmentID And FileCategoryID NOT IN (Select FileCategoryID from @CanUploadTbl)

	Declare @Count as int = 1
	Declare @NCount as int = 0

	Select @NCount = Count(*) from @CanUploadTbl

	While @Count <= @NCount
	begin
		Select top 1 @FileCategoryID=FileCategoryID from @CanUploadTbl Where Taken = 0

		If exists (Select* from tblFileStoragePermission Where DepartmentID = @DepartmentID And FileCategoryID=@FileCategoryID)
		begin
			Update tblFileStoragePermission Set CanUpload = 1 Where DepartmentID=@DepartmentID And FileCategoryID=@FileCategoryID
			IF (@@ERROR <> 0) GOTO ERR_HANDLER
		end
		else
		begin
			exec spInsertFileStoragePermission @DepartmentID,@FileCategoryID,1,0,@EntryBy
			IF (@@ERROR <> 0) GOTO ERR_HANDLER
		end

		Set @Count = @Count + 1
		Update @CanUploadTbl Set Taken = 1 Where FileCategoryID = @FileCategoryID
	end
	
	IF (@@ERROR <> 0) GOTO ERR_HANDLER
	
COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

if object_id ('spBulkInsertCanViewFileStoPerm') is not null
	drop proc spBulkInsertCanViewFileStoPerm

GO

Create proc spBulkInsertCanViewFileStoPerm
@DepartmentID nvarchar(50),
@CanViewList nvarchar(4000),
@EntryBy nvarchar(50)
as
begin

	Declare @FileCategoryID as nvarchar(50) = ''

	Declare @CanViewTbl table(
	FileCategoryID nvarchar(50),
	Taken bit default 0
	);
	
begin tran

	Insert Into @CanViewTbl(FileCategoryID)
	Select Value from dbo.Split('~',@CanViewList)

	Update tblFileStoragePermission Set CanView=0 
	Where DepartmentID = @DepartmentID And FileCategoryID NOT IN (Select FileCategoryID from @CanViewTbl)

	Declare @Count as int = 1
	Declare @NCount as int = 0

	Select @NCount = Count(*) from @CanViewTbl

	While @Count <= @NCount
	begin
		Select top 1 @FileCategoryID=FileCategoryID from @CanViewTbl Where Taken = 0

		If exists (Select* from tblFileStoragePermission Where DepartmentID = @DepartmentID And FileCategoryID=@FileCategoryID)
		begin
			Update tblFileStoragePermission Set CanView = 1 Where DepartmentID=@DepartmentID And FileCategoryID=@FileCategoryID
			IF (@@ERROR <> 0) GOTO ERR_HANDLER
		end
		else
		begin
			exec spInsertFileStoragePermission @DepartmentID,@FileCategoryID,0,1,@EntryBy
			IF (@@ERROR <> 0) GOTO ERR_HANDLER
		end

		Set @Count = @Count + 1
		Update @CanViewTbl Set Taken = 1 Where FileCategoryID = @FileCategoryID
		Set @FileCategoryID = ''
	end
	
	IF (@@ERROR <> 0) GOTO ERR_HANDLER
	
COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end
