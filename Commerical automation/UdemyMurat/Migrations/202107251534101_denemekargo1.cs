namespace UdemyMurat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class denemekargo1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KargoDetays",
                c => new
                    {
                        KargoDetayId = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(maxLength: 300),
                        TakipKodu = c.String(maxLength: 10),
                        Personel = c.String(maxLength: 20),
                        Alici = c.String(maxLength: 25),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoDetayId);
            
            CreateTable(
                "dbo.KargoTakips",
                c => new
                    {
                        KargoTakipId = c.Int(nullable: false, identity: true),
                        TakipKodu = c.String(maxLength: 10),
                        Aciklama = c.String(maxLength: 100),
                        TarihZaman = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoTakipId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KargoTakips");
            DropTable("dbo.KargoDetays");
        }
    }
}
