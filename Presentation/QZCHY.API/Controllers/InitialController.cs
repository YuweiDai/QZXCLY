using QZCHY.Core;
using QZCHY.Core.Domain.AccountUsers;
using QZCHY.Core.Domain.Common;
using QZCHY.Core.Domain.Media;
using QZCHY.Core.Domain.Security;
using QZCHY.Core.Domain.Villages;
using QZCHY.Services.AccountUsers;
using QZCHY.Services.Authentication;
using QZCHY.Services.Common;
using QZCHY.Services.Configuration;
using QZCHY.Services.Media;
using QZCHY.Services.Messages;
using QZCHY.Services.Security;
using QZCHY.Services.Villages;
using QZCHY.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace QZCHY.API.Controllers
{
    [RoutePrefix("Initial")]
    public class InitialController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountUserService _accountUserService;
        private readonly IAccountUserRegistrationService _accountUserRegistrationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IEncryptionService _encryptionService;
        private readonly IPictureService _pictureService;

        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly AccountUserSettings _accountUserSettings;
        private readonly CommonSettings _commonSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly ISettingService _settingService;

        private readonly IVillageService _villageService;
        private readonly IVillagePlayService _villagePlayService;
        private readonly IStrategyService _strategyService;

        public InitialController(IAuthenticationService authenticationService, IAccountUserService customerService,
            IAccountUserRegistrationService customerRegistrationService, IGenericAttributeService genericAttributeService,
       IWorkflowMessageService workflowMessageService, IEncryptionService encryptionService, IPictureService pictureService,
        IWebHelper webHelper,IWorkContext workContext,
        AccountUserSettings customerSettings, CommonSettings commonSettings, SecuritySettings securitySettings, ISettingService settingService,
        IVillageService villageService, IStrategyService strategyService, IVillagePlayService villagePlayService)
        {
            _authenticationService = authenticationService;
            _accountUserService = customerService;
            _accountUserRegistrationService = customerRegistrationService;
            _genericAttributeService = genericAttributeService;
            _workflowMessageService = workflowMessageService;
            _encryptionService = encryptionService;
            _pictureService = pictureService;

            _webHelper = webHelper;
            _workContext = workContext;
            _accountUserSettings = customerSettings;

            _commonSettings = commonSettings;
            _securitySettings = securitySettings;
            _settingService = settingService;

            _villageService = villageService;
            _strategyService = strategyService;
            _villagePlayService = villagePlayService;
        }

        [HttpGet]
        [Route("Hello")]
        public IHttpActionResult Hello()
        {
            return Ok("Hello");
        }

        [HttpGet]
        [Route("SetRoles")]
        public IHttpActionResult SetRoles()
        {
            #region 用户角色创建

            var roleNames = new List<string> {
                SystemAccountUserRoleNames.Administrators,
                SystemAccountUserRoleNames.Registered
            };

            foreach (var roleName in roleNames)
            {
                var role = _accountUserService.GetAccountUserRoleBySystemName(roleName);
                if (role == null)
                {
                    role = new AccountUserRole
                    {
                        Name = roleName,
                        Active = true,
                        IsSystemRole = true,
                        SystemName = roleName
                    };

                    _accountUserService.InsertAccountUserRole(role);
                }
            }
            #endregion

            return Ok("角色配置完成");
        }

        [HttpGet]
        [Route("Settings")]
        public  IHttpActionResult Settings()
        {
            _accountUserSettings.DefaultPasswordFormat = PasswordFormat.Encrypted;

            _settingService.SaveSetting<AccountUserSettings>(_accountUserSettings);

            _securitySettings.EncryptionKey = "qzczjwithqzghchy";
            _settingService.SaveSetting(_securitySettings);

            _commonSettings.TelAndMobliePartten = @"^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$|(^(13[0-9]|15[0|3|6|7|8|9]|18[0-9])\d{8}$)";
            _commonSettings.Time24Partten = @"^((1|0?)[0-9]|2[0-4]):([0-5][0-9])";

            _settingService.SaveSetting<CommonSettings>(_commonSettings);

            _settingService.SaveSetting(new MediaSettings
            {
                AvatarPictureSize = 120,
                ProductThumbPictureSize = 415,
                ProductDetailsPictureSize = 550,
                ProductThumbPictureSizeOnProductDetailsPage = 100,
                AssociatedProductPictureSize = 220,
                CategoryThumbPictureSize = 450,
                ManufacturerThumbPictureSize = 420,
                CartThumbPictureSize = 80,
                MiniCartThumbPictureSize = 70,
                AutoCompleteSearchThumbPictureSize = 20,
                MaximumImageSize = 1980,
                DefaultPictureZoomEnabled = false,
                DefaultImageQuality = 80,
                MultipleThumbDirectories = false
            });

            _settingService.SaveSetting(_accountUserSettings);

            return Ok("配置保存成功");
        }

        [HttpGet]
        [Route("Import")]
        public IHttpActionResult ImportData()
        {
            return Ok("closed");
            var relativePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/Imports/");
            var imageExts = new List<string>
            {
                ".png", ".jpg", ".bmp", ".gif"
            };

            var dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyyMMdd";

            try
            {

                #region 东坪入库

                #region 基本信息
                var dp = new Village
                {
                    Name = "康养衢江· 隐柿东坪",
                    Address = "衢州市衢江区峡川镇驻地峡口村东北部13公里",
                    Desc = "东坪村地处衢州市衢江区峡川镇驻地峡口村东北部13公里，距衢州市区38公里，东临龙游县塔石镇金村村、西与乌石坂村相交、南与大理、李泽村为界，北与高岭村相连，地理位置险要，村落分散，村坊两旁山高林密，仅有一条乡村公路和千年古道与外界相连，村坊后背山山峰最高点海拔610米，村坊位置海拔420米，属典型的山区古村落。东坪村村史悠久，至今已有一千年左右，据《李氏宗谱》记载，高宗七子李烨为避武则天残害宗室子弟，远途跋涉，隐居福建古田长汀麻团岭，后移居浙江衢之西邑，往返于东坪与石屏源（今李泽）之间。元朝元统年间（1333—1335年）正式创业于东坪，男耕女织、读书知礼、克勤克俭，延续至今。东坪村气候四季分明，雨量充沛，属亚热带季风性气候，适宜针、阔叶林等多种作物生长。红柿、竹笋、油茶、板栗、粮食、蔬菜是东坪村的主要物产，其中红柿又是东坪村的主打产品历史悠久，源于唐末，盛产至今，其中号称“柿王”的一棵柿树，年产量均在3000斤左右，因为这里海拔高，温差大，光照足，无污染，特别适应柿树的种植、生长，柿果个大味甘，极富营养，曾作为贡品进献朝廷。相传，广为衢北百姓传诵的明朝中后期的“李泽娘娘”就是东坪人，这位娘娘入宫后的第一个中秋，望月思乡，想起家乡父老，想起家中红柿。然而，吃遍了全国各地送来的红柿，娘娘皆不如意，故决定以后的每年中秋，由衢州进贡一篮东坪红柿进皇宫。久而久之，东坪红柿便得名“贡柿”。东坪红柿品种众多，有客柿、西瓜柿、牛奶柿、鸟柿、汤瓶柿、六柿等。柿干质地柔软、肉质细嫩、霜厚均匀，含有蛋白质、糖、胡萝卜素等丰富的营养及磷、铁、钙、碘等多种微量元素，具有很高的营养及药用价值。",
                    Location = DbGeography.FromText("POINT(119.0333890915 29.1851002329)"),
                    OpenTime = "9:00-16:00",
                    Phone = "18057081250",
                    Tags = "AAA级景区;柿子节;康养",
                    Price = 0,
                    Icon = "dp",
                    Triffic = "隐柿东坪旅游景区位于峡川镇,有公交专线来往于衢州市区与东坪村之间，途径峡口村、后山村、李泽村、乌石坂村，全天共有18个班次，其中通往东坪古村全天3个班次，旅客出行方便快捷。",
                    Panorama="dp",
                    TourRoute = "Day 1：下停车场--古道入口--山涧戏水--古树观景--古树群--火烧红枫--唐官文化岩壁石刻--东坪村山寨门-游客接待中心--民俗文化广场--古村漫游--东坪柿林--农耕乐园--农家住宿；Day 2 古村--竹文化园--竹林素拓基地--仙岩古寺遗址--天池--徒步驿站；",
                   // GeoTourRoute = DbGeography.FromText("LINESTRING (119.028514065866 29.1848777616058,119.028726913651 29.1848799086025,119.028907699531 29.1848100607183,119.029056533039 29.1846539077354,119.029188465675 29.1845549105217,119.029302940581 29.1845703910883,119.029483037913 29.1845722035611,119.029579796395 29.1847308326767,119.029974762967 29.1845198088051,119.030286647614 29.1844369492818,119.030715695806 29.1840829353809,119.031093732184 29.1839290691881,119.031569463693 29.1838335018403,119.031881057974 29.1837792829735,119.032028807272 29.1837377648857,119.032193733685 29.1836104180754,119.032123004627 29.1841686684102,119.031989480462 29.1844396494739,119.032151454102 29.184627593673,119.032034968515 29.1848270870913,119.03172215232 29.1850102812681,119.031639484385 29.1850954495793,119.031819044938 29.185154575445,119.031916066533 29.1852845289049,119.032226455162 29.1853592921575,119.032503838625 29.1854623850709,119.032782025484 29.1854794833945,119.032996870064 29.1852666444922,119.033044375309 29.1854391036524,119.033092675789 29.1855255798142,119.03333852249 29.1854993544297,119.033536723807 29.1853150095895,119.033500619801 29.1856729532013,119.033482378996 29.1858734257268,119.033531484247 29.1858739185007,119.034040476507 29.1857213121902,119.034562204398 29.1859558069461,119.03492021389 29.1861886683149,119.035887023115 29.1860978940389,119.036295741644 29.186159257826,119.03655793743 29.1861331770873,119.036604374499 29.1864202761216,119.036929225624 29.1866957854103,119.03713963227 29.1869558417939,119.037252085743 29.1871862593477,119.037530107465 29.1872176570113,119.037875743274 29.1870203951584,119.038057003071 29.1868931783097,119.038430969041 29.1871691525915,119.038362956231 29.1874408025601,119.038262064909 29.1877264580753,119.038440927121 29.1878572082183,119.038623396658 29.1876010044518,119.039084937601 29.1872615423787,119.039547664741 29.1867930801145,119.039944115231 29.1864099684607,119.039835666497 29.1857496234904,119.039741051649 29.1853617293983,119.039599069288 29.1847870529083,119.039403844358 29.184656156073,119.039291000526 29.184468735957,119.039409718801 29.1840255932813,119.039215297911 29.1838087022758,119.03913638447 29.1834926207897,119.039023937295 29.1832622081542,119.039075311166 29.1830190553133,119.039452853724 29.1829080859707,119.039812412785 29.1829689174784,119.040206315367 29.1828580922258,119.040500531415 29.1829039403174)"),
                    VideoSize=78687,
                    VideoUrl= "http://qzch.qz.gov.cn/qzxcly/resources/vedios/dp.mp4"

                };

                //图片信息
                var images = System.IO.Directory.GetFiles(relativePath + "dp/images/");
                foreach (var imagePath in images)
                {
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                    var fileExt = System.IO.Path.GetExtension(imagePath);

                    if (imageExts.Contains(fileExt.ToLower()))
                    {
                        var villagePicture = new VillagePicture();

                        var byData = GetPictureData(imagePath);

                        var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                        villagePicture.Picture = picture;
                        villagePicture.IsLogo = fileName.ToUpper() == "LOGO";
                        villagePicture.IsRoute = fileName.ToUpper() == "ROUTE";

                        dp.VillagePictures.Add(villagePicture);
                    }
                } 
                #endregion

                #region 服务设施

                dp.Services.Add(new Core.Domain.Villages.VillageService() { Name = "山脚停车场", Description = "", Location = DbGeography.FromText("POINT(119.028336339932 29.185065206383)"), Icon = "parking", ServiceType= ServiceType.Park });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Name = "山上停车场", Description = "", Location = DbGeography.FromText("POINT(119.033093070437 29.1872311309559)"), Icon = "parking", ServiceType = ServiceType.Park });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Name = "游客接待中心", Description = "", Location = DbGeography.FromText("POINT(119.032570530284 29.1853340591726)"), Icon = "ticket", ServiceType = ServiceType.Others,Deleted=true });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Name = "山脚厕所", Description = "", Location = DbGeography.FromText("POINT(119.028073811557 29.1847519536177)"), Icon = "wc", ServiceType = ServiceType.WC });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Name = "山上厕所", Description = "", Location = DbGeography.FromText("POINT(119.032515188008 29.1852134615949)"), Icon = "wc", ServiceType = ServiceType.WC });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Name = "红芬小卖部", Description = "", Location = DbGeography.FromText("POINT(119.032749318041 29.1854837977747)"), Icon = "wc", ServiceType = ServiceType.WC, Deleted = true });

                #region 图片路径
                var service_imagesPath = relativePath + "dp/service_images/";

                foreach (var service in dp.Services)
                {
                    var directory = service_imagesPath + service.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var service_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = service_images.Length == 1;

                        foreach (var imagePath in service_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var villagePicture = new ServicePicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                villagePicture.Picture = picture;
                                villagePicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                service.ServicePictures.Add(villagePicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 玩的景点

                dp.Plays.Add(new VillagePlay
                {
                    Name = "千年古道",
                    Description = "东坪千年古道位于衢州市峡川镇境内，距市区40公里。从东坪山脚下起，一条总长1500米，宽2米，共1144级的青石板古道蜿蜒盘曲伸向山顶，随着岁月的流逝，村民的脚步磨去了古道青石块的棱角而变得光滑。这就是相传具有1300多年历史的唐朝古道。",
                    Location = DbGeography.FromText("POINT(119.0282124281 29.1850908661)"),
                    Icon = "qngd",
                    AudioUrl= "http://qzch.qz.gov.cn/qzxcly/resources/audios/dp001.mp3"
                });

                dp.Plays.Add(new VillagePlay { Name = "龙石潭", Description = "相传，南宋末年的一天，东坪仙岩寺内有个老和尚，掐指一算，得知近期东坪山要出“龙”。传说出“龙”之地，意味着山洪爆发，村民就要遭殃。村民们一时惊慌失措，忙问老和尚有何破解之法。老和尚说：“龙刚从山洞里出来时，只是一条小泥鳅，要到河、沟里经过雷电闪烁才会变成大龙。”于是族长马上叫大家上山砍毛竹，把毛竹对半剖开，去掉骨节，接成水笕接到山洞里，由村民一根一根把每节水笕接到山脚。谁知快到山脚时，有个村民疲倦难当，认为接到此处应该差不多了，于是坐下来休息。恰恰这时，“龙”像一根泥鳅粗细慢慢地沿水管游出来，至水管中断处，“噔”地一声掉到山沟里。“龙”受惊，立马幻化身形，变成大龙游出去，落地之处至今留有一大坑，当地人称“龙石潭”。", Location = DbGeography.FromText("POINT(119.029138979441 29.1847379412537)"), Icon = "lst" });
                dp.Plays.Add(new VillagePlay { Name = "古樟指路", Description = "李烨在东坪定居以后，当年一些私下的挚友就去拜访李烨，当他们走到山脚下的时候，因为草木茂盛，不知道往那里走的时候，前面一颗樟树似乎知晓来人并无敌意，只是前来拜访李烨，根部突生一根枝节，枝节伸展的方向正好对着现东坪山，沿着枝节指引的方向，顺利来到李烨居住的地方，众挚友不无感慨： “真是天佑李烨”——上天都还眷顾着李烨。", Location = DbGeography.FromText("POINT(119.029467879878 29.184588373659)"), Icon = "gzzl" });
                dp.Plays.Add(new VillagePlay { Name = "神狮守道", Description = "自从李烨一行在此定居之后，方圆百里的鸟兽听闻有皇室血统的贵人居住在此地，纷纷前来保护贵人的安全，神狮带着的孩子也来到东坪，守护上山的必经之路，日日夜夜的守护山道，提防有人上山危害贵人的生命安全，一旦有异常可以及时警示山上的人做好抵御的准备。神狮也非常疼爱自己的孩子，担心小狮子受累，让其睡在自己的身子上，，而自己却依然恪职敬守。", Location = DbGeography.FromText("POINT(119.029467879878 29.184588373659)"), Icon = "sssd", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "神镜石", Description = "据《衢县志》等史料记载，武则天在位时，为防武后残害，一批（李姓）宗室外迁。李治（唐高宗）第七子李烨，从长安远避福建古田长河麻团岭。唐中宗时（公元705—709）为防止遭到进一步的迫害，李烨携家属以及亲信从麻团岭转迁，沿着现东坪线一路寻找新的定居点，当一行人经过（现东坪山脚下）时，发现一潭清水，清澈见底，味道甘甜，决定就地休整之后继续找寻。休息之时，李烨四处查看地形，当走到这块石头（神镜石）跟前的时候，不由自主停住脚步，审视此石，此石突显灵性，仿佛就是在等待主人的到来，大放异光，幻化成一块镜子，从镜子中隐约可以看到（现东坪山上）散发出阵阵的金光，李烨心想莫非是上天的指示，要我去山顶？要我去山顶定居？李烨毫不迟疑带领家属以及亲信向山顶进发，登上山顶一看，果然是块风水宝地，地理位置优越，居高临下，易守难攻，并且环境优美，茂林修竹，空气清新。远离朝廷的纷争，适宜清修，同时这里水资源非常丰富，适合农业生产，李烨勘察之后，聚集亲信前来商议，众人都十分钟情这块风水宝地，于是李烨决定定居此处。", Location = DbGeography.FromText("POINT(119.030056939189 29.1846325118199)"), Icon = "sjs", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "双枫迎客", Description = "李烨定居东坪之后，已经脱离朝廷的残害，心神俱宁，每日在犹如仙境的东坪吟诗作对，久而久之李烨心想能有更多的人来到这块仙境，一起修身养性、肆意酣畅，开展居住地的建设多好啊，上天好像能够读懂李烨的心思，派两位神仙化作枫树于现东坪古道，一只手化作遒劲的树枝，迎接过往来客，邀请客人作客东坪，领略山中秀色。", Location = DbGeography.FromText("POINT(119.031879550028 29.1835041602798)"), Icon = "sfyk" });
                dp.Plays.Add(new VillagePlay { Name = "唐官文化岩壁石刻", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.032201787321 29.1840615619858)"), Icon = "sgwhybsk", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "东坪村山寨门", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.032291278184 29.1854316034564)"), Icon = "dpcszm" });
                dp.Plays.Add(new VillagePlay { Name = "民俗文化广场", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.032800008211 29.1853076821093)"), Icon = "mswhgc" });
                dp.Plays.Add(new VillagePlay { Name = "东坪柿林", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.033592139316 29.1863904827778)"), Icon = "dpsl", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "农耕乐园", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.033489892298 29.1868194415728)"), Icon = "ngly", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "竹文化园", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.032023322173 29.1825767794082)"), Icon = "zwhy", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "竹林素拓基地", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.030081858842 29.1800921751621)"), Icon = "zlstjd", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "天池", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.038584168861 29.1865400314482)"), Icon = "tc", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "徒步驿站", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.039391610273 29.1842117381383)"), Icon = "tbyz", Deleted = true });
                dp.Plays.Add(new VillagePlay { Name = "李氏宗祠", Description = "暂无介绍", Location = DbGeography.FromText("POINT(119.032999154672 29.1858501345983)"), Icon = "lszc", Deleted = true });

                #region 图片路径
                var play_imagesPath = relativePath + "dp/play_images/";

                foreach (var play in dp.Plays)
                {
                    var directory = play_imagesPath + play.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var play_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = play_images.Length == 1;

                        foreach (var imagePath in play_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var playPicture = new PlayPicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                playPicture.Picture = picture;
                                playPicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                play.PlayPictures.Add(playPicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 农家乐

                dp.Eats.Add(new VillageEat() { Name = "红枫堂", Address = "峡川镇东坪村", Person = "李延标", Description = "", Tel = "0570-2610068", ReceptionNumber = 60, Level = 3, Price = 0, Location = DbGeography.FromText("POINT(119.032699213792 29.1853854672489)"), Icon = "eat3" });
                dp.Eats.Add(new VillageEat() { Name = "外婆家", Address = "峡川镇东坪村", Person = "郑银英", Description = "", Tel = "13506708395", ReceptionNumber = 40, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "eat", Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "柿民客栈", Address = "峡川镇东坪村", Person = "李诗毅", Description = "", Tel = "666486", ReceptionNumber = 200, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033435575377 29.1841740615007)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Name = "荷塘轩", Address = "峡川镇东坪村", Person = "姚银花", Description = "", Tel = "13575661123", ReceptionNumber = 20, Level = 2, Price = 0, Location = DbGeography.FromText("POINT(119.0329237403 29.1854197393375)"), Icon = "eat2" });
                dp.Eats.Add(new VillageEat() { Name = "古道居", Address = "峡川镇东坪村", Person = "李招耀", Description = "", Tel = "13615702342", ReceptionNumber = 40, Level = 2, Price = 0, Location = DbGeography.FromText("POINT(119.032292242207 29.1854258964995)"), Icon = "eat2" });
                dp.Eats.Add(new VillageEat() { Name = "东坪院", Address = "峡川镇东坪村", Person = "姚水仙", Description = "", Tel = "13819019293", ReceptionNumber = 50, Level = 3, Price = 0, Location = DbGeography.FromText("POINT(119.033051947207 29.1852506910921)"), Icon = "eat3" });
                dp.Eats.Add(new VillageEat() { Name = "华兰阁", Address = "峡川镇东坪村", Person = "李国华", Description = "", Tel = "13676611662", ReceptionNumber = 50, Level = 3, Price = 0, Location = DbGeography.FromText("POINT(119.033121602618 29.1860496505277)"), Icon = "eat3" });
                dp.Eats.Add(new VillageEat() { Name = "红柿斋", Address = "峡川镇东坪村", Person = "李诗良", Description = "", Tel = "15957004737", ReceptionNumber = 40, Level = 2, Price = 0, Location = DbGeography.FromText("POINT(119.033222881785 29.1855682447133)"), Icon = "eat2" });
                dp.Eats.Add(new VillageEat() { Name = "米兰农庄", Address = "峡川镇东坪村", Person = "危米兰", Description = "", Tel = "13511405431", ReceptionNumber = 60, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.03248017981 29.1856409383757)"), Icon = "eat", Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "冬雪苑", Address = "峡川镇东坪村", Person = "杨渭花", Description = "", Tel = "15924079115", ReceptionNumber = 50, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.032932747381 29.1847460420451)"), Icon = "eat",Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "松清阁", Address = "峡川镇东坪村", Person = "黄花化", Description = "", Tel = "15215790797", ReceptionNumber = 40, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033036399867 29.1854129703936)"), Icon = "eat", Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "李家庄", Address = "峡川镇东坪村", Person = "李荣华", Description = "", Tel = "15857082393", ReceptionNumber = 20, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033396799926 29.1842951926885)"), Icon = "eat", Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "满意楼", Address = "峡川镇东坪村", Person = "饶志英", Description = "", Tel = "13857018246", ReceptionNumber = 160, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033041670312 29.1856980166666)"), Icon = "eat", Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "驴友之家", Address = "峡川镇东坪村", Person = "章银兰", Description = "", Tel = "15372723626", ReceptionNumber = 50, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.032658398439 29.1858833715623)"), Icon = "eat", Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "隐柿东坪", Address = "峡川镇东坪村", Person = "余夏仙", Description = "", Tel = "13867478563", ReceptionNumber = 200, Level = 0, Price = 20, Location = DbGeography.FromText("POINT(119.033311824464 29.1855339146446)"), Icon = "eat", Deleted = true });
                dp.Eats.Add(new VillageEat() { Name = "叙月楼", Address = "峡川镇东坪村", Person = "李雪堂", Description = "", Tel = "13819010468", ReceptionNumber = 15, Level = 0, Price = 20, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "eat", Deleted = true });

                #region 图片路径
                var eat_imagesPath = relativePath + "dp/eat_images/";

                foreach (var eat in dp.Eats)
                {
                    var directory = eat_imagesPath + eat.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var eat_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = eat_images.Length == 1;

                        foreach (var imagePath in eat_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var eatPicture = new EatPicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                eatPicture.Picture = picture;
                                eatPicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                eat.EatPictures.Add(eatPicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 民宿

                dp.Lives.Add(new VillageLive() { Name = "隐柿东坪", Address = "峡川镇东坪村", Person = "余夏仙", Description = "", Tel = "13867478563", BedsNumber = 3, Location = DbGeography.FromText("POINT(119.033311824464 29.1855339146446)"), Icon = "live_hot" });
                dp.Lives.Add(new VillageLive() { Name = "红枫堂", Address = "峡川镇东坪村", Person = "李延标", Description = "", Tel = "0570-2610068", BedsNumber = 8, Location = DbGeography.FromText("POINT(119.032699213792 29.1853854672489)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "古道居", Address = "峡川镇东坪村", Person = "李招耀", Description = "", Tel = "13615702342", BedsNumber = 4, Location = DbGeography.FromText("POINT(119.032292242207 29.1854258964995)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "华兰阁", Address = "峡川镇东坪村", Person = "李国华", Description = "", Tel = "13676611662", BedsNumber = 11, Location = DbGeography.FromText("POINT(119.033121602618 29.1860496505277)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "米兰农庄", Address = "峡川镇东坪村", Person = "危米兰", Description = "", Tel = "13511405431", BedsNumber = 5, Location = DbGeography.FromText("POINT(119.03248017981 29.1856409383757)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "冬雪苑", Address = "峡川镇东坪村", Person = "杨渭花", Description = "", Tel = "15957004737", BedsNumber = 5, Location = DbGeography.FromText("POINT(119.032932747381 29.1847460420451)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "松清阁", Address = "峡川镇东坪村", Person = "黄花化", Description = "", Tel = "15924079115", BedsNumber = 3, Location = DbGeography.FromText("POINT(119.033036399867 29.1854129703936)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "李家庄", Address = "峡川镇东坪村", Person = "李荣华", Description = "", Tel = "15857082393", BedsNumber = 10, Location = DbGeography.FromText("POINT(119.033396799926 29.1842951926885)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "外婆家", Address = "峡川镇东坪村", Person = "郑银英", Description = "", Tel = "13506708395", BedsNumber = 4, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "满意楼", Address = "峡川镇东坪村", Person = "饶志英", Description = "", Tel = "13857018246", BedsNumber = 12, Location = DbGeography.FromText("POINT(119.033041670312 29.1856980166666)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "驴友之家", Address = "峡川镇东坪村", Person = "章银兰", Description = "", Tel = "15372723626", BedsNumber = 14, Location = DbGeography.FromText("POINT(119.032658398439 29.1858833715623)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "柿民客栈", Address = "峡川镇东坪村", Person = "李诗毅", Description = "", Tel = "666486", BedsNumber = 18, Location = DbGeography.FromText("POINT(119.033435575377 29.1841740615007)"), Icon = "live", Deleted = true });
                dp.Lives.Add(new VillageLive() { Name = "叙月楼", Address = "峡川镇东坪村", Person = "李雪堂", Description = "", Tel = "13819010468", BedsNumber = 10, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "live", Deleted = true });

                #region 图片路径
                var live_imagesPath = relativePath + "dp/live_images/";

                foreach (var live in dp.Lives)
                {
                    var directory = live_imagesPath + live.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var live_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = live_images.Length == 1;

                        foreach (var imagePath in live_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var livePicture = new LivePicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                livePicture.Picture = picture;
                                livePicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                live.LivePictures.Add(livePicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 玩法攻略

                var strategyDirectories = System.IO.Directory.GetDirectories(relativePath + "dp/strategies/");

                foreach (var directory in strategyDirectories)
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    FileInfo[] htmls = di.GetFiles("*.html");//文件夹下的.xls文件
                    if (htmls.Length > 0)
                    {
                        var strategy = new Strategy
                        {
                            Title = di.Name.Split('_')[1],
                            Date = DateTime.ParseExact(di.Name.Split('_')[0], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture),
                            Src = htmls[0].Name,
                            Village = dp
                        };

                        if (File.Exists(directory + "/logo.jpg"))
                        {
                            var strategyPicture = new StrategyPicture();

                            var byData = GetPictureData(directory + "/logo.jpg");

                            var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                            strategyPicture.Picture = picture;

                            strategy.StrategyPictures.Add(strategyPicture);
                        }

                        dp.Strategies.Add(strategy);
                    }
                }

                #endregion

                _villageService.InsertVillage(dp);

                #endregion
              
                #region 长虹入库

                #region 基本信息
                var ch = new Village
                {
                    Name = "诗画乡村·七彩长虹",
                    Address = "开化国家公园西北部，距县城35公里",
                    Desc = "七彩长虹旅游景区位于开化国家公园西北部，距县城35公里， 九山半水半分田，森林覆盖率85.1%，山林面积17.2万亩，耕地8287亩。与江西婺源江湾镇相邻，是浙赣省际协作的主要窗口之一。景区山清水秀、人杰地灵，文化底蕴深厚，是浙西具有典型性的乡村旅游景区。目前开发成熟的有江南布达拉宫台回山、高田坑古村落两大特色景区 ，是浙西红色胜地、风水宝地、养生福地，近年来，七彩长虹成为浙西乡村乡村旅游的一个“引爆点”，成为浙西乃至长三角地区乡村旅游的重要目的地之一。长虹是方志敏、黄道、邵式平等共产党人创立的闽浙（皖）赣革命根据地的重要组成部分。 九山半水半分田，森林覆盖率85.1%，山林面积17.2万亩，耕地8287亩。水力资源丰厚，碧家河水库是开化第三大水库。富有乡村特色的景观使长虹成为国家东部公园的一颗明珠，是中国美院美院写生创作基地、衢州市摄影创作基地，开化乡村休闲旅游示范区 。\n七彩长虹，是一个“望得见山、看得见水、记得住乡愁”美丽乡村。著有江南布达拉宫之称的台回山，层层梯田如行云流水，气势恢宏，磅礴壮观。春天的金色油菜花绒毯、夏天的七彩梯田、秋天的稻穗麦浪和冬天的霜雪玉砌，一年四季，总有可拍之景。高山古村•高田坑浙江保存最完善的原生态古村落，其暗夜公园更是星空摄影极佳拍摄地。长虹是一方风水宝地，钱王祖墓，状元故里，范氏文化，皆踞于此；长虹是一片养生福地，山河俊秀，有水皆碧，无山不青，时光静好。\n长虹是一方红色胜地，中共闽浙赣省委旧址，是方志敏、关英等老一辈革命家开创的革命根据地。这里是方志敏、黄道、邵式平等共产党人创立的闽浙（皖）赣革命根据地的重要组成部分。1935年5月，关英率部在库坑成立闽浙赣省委在浙西的第一个中心县委——中共开婺休中心县委，７月，建立中共闽浙赣省委秘密机关，成为当时浙皖边区的革命中心，先后建立起7个中心区委，党支部107个，团支部31个。国民党军对库坑红军进行多次“清剿”，进行大小战役9次，长虹籍牺牲的红军就达50多人。现建有中共闽浙赣省委纪念馆、红色广场、红军烈士纪念馆、红军被服厂等，被列为全国红色旅游经典景区名录，为浙江省第八批爱国主义教育基地，现每年接待参观人数超4万人次。库坑已成为浙西最闪亮的红色胜地。\n长虹是一方风水宝地，是浙江第一状元程宿的故里。宋端拱元年（公元988年），北源村程宿18岁状元及第，之后其儿子程迪为榜眼，孙子程天民、曾孙程俱均为进士，一门四进士。其曾孙程俱所作《麟台故事》和《北山小集》收入《四库全书》。桃源是范仲淹后裔迁居地，范仲淹的长孙范旭定居长虹桃源大举自然村，曾三次为所居山村取名“野墅、桃园、大举”。是钱王祖墓所在地。明崇祯至民国的七部《开化县志》记载，“钱王冢在县北三十里云台真子坑，传吴越王钱缪祖坟地也……”。真子坑就是现在的老屋基自然村，当地宗谱有钱王祖坟的墓图所示。有集贤祠、顺应桥、云门寺等7个文物保护点。有开化海拔最高、保存最完好的历史文化古村落高田坑。\n长虹是一方养生福地，长虹九山半水半分田，森林覆盖率85.1%，山林面积17.2万亩，耕地8287亩。水利资源丰富，碧家河水库是开化第三大水库。富有乡村特色的景观使长虹成为国家东部公园里的一颗明珠，有江南布达拉宫台回山、真子坑浙西小龙脉、中山堂江南最美茶园、天上人间西山、西坑野生石斑鱼观赏基地等景点，是中国美院写生创作基地、衢州市摄影创作基地。万梓良主演的电影《回到原点》、《英秀》在长虹成功拍摄，是开化乡村休闲旅游示范区。",
                    Location = DbGeography.FromText("POINT(118.23096992496347 29.220730023463066)"),
                    OpenTime = "9:00-16:00",
                    Phone = "0570-6084444",
                    Tags = "AAAA级景区;乡村休闲旅游示范区;摄影创作基地",
                    Price = 0,
                    Icon = "ch",
                    Triffic = "途经星口、池淮、皇岸、立江、虹桥、芳村、下坞、河滩每40分钟一班",
                    TourRoute = "一路游：北源状元文化村（云门寺）——桃源范氏宗祠——江南最美茶园——江南布达拉宫（台回山）——钱王祖墓——高田坑古村落——霞川红军烈士纪念馆（集贤祠）——红色库坑——天上人间西山——红军烈士纪念墙——西坑野生石斑鱼基地——一村居两省河滩（红军被服厂）。; 红色游：霞坞红军烈士纪念馆（红军樟、国民党重兵驻地）——库坑中共闽浙赣省委旧址——红军烈士纪念墙——石盔山红军被服厂; 古色游：北源状元文化村（云门寺、大麦山）——桃源范氏文化村——钱王祖墓——高田坑古村落——霞坞集贤祠——一村居两省河滩。; 绿色游：江南最美茶园——江南布达拉宫（台回山）——碧家湖——天上人间西山——西坑野生石斑鱼基地——一村居两省河滩",
                    //GeoTourRoute = DbGeography.FromText("POLYGON((118.819692134857 28.9730174392434,118.819787353277 28.9730053693029,118.81977930665 28.9730013459894,118.819692134857 28.9730174392434))"),
                    VideoSize=58034,
                    VideoUrl= "http://qzch.qz.gov.cn/qzxcly/resources/vedios/dp.mp4"
                };

                //图片信息
                images = System.IO.Directory.GetFiles(relativePath + "ch/images/");
                foreach (var imagePath in images)
                {
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                    var fileExt = System.IO.Path.GetExtension(imagePath);

                    if (imageExts.Contains(fileExt.ToLower()))
                    {
                        var villagePicture = new VillagePicture();

                        var byData = GetPictureData(imagePath);

                        var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                        villagePicture.Picture = picture;
                        villagePicture.IsLogo = fileName.ToUpper() == "LOGO";
                        villagePicture.IsRoute = fileName.ToUpper() == "ROUTE";

                        ch.VillagePictures.Add(villagePicture);
                    }
                }
                #endregion

                #region 服务设施

                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "七彩长虹游客服务中心", Description = "", Location = DbGeography.FromText("POINT(118.23096992496347 29.220730023463066)"), Icon = "service", ServiceType = ServiceType.Others,Deleted=true });
                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "台回山游客服务中心", Description = "", Location = DbGeography.FromText("POINT(118.21898247916494 29.276036035563667)"), Icon = "service", ServiceType = ServiceType.Others, Deleted = true });
                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "舒园",Description = "三星级厕所", Location = DbGeography.FromText("POINT(118.23167688702317 29.220346136287962)"), Icon = "wc_hot", ServiceType = ServiceType.WC });
                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "厕所", Description = "", Location = DbGeography.FromText("POINT(118.21894261979105 29.276819622922055)"), Icon = "wc", ServiceType = ServiceType.WC });
                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "生态停车场", Description = "大巴停车场，11个车位，", Location = DbGeography.FromText("POINT(118.23174881417336 29.220685323999167)"), Icon = "parking", ServiceType = ServiceType.Park });
                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "停车场（带充电装）", Description = "12个车位，充电车位5个", Location = DbGeography.FromText("POINT(118.23090200094957 29.220470892812539)"), Icon = "parking", ServiceType = ServiceType.Park });
                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "停车场", Description = "10个车位", Location = DbGeography.FromText("POINT(118.21853031550714 29.275862482675571)"), Icon = "parking", ServiceType = ServiceType.Park, Deleted = true });
                ch.Services.Add(new Core.Domain.Villages.VillageService() { Name = "台回山停车场（带充电装）", Description = "30个车位，充电车位5个", Location = DbGeography.FromText("POINT(118.21853031550714 29.275862482675571)"), Icon = "parking", ServiceType = ServiceType.Park, Deleted = true });

                #region 图片路径
                service_imagesPath = relativePath + "ch/service_images/";

                foreach (var service in ch.Services)
                {
                    var directory = service_imagesPath + service.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var service_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = service_images.Length == 1;

                        foreach (var imagePath in service_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var villagePicture = new ServicePicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                villagePicture.Picture = picture;
                                villagePicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                service.ServicePictures.Add(villagePicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 玩的景点

                ch.Plays.Add(new VillagePlay { Name = "江南布达拉宫·台回山", Description = "村庄一层一层从山脚到半山腰分布，房前屋后是梯田，形成布达拉宫式的景观。当漫山的油菜花开时，花在村中，村在画中。传有钱王逃（台）回之山、敲石传音等故事。每年清明节前后为来此游客5000多人次。目前，长虹乡已开始实施该景点提升整治，将修筑景观坝，整理河道，增设盘山公路会车道，修建道路边沟、上下2处停车场，统一种植油菜，对山体进行绿化彩化，修建公厕，改造农房，发展农家乐等，致力把该景点打造成为钱江源生态文化休闲旅游度假区的重要景点。", Location = DbGeography.FromText("POINT(118.2185769081 29.2764700665)"), Icon = "ths", AudioUrl = "http://qzch.qz.gov.cn/qzxcly/resources/audios/ch001.mp3" });
                ch.Plays.Add(new VillagePlay { Name = "钱王祖墓·真子坑", Description = "民国三十八年《开化县志稿》记载，“钱王冢在县北三十里云台真子坑，传吴越王钱缪祖坟地也……”。真子坑就是现在长虹乡老屋基村。如今在长虹乡一带还传诵着许多有关钱王的民间故事。相传一风水先生来到钱塘江，观其江水，自言自语道：“随江而上必有宝地，不出天子也出将相。”风水先生一路寻来，在长虹乡老屋基农户住下，爬遍了方圆每座山。最后，选定老屋基村东北面的茅山，称该地为“万山龙脉一山通，山前溪水玉带环，远近群山齐相拥，万马千军呼应来”，为“天子地”。却被姓钱的放牛娃把自己祖宗遗骨埋在此地。从此钱娃（即钱缪王）一天天发迹起来，建立了吴越国。现存老屋基村《邹氏宗谱》第四卷墓图上标有“钱王祖坟”位置，《邹氏宗谱》塘流降派里居图标有“钱墓”位置，题为《钱缪遗址》，诗曰“潮声涛涌吞江海，电闪雷轰时炫彩。迄今谁是帝王家，古冢累累终不改”。", Location = DbGeography.FromText("POINT(118.2277822495 29.2930050986)"), Icon = "zzk" });
                ch.Plays.Add(new VillagePlay { Name = "壁水观鱼·西坑", Description = "碧家河尽源头，一条流量不小的溪河穿村而过，河两边村民房屋密置。河道常年禁渔，夏至，小河清澈见底，成群成片的溪鱼在河里游来游，村民喂点食，成团的小鱼竞相争食，一幅鱼儿欢、村民乐的生态自然和谐图，是原生态河鱼观赏的绝佳之地。", Location = DbGeography.FromText("POINT(118.2156586647 29.3261045474)"), Icon = "xk" });
                ch.Plays.Add(new VillagePlay { Name = "天上人间·西山", Description = "位于库坑村西山自然村，30余户人家原生态居住在一个很高又平阔的山顶上，蓝天为顶，大地为毯，举目远望，众山尽收眼底，宽广无限，是可遇不可求的天堂邻地。", Location = DbGeography.FromText("POINT(118.2004101130 29.2987313342)"), Icon = "xs" });
                ch.Plays.Add(new VillagePlay { Name = "两省一村·河滩", Description = "浙江河滩和江西河滩两省村民共居一村，两省村民人口近800人（浙江人口300多人）。“开化”和“婺源”有最美的诗画乡村名号的两个点在这儿交汇成“河滩”村，一条溪水左为浙江地域、右为江西地域，几座小桥和谐了两省。", Location = DbGeography.FromText("POINT(118.1779329816 29.2944931710)"), Icon = "ht" });
                ch.Plays.Add(new VillagePlay { Name = "最美茶园·中山堂", Description = "该茶园有160多亩，处在半山，山腰一“小天池”，这里山水相接，云蒸雾蔚，茶芽茵绿，山色如黛。新任市委书记考察该茶园时，称中山堂茶园为“江南最美茶园”。该茶园为桃源村在1990年“路教”时种植发展的，三年一轮承包，现每年增加村集体收入10万余元，年产名茶5000多斤，产值90万元。", Location = DbGeography.FromText("POINT(118.2198423403 29.2605925451)"), Icon = "zst" });
                ch.Plays.Add(new VillagePlay { Name = "省委旧址·库坑", Description = "1935年5月，闽浙赣省委书记关英带领一个突击队在开化与赵礼生、邱老金会合，在库坑成立浙西建立的第一个中心县委——开婺休中心县委，并建立开婺休中心县游击大队，成为浙西革命根据地的中心。当时，全乡人口有4789人，建有15个党支部71名党员； 7个团支部38名团员；32个贫农团，390人参加；一个妇女会，18个地下工作队。先后与敌人激战9次，其中库坑天堂山战役最为著名，战役打了3个多小时，歼敌200余人，俘敌90余人，缴获长短枪120余支。库坑现有中共闽浙赣省委陈列室，展阵面积120平方。陈列着闽浙赣省委创始人方志敏、省委书记关英的生平事迹和在开化开展建党活动、扩大武装斗争的业绩和图片以及开化英烈的感人事迹，外有一个红军练兵场，总展览面积500平方米，有展板25块，设有一个接待服务中心。红军被服厂展出有当年村民为红军做衣服的缝纫机具、衣厨等。霞坞国民党军进剿驻地有当时国民党军队驻扎的祠堂、曾悬挂烈士头颅的红军樟。1983年1月，该旧址被列为县级重点文物保护单位。2002年4月，被列为县爱国主义教育基地，2007年1月被批准为市级爱国主义教育基地。2005年4月，被列为衢州市党史教育基地。2005年4月 ，被省委党史研究室命名省级党史教育基地。2012年库坑闽浙赣省委旧址被列为“省级爱国主义教育基地”。", Location = DbGeography.FromText("POINT(118.2029210006 29.3128785706)"), Icon = "kk" });
                ch.Plays.Add(new VillagePlay { Name = "高山古村·高田坑", Description = "海拔500多米，住有500多村民，是开化县地处海拔最高、保存最完整的原生态村落。该村地处山高地茂，夏季平均气温比其它地方要低5℃左右，有奇特壮观的原始次森林，清澈幽深的瀑布，古朴优雅的红豆杉，身在此地，一切返璞归真，是天然的高山避暑胜地。", Location = DbGeography.FromText("POINT(118.2365572646 29.3121048142)"), Icon = "gtk" });
                ch.Plays.Add(new VillagePlay { Name = "范仲淹后裔迁居地·桃源", Description = "现存桃源范氏世谱（修于乾隆15年）记载，宋朝大名人文学家、思想家范仲淹先生之孙范旭迁居大举村，繁衍后代至今已有一千余年历史。在乾隆15年（公元1750年）大举村就建有范氏宗祠。大举自然村人口600多人，全村范氏村民300多人。1995年，该村范氏村民共同集资5万多元，将范氏宗祠修葺一新，现有建筑面积300余平方米。", Location = DbGeography.FromText("POINT(118.2140610000 29.2579920000)"), Icon = "ty" });
                ch.Plays.Add(new VillagePlay { Name = "状元故里·北源", Description = "宋端拱元年(988年)，程宿18岁即登进士，殿试第一，成为史书上记载的浙江省第一位科举状元。程宿名登金榜后，遂除翰林编修，以殿中丞、直集贤院的身份参与编撰有关太子、亲皇、皇族等事迹。后对策称旨，被提升为川陕督抚、江西安抚使。他为官清正廉明，勤政爱民，深得百姓敬重。咸平三年，病死于任所，朝中士大夫莫不叹悼，连真宗皇帝也为之掉泪，诏缢号“文熙”。程宿的儿子程迪是宋仁宗庆历二年(1042)榜眼（第一名为状元、第二名为榜眼），程宿的孙子程天民于熙宁六年(1073年)考中进士，至重孙程俱宋绍圣四年(1097)参加京都进士考试，中南宫廷试甲科第一，赐上舍出身，拜礼部郎官。程俱，字致道，以太常少卿知秀州，历任秘书省少监、集贤殿编撰、徽猷阁待制。程具所作《麟台故事》、《北山集》均收入《四库全书》。据不完全统计，北源村历史考中进士有20多人，特别是宋代程家“一门四进士”全国罕见。", Location = DbGeography.FromText("POINT(118.2363450000 29.1987020000)"), Icon = "by" });

                #region 图片路径
                play_imagesPath = relativePath + "ch/play_images/";

                foreach (var play in ch.Plays)
                {
                    var directory = play_imagesPath + play.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var play_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = play_images.Length == 1;

                        foreach (var imagePath in play_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var playPicture = new PlayPicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                playPicture.Picture = picture;
                                playPicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                play.PlayPictures.Add(playPicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 农家乐

                ch.Eats.Add(new VillageEat() { Name = "醉美农庄", Address = "", Person = "", Description = "", Tel = "", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2151107 29.27662663)"), Icon = "eat",Deleted=true });
                ch.Eats.Add(new VillageEat() { Name = "金仙菜馆", Address = "长虹乡桃源村下山蛇15号", Person = "邱桃英", Description = "", Tel = "18857035780", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2151617 29.27639345)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "丽仙菜馆", Address = "长虹乡桃源村下山蛇19号", Person = "邱娣仂", Description = "", Tel = "15268076791", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2151519 29.27682628)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "红卫菜馆", Address = "长虹乡桃源村下山蛇18号", Person = "余女兰", Description = "", Tel = "15167081989", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2152905 29.27673033)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "玲玲土菜馆", Address = "长虹乡桃源村下山蛇7号", Person = "朱玲", Description = "", Tel = "13750705939", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2155372 29.27680702)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "枫云山庄", Address = "长虹乡桃源村下首删8号", Person = "", Description = "", Tel = "", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2156739 29.27638355)"), Icon = "eat" });
                ch.Eats.Add(new VillageEat() { Name = "爱珍农庄", Address = "长虹乡桃源村下山蛇4号", Person = "方爱珍", Description = "", Tel = "18757031087", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2175602 29.27650015)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "爱客来农庄", Address = "长虹乡桃源村下山蛇", Person = "邱龙炳", Description = "", Tel = "13665701403", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2177169 29.27663505)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "红苏农庄", Address = "长虹乡桃源村下山蛇2号", Person = "邱振英", Description = "", Tel = "13506707197", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2178327 29.27673797)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "福客来菜馆", Address = "长虹乡桃源村下山蛇1号", Person = "程学春", Description = "", Tel = "18767063498", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2185144 29.2767351)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "英英菜馆", Address = "长虹乡桃源村下山蛇", Person = "俞连英", Description = "", Tel = "15167086293", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.218766  29.27688889)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "开化佳艺家庭农场", Address = "长虹乡星河村", Person = "方进林", Description = "", Tel = "13757058770", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2299753 29.22029959)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "长虹民俗苑", Address = "长虹乡星河村", Person = "江建坤", Description = "", Tel = "18957026099", ReceptionNumber = 0, Level = 3, Price = 0, Location = DbGeography.FromText("POINT(118.2311617 29.22086095)"), Icon = "eat", Deleted = true });

                ch.Eats.Add(new VillageEat() { Name = "钱王客栈叁号", Address = "长虹乡真子坑村老屋基", Person = "邹丰明", Description = "", Tel = "15857036338", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.228508 29.29422)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "艳艳菜馆", Address = "长虹乡桃源村下山蛇17号", Person = "夏素英", Description = "", Tel = "18767066877", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2152046088 29.2761895696)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "台回山农庄", Address = "长虹乡桃源村台回山6号", Person = "曹顺子", Description = "", Tel = "15157063100", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2103017975 29.2764574573)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "闽台农庄", Address = "长虹乡桃源村台回山52号", Person = "张桂梅", Description = "", Tel = "15167086802", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2104412724 29.2779735140)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "龙标土菜馆", Address = "长虹乡桃源村台回山38号", Person = "吴国仙", Description = "", Tel = "15067033959", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2114229609 29.2774260517)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "落凤山庄", Address = "长虹乡桃源村台回山24号", Person = "邹林凤", Description = "", Tel = "18758982800", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2117126394 29.2774494476)"), Icon = "eat" });
                ch.Eats.Add(new VillageEat() { Name = "云雾山庄", Address = "长虹乡桃源村台回山337号", Person = "程根元", Description = "", Tel = "15857038489", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2107685019 29.2765042495)"), Icon = "eat" });
                ch.Eats.Add(new VillageEat() { Name = "峰回台客栈", Address = "长虹乡桃源村台回山", Person = "邱龙根", Description = "", Tel = "15924086431", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2115517069 29.2783993160)"), Icon = "eat" });
                ch.Eats.Add(new VillageEat() { Name = "田英山庄", Address = "长虹乡桃源村长芦61号", Person = "程田英", Description = "", Tel = "15257023563", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2127619654 29.2583806553)"), Icon = "eat", Deleted = true });
                ch.Eats.Add(new VillageEat() { Name = "江南人家", Address = "长虹乡桃源村长芦", Person = "余素君", Description = "", Tel = "15167084006", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.2131160170 29.2601543787)"), Icon = "eat", Deleted = true });

                #region 图片路径
                eat_imagesPath = relativePath + "ch/eat_images/";

                foreach (var eat in ch.Eats)
                {
                    var directory = eat_imagesPath + eat.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var eat_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = eat_images.Length == 1;

                        foreach (var imagePath in eat_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var eatPicture = new EatPicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                eatPicture.Picture = picture;
                                eatPicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                eat.EatPictures.Add(eatPicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 民宿

                ch.Lives.Add(new VillageLive() { Name = "舍予阁", Address = "长虹乡星河村", Person = "方进林", Description = "", Tel = "13757058770", BedsNumber = 3, Location = DbGeography.FromText("POINT(118.2299753 29.22029959)"), Icon = "live_hot" });
                ch.Lives.Add(new VillageLive() { Name = "长虹民俗苑", Address = "长虹乡星河村", Person = "江建坤", Description = "", Tel = "18957026099", BedsNumber = 150, Location = DbGeography.FromText("POINT(118.2290907 29.22058961)"), Icon = "live" });
                
                #region 图片路径
                live_imagesPath = relativePath + "ch/live_images/";

                foreach (var live in ch.Lives)
                {
                    var directory = live_imagesPath + live.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var live_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = live_images.Length == 1;

                        foreach (var imagePath in live_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var livePicture = new LivePicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                livePicture.Picture = picture;
                                livePicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                live.LivePictures.Add(livePicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 玩法攻略

                strategyDirectories = System.IO.Directory.GetDirectories(relativePath + "ch/strategies/");

                foreach (var directory in strategyDirectories)
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    FileInfo[] htmls = di.GetFiles("*.html");//文件夹下的.xls文件
                    if (htmls.Length > 0)
                    {

                        var strategy = new Strategy
                        {
                            Title = di.Name.Split('_')[1],
                            Date = DateTime.ParseExact(di.Name.Split('_')[0], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture),
                            Src = htmls[0].Name,
                        };

                        if (File.Exists(directory + "/logo.jpg"))
                        {
                            var strategyPicture = new StrategyPicture();

                            var byData = GetPictureData(directory + "/logo.jpg");

                            var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                            strategyPicture.Picture = picture;

                            strategy.StrategyPictures.Add(strategyPicture);
                        }


                        ch.Strategies.Add(strategy);
                    }
                }

                #endregion

                _villageService.InsertVillage(ch);

                #endregion

                #region 七里入库

                #region 基本信息
                var ql = new Village
                {
                    Name = "桃源七里",
                    Address = "浙江省钱塘江源头的衢州市西北部七里乡",
                    Desc = "桃源七里风景区东离杭州245公里，西距婺源140公里，南距武夷山270公里，北距黄山300公里。属亚热带季风气候区，常年平均气温在14.3C，四季分明、冬夏长春秋短、气候温和。区内平均海拔650米，面积60.45平方公里，森林覆盖率达98%，总人口5082人.。",
                    Location = DbGeography.FromText("POINT(118.7620700000   29.1469400000)"),
                    OpenTime = "全天开发",
                    Phone = "",
                    Tags = "AAA级景区;漂流;竹海",
                    Panorama = "ql",
                    Price = 0,
                    Icon = "ql",
                    Triffic = "",
                    TourRoute = "D1  早晨出发地出发，抵达景区，到杨坞·森林氧吧寻访杨花瀑布、感受森林负离子浴。午餐后，可前往七里·三仙圣地探访古七里的怡然古意，而后在七里排·峡谷漂流进行一次激情漂流嬉戏之旅。晚餐后，可前往游客接待中心文化广场散步纳凉，在游客中心影院内观赏院线大片，结束第一天的旅程。D2  早晨到上村千里岗观景台赏千里岗千峰竞秀、云海日出，到上村·蔬果长廊观赏奇瓜异果，然后到大头·石尖问顶游览乡村风情。午餐后，可往新店·竹海绿道悠然漫步，接着到黄土岭·三叠龙潭观赏龙潭飞瀑、狮子头瀑布的雄壮景观，之后返程。",
                    // GeoTourRoute = DbGeography.FromText("LINESTRING (119.028514065866 29.1848777616058,119.028726913651 29.1848799086025,119.028907699531 29.1848100607183,119.029056533039 29.1846539077354,119.029188465675 29.1845549105217,119.029302940581 29.1845703910883,119.029483037913 29.1845722035611,119.029579796395 29.1847308326767,119.029974762967 29.1845198088051,119.030286647614 29.1844369492818,119.030715695806 29.1840829353809,119.031093732184 29.1839290691881,119.031569463693 29.1838335018403,119.031881057974 29.1837792829735,119.032028807272 29.1837377648857,119.032193733685 29.1836104180754,119.032123004627 29.1841686684102,119.031989480462 29.1844396494739,119.032151454102 29.184627593673,119.032034968515 29.1848270870913,119.03172215232 29.1850102812681,119.031639484385 29.1850954495793,119.031819044938 29.185154575445,119.031916066533 29.1852845289049,119.032226455162 29.1853592921575,119.032503838625 29.1854623850709,119.032782025484 29.1854794833945,119.032996870064 29.1852666444922,119.033044375309 29.1854391036524,119.033092675789 29.1855255798142,119.03333852249 29.1854993544297,119.033536723807 29.1853150095895,119.033500619801 29.1856729532013,119.033482378996 29.1858734257268,119.033531484247 29.1858739185007,119.034040476507 29.1857213121902,119.034562204398 29.1859558069461,119.03492021389 29.1861886683149,119.035887023115 29.1860978940389,119.036295741644 29.186159257826,119.03655793743 29.1861331770873,119.036604374499 29.1864202761216,119.036929225624 29.1866957854103,119.03713963227 29.1869558417939,119.037252085743 29.1871862593477,119.037530107465 29.1872176570113,119.037875743274 29.1870203951584,119.038057003071 29.1868931783097,119.038430969041 29.1871691525915,119.038362956231 29.1874408025601,119.038262064909 29.1877264580753,119.038440927121 29.1878572082183,119.038623396658 29.1876010044518,119.039084937601 29.1872615423787,119.039547664741 29.1867930801145,119.039944115231 29.1864099684607,119.039835666497 29.1857496234904,119.039741051649 29.1853617293983,119.039599069288 29.1847870529083,119.039403844358 29.184656156073,119.039291000526 29.184468735957,119.039409718801 29.1840255932813,119.039215297911 29.1838087022758,119.03913638447 29.1834926207897,119.039023937295 29.1832622081542,119.039075311166 29.1830190553133,119.039452853724 29.1829080859707,119.039812412785 29.1829689174784,119.040206315367 29.1828580922258,119.040500531415 29.1829039403174)")
                    VideoSize =38005,
                    VideoUrl = "http://qzch.qz.gov.cn/qzxcly/resources/vedios/ql.mp4"
                };

                 images = System.IO.Directory.GetFiles(relativePath + "ql/images/");
                foreach (var imagePath in images)
                {
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                    var fileExt = System.IO.Path.GetExtension(imagePath);

                    if (imageExts.Contains(fileExt.ToLower()))
                    {
                        var villagePicture = new VillagePicture();

                        var byData = GetPictureData(imagePath);

                        var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                        villagePicture.Picture = picture;
                        villagePicture.IsLogo = fileName.ToUpper() == "LOGO";
                        villagePicture.IsRoute = fileName.ToUpper() == "ROUTE";

                        ql.VillagePictures.Add(villagePicture);
                    }
                }

                #endregion

                #region 服务设施
                ql.Services.Add(new Core.Domain.Villages.VillageService() { Name = "停车场", Description = "", Location = DbGeography.FromText("POINT(118.7617785879  29.1489522084)"), Icon = "parking",ServiceType=ServiceType.Park });
                ql.Services.Add(new Core.Domain.Villages.VillageService() { Name = "厕所", Description = "", Location = DbGeography.FromText("POINT(118.7618483253 29.1486664201)"), Icon = "ticket",ServiceType=ServiceType.WC });

                #region 图片路径
                service_imagesPath = relativePath + "ch/service_images/";

                foreach (var service in ch.Services)
                {
                    var directory = service_imagesPath + service.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var service_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = service_images.Length == 1;

                        foreach (var imagePath in service_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var villagePicture = new ServicePicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                villagePicture.Picture = picture;
                                villagePicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                service.ServicePictures.Add(villagePicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 农家乐
                ql.Eats.Add(new VillageEat() { Name = "笔峰大院", Address = "", Person = "", Description = "", Tel = "", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.75788381538 29.133658759857017)"), Icon = "eat" });
                ql.Eats.Add(new VillageEat() { Name = "建英农家乐", Address = "", Person = "邱桃英", Description = "", Tel = "13675710824", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.75771887974152 29.134295174896312)"), Icon = "eat" });
                ql.Eats.Add(new VillageEat() { Name = "桥头农家饭店", Address = "", Person = "赖月春", Description = "", Tel = "13004282200", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.7576988554179 29.133984933261612)"), Icon = "eat" });
                ql.Eats.Add(new VillageEat() { Name = "拾柴小屋", Address = "", Person = "", Description = "", Tel = "13732505595", ReceptionNumber = 0, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(118.75814873692632 29.13296708964365)"), Icon = "eat" });       
                #region 图片路径
                eat_imagesPath = relativePath + "ql/eat_images/";

                foreach (var eat in ql.Eats)
                {
                    var directory = eat_imagesPath + eat.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var eat_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = eat_images.Length == 1;

                        foreach (var imagePath in eat_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var eatPicture = new EatPicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                eatPicture.Picture = picture;
                                eatPicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                eat.EatPictures.Add(eatPicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 民宿

                ql.Lives.Add(new VillageLive() { Name = "黄土岭民俗宿", Address = "", Person = "", Description = "", Tel = "", BedsNumber = 0, Location = DbGeography.FromText("POINT(118.75796470471964 29.132395660016673)"), Icon = "live" });
                ql.Lives.Add(new VillageLive() { Name = "龙潭山庄", Address = "", Person = "", Description = "", Tel = "", BedsNumber = 0, Location = DbGeography.FromText("POINT(118.756163018515 29.135326079738665)"), Icon = "live_hot" });

                #region 图片路径
                live_imagesPath = relativePath + "ql/live_images/";

                foreach (var live in ql.Lives)
                {
                    var directory = live_imagesPath + live.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var live_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = live_images.Length == 1;

                        foreach (var imagePath in live_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var livePicture = new LivePicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                livePicture.Picture = picture;
                                livePicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                live.LivePictures.Add(livePicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 玩的景点
                
                ql.Plays.Add(new VillagePlay { Name = "七里排·香溪漂流", Description = "“浙西生态第一漂”七里香溪峡谷漂流位于七里乡七里排村，整条漂流河道于瀑布、深潭、巨石、密林、清流中蜿蜒2.5公里，首尾落差100多米，水道虽狭急却清浅，让您在有惊无险的刺激中尽享大自然的魅力，感受纵情山水、放飞心灵的惬意。", Location = DbGeography.FromText("POINT(118.7703861377 29.1022933757)"), Icon = "pl" });
                ql.Plays.Add(new VillagePlay { Name = "杨坞·森林氧吧", Description = "位于七里乡杨坞村，由红军烈士墓、杨花瀑布和杨坞农家民居群等景点组成，景区内流水人家，诗情画意，古木参天，郁郁葱葱。位于密林深处的杨花瀑布，其负离子值最高超过每立方厘米8万个，极具保健功能，素有“森林氧吧”之称。", Location = DbGeography.FromText("POINT(118.7801697522 29.1263842422)"), Icon = "yw" });
                ql.Plays.Add(new VillagePlay { Name = "七里·三仙圣地", Description = "地位于七里乡七里村，由三仙桥、七里古道、亲水台和观瀑亭等景点组成。相传有三位神仙下凡济世，为七里山民的淳朴善良所感动，主动为其开路造桥，因此得名三仙桥。七里古道是“衢徽古道”的重要路段，全程皆为山路，以石材铺就，留存至今。 黄土岭·三叠龙潭位于七里乡黄土岭村，七里香溪伴村而过，河道两侧农家乐林立，是桃源七里景区目前接待规模最大的农家乐经营村。", Location = DbGeography.FromText("POINT(118.7560863703 29.1351742486)"), Icon = "sxsd" });
                ql.Plays.Add(new VillagePlay { Name = "龙潭瀑布", Description = "位于七里乡黄土岭村，七里香溪伴村而过，河道两侧农家乐林立，是桃源七里景区目前接待规模最大的农家乐经营村。景区内有龙潭瀑布、观瀑桥、龙角石、香溪廊桥等多处景点。来到黄土岭，憩于农家，看溪水潺潺；漫行古道，见竹径幽幽，山里人家的风味油然而生。", Location = DbGeography.FromText("POINT(118.7551127285 29.1367064536)"), Icon = "ltpb" });
                ql.Plays.Add(new VillagePlay { Name = "新店·竹海绿道", Description = "位于七里乡新店村，全长1.7公里，共有罗家新村、新店、大头三个出入口。整条游步道贯穿无垠竹海，置身其内，您看到的是翠竹绵延、浩如烟海的竹林景致；放眼远眺，你可以欣赏到金衢日出、竹海拖云的瑰丽天象；细观脚下，你还可以发现竹笋破土、杜鹃遍野的山野情趣。", Location = DbGeography.FromText("POINT(118.7603167840 29.1454079079)"), Icon = "zh" });
              

                #region 图片路径
                play_imagesPath = relativePath + "ql/play_images/";

                foreach (var play in ql.Plays)
                {
                    var directory = play_imagesPath + play.Name + "/";
                    if (Directory.Exists(directory))
                    {
                        var play_images = System.IO.Directory.GetFiles(directory);

                        bool setLogo = play_images.Length == 1;

                        foreach (var imagePath in play_images)
                        {
                            var fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                            var fileExt = System.IO.Path.GetExtension(imagePath);

                            if (imageExts.Contains(fileExt.ToLower()))
                            {
                                var playPicture = new PlayPicture();

                                var byData = GetPictureData(imagePath);

                                var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                                playPicture.Picture = picture;
                                playPicture.IsLogo = fileName.ToUpper() == "LOGO" || setLogo;

                                play.PlayPictures.Add(playPicture);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region 玩法攻略

                strategyDirectories = System.IO.Directory.GetDirectories(relativePath + "ql/strategies/");

                foreach (var directory in strategyDirectories)
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    FileInfo[] htmls = di.GetFiles("*.html");//文件夹下的.xls文件
                    if (htmls.Length > 0)
                    {

                        var strategy = new Strategy
                        {
                            Title = di.Name.Split('_')[1],
                            Date = DateTime.ParseExact(di.Name.Split('_')[0], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture),
                            Src = htmls[0].Name,
                        };

                        if (File.Exists(directory + "/logo.jpg"))
                        {
                            var strategyPicture = new StrategyPicture();

                            var byData = GetPictureData(directory + "/logo.jpg");

                            var picture = _pictureService.InsertPicture(byData, "image/jpeg", "", "", "");

                            strategyPicture.Picture = picture;

                            strategy.StrategyPictures.Add(strategyPicture);
                        }


                        ql.Strategies.Add(strategy);
                    }
                }

                #endregion

                _villageService.InsertVillage(ql);

                #endregion

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok("finished");
        }

        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok("closed");

            var dp = _villageService.GetVillageById(1);
            var qngd = dp.Plays.Where(v => v.Id == 1).SingleOrDefault();

            qngd.Location = DbGeography.FromText("POINT(119.0282124281 29.1850908661)");

            _villagePlayService.UpdateVillagePlay(qngd);

            //if (qngd != null) qngd.AudioUrl = "https://www.luckyday.top/resources/audios/dp001.mp3";

            //var ch = _villageService.GetVillageById(1);
            //var ths = ch.Plays.Where(v => v.Id == 17).SingleOrDefault();

            //if (qngd != null) qngd.AudioUrl = "https://www.luckyday.top/resources/audios/ch001.mp3";
            

            return Ok("");
        }

        /// <summary>  
        /// 二进制流转图片  
        /// </summary>  
        /// <param name="streamByte">二进制流</param>  
        /// <returns>图片</returns>  
        [NonAction]
        private System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

        /// <summary>  
        /// 图片转二进制  
        /// </summary>  
        /// <param name="imagepath">图片地址</param>  
        /// <returns>二进制</returns>  
        [NonAction]
        private byte[] GetPictureData(string imagepath)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]   
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法   
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }

    }
}