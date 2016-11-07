using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using welcome.Data;

namespace welcome.Migrations
{
    [DbContext(typeof(WelcomeContext))]
    [Migration("20161107150137_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("welcome.Models.Agent", b =>
                {
                    b.Property<Guid>("AgentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChannelID")
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<int>("DisplayOrder");

                    b.Property<Guid>("HotelID");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("Type");

                    b.HasKey("AgentID");

                    b.HasIndex("HotelID");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("welcome.Models.AgentVardata", b =>
                {
                    b.Property<Guid>("AgentVardataID");

                    b.Property<string>("AccountCode")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Color")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<decimal>("Commission");

                    b.Property<string>("Contact_Fax")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Mobile")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Phone1")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Phone2")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_WebSite")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Contact_email")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreateOn");

                    b.Property<Guid?>("InvoiceDetailID");

                    b.Property<string>("Person")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("PreDefinedInvoiceRemarks")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Remarks");

                    b.Property<string>("emailForInvoices")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("AgentVardataID");

                    b.HasIndex("AgentVardataID")
                        .IsUnique();

                    b.HasIndex("InvoiceDetailID")
                        .IsUnique();

                    b.ToTable("AgentsVardatas");
                });

            modelBuilder.Entity("welcome.Models.Bill", b =>
                {
                    b.Property<Guid>("BillID")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("AgentCommission");

                    b.Property<Guid?>("AgentID");

                    b.Property<DateTime>("BillTimeStamp");

                    b.Property<Guid?>("BoardID");

                    b.Property<Guid>("BranchID");

                    b.Property<Guid?>("CreditCardOrBankID");

                    b.Property<Guid?>("DepositID");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<decimal>("Euro");

                    b.Property<DateTime>("HotelDate");

                    b.Property<Guid?>("InvoiceID");

                    b.Property<string>("Parastatiko")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("PreInvoiceHotelDate");

                    b.Property<Guid?>("PricelistID");

                    b.Property<Guid?>("RoomID");

                    b.Property<Guid?>("RoomTypeID");

                    b.Property<Guid?>("StayPersonID");

                    b.Property<Guid?>("StayRoomID");

                    b.Property<double>("TaxPercentage");

                    b.Property<int>("Type");

                    b.Property<Guid?>("UserId");

                    b.Property<double>("VatPercentage");

                    b.HasKey("BillID");

                    b.HasIndex("AgentID");

                    b.HasIndex("BoardID");

                    b.HasIndex("BranchID");

                    b.HasIndex("CreditCardOrBankID");

                    b.HasIndex("DepositID");

                    b.HasIndex("InvoiceID");

                    b.HasIndex("PricelistID");

                    b.HasIndex("RoomID");

                    b.HasIndex("RoomTypeID");

                    b.HasIndex("StayPersonID");

                    b.HasIndex("StayRoomID");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("welcome.Models.BillDetail", b =>
                {
                    b.Property<Guid>("BillDetailID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BillID");

                    b.Property<Guid>("DepartmentID");

                    b.Property<double>("Percentage");

                    b.HasKey("BillDetailID");

                    b.HasIndex("BillID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("welcome.Models.Board", b =>
                {
                    b.Property<Guid>("BoardID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbrevation")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 5);

                    b.Property<int>("DisplayOrder");

                    b.Property<Guid>("HotelID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("BoardID");

                    b.HasIndex("HotelID");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("welcome.Models.BoardPart", b =>
                {
                    b.Property<Guid>("BoardPartID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BoardID");

                    b.Property<Guid>("DepartmentID");

                    b.Property<double>("ParticipationRate");

                    b.HasKey("BoardPartID");

                    b.HasIndex("BoardID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("BoardParts");
                });

            modelBuilder.Entity("welcome.Models.Branch", b =>
                {
                    b.Property<Guid>("BranchID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("HotelID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 80);

                    b.HasKey("BranchID");

                    b.HasIndex("HotelID");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("welcome.Models.BranchVardata", b =>
                {
                    b.Property<Guid>("BranchVardataID");

                    b.Property<string>("AFM")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Address_AddressLine1")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Address_AddressLine2")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Address_City")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Address_Country")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Address_PostCode")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Color")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<string>("Contact_Fax")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Mobile")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Phone1")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Phone2")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_WebSite")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Contact_email")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("DOY")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<bool>("IsozygioPerYear");

                    b.Property<string>("Job")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("LicenseSerialNumber")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<bool>("OnlyReservations");

                    b.Property<string>("SMSSignature")
                        .HasAnnotation("MaxLength", 11);

                    b.HasKey("BranchVardataID");

                    b.HasIndex("BranchVardataID")
                        .IsUnique();

                    b.ToTable("BranchVardatas");
                });

            modelBuilder.Entity("welcome.Models.BranchVardataReservation", b =>
                {
                    b.Property<Guid>("BranchVardataReservationID");

                    b.Property<bool>("AcceptPastDatesForDeposit");

                    b.Property<bool>("ResetResAAEveryYear");

                    b.Property<bool?>("UseOfCreditCardPreAuthorization");

                    b.Property<int>("UsualNationalityID");

                    b.Property<string>("UsualPricelistCode")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("UsualStay");

                    b.Property<string>("emailOnNonShowOrCancellation")
                        .HasAnnotation("MaxLength", 40);

                    b.HasKey("BranchVardataReservationID");

                    b.HasIndex("BranchVardataReservationID")
                        .IsUnique();

                    b.HasIndex("UsualNationalityID");

                    b.ToTable("BranchVardataReservations");
                });

            modelBuilder.Entity("welcome.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address_AddressLine1")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Address_AddressLine2")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Address_City")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Address_Country")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Address_PostCode")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Contact_Fax")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Mobile")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Phone1")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_Phone2")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Contact_WebSite")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Contact_email")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FathersName")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("Folio");

                    b.Property<Guid>("HotelID");

                    b.Property<Guid?>("InvoiceDetailId");

                    b.Property<bool>("IsBlackListed");

                    b.Property<string>("Job")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("NationalityID");

                    b.Property<string>("Passport")
                        .HasAnnotation("MaxLength", 40);

                    b.Property<DateTime?>("Person_BirthDate");

                    b.Property<string>("Person_FirstName")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Person_LastName")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Remarks");

                    b.Property<int>("Sex");

                    b.HasKey("CustomerID");

                    b.HasIndex("HotelID");

                    b.HasIndex("InvoiceDetailId");

                    b.HasIndex("NationalityID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("welcome.Models.Department", b =>
                {
                    b.Property<Guid>("DepartmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DisplayOrder");

                    b.Property<Guid>("HotelID");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 35);

                    b.Property<decimal>("TaxPrcentage");

                    b.Property<decimal>("VatPercentage");

                    b.HasKey("DepartmentID");

                    b.HasIndex("HotelID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("welcome.Models.Deposit", b =>
                {
                    b.Property<Guid>("DepositID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CCV")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("CardHolder")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("CardNumber")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<Guid?>("CreditCardOrBankID");

                    b.Property<DateTime>("DepositTimeStamp");

                    b.Property<decimal>("Euro");

                    b.Property<int>("Expiration_Month");

                    b.Property<int>("Expiration_Year");

                    b.Property<DateTime?>("HotelDate");

                    b.Property<bool?>("IsPreAuthorization");

                    b.Property<string>("Remarks");

                    b.Property<Guid?>("ReservationID");

                    b.Property<Guid?>("StayRoomID");

                    b.Property<Guid>("UserId");

                    b.HasKey("DepositID");

                    b.HasIndex("CreditCardOrBankID");

                    b.HasIndex("ReservationID");

                    b.HasIndex("StayRoomID");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("welcome.Models.Hotel", b =>
                {
                    b.Property<Guid>("HotelID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Dealer")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<DateTime>("HotelDate");

                    b.Property<Guid>("HotelGroupID");

                    b.Property<bool>("IsPayingSupport");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 80);

                    b.HasKey("HotelID");

                    b.HasIndex("HotelGroupID");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("welcome.Models.HotelGroup", b =>
                {
                    b.Property<Guid>("HotelGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 80);

                    b.HasKey("HotelGroupID");

                    b.ToTable("HotelGroups");
                });

            modelBuilder.Entity("welcome.Models.HotelVardataInvoice", b =>
                {
                    b.Property<Guid>("HotelVardataInvoiceID");

                    b.Property<string>("ArrangementDescription")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("DefaultPaymentMethod");

                    b.Property<int>("DefaultView");

                    b.Property<bool>("IncludePaxInInvoice");

                    b.Property<int>("InvoiceCopies");

                    b.Property<string>("InvoiceEmailBody");

                    b.Property<bool>("ObligatoryAddressAtInvoices");

                    b.Property<bool>("ResetInvoiceNumbers");

                    b.Property<string>("SendInvoiceToHotelMailAsCC")
                        .HasAnnotation("MaxLength", 40);

                    b.HasKey("HotelVardataInvoiceID");

                    b.HasIndex("HotelVardataInvoiceID")
                        .IsUnique();

                    b.ToTable("HotelVardataInvoices");
                });

            modelBuilder.Entity("welcome.Models.HotelVardataPlan", b =>
                {
                    b.Property<Guid>("HotelVardataPlanID");

                    b.Property<bool>("DisplayAgentCode");

                    b.Property<string>("HistoryColor")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<string>("InHouseColor")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<int>("LabelAddins");

                    b.Property<string>("OptionReservationColor")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<string>("PendingDepositColor")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<string>("ReservationColor")
                        .HasAnnotation("MaxLength", 12);

                    b.HasKey("HotelVardataPlanID");

                    b.HasIndex("HotelVardataPlanID")
                        .IsUnique();

                    b.ToTable("HotelVardataPlans");
                });

            modelBuilder.Entity("welcome.Models.Invoice", b =>
                {
                    b.Property<Guid>("InvoiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArrangementsView");

                    b.Property<int>("ExtrasView");

                    b.Property<Guid>("InvoiceDetailID");

                    b.HasKey("InvoiceID");

                    b.HasIndex("InvoiceDetailID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("welcome.Models.InvoiceDetail", b =>
                {
                    b.Property<Guid>("InvoiceDetailID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("HotelID");

                    b.Property<string>("InvoiceStructure_Address_AddressLine1")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("InvoiceStructure_Address_AddressLine2")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("InvoiceStructure_Address_City")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("InvoiceStructure_Address_Country")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("InvoiceStructure_Address_PostCode")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("InvoiceStructure_InvoiceName")
                        .HasAnnotation("MaxLength", 120);

                    b.Property<string>("InvoiceStructure_Job")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("InvoiceStructure_TaxDepartment")
                        .HasAnnotation("MaxLength", 80);

                    b.Property<string>("InvoiceStructure_VATNumber")
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("InvoiceDetailID");

                    b.HasIndex("HotelID");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("welcome.Models.Nationality", b =>
                {
                    b.Property<int>("NationalityID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbrevation")
                        .HasAnnotation("MaxLength", 3);

                    b.Property<string>("EnglishName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("GreekName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("NationalityID");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("welcome.Models.Preference", b =>
                {
                    b.Property<Guid>("PreferenceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid>("HotelId");

                    b.HasKey("PreferenceID");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("welcome.Models.Pricelist", b =>
                {
                    b.Property<Guid>("PricelistID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<Guid>("HotelID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("PricelistID");

                    b.HasIndex("HotelID");

                    b.ToTable("Pricelists");
                });

            modelBuilder.Entity("welcome.Models.Reservation", b =>
                {
                    b.Property<Guid>("ReservationID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AA");

                    b.Property<decimal?>("AskPrePay");

                    b.Property<DateTime?>("AskPrePayDate");

                    b.Property<string>("AskPrePayRemarks")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("GuestOrGroup")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("HotelID");

                    b.Property<string>("Remarks");

                    b.HasKey("ReservationID");

                    b.HasIndex("HotelID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("welcome.Models.ReservationReference", b =>
                {
                    b.Property<Guid>("ReservationID");

                    b.Property<Guid>("PreferenceID");

                    b.HasKey("ReservationID", "PreferenceID");

                    b.HasIndex("PreferenceID");

                    b.HasIndex("ReservationID");

                    b.ToTable("ReservationReferences");
                });

            modelBuilder.Entity("welcome.Models.Room", b =>
                {
                    b.Property<Guid>("RoomID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BranchID");

                    b.Property<int>("DisplayOrder");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<Guid>("RoomTypeID");

                    b.HasKey("RoomID");

                    b.HasIndex("BranchID");

                    b.HasIndex("RoomTypeID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("welcome.Models.RoomType", b =>
                {
                    b.Property<Guid>("RoomTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Color")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<int>("DisplayOrder");

                    b.Property<int>("Grade");

                    b.Property<Guid>("HotelID");

                    b.Property<bool>("IncludeInOnlineAvailabilities");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("SuggestedPax");

                    b.HasKey("RoomTypeID");

                    b.HasIndex("HotelID");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("welcome.Models.RoomTypeOnLineMapping", b =>
                {
                    b.Property<Guid>("RoomTypeOnLineMappingID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OnLineID")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<Guid>("RoomTypeID");

                    b.HasKey("RoomTypeOnLineMappingID");

                    b.HasIndex("RoomTypeID");

                    b.ToTable("RoomTypeOnLineMappings");
                });

            modelBuilder.Entity("welcome.Models.StayPerson", b =>
                {
                    b.Property<Guid>("StayPersonID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActualDeparture");

                    b.Property<DateTime>("Arrival");

                    b.Property<Guid>("CustomerID");

                    b.Property<bool>("IsFree");

                    b.Property<int>("Order");

                    b.Property<int>("PaxSymbol");

                    b.Property<Guid>("StayRoomID");

                    b.HasKey("StayPersonID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("StayRoomID");

                    b.ToTable("StayPersons");
                });

            modelBuilder.Entity("welcome.Models.StayRoom", b =>
                {
                    b.Property<Guid>("StayRoomID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActualDeparture");

                    b.Property<int>("Adults");

                    b.Property<int>("AdultsCharge");

                    b.Property<decimal>("AgentCommissionPercentage");

                    b.Property<Guid?>("AgentID");

                    b.Property<DateTime>("Arrival");

                    b.Property<DateTime?>("ArrivalTimeStamp");

                    b.Property<Guid>("ArrivalUserId");

                    b.Property<Guid>("BoardID");

                    b.Property<string>("ChannelReference")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<Guid>("ChargeRoomTypeID");

                    b.Property<int>("Children");

                    b.Property<int>("Children2");

                    b.Property<int>("Children2Charge");

                    b.Property<int>("ChildrenCharge");

                    b.Property<string>("Color")
                        .HasAnnotation("MaxLength", 12);

                    b.Property<DateTime>("Departure");

                    b.Property<DateTime?>("DepartureTimeStamp");

                    b.Property<Guid>("DepartureUserId");

                    b.Property<int>("Infants");

                    b.Property<int>("InfantsCharge");

                    b.Property<bool>("IsAgentCharge");

                    b.Property<bool>("IsFree");

                    b.Property<bool>("IsLocked");

                    b.Property<Guid>("PricelistID");

                    b.Property<string>("Reference")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Remarks");

                    b.Property<Guid>("ReservationID");

                    b.Property<Guid?>("RoomID");

                    b.Property<decimal?>("RoomPrice");

                    b.Property<int>("Status");

                    b.HasKey("StayRoomID");

                    b.HasIndex("AgentID");

                    b.HasIndex("BoardID");

                    b.HasIndex("ChargeRoomTypeID");

                    b.HasIndex("PricelistID");

                    b.HasIndex("ReservationID");

                    b.HasIndex("RoomID");

                    b.ToTable("StayRooms");
                });

            modelBuilder.Entity("welcome.Models.Supplement", b =>
                {
                    b.Property<Guid>("SupplementID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BoardID");

                    b.Property<Guid?>("DepartmentID");

                    b.Property<decimal>("Euro");

                    b.Property<string>("InvoiceDescription")
                        .HasAnnotation("MaxLength", 40);

                    b.Property<bool>("IsAgentCharge");

                    b.Property<Guid>("VaryingStayID");

                    b.HasKey("SupplementID");

                    b.HasIndex("BoardID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("VaryingStayID");

                    b.ToTable("Supplements");
                });

            modelBuilder.Entity("welcome.Models.VaryingStay", b =>
                {
                    b.Property<Guid>("VaryingStayID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BoardID");

                    b.Property<Guid>("ChargeRoomTypeID");

                    b.Property<DateTime>("HotelDate");

                    b.Property<bool>("IsAgentCharge");

                    b.Property<Guid>("PricelistID");

                    b.Property<decimal?>("RoomPrice");

                    b.Property<Guid>("StayRoomID");

                    b.HasKey("VaryingStayID");

                    b.HasIndex("BoardID");

                    b.HasIndex("ChargeRoomTypeID");

                    b.HasIndex("PricelistID");

                    b.HasIndex("StayRoomID");

                    b.ToTable("VaryingStays");
                });

            modelBuilder.Entity("welcome.Models.Agent", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("Agents")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.AgentVardata", b =>
                {
                    b.HasOne("welcome.Models.Agent", "Agent")
                        .WithOne("Vardata")
                        .HasForeignKey("welcome.Models.AgentVardata", "AgentVardataID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.InvoiceDetail", "InvoiceDetail")
                        .WithOne()
                        .HasForeignKey("welcome.Models.AgentVardata", "InvoiceDetailID")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("welcome.Models.Bill", b =>
                {
                    b.HasOne("welcome.Models.Agent", "Agent")
                        .WithMany("AgentBills")
                        .HasForeignKey("AgentID");

                    b.HasOne("welcome.Models.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardID");

                    b.HasOne("welcome.Models.Branch", "Branch")
                        .WithMany("Bills")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.Agent", "CreditCardOrBank")
                        .WithMany("CardOrBankBills")
                        .HasForeignKey("CreditCardOrBankID");

                    b.HasOne("welcome.Models.Deposit", "Deposit")
                        .WithMany("Bills")
                        .HasForeignKey("DepositID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.Invoice", "Invoice")
                        .WithMany("BillRecords")
                        .HasForeignKey("InvoiceID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("welcome.Models.Pricelist", "Pricelist")
                        .WithMany()
                        .HasForeignKey("PricelistID");

                    b.HasOne("welcome.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomID");

                    b.HasOne("welcome.Models.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeID");

                    b.HasOne("welcome.Models.StayPerson", "StayPerson")
                        .WithMany("Bills")
                        .HasForeignKey("StayPersonID");

                    b.HasOne("welcome.Models.StayRoom", "StayRoom")
                        .WithMany("Bills")
                        .HasForeignKey("StayRoomID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.BillDetail", b =>
                {
                    b.HasOne("welcome.Models.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.Department", "Department")
                        .WithMany("BillDetails")
                        .HasForeignKey("DepartmentID");
                });

            modelBuilder.Entity("welcome.Models.Board", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("Boards")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.BoardPart", b =>
                {
                    b.HasOne("welcome.Models.Board", "Board")
                        .WithMany("BoardParts")
                        .HasForeignKey("BoardID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.Department", "Department")
                        .WithMany("BoardParts")
                        .HasForeignKey("DepartmentID");
                });

            modelBuilder.Entity("welcome.Models.Branch", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("Branches")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.BranchVardata", b =>
                {
                    b.HasOne("welcome.Models.Branch", "Branch")
                        .WithOne("Vardata")
                        .HasForeignKey("welcome.Models.BranchVardata", "BranchVardataID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.BranchVardataReservation", b =>
                {
                    b.HasOne("welcome.Models.Branch", "Branch")
                        .WithOne("VardataReservations")
                        .HasForeignKey("welcome.Models.BranchVardataReservation", "BranchVardataReservationID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.Nationality", "UsualNationality")
                        .WithMany()
                        .HasForeignKey("UsualNationalityID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.Customer", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("Customers")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.InvoiceDetail", "InvoiceDetail")
                        .WithMany()
                        .HasForeignKey("InvoiceDetailId");

                    b.HasOne("welcome.Models.Nationality", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityID");
                });

            modelBuilder.Entity("welcome.Models.Department", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("Departments")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.Deposit", b =>
                {
                    b.HasOne("welcome.Models.Agent", "CreditCardOrBank")
                        .WithMany()
                        .HasForeignKey("CreditCardOrBankID");

                    b.HasOne("welcome.Models.Reservation", "Reservation")
                        .WithMany("Deposits")
                        .HasForeignKey("ReservationID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("welcome.Models.StayRoom", "StayRoom")
                        .WithMany("Deposits")
                        .HasForeignKey("StayRoomID")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("welcome.Models.Hotel", b =>
                {
                    b.HasOne("welcome.Models.HotelGroup", "HotelGroup")
                        .WithMany("Hotels")
                        .HasForeignKey("HotelGroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.HotelVardataInvoice", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithOne("VardataInvoice")
                        .HasForeignKey("welcome.Models.HotelVardataInvoice", "HotelVardataInvoiceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.HotelVardataPlan", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithOne("VardataPlan")
                        .HasForeignKey("welcome.Models.HotelVardataPlan", "HotelVardataPlanID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.Invoice", b =>
                {
                    b.HasOne("welcome.Models.InvoiceDetail", "InvoiceDetail")
                        .WithMany("Invoices")
                        .HasForeignKey("InvoiceDetailID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.InvoiceDetail", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("HotelID");
                });

            modelBuilder.Entity("welcome.Models.Pricelist", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("Pricelists")
                        .HasForeignKey("HotelID");
                });

            modelBuilder.Entity("welcome.Models.Reservation", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("Reservations")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.ReservationReference", b =>
                {
                    b.HasOne("welcome.Models.Preference", "Preference")
                        .WithMany("ReservationReferences")
                        .HasForeignKey("PreferenceID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.Reservation", "Reservation")
                        .WithMany("ReservationReferences")
                        .HasForeignKey("ReservationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.Room", b =>
                {
                    b.HasOne("welcome.Models.Branch", "Branch")
                        .WithMany("Rooms")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeID");
                });

            modelBuilder.Entity("welcome.Models.RoomType", b =>
                {
                    b.HasOne("welcome.Models.Hotel", "Hotel")
                        .WithMany("RoomTypes")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.RoomTypeOnLineMapping", b =>
                {
                    b.HasOne("welcome.Models.RoomType", "RoomType")
                        .WithMany("RoomTypeOnLineMappings")
                        .HasForeignKey("RoomTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.StayPerson", b =>
                {
                    b.HasOne("welcome.Models.Customer", "Customer")
                        .WithMany("StaysAsPerson")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.StayRoom", "StayRoom")
                        .WithMany("StayPersons")
                        .HasForeignKey("StayRoomID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.StayRoom", b =>
                {
                    b.HasOne("welcome.Models.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID");

                    b.HasOne("welcome.Models.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardID");

                    b.HasOne("welcome.Models.RoomType", "ChargeRoomType")
                        .WithMany()
                        .HasForeignKey("ChargeRoomTypeID");

                    b.HasOne("welcome.Models.Pricelist", "Pricelist")
                        .WithMany()
                        .HasForeignKey("PricelistID");

                    b.HasOne("welcome.Models.Reservation", "Reservation")
                        .WithMany("StayRooms")
                        .HasForeignKey("ReservationID");

                    b.HasOne("welcome.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomID");
                });

            modelBuilder.Entity("welcome.Models.Supplement", b =>
                {
                    b.HasOne("welcome.Models.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardID");

                    b.HasOne("welcome.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID");

                    b.HasOne("welcome.Models.VaryingStay", "VaryingStay")
                        .WithMany("Supplements")
                        .HasForeignKey("VaryingStayID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("welcome.Models.VaryingStay", b =>
                {
                    b.HasOne("welcome.Models.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.RoomType", "ChargeRoomType")
                        .WithMany()
                        .HasForeignKey("ChargeRoomTypeID");

                    b.HasOne("welcome.Models.Pricelist", "PriceList")
                        .WithMany()
                        .HasForeignKey("PricelistID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("welcome.Models.StayRoom", "StayRoom")
                        .WithMany("VaryingStays")
                        .HasForeignKey("StayRoomID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
