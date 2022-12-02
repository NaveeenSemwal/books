using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Core.Migrations
{
    public partial class PhotoKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("493c3228-3444-4a49-9cc0-e8532edc59b2"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("24810dfc-2d94-4cc7-aab5-cdf98b83f0c9"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2902b665-1190-4c70-9915-b9c2d7680450"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "Identity",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Identity",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("24810dfc-2d94-4cc7-aab5-cdf98b83f0c9"), "James", "Elroy" },
                    { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Douglas", "Adams" },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "George", "RR Martin" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Stephen", "Fry" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "In The Hitchhiker's Guide to the Galaxy, the characters visit the legendary planet Magrathea, home to the now-collapsed planet-building industry, and meet Slartibartfast, a planetary coastline designer who was responsible for the fjords of Norway. Through archival recordings, he relates the story of a race of hyper-intelligent pan-dimensional beings who built a computer named Deep Thought to calculate the Answer to the Ultimate Question of Life, the Universe, and Everything.", "The Hitchhiker's Guide to the Galaxy" },
                    { new Guid("493c3228-3444-4a49-9cc0-e8532edc59b2"), new Guid("24810dfc-2d94-4cc7-aab5-cdf98b83f0c9"), "American Tabloid is a 1995 novel by James Ellroy that chronicles the events surrounding three rogue American law enforcement officers from November 22, 1958 through November 22, 1963. Each becomes entangled in a web of interconnecting associations between the FBI, the CIA, and the mafia, which eventually leads to their collective involvement in the John F. Kennedy assassination.", "American Tabloid" },
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "The book that seems impossible to write.", "The Winds of Winter" },
                    { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "The Greek myths are amongst the best stories ever told, passed down through millennia and inspiring writers and artists as varied as Shakespeare, Michelangelo, James Joyce and Walt Disney.  They are embedded deeply in the traditions, tales and cultural DNA of the West.You'll fall in love with Zeus, marvel at the birth of Athena, wince at Cronus and Gaia's revenge on Ouranos, weep with King Midas and hunt with the beautiful and ferocious Artemis. Spellbinding, informative and moving, Stephen Fry's Mythos perfectly captures these stories for the modern age - in all their rich and deeply human relevance.", "Mythos" },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "A Game of Thrones is the first novel in A Song of Ice and Fire, a series of fantasy novels by American author George R. R. ... In the novel, recounting events from various points of view, Martin introduces the plot-lines of the noble houses of Westeros, the Wall, and the Targaryens.", "A Game of Thrones" }
                });
        }
    }
}
