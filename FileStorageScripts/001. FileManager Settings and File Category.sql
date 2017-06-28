
CREATE TABLE [dbo].[tblAppSettings](
	[PropertyName] [nvarchar](50) NULL,
	[PropertyValue] [nvarchar](500) NULL,
UNIQUE NONCLUSTERED 
(
	[PropertyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Create function generateID(@Prefix nvarchar(50),@sl int, @Len int)
returns nvarchar(50)
as
begin
	Declare @SLen as int
	declare @GID as nvarchar(50)

	set @SLen = @Len - len(@sl)
	set @GID=''

	while @SLen>0
	begin
		set @GID=@GID+'0'
		set @SLen=@SLen-1
	end
	
	set @GID = @Prefix + @GID + convert(nvarchar,@sl)
	
	return @GID
end


GO

Insert Into tblAppSettings(PropertyName,PropertyValue)
Values('CurrentFileCategoryID',0)

-- Select * from tblAppSettings

GO

create table tblFileCategory(
FileCategoryID nvarchar(50) primary key,
FileCategory nvarchar(200) unique(FileCategory),
IsActive bit default 1,
EntryBy nvarchar(50),
EntryDate datetime default getdate()
)

go

if object_id('spInsertFileCategory') is not null
	drop proc spInsertFileCategory

GO

create proc spInsertFileCategory
@FileCategory nvarchar(200),
@IsActive bit,
@EntryBy nvarchar(50)
as
begin

	Declare @FileCategoryID nvarchar(50)=''
	Declare @CurrentFileCategoryID numeric(18,0)=0
	Declare @FileCategoryIDPrefix as nvarchar(3)=''

	set @FileCategoryIDPrefix='FC-'

begin tran

	select @CurrentFileCategoryID = cast(PropertyValue as numeric(18,0)) from tblAppSettings where  PropertyName='CurrentFileCategoryID'
	
	set @CurrentFileCategoryID=isnull(@CurrentFileCategoryID,0)+1
	Select @FileCategoryID=dbo.generateID(@FileCategoryIDPrefix,@CurrentFileCategoryID,8)		
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	Insert Into tblFileCategory(FileCategoryID,FileCategory,IsActive,EntryBy)
	Values(@FileCategoryID,@FileCategory,@IsActive,@EntryBy)
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	update tblAppSettings set PropertyValue=@CurrentFileCategoryID where PropertyName='CurrentFileCategoryID'
	IF (@@ERROR <> 0) GOTO ERR_HANDLER
	

COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

if object_id('spListFileCategories') is not null
	drop proc spListFileCategories

GO

Create proc spListFileCategories
as
begin
	Select FileCategoryID,FileCategory,
	Case IsActive When 1 Then 'YES' Else 'NO' End as 'IsActive',
	EntryBy, Convert(nvarchar,EntryDate,106) as 'EntryDate'
	from tblFileCategory
end

-- exec spListFileCategories

GO


if object_id('spListFileCategoriesForEmp') is not null
	drop proc spListFileCategoriesForEmp

GO

Create proc spListFileCategoriesForEmp
@EmployeeID nvarchar(50)
as
begin

	Declare @DepartmentID as nvarchar(50) = ''
	
	Select @DepartmentID=DepartmentID from tblEmployeeInfo Where EmployeeID = @EmployeeID

	Select FileCategoryID,FileCategory,
	Case IsActive When 1 Then 'YES' Else 'NO' End as 'IsActive',
	EntryBy, Convert(nvarchar,EntryDate,106) as 'EntryDate'
	from tblFileCategory
	Where FileCategoryID In (Select FileCategoryID from tblFileStoragePermission Where DepartmentID = @DepartmentID And CanUpload=1)
end

-- exec spListFileCategoriesForEmp 'EMP-00000098'

GO

if object_id('spUpdateFileCategory') is not null
	drop proc spUpdateFileCategory

GO

Create proc spUpdateFileCategory
@FileCategoryID nvarchar(50),
@FileCategory nvarchar(200),
@IsActive bit,
@EntryBy nvarchar(50)
as
begin
	Update tblFileCategory Set FileCategory=@FileCategory,IsActive=@IsActive,EntryBy=@EntryBy
	Where FileCategoryID = @FileCategoryID
end
