
Insert Into tblAppSettings(PropertyName,PropertyValue)
Values('CurrentFileStorageID',0)

GO

if object_id('tblFileStorage') is not null
	drop table tblFileStorage

GO

Create table tblFileStorage(
FileStorageID nvarchar(50) primary key,
AgreementID nvarchar(50) foreign key references tblAgreements(AgreementID),
ReferenceNo nvarchar(50),
FileName nvarchar(200),
Attachment nvarchar(50),
FileCategoryID nvarchar(50) foreign key references tblFileCategory(FileCategoryID),
CustodianID nvarchar(50) foreign key references tblDepartment(DepartmentID),
ResponsibleID nvarchar(50) foreign key references tblEmployeeInfo(EmployeeID),
CabinetLocationID nvarchar(50) foreign key references tblCabinetLocation(CabinetLocationID),
Remarks nvarchar(50),
EffectiveDate datetime,
IsIssued bit default 0,
LastIssuedBy nvarchar(50),
LastIssuedOn datetime,
IsActive bit default 1,
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

GO

if object_id('tblFileStorageLog') is not null
	drop table tblFileStorageLog

GO

Create table tblFileStorageLog(
FileStorageID nvarchar(50) primary key,
AgreementID nvarchar(50) foreign key references tblAgreements(AgreementID),
ReferenceNo nvarchar(50),
FileName nvarchar(200),
Attachment nvarchar(50),
FileCategoryID nvarchar(50) foreign key references tblFileCategory(FileCategoryID),
CustodianID nvarchar(50) foreign key references tblDepartment(DepartmentID),
ResponsibleID nvarchar(50) foreign key references tblEmployeeInfo(EmployeeID),
CabinetLocationID nvarchar(50) foreign key references tblCabinetLocation(CabinetLocationID),
Remarks nvarchar(50),
EffectiveDate datetime,
IsIssued bit default 0,
LastIssuedBy nvarchar(50),
LastIssuedOn datetime,
IsActive bit default 1,
Activity nvarchar(50),
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);


GO

if object_id('trgFileStorage') is not null
	drop trigger trgFileStorage

GO

Create trigger trgFileStorage ON tblFileStorage
After Insert,Delete,Update
as
begin
	Declare @Activity as nvarchar(50)=''

	if exists(SELECT * from inserted) and exists (SELECT * from deleted)
	begin
		SET @Activity = 'UPDATE';
		
		Insert Into tblFileStorageLog(FileStorageID,AgreementID,ReferenceNo,FileName,Attachment,FileCategoryID,
		CustodianID,ResponsibleID,CabinetLocationID,Remarks,IsIssued,LastIssuedBy,LastIssuedOn,IsActive,Activity,EntryBy,EntryDate)
		select FileStorageID,AgreementID,ReferenceNo,FileName,Attachment,FileCategoryID,
		CustodianID,ResponsibleID,CabinetLocationID,Remarks,IsIssued,LastIssuedBy,LastIssuedOn,IsActive,@Activity,EntryBy,EntryDate
		from inserted
	end

	If exists (Select * from inserted) and not exists(Select * from deleted)
	begin
		SET @activity = 'INSERT';
		
		Insert Into tblFileStorageLog(FileStorageID,AgreementID,ReferenceNo,FileName,Attachment,FileCategoryID,
		CustodianID,ResponsibleID,CabinetLocationID,Remarks,IsIssued,LastIssuedBy,LastIssuedOn,IsActive,Activity,EntryBy,EntryDate)
		select FileStorageID,AgreementID,ReferenceNo,FileName,Attachment,FileCategoryID,
		CustodianID,ResponsibleID,CabinetLocationID,Remarks,IsIssued,LastIssuedBy,LastIssuedOn,IsActive,@Activity,EntryBy,EntryDate
		from inserted
	end

	If exists(select * from deleted) and not exists(Select * from inserted)
	begin 
		SET @activity = 'DELETE';

		Insert Into tblFileStorageLog(FileStorageID,AgreementID,ReferenceNo,FileName,Attachment,FileCategoryID,
		CustodianID,ResponsibleID,CabinetLocationID,Remarks,IsIssued,LastIssuedBy,LastIssuedOn,IsActive,Activity,EntryBy,EntryDate)
		select FileStorageID,AgreementID,ReferenceNo,FileName,Attachment,FileCategoryID,
		CustodianID,ResponsibleID,CabinetLocationID,Remarks,IsIssued,LastIssuedBy,LastIssuedOn,IsActive,@Activity,EntryBy,EntryDate
		from deleted
	end

end

GO

if object_id('spInsertFileStorage') is not null
	drop proc spInsertFileStorage

GO

Create proc spInsertFileStorage
@AgreementID nvarchar(50),
@ReferenceNo nvarchar(50),
@FileName nvarchar(200),
@Attachment nvarchar(50),
@FileCategoryID nvarchar(50),
@CustodianID nvarchar(50),
@ResponsibleID nvarchar(50),
@CabinetLocationID nvarchar(50),
@Remarks nvarchar(50),
@EffectiveDate datetime,
@IsActive bit,
@EntryBy nvarchar(50)
as
begin
	Declare @FileStorageID nvarchar(50)=''
	Declare @CurrentFileStorageID numeric(18,0)=0
	Declare @FileStorageIDPrefix as nvarchar(3)=''

	set @FileStorageIDPrefix='FS-'
begin tran

	select @CurrentFileStorageID = cast(PropertyValue as numeric(18,0)) from tblAppSettings where  PropertyName='CurrentFileStorageID'
	
	set @CurrentFileStorageID=isnull(@CurrentFileStorageID,0)+1
	Select @FileStorageID=dbo.generateID(@FileStorageIDPrefix,@CurrentFileStorageID,8)		
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	Insert Into tblFileStorage(FileStorageID,AgreementID,ReferenceNo,FileName,Attachment,FileCategoryID,
	CustodianID,ResponsibleID,CabinetLocationID,Remarks,EffectiveDate,IsActive,EntryBy)
	Values(@FileStorageID,@AgreementID,@ReferenceNo,@FileName,@Attachment,@FileCategoryID,
	@CustodianID,@ResponsibleID,@CabinetLocationID,@Remarks,@EffectiveDate,@IsActive,@EntryBy)
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	update tblAppSettings set PropertyValue=@CurrentFileStorageID where PropertyName='CurrentFileStorageID'
	IF (@@ERROR <> 0) GOTO ERR_HANDLER
	
COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

if object_id('vwFileStorage') is not null
	drop view vwFileStorage

GO

Create view vwFileStorage
as
Select FS.FileStorageID,FS.AgreementID,A.AgreementNo,A.ClientName,A.ClientID,FS.ReferenceNo,
FS.FileName,FS.Attachment,FS.FileCategoryID,FC.FileCategory,FS.CustodianID,D.DeptName as 'Custodian',
FS.ResponsibleID,E.EmployeeName as 'Responsible',FS.CabinetLocationID,CL.CabinetLocation,
FS.Remarks,FS.EffectiveDate,
Case FS.IsIssued when 1 then 'YES' else 'NO' end as 'IsIssued',
FS.LastIssuedBy,FS.LastIssuedOn,FS.EntryBy,
Convert(nvarchar,FS.EntryDate,106) as 'EntryDate'
from 
tblFileStorage FS 
INNER JOIN tblAgreements A ON FS.AgreementID=A.AgreementID
INNER JOIN tblFileCategory FC ON FC.FileCategoryID = FS.FileCategoryID
INNER JOIN tblDepartment D ON FS.CustodianID = D.DepartmentID
INNER JOIN tblEmployeeInfo E ON FS.ResponsibleID = E.EmployeeID
INNER JOIN tblCabinetLocation CL ON FS.CabinetLocationID = CL.CabinetLocationID

-- select * from vwFileStorage


GO

if object_id('spGetUploadedDocsByAgrID') is not null
	drop proc spGetUploadedDocsByAgrID

GO

Create proc spGetUploadedDocsByAgrID
@AgreementID nvarchar(50)
as
begin
	Select FileStorageID,AgreementNo,ClientName,ClientID,ReferenceNo,
	FileName,Attachment,
	FileCategory,Custodian,Responsible,CabinetLocation,Remarks,EffectiveDate,IsIssued,
	LastIssuedBy,LastIssuedOn,EntryBy,EntryDate
	from vwFileStorage
	Where AgreementID=@AgreementID
end

GO

if object_id('spSearchStorageFiles') is not null
	drop proc spSearchStorageFiles

GO

Create proc spSearchStorageFiles
@SearchText nvarchar(50)
as
begin
	Select FileStorageID,AgreementNo,ClientName,ClientID,ReferenceNo,
	FileName,Attachment,
	FileCategory,Custodian,Responsible,CabinetLocation,Remarks,EffectiveDate,IsIssued,
	LastIssuedBy,LastIssuedOn,EntryBy,EntryDate
	from vwFileStorage
	Where AgreementNo like '%'+@SearchText+'%'
	Or ReferenceNo like '%'+@SearchText+'%'
	Or FileName like '%'+@SearchText+'%'
	Or ClientName like '%'+@SearchText+'%'
	Or ClientID like '%'+@SearchText+'%'
	Order By AgreementNo,ReferenceNo,FileName,ClientName,ClientID
end

GO


if object_id('spSearchStorageFilesForEmp') is not null
	drop proc spSearchStorageFilesForEmp

GO

Create proc spSearchStorageFilesForEmp
@EmployeeID nvarchar(50),
@SearchText nvarchar(50)
as
begin

	Declare @DepartmentID as nvarchar(50) = ''
	Select @DepartmentID=DepartmentID from tblEmployeeInfo Where EmployeeID = @EmployeeID

	Select FileStorageID,AgreementNo,ClientName,ClientID,ReferenceNo,
	FileName,Attachment,
	FileCategory,Custodian,Responsible,CabinetLocation,Remarks,EffectiveDate,IsIssued,
	LastIssuedBy,LastIssuedOn,EntryBy,EntryDate
	from vwFileStorage
	Where (AgreementNo like '%'+@SearchText+'%'
	Or ReferenceNo like '%'+@SearchText+'%'
	Or FileName like '%'+@SearchText+'%'
	Or ClientName like '%'+@SearchText+'%'
	Or ClientID like '%'+@SearchText+'%'
	)
	And FileCategoryID In (Select FileCategoryID from tblFileStoragePermission Where DepartmentID = @DepartmentID And CanView=1)
	Order By AgreementNo,ReferenceNo,FileName,ClientName,ClientID
end

-- exec spSearchStorageFilesForEmp 'EMP-00000098','rani'

GO
