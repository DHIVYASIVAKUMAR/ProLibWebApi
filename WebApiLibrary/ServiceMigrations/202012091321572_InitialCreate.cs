namespace WebApiLibrary.ServiceMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceAuthors",
                c => new
                    {
                        serviceAuthorId = c.Int(nullable: false, identity: true),
                        serviceAuthorName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.serviceAuthorId);
            
            CreateTable(
                "dbo.ServiceBooks",
                c => new
                    {
                        serviceBookId = c.Int(nullable: false, identity: true),
                        serviceBookName = c.String(nullable: false),
                        serviceSerialNumber = c.String(nullable: false),
                        serviceAuthorName = c.String(nullable: false),
                        serviceBranch = c.String(nullable: false),
                        servicePublications = c.String(nullable: false),
                        serviceIsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.serviceBookId);
            
            CreateTable(
                "dbo.ServiceBookBranches",
                c => new
                    {
                        serviceBookBranchId = c.Int(nullable: false, identity: true),
                        serviceBranch = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.serviceBookBranchId);
            
            CreateTable(
                "dbo.ServiceBookPublications",
                c => new
                    {
                        serviceBookPublicationId = c.Int(nullable: false, identity: true),
                        servicePublications = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.serviceBookPublicationId);
            
            CreateTable(
                "dbo.ServiceIssuedBooks",
                c => new
                    {
                        serviceIssuedId = c.Int(nullable: false, identity: true),
                        bookId = c.Int(nullable: false),
                        studentId = c.Int(nullable: false),
                        serviceFromDate = c.DateTime(nullable: false),
                        serviceToDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.serviceIssuedId)
                .ForeignKey("dbo.ServiceBooks", t => t.bookId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceStudents", t => t.studentId, cascadeDelete: true)
                .Index(t => t.bookId)
                .Index(t => t.studentId);
            
            CreateTable(
                "dbo.ServiceStudents",
                c => new
                    {
                        serviceStudentId = c.Int(nullable: false, identity: true),
                        serviceStudentName = c.String(nullable: false, maxLength: 30),
                        serviceStudentBranch = c.String(nullable: false),
                        serviceGender = c.Int(nullable: false),
                        servicePhoneNumber = c.String(nullable: false),
                        serviceAddress = c.String(nullable: false, maxLength: 150),
                        serviceCity = c.String(nullable: false, maxLength: 50),
                        serviceEmail = c.String(nullable: false),
                        servicePassword = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.serviceStudentId);
            
            CreateTable(
                "dbo.ServiceStudentBranches",
                c => new
                    {
                        serviceStudentBranchId = c.Int(nullable: false, identity: true),
                        serviceStudentBranch = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.serviceStudentBranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceIssuedBooks", "studentId", "dbo.ServiceStudents");
            DropForeignKey("dbo.ServiceIssuedBooks", "bookId", "dbo.ServiceBooks");
            DropIndex("dbo.ServiceIssuedBooks", new[] { "studentId" });
            DropIndex("dbo.ServiceIssuedBooks", new[] { "bookId" });
            DropTable("dbo.ServiceStudentBranches");
            DropTable("dbo.ServiceStudents");
            DropTable("dbo.ServiceIssuedBooks");
            DropTable("dbo.ServiceBookPublications");
            DropTable("dbo.ServiceBookBranches");
            DropTable("dbo.ServiceBooks");
            DropTable("dbo.ServiceAuthors");
        }
    }
}
