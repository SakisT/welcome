using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace welcome.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelGroups",
                columns: table => new
                {
                    HotelGroupID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelGroups", x => x.HotelGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abbrevation = table.Column<string>(maxLength: 3, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 50, nullable: true),
                    GreekName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityID);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    PreferenceID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HotelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.PreferenceID);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelID = table.Column<Guid>(nullable: false),
                    Dealer = table.Column<string>(maxLength: 100, nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    HotelDate = table.Column<DateTime>(nullable: false),
                    HotelGroupID = table.Column<Guid>(nullable: false),
                    IsPayingSupport = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelID);
                    table.ForeignKey(
                        name: "FK_Hotels_HotelGroups_HotelGroupID",
                        column: x => x.HotelGroupID,
                        principalTable: "HotelGroups",
                        principalColumn: "HotelGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentID = table.Column<Guid>(nullable: false),
                    ChannelID = table.Column<string>(maxLength: 15, nullable: true),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentID);
                    table.ForeignKey(
                        name: "FK_Agents_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    BoardID = table.Column<Guid>(nullable: false),
                    Abbrevation = table.Column<string>(maxLength: 5, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.BoardID);
                    table.ForeignKey(
                        name: "FK_Boards_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<Guid>(nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_Branches_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<Guid>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 35, nullable: false),
                    TaxPrcentage = table.Column<decimal>(nullable: false),
                    VatPercentage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Departments_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelVardataInvoices",
                columns: table => new
                {
                    HotelVardataInvoiceID = table.Column<Guid>(nullable: false),
                    ArrangementDescription = table.Column<string>(maxLength: 50, nullable: true),
                    DefaultPaymentMethod = table.Column<int>(nullable: false),
                    DefaultView = table.Column<int>(nullable: false),
                    IncludePaxInInvoice = table.Column<bool>(nullable: false),
                    InvoiceCopies = table.Column<int>(nullable: false),
                    InvoiceEmailBody = table.Column<string>(nullable: true),
                    ObligatoryAddressAtInvoices = table.Column<bool>(nullable: false),
                    ResetInvoiceNumbers = table.Column<bool>(nullable: false),
                    SendInvoiceToHotelMailAsCC = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelVardataInvoices", x => x.HotelVardataInvoiceID);
                    table.ForeignKey(
                        name: "FK_HotelVardataInvoices_Hotels_HotelVardataInvoiceID",
                        column: x => x.HotelVardataInvoiceID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelVardataPlans",
                columns: table => new
                {
                    HotelVardataPlanID = table.Column<Guid>(nullable: false),
                    DisplayAgentCode = table.Column<bool>(nullable: false),
                    HistoryColor = table.Column<string>(maxLength: 12, nullable: true),
                    InHouseColor = table.Column<string>(maxLength: 12, nullable: true),
                    LabelAddins = table.Column<int>(nullable: false),
                    OptionReservationColor = table.Column<string>(maxLength: 12, nullable: true),
                    PendingDepositColor = table.Column<string>(maxLength: 12, nullable: true),
                    ReservationColor = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelVardataPlans", x => x.HotelVardataPlanID);
                    table.ForeignKey(
                        name: "FK_HotelVardataPlans_Hotels_HotelVardataPlanID",
                        column: x => x.HotelVardataPlanID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    InvoiceDetailID = table.Column<Guid>(nullable: false),
                    HotelID = table.Column<Guid>(nullable: true),
                    InvoiceStructure_Address_AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    InvoiceStructure_Address_AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    InvoiceStructure_Address_City = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceStructure_Address_Country = table.Column<string>(maxLength: 30, nullable: true),
                    InvoiceStructure_Address_PostCode = table.Column<string>(maxLength: 10, nullable: true),
                    InvoiceStructure_InvoiceName = table.Column<string>(maxLength: 120, nullable: true),
                    InvoiceStructure_Job = table.Column<string>(maxLength: 150, nullable: true),
                    InvoiceStructure_TaxDepartment = table.Column<string>(maxLength: 80, nullable: true),
                    InvoiceStructure_VATNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceDetailID);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pricelists",
                columns: table => new
                {
                    PricelistID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricelists", x => x.PricelistID);
                    table.ForeignKey(
                        name: "FK_Pricelists_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<Guid>(nullable: false),
                    AA = table.Column<int>(nullable: false),
                    AskPrePay = table.Column<decimal>(nullable: true),
                    AskPrePayDate = table.Column<DateTime>(nullable: true),
                    AskPrePayRemarks = table.Column<string>(maxLength: 100, nullable: true),
                    GuestOrGroup = table.Column<string>(maxLength: 50, nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeID = table.Column<Guid>(nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 10, nullable: false),
                    Color = table.Column<string>(maxLength: 12, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    IncludeInOnlineAvailabilities = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    SuggestedPax = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeID);
                    table.ForeignKey(
                        name: "FK_RoomTypes_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchVardatas",
                columns: table => new
                {
                    BranchVardataID = table.Column<Guid>(nullable: false),
                    AFM = table.Column<string>(maxLength: 30, nullable: true),
                    Address_AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    Address_AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    Address_City = table.Column<string>(maxLength: 50, nullable: true),
                    Address_Country = table.Column<string>(maxLength: 30, nullable: true),
                    Address_PostCode = table.Column<string>(maxLength: 10, nullable: true),
                    Color = table.Column<string>(maxLength: 12, nullable: true),
                    Contact_Fax = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Phone1 = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Phone2 = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_WebSite = table.Column<string>(maxLength: 100, nullable: true),
                    Contact_email = table.Column<string>(maxLength: 50, nullable: true),
                    DOY = table.Column<string>(maxLength: 30, nullable: true),
                    IsozygioPerYear = table.Column<bool>(nullable: false),
                    Job = table.Column<string>(maxLength: 100, nullable: true),
                    LicenseSerialNumber = table.Column<string>(maxLength: 8, nullable: true),
                    OnlyReservations = table.Column<bool>(nullable: false),
                    SMSSignature = table.Column<string>(maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchVardatas", x => x.BranchVardataID);
                    table.ForeignKey(
                        name: "FK_BranchVardatas_Branches_BranchVardataID",
                        column: x => x.BranchVardataID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchVardataReservations",
                columns: table => new
                {
                    BranchVardataReservationID = table.Column<Guid>(nullable: false),
                    AcceptPastDatesForDeposit = table.Column<bool>(nullable: false),
                    ResetResAAEveryYear = table.Column<bool>(nullable: false),
                    UseOfCreditCardPreAuthorization = table.Column<bool>(nullable: true),
                    UsualNationalityID = table.Column<int>(nullable: false),
                    UsualPricelistCode = table.Column<string>(maxLength: 20, nullable: true),
                    UsualStay = table.Column<int>(nullable: false),
                    emailOnNonShowOrCancellation = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchVardataReservations", x => x.BranchVardataReservationID);
                    table.ForeignKey(
                        name: "FK_BranchVardataReservations_Branches_BranchVardataReservationID",
                        column: x => x.BranchVardataReservationID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchVardataReservations_Nationalities_UsualNationalityID",
                        column: x => x.UsualNationalityID,
                        principalTable: "Nationalities",
                        principalColumn: "NationalityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardParts",
                columns: table => new
                {
                    BoardPartID = table.Column<Guid>(nullable: false),
                    BoardID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    ParticipationRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardParts", x => x.BoardPartID);
                    table.ForeignKey(
                        name: "FK_BoardParts_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardParts_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentsVardatas",
                columns: table => new
                {
                    AgentVardataID = table.Column<Guid>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 20, nullable: true),
                    Color = table.Column<string>(maxLength: 12, nullable: true),
                    Commission = table.Column<decimal>(nullable: false),
                    Contact_Fax = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Phone1 = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Phone2 = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_WebSite = table.Column<string>(maxLength: 100, nullable: true),
                    Contact_email = table.Column<string>(maxLength: 50, nullable: true),
                    CreateOn = table.Column<DateTime>(nullable: false),
                    InvoiceDetailID = table.Column<Guid>(nullable: true),
                    Person = table.Column<string>(maxLength: 255, nullable: true),
                    PreDefinedInvoiceRemarks = table.Column<string>(maxLength: 200, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    emailForInvoices = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsVardatas", x => x.AgentVardataID);
                    table.ForeignKey(
                        name: "FK_AgentsVardatas_Agents_AgentVardataID",
                        column: x => x.AgentVardataID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentsVardatas_InvoiceDetails_InvoiceDetailID",
                        column: x => x.InvoiceDetailID,
                        principalTable: "InvoiceDetails",
                        principalColumn: "InvoiceDetailID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(nullable: false),
                    Address_AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    Address_AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    Address_City = table.Column<string>(maxLength: 50, nullable: true),
                    Address_Country = table.Column<string>(maxLength: 30, nullable: true),
                    Address_PostCode = table.Column<string>(maxLength: 10, nullable: true),
                    Contact_Fax = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Phone1 = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_Phone2 = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_WebSite = table.Column<string>(maxLength: 100, nullable: true),
                    Contact_email = table.Column<string>(maxLength: 50, nullable: true),
                    FathersName = table.Column<string>(maxLength: 20, nullable: true),
                    Folio = table.Column<int>(nullable: false),
                    HotelID = table.Column<Guid>(nullable: false),
                    InvoiceDetailId = table.Column<Guid>(nullable: true),
                    IsBlackListed = table.Column<bool>(nullable: false),
                    Job = table.Column<string>(maxLength: 50, nullable: true),
                    NationalityID = table.Column<int>(nullable: true),
                    Passport = table.Column<string>(maxLength: 40, nullable: true),
                    Person_BirthDate = table.Column<DateTime>(nullable: true),
                    Person_FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    Person_LastName = table.Column<string>(maxLength: 30, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_InvoiceDetails_InvoiceDetailId",
                        column: x => x.InvoiceDetailId,
                        principalTable: "InvoiceDetails",
                        principalColumn: "InvoiceDetailID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Nationalities_NationalityID",
                        column: x => x.NationalityID,
                        principalTable: "Nationalities",
                        principalColumn: "NationalityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<Guid>(nullable: false),
                    ArrangementsView = table.Column<int>(nullable: false),
                    ExtrasView = table.Column<int>(nullable: false),
                    InvoiceDetailID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceDetails_InvoiceDetailID",
                        column: x => x.InvoiceDetailID,
                        principalTable: "InvoiceDetails",
                        principalColumn: "InvoiceDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationReferences",
                columns: table => new
                {
                    ReservationID = table.Column<Guid>(nullable: false),
                    PreferenceID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationReferences", x => new { x.ReservationID, x.PreferenceID });
                    table.ForeignKey(
                        name: "FK_ReservationReferences_Preferences_PreferenceID",
                        column: x => x.PreferenceID,
                        principalTable: "Preferences",
                        principalColumn: "PreferenceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationReferences_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<Guid>(nullable: false),
                    BranchID = table.Column<Guid>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Number = table.Column<string>(maxLength: 15, nullable: false),
                    RoomTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypeOnLineMappings",
                columns: table => new
                {
                    RoomTypeOnLineMappingID = table.Column<Guid>(nullable: false),
                    OnLineID = table.Column<string>(maxLength: 10, nullable: false),
                    RoomTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeOnLineMappings", x => x.RoomTypeOnLineMappingID);
                    table.ForeignKey(
                        name: "FK_RoomTypeOnLineMappings_RoomTypes_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayRooms",
                columns: table => new
                {
                    StayRoomID = table.Column<Guid>(nullable: false),
                    ActualDeparture = table.Column<DateTime>(nullable: true),
                    Adults = table.Column<int>(nullable: false),
                    AdultsCharge = table.Column<int>(nullable: false),
                    AgentCommissionPercentage = table.Column<decimal>(nullable: false),
                    AgentID = table.Column<Guid>(nullable: true),
                    Arrival = table.Column<DateTime>(nullable: false),
                    ArrivalTimeStamp = table.Column<DateTime>(nullable: true),
                    ArrivalUserId = table.Column<Guid>(nullable: false),
                    BoardID = table.Column<Guid>(nullable: false),
                    ChannelReference = table.Column<string>(maxLength: 30, nullable: true),
                    ChargeRoomTypeID = table.Column<Guid>(nullable: false),
                    Children = table.Column<int>(nullable: false),
                    Children2 = table.Column<int>(nullable: false),
                    Children2Charge = table.Column<int>(nullable: false),
                    ChildrenCharge = table.Column<int>(nullable: false),
                    Color = table.Column<string>(maxLength: 12, nullable: true),
                    Departure = table.Column<DateTime>(nullable: false),
                    DepartureTimeStamp = table.Column<DateTime>(nullable: true),
                    DepartureUserId = table.Column<Guid>(nullable: false),
                    Infants = table.Column<int>(nullable: false),
                    InfantsCharge = table.Column<int>(nullable: false),
                    IsAgentCharge = table.Column<bool>(nullable: false),
                    IsFree = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    PricelistID = table.Column<Guid>(nullable: false),
                    Reference = table.Column<string>(maxLength: 30, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ReservationID = table.Column<Guid>(nullable: false),
                    RoomID = table.Column<Guid>(nullable: true),
                    RoomPrice = table.Column<decimal>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayRooms", x => x.StayRoomID);
                    table.ForeignKey(
                        name: "FK_StayRooms_Agents_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StayRooms_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StayRooms_RoomTypes_ChargeRoomTypeID",
                        column: x => x.ChargeRoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StayRooms_Pricelists_PricelistID",
                        column: x => x.PricelistID,
                        principalTable: "Pricelists",
                        principalColumn: "PricelistID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StayRooms_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StayRooms_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    DepositID = table.Column<Guid>(nullable: false),
                    CCV = table.Column<string>(maxLength: 10, nullable: true),
                    CardHolder = table.Column<string>(maxLength: 25, nullable: true),
                    CardNumber = table.Column<string>(maxLength: 25, nullable: true),
                    CreditCardOrBankID = table.Column<Guid>(nullable: true),
                    DepositTimeStamp = table.Column<DateTime>(nullable: false),
                    Euro = table.Column<decimal>(nullable: false),
                    Expiration_Month = table.Column<int>(nullable: false),
                    Expiration_Year = table.Column<int>(nullable: false),
                    HotelDate = table.Column<DateTime>(nullable: true),
                    IsPreAuthorization = table.Column<bool>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ReservationID = table.Column<Guid>(nullable: true),
                    StayRoomID = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.DepositID);
                    table.ForeignKey(
                        name: "FK_Deposits_Agents_CreditCardOrBankID",
                        column: x => x.CreditCardOrBankID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Deposits_StayRooms_StayRoomID",
                        column: x => x.StayRoomID,
                        principalTable: "StayRooms",
                        principalColumn: "StayRoomID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "StayPersons",
                columns: table => new
                {
                    StayPersonID = table.Column<Guid>(nullable: false),
                    ActualDeparture = table.Column<DateTime>(nullable: true),
                    Arrival = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<Guid>(nullable: false),
                    IsFree = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PaxSymbol = table.Column<int>(nullable: false),
                    StayRoomID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayPersons", x => x.StayPersonID);
                    table.ForeignKey(
                        name: "FK_StayPersons_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StayPersons_StayRooms_StayRoomID",
                        column: x => x.StayRoomID,
                        principalTable: "StayRooms",
                        principalColumn: "StayRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaryingStays",
                columns: table => new
                {
                    VaryingStayID = table.Column<Guid>(nullable: false),
                    BoardID = table.Column<Guid>(nullable: false),
                    ChargeRoomTypeID = table.Column<Guid>(nullable: false),
                    HotelDate = table.Column<DateTime>(nullable: false),
                    IsAgentCharge = table.Column<bool>(nullable: false),
                    PricelistID = table.Column<Guid>(nullable: false),
                    RoomPrice = table.Column<decimal>(nullable: true),
                    StayRoomID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaryingStays", x => x.VaryingStayID);
                    table.ForeignKey(
                        name: "FK_VaryingStays_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaryingStays_RoomTypes_ChargeRoomTypeID",
                        column: x => x.ChargeRoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaryingStays_Pricelists_PricelistID",
                        column: x => x.PricelistID,
                        principalTable: "Pricelists",
                        principalColumn: "PricelistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaryingStays_StayRooms_StayRoomID",
                        column: x => x.StayRoomID,
                        principalTable: "StayRooms",
                        principalColumn: "StayRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillID = table.Column<Guid>(nullable: false),
                    AgentCommission = table.Column<double>(nullable: true),
                    AgentID = table.Column<Guid>(nullable: true),
                    BillTimeStamp = table.Column<DateTime>(nullable: false),
                    BoardID = table.Column<Guid>(nullable: true),
                    BranchID = table.Column<Guid>(nullable: false),
                    CreditCardOrBankID = table.Column<Guid>(nullable: true),
                    DepositID = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    Euro = table.Column<decimal>(nullable: false),
                    HotelDate = table.Column<DateTime>(nullable: false),
                    InvoiceID = table.Column<Guid>(nullable: true),
                    Parastatiko = table.Column<string>(maxLength: 50, nullable: true),
                    PreInvoiceHotelDate = table.Column<DateTime>(nullable: false),
                    PricelistID = table.Column<Guid>(nullable: true),
                    RoomID = table.Column<Guid>(nullable: true),
                    RoomTypeID = table.Column<Guid>(nullable: true),
                    StayPersonID = table.Column<Guid>(nullable: true),
                    StayRoomID = table.Column<Guid>(nullable: true),
                    TaxPercentage = table.Column<double>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    VatPercentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_Bills_Agents_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Agents_CreditCardOrBankID",
                        column: x => x.CreditCardOrBankID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Deposits_DepositID",
                        column: x => x.DepositID,
                        principalTable: "Deposits",
                        principalColumn: "DepositID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Bills_Pricelists_PricelistID",
                        column: x => x.PricelistID,
                        principalTable: "Pricelists",
                        principalColumn: "PricelistID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_RoomTypes_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_StayPersons_StayPersonID",
                        column: x => x.StayPersonID,
                        principalTable: "StayPersons",
                        principalColumn: "StayPersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_StayRooms_StayRoomID",
                        column: x => x.StayRoomID,
                        principalTable: "StayRooms",
                        principalColumn: "StayRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplements",
                columns: table => new
                {
                    SupplementID = table.Column<Guid>(nullable: false),
                    BoardID = table.Column<Guid>(nullable: true),
                    DepartmentID = table.Column<Guid>(nullable: true),
                    Euro = table.Column<decimal>(nullable: false),
                    InvoiceDescription = table.Column<string>(maxLength: 40, nullable: true),
                    IsAgentCharge = table.Column<bool>(nullable: false),
                    VaryingStayID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplements", x => x.SupplementID);
                    table.ForeignKey(
                        name: "FK_Supplements_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplements_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplements_VaryingStays_VaryingStayID",
                        column: x => x.VaryingStayID,
                        principalTable: "VaryingStays",
                        principalColumn: "VaryingStayID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    BillDetailID = table.Column<Guid>(nullable: false),
                    BillID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    Percentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.BillDetailID);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillID",
                        column: x => x.BillID,
                        principalTable: "Bills",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_HotelID",
                table: "Agents",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_AgentsVardatas_AgentVardataID",
                table: "AgentsVardatas",
                column: "AgentVardataID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentsVardatas_InvoiceDetailID",
                table: "AgentsVardatas",
                column: "InvoiceDetailID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AgentID",
                table: "Bills",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BoardID",
                table: "Bills",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BranchID",
                table: "Bills",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CreditCardOrBankID",
                table: "Bills",
                column: "CreditCardOrBankID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_DepositID",
                table: "Bills",
                column: "DepositID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_InvoiceID",
                table: "Bills",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PricelistID",
                table: "Bills",
                column: "PricelistID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_RoomID",
                table: "Bills",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_RoomTypeID",
                table: "Bills",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StayPersonID",
                table: "Bills",
                column: "StayPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StayRoomID",
                table: "Bills",
                column: "StayRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillID",
                table: "BillDetails",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_DepartmentID",
                table: "BillDetails",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_HotelID",
                table: "Boards",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_BoardParts_BoardID",
                table: "BoardParts",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_BoardParts_DepartmentID",
                table: "BoardParts",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_HotelID",
                table: "Branches",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchVardatas_BranchVardataID",
                table: "BranchVardatas",
                column: "BranchVardataID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchVardataReservations_BranchVardataReservationID",
                table: "BranchVardataReservations",
                column: "BranchVardataReservationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchVardataReservations_UsualNationalityID",
                table: "BranchVardataReservations",
                column: "UsualNationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_HotelID",
                table: "Customers",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_InvoiceDetailId",
                table: "Customers",
                column: "InvoiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_NationalityID",
                table: "Customers",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HotelID",
                table: "Departments",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_CreditCardOrBankID",
                table: "Deposits",
                column: "CreditCardOrBankID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_ReservationID",
                table: "Deposits",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_StayRoomID",
                table: "Deposits",
                column: "StayRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelGroupID",
                table: "Hotels",
                column: "HotelGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelVardataInvoices_HotelVardataInvoiceID",
                table: "HotelVardataInvoices",
                column: "HotelVardataInvoiceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelVardataPlans_HotelVardataPlanID",
                table: "HotelVardataPlans",
                column: "HotelVardataPlanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceDetailID",
                table: "Invoices",
                column: "InvoiceDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_HotelID",
                table: "InvoiceDetails",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_HotelID",
                table: "Pricelists",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HotelID",
                table: "Reservations",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationReferences_PreferenceID",
                table: "ReservationReferences",
                column: "PreferenceID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationReferences_ReservationID",
                table: "ReservationReferences",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BranchID",
                table: "Rooms",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_HotelID",
                table: "RoomTypes",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeOnLineMappings_RoomTypeID",
                table: "RoomTypeOnLineMappings",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StayPersons_CustomerID",
                table: "StayPersons",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_StayPersons_StayRoomID",
                table: "StayPersons",
                column: "StayRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_StayRooms_AgentID",
                table: "StayRooms",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_StayRooms_BoardID",
                table: "StayRooms",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_StayRooms_ChargeRoomTypeID",
                table: "StayRooms",
                column: "ChargeRoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StayRooms_PricelistID",
                table: "StayRooms",
                column: "PricelistID");

            migrationBuilder.CreateIndex(
                name: "IX_StayRooms_ReservationID",
                table: "StayRooms",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_StayRooms_RoomID",
                table: "StayRooms",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplements_BoardID",
                table: "Supplements",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplements_DepartmentID",
                table: "Supplements",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplements_VaryingStayID",
                table: "Supplements",
                column: "VaryingStayID");

            migrationBuilder.CreateIndex(
                name: "IX_VaryingStays_BoardID",
                table: "VaryingStays",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_VaryingStays_ChargeRoomTypeID",
                table: "VaryingStays",
                column: "ChargeRoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VaryingStays_PricelistID",
                table: "VaryingStays",
                column: "PricelistID");

            migrationBuilder.CreateIndex(
                name: "IX_VaryingStays_StayRoomID",
                table: "VaryingStays",
                column: "StayRoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentsVardatas");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "BoardParts");

            migrationBuilder.DropTable(
                name: "BranchVardatas");

            migrationBuilder.DropTable(
                name: "BranchVardataReservations");

            migrationBuilder.DropTable(
                name: "HotelVardataInvoices");

            migrationBuilder.DropTable(
                name: "HotelVardataPlans");

            migrationBuilder.DropTable(
                name: "ReservationReferences");

            migrationBuilder.DropTable(
                name: "RoomTypeOnLineMappings");

            migrationBuilder.DropTable(
                name: "Supplements");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "VaryingStays");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "StayPersons");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "StayRooms");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Pricelists");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "HotelGroups");
        }
    }
}
