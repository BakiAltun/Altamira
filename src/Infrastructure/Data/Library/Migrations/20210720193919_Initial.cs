using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vimo.Infrastructure.Data.Library.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zipcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoLat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoLng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_UserAddresses_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatchPhrase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanies", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_UserCompanies_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "InsertedAt", "Name", "Password", "Phone", "Salt", "UpdatedAt", "Username", "WebSite" },
                values: new object[,]
                {
                    { 1, "Sincere@april.biz", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 128, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, 0, 0, 0, 0)), "Leanne Graham", "956S2U6O", "1-770-736-8031 x56442", null, null, "Bret", "hildegard.org" },
                    { 2, "Shanna@melissa.tv", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(8560), new TimeSpan(0, 0, 0, 0, 0)), "Ervin Howell", "UO22DKZO", "010-692-6593 x09125", null, null, "Antonette", "anastasia.net" },
                    { 3, "Nathan@yesenia.net", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(8900), new TimeSpan(0, 0, 0, 0, 0)), "Clementine Bauch", "1EWLM2NN", "1-463-123-4447", null, null, "Samantha", "ramiro.info" },
                    { 4, "Julianne.OConner@kory.org", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(8950), new TimeSpan(0, 0, 0, 0, 0)), "Patricia Lebsack", "VIGCDZQQ", "493-170-9623 x156", null, null, "Karianne", "kale.biz" },
                    { 5, "Lucio_Hettinger@annie.ca", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(8980), new TimeSpan(0, 0, 0, 0, 0)), "Chelsey Dietrich", "6DJ3FY3O", "(254)954-1289", null, null, "Kamren", "demarco.info" },
                    { 6, "Karley_Dach@jasper.info", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(9030), new TimeSpan(0, 0, 0, 0, 0)), "Mrs. Dennis Schulist", "B7VCVKE4", "1-477-935-8478 x6430", null, null, "Leopoldo_Corkery", "ola.org" },
                    { 7, "Telly.Hoeger@billy.biz", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(9060), new TimeSpan(0, 0, 0, 0, 0)), "Kurtis Weissnat", "D7ZX5FSU", "210.067.6132", null, null, "Elwyn.Skiles", "elvis.io" },
                    { 8, "Sherwood@rosamond.me", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(9100), new TimeSpan(0, 0, 0, 0, 0)), "Nicholas Runolfsdottir V", "SEYPDD4A", "586.493.6943 x140", null, null, "Maxime_Nienow", "jacynthe.com" },
                    { 9, "Chaim_McDermott@dana.io", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(9270), new TimeSpan(0, 0, 0, 0, 0)), "Glenna Reichert", "PRN4Q89Z", "(775)976-6794 x41206", null, null, "Delphine", "conrad.com" },
                    { 10, "Rey.Padberg@karina.biz", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 131, DateTimeKind.Unspecified).AddTicks(9310), new TimeSpan(0, 0, 0, 0, 0)), "Clementina DuBuque", "QMT5LFUQ", "024-648-3804", null, null, "Moriah.Stanton", "ambrose.net" }
                });

            migrationBuilder.InsertData(
                table: "UserAddresses",
                columns: new[] { "Id", "UserId", "City", "GeoLat", "GeoLng", "InsertedAt", "Name", "Street", "Suite", "UpdatedAt", "Zipcode" },
                values: new object[,]
                {
                    { 1, 1, "Gwenborough", "-37.3159", "81.1496", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(8430), new TimeSpan(0, 0, 0, 0, 0)), null, "Kulas Light", "Apt. 556", null, "92998-3874" },
                    { 2, 2, "Wisokyburgh", "-43.9509", "-34.4618", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9900), new TimeSpan(0, 0, 0, 0, 0)), null, "Victor Plains", "Suite 879", null, "90566-7771" },
                    { 9, 9, "Bartholomebury", "24.6463", "-168.8889", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9940), new TimeSpan(0, 0, 0, 0, 0)), null, "Dayna Park", "Suite 449", null, "76495-3109" },
                    { 3, 3, "McKenziehaven", "-68.6102", "-47.0653", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9910), new TimeSpan(0, 0, 0, 0, 0)), null, "Douglas Extension", "Suite 847", null, "59590-4157" },
                    { 4, 4, "South Elvis", "29.4572", "-164.2990", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9920), new TimeSpan(0, 0, 0, 0, 0)), null, "Hoeger Mall", "Apt. 692", null, "53919-4257" },
                    { 8, 8, "Aliyaview", "-14.3990", "-120.7677", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9940), new TimeSpan(0, 0, 0, 0, 0)), null, "Ellsworth Summit", "Suite 729", null, "45169" },
                    { 5, 5, "Roscoeview", "-31.8129", "62.5342", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9920), new TimeSpan(0, 0, 0, 0, 0)), null, "Skiles Walks", "Suite 351", null, "33263" },
                    { 10, 10, "Lebsackbury", "-38.2386", "57.2232", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9950), new TimeSpan(0, 0, 0, 0, 0)), null, "Kattie Turnpike", "Suite 198", null, "31428-2261" },
                    { 6, 6, "South Christy", "-71.4197", "71.7478", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9930), new TimeSpan(0, 0, 0, 0, 0)), null, "Norberto Crossing", "Apt. 950", null, "23505-1337" },
                    { 7, 7, "Howemouth", "24.8918", "21.8984", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 214, DateTimeKind.Unspecified).AddTicks(9940), new TimeSpan(0, 0, 0, 0, 0)), null, "Rex Trail", "Suite 280", null, "58804-1099" }
                });

            migrationBuilder.InsertData(
                table: "UserCompanies",
                columns: new[] { "Id", "UserId", "Bs", "CatchPhrase", "InsertedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 19, 9, "aggregate real-time technologies", "Switchable contextually-based project", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5110), new TimeSpan(0, 0, 0, 0, 0)), "Yost and Sons", null },
                    { 18, 8, "e-enable extensible e-tailers", "Implemented secondary concept", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5110), new TimeSpan(0, 0, 0, 0, 0)), "Abernathy Group", null },
                    { 17, 7, "generate enterprise e-tailers", "Configurable multimedia task-force", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5100), new TimeSpan(0, 0, 0, 0, 0)), "Johns Group", null },
                    { 15, 5, "revolutionize end-to-end systems", "User-centric fault-tolerant solution", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5090), new TimeSpan(0, 0, 0, 0, 0)), "Keebler LLC", null },
                    { 14, 4, "transition cutting-edge web services", "Multi-tiered zero tolerance productivity", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5090), new TimeSpan(0, 0, 0, 0, 0)), "Robel-Corkery", null },
                    { 13, 3, "e-enable strategic applications", "Face to face bifurcated interface", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5080), new TimeSpan(0, 0, 0, 0, 0)), "Romaguera-Jacobson", null },
                    { 12, 2, "synergize scalable supply-chains", "Proactive didactic contingency", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5080), new TimeSpan(0, 0, 0, 0, 0)), "Deckow-Crist", null },
                    { 11, 1, "harness real-time e-markets", "Multi-layered client-server neural-net", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(3950), new TimeSpan(0, 0, 0, 0, 0)), "Romaguera-Crona", null },
                    { 16, 6, "e-enable innovative applications", "Synchronised bottom-line interface", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5100), new TimeSpan(0, 0, 0, 0, 0)), "Considine-Lockman", null },
                    { 20, 10, "target end-to-end models", "Centralized empowering task-force", new DateTimeOffset(new DateTime(2021, 7, 20, 19, 39, 19, 216, DateTimeKind.Unspecified).AddTicks(5110), new TimeSpan(0, 0, 0, 0, 0)), "Hoeger LLC", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "UserCompanies");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
