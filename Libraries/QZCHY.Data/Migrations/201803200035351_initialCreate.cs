namespace QZCHY.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EatPicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EatId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        IsLogo = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        VillageEat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.VillageEat", t => t.VillageEat_Id)
                .ForeignKey("dbo.VillageEat", t => t.EatId, cascadeDelete: true)
                .Index(t => t.EatId)
                .Index(t => t.PictureId)
                .Index(t => t.VillageEat_Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PictureBinary = c.Binary(),
                        MimeType = c.String(),
                        SeoFilename = c.String(),
                        AltAttribute = c.String(),
                        TitleAttribute = c.String(),
                        IsNew = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VillageEat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Address = c.String(),
                        Description = c.String(),
                        Tel = c.String(),
                        Person = c.String(),
                        Price = c.Double(nullable: false),
                        Level = c.Int(nullable: false),
                        ReceptionNumber = c.Int(nullable: false),
                        Tags = c.String(),
                        Suggestion = c.String(),
                        Location = c.Geography(),
                        Icon = c.String(),
                        Order = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        Village_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Village", t => t.Village_Id, cascadeDelete: true)
                .Index(t => t.Village_Id);
            
            CreateTable(
                "dbo.Village",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Address = c.String(),
                        Phone = c.String(),
                        OpenTime = c.String(),
                        Desc = c.String(),
                        Tags = c.String(),
                        Price = c.Double(nullable: false),
                        TourRoute = c.String(),
                        GeoTourRoute = c.Geography(),
                        Icon = c.String(),
                        Location = c.Geography(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VillageLive",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Address = c.String(),
                        Description = c.String(),
                        Tel = c.String(),
                        Person = c.String(),
                        Location = c.Geography(),
                        Icon = c.String(),
                        BedsNumber = c.Int(nullable: false),
                        RoomPrice = c.String(),
                        Tags = c.String(),
                        Facilities = c.String(),
                        Order = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        Village_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Village", t => t.Village_Id, cascadeDelete: true)
                .Index(t => t.Village_Id);
            
            CreateTable(
                "dbo.LivePicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LiveId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        IsLogo = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        VillageLive_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.VillageLive", t => t.LiveId, cascadeDelete: true)
                .ForeignKey("dbo.VillageLive", t => t.VillageLive_Id)
                .Index(t => t.LiveId)
                .Index(t => t.PictureId)
                .Index(t => t.VillageLive_Id);
            
            CreateTable(
                "dbo.VillagePlay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        PanoramaId = c.Int(nullable: false),
                        AudioUrl = c.String(),
                        Icon = c.String(),
                        Location = c.Geography(),
                        Order = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        Village_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Village", t => t.Village_Id, cascadeDelete: true)
                .Index(t => t.Village_Id);
            
            CreateTable(
                "dbo.PlayPicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        IsLogo = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        VillagePlay_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.VillagePlay", t => t.PlayId, cascadeDelete: true)
                .ForeignKey("dbo.VillagePlay", t => t.VillagePlay_Id)
                .Index(t => t.PlayId)
                .Index(t => t.PictureId)
                .Index(t => t.VillagePlay_Id);
            
            CreateTable(
                "dbo.VillageService",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        PanoramaId = c.Int(nullable: false),
                        Icon = c.String(),
                        Location = c.Geography(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        Village_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Village", t => t.Village_Id, cascadeDelete: true)
                .Index(t => t.Village_Id);
            
            CreateTable(
                "dbo.ServicePicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        IsLogo = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        VillageService_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.VillageService", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.VillageService", t => t.VillageService_Id)
                .Index(t => t.ServiceId)
                .Index(t => t.PictureId)
                .Index(t => t.VillageService_Id);
            
            CreateTable(
                "dbo.VillagePicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VillageId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        IsLogo = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Village", t => t.VillageId, cascadeDelete: true)
                .Index(t => t.VillageId)
                .Index(t => t.PictureId);
            
            CreateTable(
                "dbo.VillageVedios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VillageId = c.Int(nullable: false),
                        VedioId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vedios", t => t.VedioId, cascadeDelete: true)
                .ForeignKey("dbo.Village", t => t.VillageId, cascadeDelete: true)
                .Index(t => t.VillageId)
                .Index(t => t.VedioId);
            
            CreateTable(
                "dbo.Vedios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 255),
                        DisplayName = c.String(maxLength: 255),
                        Host = c.String(nullable: false, maxLength: 255),
                        Port = c.Int(nullable: false),
                        Username = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        EnableSsl = c.Boolean(nullable: false),
                        UseDefaultCredentials = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageTemplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        BccEmailAddresses = c.String(maxLength: 200),
                        Subject = c.String(maxLength: 1000),
                        Body = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        AttachedDownloadId = c.Int(nullable: false),
                        EmailAccountId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QueuedEmail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriorityId = c.Int(nullable: false),
                        From = c.String(nullable: false, maxLength: 500),
                        FromName = c.String(maxLength: 500),
                        To = c.String(nullable: false, maxLength: 500),
                        ToName = c.String(maxLength: 500),
                        ReplyTo = c.String(maxLength: 500),
                        ReplyToName = c.String(maxLength: 500),
                        CC = c.String(maxLength: 500),
                        Bcc = c.String(maxLength: 500),
                        Subject = c.String(maxLength: 1000),
                        Body = c.String(),
                        AttachmentFilePath = c.String(),
                        AttachmentFileName = c.String(),
                        AttachedDownloadId = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        SentTries = c.Int(nullable: false),
                        SentOnUtc = c.DateTime(),
                        EmailAccountId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailAccount", t => t.EmailAccountId, cascadeDelete: true)
                .Index(t => t.EmailAccountId);
            
            CreateTable(
                "dbo.ActivityLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityLogTypeId = c.Int(nullable: false),
                        AccountUserId = c.Int(nullable: false),
                        Comment = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountUser", t => t.AccountUserId, cascadeDelete: true)
                .ForeignKey("dbo.ActivityLogType", t => t.ActivityLogTypeId, cascadeDelete: true)
                .Index(t => t.ActivityLogTypeId)
                .Index(t => t.AccountUserId);
            
            CreateTable(
                "dbo.AccountUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 1000),
                        Active = c.Boolean(nullable: false),
                        AccountUserGuid = c.Guid(nullable: false),
                        LastIpAddress = c.String(),
                        LastActivityDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                        Password = c.String(),
                        PasswordFormatId = c.Int(nullable: false),
                        PasswordSalt = c.String(),
                        IsSystemAccount = c.Boolean(nullable: false),
                        SystemName = c.String(maxLength: 400),
                        Remark = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountUserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        IsSystemRole = c.Boolean(nullable: false),
                        SystemName = c.String(maxLength: 255),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityLogType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemKeyword = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 200),
                        Enabled = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogLevelId = c.Int(nullable: false),
                        ShortMessage = c.String(nullable: false),
                        FullMessage = c.String(),
                        IpAddress = c.String(maxLength: 200),
                        CustomerId = c.Int(),
                        PageUrl = c.String(),
                        ReferrerUrl = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountUser", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Value = c.String(nullable: false, maxLength: 2000),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenericAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        KeyGroup = c.String(nullable: false, maxLength: 400),
                        Key = c.String(nullable: false, maxLength: 400),
                        Value = c.String(nullable: false),
                        StoreId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HashId = c.String(),
                        Subject = c.String(),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountUser_AccountUserRole_Mapping",
                c => new
                    {
                        AccountUser_Id = c.Int(nullable: false),
                        AccountUserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountUser_Id, t.AccountUserRole_Id })
                .ForeignKey("dbo.AccountUser", t => t.AccountUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.AccountUserRole", t => t.AccountUserRole_Id, cascadeDelete: true)
                .Index(t => t.AccountUser_Id)
                .Index(t => t.AccountUserRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Log", "CustomerId", "dbo.AccountUser");
            DropForeignKey("dbo.ActivityLog", "ActivityLogTypeId", "dbo.ActivityLogType");
            DropForeignKey("dbo.ActivityLog", "AccountUserId", "dbo.AccountUser");
            DropForeignKey("dbo.AccountUser_AccountUserRole_Mapping", "AccountUserRole_Id", "dbo.AccountUserRole");
            DropForeignKey("dbo.AccountUser_AccountUserRole_Mapping", "AccountUser_Id", "dbo.AccountUser");
            DropForeignKey("dbo.QueuedEmail", "EmailAccountId", "dbo.EmailAccount");
            DropForeignKey("dbo.EatPicture", "EatId", "dbo.VillageEat");
            DropForeignKey("dbo.VillageVedios", "VillageId", "dbo.Village");
            DropForeignKey("dbo.VillageVedios", "VedioId", "dbo.Vedios");
            DropForeignKey("dbo.VillagePicture", "VillageId", "dbo.Village");
            DropForeignKey("dbo.VillagePicture", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.VillageService", "Village_Id", "dbo.Village");
            DropForeignKey("dbo.ServicePicture", "VillageService_Id", "dbo.VillageService");
            DropForeignKey("dbo.ServicePicture", "ServiceId", "dbo.VillageService");
            DropForeignKey("dbo.ServicePicture", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.VillagePlay", "Village_Id", "dbo.Village");
            DropForeignKey("dbo.PlayPicture", "VillagePlay_Id", "dbo.VillagePlay");
            DropForeignKey("dbo.PlayPicture", "PlayId", "dbo.VillagePlay");
            DropForeignKey("dbo.PlayPicture", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.VillageLive", "Village_Id", "dbo.Village");
            DropForeignKey("dbo.LivePicture", "VillageLive_Id", "dbo.VillageLive");
            DropForeignKey("dbo.LivePicture", "LiveId", "dbo.VillageLive");
            DropForeignKey("dbo.LivePicture", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.VillageEat", "Village_Id", "dbo.Village");
            DropForeignKey("dbo.EatPicture", "VillageEat_Id", "dbo.VillageEat");
            DropForeignKey("dbo.EatPicture", "PictureId", "dbo.Pictures");
            DropIndex("dbo.AccountUser_AccountUserRole_Mapping", new[] { "AccountUserRole_Id" });
            DropIndex("dbo.AccountUser_AccountUserRole_Mapping", new[] { "AccountUser_Id" });
            DropIndex("dbo.Log", new[] { "CustomerId" });
            DropIndex("dbo.ActivityLog", new[] { "AccountUserId" });
            DropIndex("dbo.ActivityLog", new[] { "ActivityLogTypeId" });
            DropIndex("dbo.QueuedEmail", new[] { "EmailAccountId" });
            DropIndex("dbo.VillageVedios", new[] { "VedioId" });
            DropIndex("dbo.VillageVedios", new[] { "VillageId" });
            DropIndex("dbo.VillagePicture", new[] { "PictureId" });
            DropIndex("dbo.VillagePicture", new[] { "VillageId" });
            DropIndex("dbo.ServicePicture", new[] { "VillageService_Id" });
            DropIndex("dbo.ServicePicture", new[] { "PictureId" });
            DropIndex("dbo.ServicePicture", new[] { "ServiceId" });
            DropIndex("dbo.VillageService", new[] { "Village_Id" });
            DropIndex("dbo.PlayPicture", new[] { "VillagePlay_Id" });
            DropIndex("dbo.PlayPicture", new[] { "PictureId" });
            DropIndex("dbo.PlayPicture", new[] { "PlayId" });
            DropIndex("dbo.VillagePlay", new[] { "Village_Id" });
            DropIndex("dbo.LivePicture", new[] { "VillageLive_Id" });
            DropIndex("dbo.LivePicture", new[] { "PictureId" });
            DropIndex("dbo.LivePicture", new[] { "LiveId" });
            DropIndex("dbo.VillageLive", new[] { "Village_Id" });
            DropIndex("dbo.VillageEat", new[] { "Village_Id" });
            DropIndex("dbo.EatPicture", new[] { "VillageEat_Id" });
            DropIndex("dbo.EatPicture", new[] { "PictureId" });
            DropIndex("dbo.EatPicture", new[] { "EatId" });
            DropTable("dbo.AccountUser_AccountUserRole_Mapping");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.GenericAttribute");
            DropTable("dbo.Setting");
            DropTable("dbo.Log");
            DropTable("dbo.ActivityLogType");
            DropTable("dbo.AccountUserRole");
            DropTable("dbo.AccountUser");
            DropTable("dbo.ActivityLog");
            DropTable("dbo.QueuedEmail");
            DropTable("dbo.MessageTemplate");
            DropTable("dbo.EmailAccount");
            DropTable("dbo.Vedios");
            DropTable("dbo.VillageVedios");
            DropTable("dbo.VillagePicture");
            DropTable("dbo.ServicePicture");
            DropTable("dbo.VillageService");
            DropTable("dbo.PlayPicture");
            DropTable("dbo.VillagePlay");
            DropTable("dbo.LivePicture");
            DropTable("dbo.VillageLive");
            DropTable("dbo.Village");
            DropTable("dbo.VillageEat");
            DropTable("dbo.Pictures");
            DropTable("dbo.EatPicture");
        }
    }
}
