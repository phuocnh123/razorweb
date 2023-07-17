using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using MyWebApp;

#nullable disable

namespace CS56.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.Id);
                });


            // InsertDatat
            // Fake Data : Bogus

            Randomizer.Seed = new Random(8675309);
            
            var fakerArticle = new Faker<Article>();
            fakerArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5,5));
            fakerArticle.RuleFor(a => a.Created, f => f.Date.Between(new DateTime(2021, 1, 1), new DateTime(2021, 7, 30)));
            fakerArticle.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1,4));

            for (int i=1; i< 150; i++) {
                Article article = fakerArticle.Generate();
                migrationBuilder.InsertData(
                table : "article",
                columns: new[] {"Title", "Created", "Content"},
                values: new object[] {
                    article.Title,
                    article.Created,
                    article.Content
                });
            }

            // migrationBuilder.InsertData(
            //     table : "article",
            //     columns: new[] {"Title", "Created", "Content"},
            //     values: new object[] {
            //         "Bai viet 2",
            //         new DateTime(2021, 8, 21),
            //         "Noi dung 2"
            //     }
            // );
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article");
        }
    }
}
