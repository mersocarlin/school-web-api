namespace School.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.course",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        created_by_id = c.Int(nullable: false),
                        updated_by_id = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.user", t => t.created_by_id)
                .ForeignKey("dbo.user", t => t.updated_by_id)
                .Index(t => t.created_by_id)
                .Index(t => t.updated_by_id);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        profile = c.Int(nullable: false),
                        username = c.String(nullable: false, maxLength: 100),
                        password = c.String(nullable: false, maxLength: 100),
                        person_id = c.Int(),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        last_login = c.DateTime(),
                        status = c.Int(nullable: false),
                        refresh_token_id = c.String(),
                        protected_ticket = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.person", t => t.person_id)
                .Index(t => t.person_id);
            
            CreateTable(
                "dbo.person",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        created_by_id = c.Int(nullable: false),
                        updated_by_id = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 100),
                        gender = c.Int(nullable: false),
                        date_of_birth = c.DateTime(nullable: false),
                        email = c.String(maxLength: 50),
                        person_type = c.Int(nullable: false),
                        home_phone = c.String(maxLength: 15),
                        mobile_phone = c.String(maxLength: 15),
                        work_phone = c.String(maxLength: 15),
                        address = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.user", t => t.created_by_id)
                .ForeignKey("dbo.user", t => t.updated_by_id)
                .Index(t => t.created_by_id)
                .Index(t => t.updated_by_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.course", "updated_by_id", "dbo.user");
            DropForeignKey("dbo.course", "created_by_id", "dbo.user");
            DropForeignKey("dbo.user", "person_id", "dbo.person");
            DropForeignKey("dbo.person", "updated_by_id", "dbo.user");
            DropForeignKey("dbo.person", "created_by_id", "dbo.user");
            DropIndex("dbo.person", new[] { "updated_by_id" });
            DropIndex("dbo.person", new[] { "created_by_id" });
            DropIndex("dbo.user", new[] { "person_id" });
            DropIndex("dbo.course", new[] { "updated_by_id" });
            DropIndex("dbo.course", new[] { "created_by_id" });
            DropTable("dbo.person");
            DropTable("dbo.user");
            DropTable("dbo.course");
        }
    }
}
