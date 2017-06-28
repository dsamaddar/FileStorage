
if object_id('tblFileIssued') is not null
	drop table tblFileIssued

GO

Create table tblFileIssued(
FileIssuedID nvarchar(50) primary key,
FileStorageID nvarchar(50) foreign key references tblFileStorage(FileStorageID),
IssueDate datetime,
IsReturned bit default 0,
ReturnedOn datetime,
EntryBy nvarchar(50),
EntryDate datetime default getdate()
)

GO

