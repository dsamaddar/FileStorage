
Insert Into tblAppSettings(PropertyName,PropertyValue)
Values('CurrentAgreementID',0)

GO

Create table tblAgreements(
AgreementID nvarchar(50) primary key,
AgreementNo nvarchar(50) unique(AgreementNo),
ClientID nvarchar(50),
ClientName nvarchar(50),
EntryBy nvarchar(50),
EntryDate datetime default getdate()
);

GO

If object_id('spInsertAgreements') is not null
	drop proc spInsertAgreements

GO

Create proc spInsertAgreements
@AgreementNo nvarchar(50),
@ClientID nvarchar(50),
@ClientName nvarchar(50),
@EntryBy nvarchar(50)
as
begin
	Declare @AgreementID nvarchar(50)=''
	Declare @CurrentAgreementID numeric(18,0)=0
	Declare @AgreementIDPrefix as nvarchar(4)=''

	set @AgreementIDPrefix='AGR-'
begin tran

	select @CurrentAgreementID = cast(PropertyValue as numeric(18,0)) from tblAppSettings where  PropertyName='CurrentCabinetLocationID'
	
	set @CurrentAgreementID=isnull(@CurrentAgreementID,0)+1
	Select @AgreementID=dbo.generateID(@AgreementIDPrefix,@CurrentAgreementID,8)		
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	Insert Into tblAgreements(AgreementID,AgreementNo,ClientID,ClientName,EntryBy)
	Values(@AgreementID,@AgreementNo,@ClientID,@ClientName,@EntryBy)
	IF (@@ERROR <> 0) GOTO ERR_HANDLER

	update tblAppSettings set PropertyValue=@CurrentAgreementID where PropertyName='CurrentCabinetLocationID'
	IF (@@ERROR <> 0) GOTO ERR_HANDLER
	
COMMIT TRAN
RETURN 0

ERR_HANDLER:
ROLLBACK TRAN
RETURN 1
end

GO

if object_id('spSearchAgreements') is not null
	drop proc spSearchAgreements

GO

Create proc spSearchAgreements
@SearchText nvarchar(50)
as
begin
	Select AgreementID,AgreementNo,ClientID,ClientName,EntryBy,
	Convert(nvarchar,EntryDate,106) as 'EntryDate'
	from tblAgreements
	Where ClientName like '%'+@SearchText+'%'
	Or ClientID like '%'+@SearchText+'%'
	Or AgreementNo like  '%'+@SearchText+'%'
	Order By AgreementNo,ClientName 
End

-- exec spSearchAgreements 'deba'

GO

if object_id('spUpdateAgreements') is not null
	drop proc spUpdateAgreements

GO

Create proc spUpdateAgreements
@AgreementID nvarchar(50),
@AgreementNo nvarchar(50),
@ClientID nvarchar(50),
@ClientName nvarchar(50),
@EntryBy nvarchar(50)
as
begin
	Update tblAgreements Set AgreementNo=@AgreementNo,ClientID=@ClientID,ClientName=@ClientName,
	EntryBy=@EntryBy
	Where AgreementID=@AgreementID
end 