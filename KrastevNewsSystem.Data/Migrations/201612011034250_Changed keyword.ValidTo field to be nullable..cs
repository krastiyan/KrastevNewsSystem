namespace KrastevNewsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedkeywordValidTofieldtobenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleKeywords", "ValidTo", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArticleKeywords", "ValidTo", c => c.DateTime(nullable: false));
        }
    }
}
