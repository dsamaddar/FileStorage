

Insert Into tblAppSettings(PropertyName,PropertyValue)
Values('CurrentCabinetLocationID',0)

GO

If object_id('tblCabinetLocation') is not null
	drop table tblCabinetLocation

GO

Create table tblCabinetLocation(
CabinetLocationID nvarchar(50) primary key,
BranchID nvarchar(50) foreign key references tblULCBranch(ULCBranchID),
BranchCode nvarchar(50),
FloorNo nvarchar(50),
CabinetNo nvarchar(50),
ShelfNo nvarchar(50),
FolderNo nvarchar(50),
CabinetLocation as 'Branch: '+ISNULL(BranchCode,'-')+' Floor: '+ISNULL(FloorNo,'-')+' Cabinet: '+ISNULL(CabinetNo,'-')+' Shelf: '+ISNULL(ShelfNo,'-')+' Folder: '+ISNULL(FolderNo,'-') Unique(CabinetLocation),
IsActive bit default 1,
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

-- Branch_Floor_CabinetNo
-- Branch:Uttara-Floor:5-Cabinet:303

GO

if object_id('spInsertCabinetLocation') is not null
	drop proc spInsertCabinetLocation

GO

create proc spInsertCabinetLocation
@BranchID nvarchar(50),
@BranchCode nvarchar(50),
@FloorNo nvarchar(50),
@CabinetNo nvarchar(50),
@ShelfNo nvarchar(50),
@FolderNo nvarchar(50),
@EntryBy nvarchar(50)
as
begin
	Declare @CabinetLocationID nvarchar(50)=''
	Declare @CurrentCabinetLocationID numeric(18,0)=0
	Declare @CabinetLocationIDPrefix as nvarchar(3)=''

	set @CabinetLocationIDPrefix='CL-'
begin tran

	select @CurrentCabinetLocationID = cast(PropertyValue as numeric(18,0)) from tblAppSettings where  PropertyName='CurrentCabinetLocationID'
	
	set @CurrentCabinetLocationID=isnull(@CurrentCabinetLocationID,0)+1
	Select @CabinetLocationID=dbo.generateID(@CabinetLocationIDPrefix,@CurrentCabinetLocationID,8)		
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	Insert Into tblCabinetLocation(CabinetLocationID,BranchID,BranchCode,FloorNo,CabinetNo,ShelfNo,FolderNo,EntryBy)
	Values(@CabinetLocationID,@BranchID,@BranchCode,@FloorNo,@CabinetNo,@ShelfNo,@FolderNo,@EntryBy)
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	update tblAppSettings set PropertyValue=@CurrentCabinetLocationID where PropertyName='CurrentCabinetLocationID'
	IF (@@ERROR <> 0) GOTO ERR_HANDLER
	
COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

if object_id('spListCabinetLocations') is not null
	drop proc spListCabinetLocations

GO

Create proc spListCabinetLocations
as
begin
	Select CabinetLocationID,BranchID,dbo.fnGetBranchNameByID(BranchID) as 'Branch',BranchCode,FloorNo,CabinetNo,ShelfNo,
	FolderNo,CabinetLocation,
	Case IsActive When 1 Then 'YES' Else 'NO' End as 'IsActive',EntryBy,
	Convert(nvarchar,EntryDate,106) as 'EntryDate'
	from tblCabinetLocation order by CabinetLocation
end

GO

If object_id('spUpdateCabinetLocation') is not null
	drop proc spUpdateCabinetLocation

GO

Create proc spUpdateCabinetLocation
@CabinetLocationID nvarchar(50),
@BranchID nvarchar(50),
@BranchCode nvarchar(50),
@FloorNo nvarchar(50),
@CabinetNo nvarchar(50),
@ShelfNo nvarchar(50),
@FolderNo nvarchar(50),
@IsActive bit,
@EntryBy nvarchar(50)
as
begin
	
	Update tblCabinetLocation Set BranchID=@BranchID,BranchCode=@BranchCode,
	FloorNo=@FloorNo,CabinetNo=@CabinetNo,ShelfNo=@ShelfNo,FolderNo=@FolderNo,
	IsActive=@IsActive
	Where CabinetLocationID = @CabinetLocationID

end
