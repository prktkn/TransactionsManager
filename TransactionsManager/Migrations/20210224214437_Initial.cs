using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionsManager.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsTemp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
            migrationBuilder.Sql(
                @"IF OBJECT_ID ( 'MergeTransactions', 'P' ) IS NOT NULL
    DROP PROCEDURE MergeTransactions;
GO
CREATE PROCEDURE MergeTransactions
AS
	BEGIN TRANSACTION;
	BEGIN TRY
	SET NOCOUNT ON;
		SET IDENTITY_INSERT Transactions ON 
		MERGE Transactions AS target  
		USING TransactionsTemp AS source 
		ON (target.id = source.id)  
		WHEN MATCHED THEN
			UPDATE SET Status = source.Status
		WHEN NOT MATCHED THEN  
			INSERT (Id, Status, Type, ClientName, Amount)  
			VALUES (source.Id, source.Status, source.Type, source.ClientName, source.Amount);
	COMMIT;
	SET IDENTITY_INSERT Transactions OFF 
	END TRY
    BEGIN CATCH
       ROLLBACK TRANSACTION;
    END CATCH
	TRUNCATE TABLE [TransactionsManager].[dbo].[TransactionsTemp]
	SET IDENTITY_INSERT Transactions OFF 
GO"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionsTemp");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
