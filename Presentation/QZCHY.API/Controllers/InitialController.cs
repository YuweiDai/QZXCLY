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
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Web.Http;

namespace QZCHY.API.Controllers
{
    [RoutePrefix("Initial")]
    public class InitialController : ApiController
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

        private readonly IVillageService _villageService = null;

        public InitialController(IAuthenticationService authenticationService, IAccountUserService customerService,
            IAccountUserRegistrationService customerRegistrationService, IGenericAttributeService genericAttributeService,
       IWorkflowMessageService workflowMessageService, IEncryptionService encryptionService, IPictureService pictureService,
        IWebHelper webHelper,IWorkContext workContext,
        AccountUserSettings customerSettings, CommonSettings commonSettings, SecuritySettings securitySettings, ISettingService settingService,
        IVillageService villageService)
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
            var relativePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/Imports/");
            var imageExts = new List<string>
            {
                ".png", ".jpg", ".bmp", ".gif"
            };

            try
            {

                #region 东坪入库

                //基本信息
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
                    Triffic = "",
                    TourRoute = "Day 1：下停车场--古道入口--山涧戏水--古树观景--古树群--火烧红枫--唐官文化岩壁石刻--东坪村山寨门-游客接待中心--民俗文化广场--古村漫游--东坪柿林--农耕乐园--农家住宿；Day 2 古村--竹文化园--竹林素拓基地--仙岩古寺遗址--天池--徒步驿站；",
                    GeoTourRoute = DbGeography.FromText("LINESTRING(119.028514065866 29.1848777616058,119.028726913651 29.1848799086025)"),
                    //DbGeography.FromText("LINESTRING (119.028514065866 29.1848777616058,119.028726913651 29.1848799086025,119.028907699531 29.1848100607183,119.029056533039 29.1846539077354,119.029188465675 29.1845549105217,119.029302940581 29.1845703910883,119.029483037913 29.1845722035611,119.029579796395 29.1847308326767,119.029974762967 29.1845198088051,119.030286647614 29.1844369492818,119.030715695806 29.1840829353809,119.031093732184 29.1839290691881,119.031569463693 29.1838335018403,119.031881057974 29.1837792829735,119.032028807272 29.1837377648857,119.032193733685 29.1836104180754,119.032123004627 29.1841686684102,119.031989480462 29.1844396494739,119.032151454102 29.184627593673,119.032034968515 29.1848270870913,119.03172215232 29.1850102812681,119.031639484385 29.1850954495793,119.031819044938 29.185154575445,119.031916066533 29.1852845289049,119.032226455162 29.1853592921575,119.032503838625 29.1854623850709,119.032782025484 29.1854794833945,119.032996870064 29.1852666444922,119.033044375309 29.1854391036524,119.033092675789 29.1855255798142,119.03333852249 29.1854993544297,119.033536723807 29.1853150095895,119.033500619801 29.1856729532013,119.033482378996 29.1858734257268,119.033531484247 29.1858739185007,119.034040476507 29.1857213121902,119.034562204398 29.1859558069461,119.03492021389 29.1861886683149,119.035887023115 29.1860978940389,119.036295741644 29.186159257826,119.03655793743 29.1861331770873,119.036604374499 29.1864202761216,119.036929225624 29.1866957854103,119.03713963227 29.1869558417939,119.037252085743 29.1871862593477,119.037530107465 29.1872176570113,119.037875743274 29.1870203951584,119.038057003071 29.1868931783097,119.038430969041 29.1871691525915,119.038362956231 29.1874408025601,119.038262064909 29.1877264580753,119.038440927121 29.1878572082183,119.038623396658 29.1876010044518,119.039084937601 29.1872615423787,119.039547664741 29.1867930801145,119.039944115231 29.1864099684607,119.039835666497 29.1857496234904,119.039741051649 29.1853617293983,119.039599069288 29.1847870529083,119.039403844358 29.184656156073,119.039291000526 29.184468735957,119.039409718801 29.1840255932813,119.039215297911 29.1838087022758,119.03913638447 29.1834926207897,119.039023937295 29.1832622081542,119.039075311166 29.1830190553133,119.039452853724 29.1829080859707,119.039812412785 29.1829689174784,119.040206315367 29.1828580922258,119.040500531415 29.1829039403174)")
//POINT(119.0333890915 29.1851002329)


                    //GeoTourRoute = DbGeography.FromText("MULTILINESTRING((119.028514065866 29.1848777616058,119.028726913651 29.1848799086025,119.028907699531 29.1848100607183,119.029056533039 29.1846539077354,119.029188465675 29.1845549105217,119.029302940581 29.1845703910883,119.029483037913 29.1845722035611,119.029579796395 29.1847308326767,119.029974762967 29.1845198088051,119.030286647614 29.1844369492818,119.030715695806 29.1840829353809,119.031093732184 29.1839290691881,119.031569463693 29.1838335018403,119.031881057974 29.1837792829735,119.032028807272 29.1837377648857,119.032193733685 29.1836104180754,119.032123004627 29.1841686684102,119.031989480462 29.1844396494739,119.032151454102 29.184627593673,119.032034968515 29.1848270870913,119.03172215232 29.1850102812681,119.031639484385 29.1850954495793,119.031819044938 29.185154575445,119.031916066533 29.1852845289049,119.032226455162 29.1853592921575,119.032503838625 29.1854623850709,119.032782025484 29.1854794833945,119.032996870064 29.1852666444922,119.033044375309 29.1854391036524,119.033092675789 29.1855255798142,119.03333852249 29.1854993544297,119.033536723807 29.1853150095895,119.033500619801 29.1856729532013,119.033482378996 29.1858734257268,119.033531484247 29.1858739185007,119.034040476507 29.1857213121902,119.034562204398 29.1859558069461,119.03492021389 29.1861886683149,119.035887023115 29.1860978940389,119.036295741644 29.186159257826,119.03655793743 29.1861331770873,119.036604374499 29.1864202761216,119.036929225624 29.1866957854103,119.03713963227 29.1869558417939,119.037252085743 29.1871862593477,119.037530107465 29.1872176570113,119.037875743274 29.1870203951584,119.038057003071 29.1868931783097,119.038430969041 29.1871691525915,119.038362956231 29.1874408025601,119.038262064909 29.1877264580753,119.038440927121 29.1878572082183,119.038623396658 29.1876010044518,119.039084937601 29.1872615423787,119.039547664741 29.1867930801145,119.039944115231 29.1864099684607,119.039835666497 29.1857496234904,119.039741051649 29.1853617293983,119.039599069288 29.1847870529083,119.039403844358 29.184656156073,119.039291000526 29.184468735957,119.039409718801 29.1840255932813,119.039215297911 29.1838087022758,119.03913638447 29.1834926207897,119.039023937295 29.1832622081542,119.039075311166 29.1830190553133,119.039452853724 29.1829080859707,119.039812412785 29.1829689174784,119.040206315367 29.1828580922258,119.040500531415 29.1829039403174),(119.029932720221 29.1802770025183,119.030520401122 29.1804692140957,119.031075685954 29.1806181066996,119.031337360659 29.1806493890124,119.031398545894 29.1811086357637,119.031479325093 29.181224104902,119.031858020567 29.1809985697732,119.032139562483 29.1806573989296,119.032437464596 29.1803163909378,119.032701009625 29.1801470282241,119.033030299192 29.1799496506814,119.033178978719 29.1798078050247,119.033309138315 29.1798950931101,119.033192397757 29.1801232511645,119.033272904891 29.1802673782973,119.033517258942 29.1803987976482,119.033877674163 29.1803737188075,119.0341728786 29.1803193099318,119.034350806865 29.1805504022773,119.034266007079 29.1808648741595,119.034000593686 29.1812348834112,119.033736520609 29.1814615757411,119.033715853954 29.1819200082233,119.033944778168 29.1819509472805,119.034583363125 29.18194294201,119.034893193406 29.1820750089256,119.034923649056 29.1823189552246,119.034560688738 29.1826163425168,119.034508363663 29.1829598032289,119.034523263634 29.1831176127592,119.03478532299 29.1831058735697,119.035161033417 29.1831955819783,119.035879313659 29.1834176635912,119.036779780446 29.1834121977872,119.037107303956 29.1834010858445,119.037236654386 29.1835743447341,119.037759270057 29.1837084658187,119.038070822154 29.1836541917738,119.038432265044 29.1835144090258,119.038578119078 29.1836734952755,119.039075922468 29.1833687320813,119.039023937295 29.1832622081542	),(119.034350806865 29.1805504022773,119.033923299268 29.1807468149831,119.033248221399 29.1811557467101,119.03267188136 29.181508313545,119.032276155165 29.1818053531115,119.032142492278 29.1820906655141,119.031909262313 29.182518313667,119.031856118246 29.1829477542997,119.031622608138 29.1834040617688,119.031488943085 29.1836893720222,119.031367939836 29.183873980252))")
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

                        dp.VillagePictures.Add(villagePicture);
                    }
                }

                #region 服务设施

                dp.Services.Add(new Core.Domain.Villages.VillageService() { Title = "山脚停车场", Description = "", Location = DbGeography.FromText("POINT(119.028336339932 29.185065206383)"), Icon = "parking" });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Title = "山上停车场", Description = "", Location = DbGeography.FromText("POINT(119.033093070437 29.1872311309559)"), Icon = "parking" });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Title = "游客接待中心", Description = "", Location = DbGeography.FromText("POINT(119.032570530284 29.1853340591726)"), Icon = "ticket" });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Title = "山脚厕所", Description = "", Location = DbGeography.FromText("POINT(119.028073811557 29.1847519536177)"), Icon = "wc" });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Title = "山上厕所", Description = "", Location = DbGeography.FromText("POINT(119.032515188008 29.1852134615949)"), Icon = "wc" });
                dp.Services.Add(new Core.Domain.Villages.VillageService() { Title = "红芬小卖部", Description = "", Location = DbGeography.FromText("POINT(119.032749318041 29.1854837977747)"), Icon = "wc" });

                #region 图片路径
                var service_imagesPath = relativePath + "dp/service_images/";

                foreach (var service in dp.Services)
                {
                    var directory = service_imagesPath + service.Title + "/";
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
                    Title = "千年古道",
                    Description = "东坪千年古道位于衢州市峡川镇境内，距市区40公里。从东坪山脚下起，一条总长1500米，宽2米，共1144级的青石板古道蜿蜒盘曲伸向山顶，随着岁月的流逝，村民的脚步磨去了古道青石块的棱角而变得光滑。这就是相传具有1300多年历史的唐朝古道。",
                    Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.0333890915 29.1851002329)"),
                    Icon = "play"
                });

                dp.Plays.Add(new VillagePlay { Title = "龙石潭", Description = "相传，南宋末年的一天，东坪仙岩寺内有个老和尚，掐指一算，得知近期东坪山要出“龙”。传说出“龙”之地，意味着山洪爆发，村民就要遭殃。村民们一时惊慌失措，忙问老和尚有何破解之法。老和尚说：“龙刚从山洞里出来时，只是一条小泥鳅，要到河、沟里经过雷电闪烁才会变成大龙。”于是族长马上叫大家上山砍毛竹，把毛竹对半剖开，去掉骨节，接成水笕接到山洞里，由村民一根一根把每节水笕接到山脚。谁知快到山脚时，有个村民疲倦难当，认为接到此处应该差不多了，于是坐下来休息。恰恰这时，“龙”像一根泥鳅粗细慢慢地沿水管游出来，至水管中断处，“噔”地一声掉到山沟里。“龙”受惊，立马幻化身形，变成大龙游出去，落地之处至今留有一大坑，当地人称“龙石潭”。", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.029138979441 29.1847379412537)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "古樟指路", Description = "李烨在东坪定居以后，当年一些私下的挚友就去拜访李烨，当他们走到山脚下的时候，因为草木茂盛，不知道往那里走的时候，前面一颗樟树似乎知晓来人并无敌意，只是前来拜访李烨，根部突生一根枝节，枝节伸展的方向正好对着现东坪山，沿着枝节指引的方向，顺利来到李烨居住的地方，众挚友不无感慨： “真是天佑李烨”——上天都还眷顾着李烨。", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.029467879878 29.184588373659)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "神狮守道", Description = "自从李烨一行在此定居之后，方圆百里的鸟兽听闻有皇室血统的贵人居住在此地，纷纷前来保护贵人的安全，神狮带着的孩子也来到东坪，守护上山的必经之路，日日夜夜的守护山道，提防有人上山危害贵人的生命安全，一旦有异常可以及时警示山上的人做好抵御的准备。神狮也非常疼爱自己的孩子，担心小狮子受累，让其睡在自己的身子上，，而自己却依然恪职敬守。", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.029467879878 29.184588373659)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "神镜石", Description = "据《衢县志》等史料记载，武则天在位时，为防武后残害，一批（李姓）宗室外迁。李治（唐高宗）第七子李烨，从长安远避福建古田长河麻团岭。唐中宗时（公元705—709）为防止遭到进一步的迫害，李烨携家属以及亲信从麻团岭转迁，沿着现东坪线一路寻找新的定居点，当一行人经过（现东坪山脚下）时，发现一潭清水，清澈见底，味道甘甜，决定就地休整之后继续找寻。休息之时，李烨四处查看地形，当走到这块石头（神镜石）跟前的时候，不由自主停住脚步，审视此石，此石突显灵性，仿佛就是在等待主人的到来，大放异光，幻化成一块镜子，从镜子中隐约可以看到（现东坪山上）散发出阵阵的金光，李烨心想莫非是上天的指示，要我去山顶？要我去山顶定居？李烨毫不迟疑带领家属以及亲信向山顶进发，登上山顶一看，果然是块风水宝地，地理位置优越，居高临下，易守难攻，并且环境优美，茂林修竹，空气清新。远离朝廷的纷争，适宜清修，同时这里水资源非常丰富，适合农业生产，李烨勘察之后，聚集亲信前来商议，众人都十分钟情这块风水宝地，于是李烨决定定居此处。", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.030056939189 29.1846325118199)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "双枫迎客", Description = "李烨定居东坪之后，已经脱离朝廷的残害，心神俱宁，每日在犹如仙境的东坪吟诗作对，久而久之李烨心想能有更多的人来到这块仙境，一起修身养性、肆意酣畅，开展居住地的建设多好啊，上天好像能够读懂李烨的心思，派两位神仙化作枫树于现东坪古道，一只手化作遒劲的树枝，迎接过往来客，邀请客人作客东坪，领略山中秀色。", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.031879550028 29.1835041602798)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "唐官文化岩壁石刻", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.032201787321 29.1840615619858)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "东坪村山寨门", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.032291278184 29.1854316034564)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "民俗文化广场", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.032800008211 29.1853076821093)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "东坪柿林", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.033592139316 29.1863904827778)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "农耕乐园", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.033489892298 29.1868194415728)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "竹文化园", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.032023322173 29.1825767794082)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "竹林素拓基地", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.030081858842 29.1800921751621)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "天池", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.038584168861 29.1865400314482)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "徒步驿站", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.039391610273 29.1842117381383)"), Icon = "play" });
                dp.Plays.Add(new VillagePlay { Title = "李氏宗祠", Description = "暂无介绍", Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(119.032999154672 29.1858501345983)"), Icon = "play" });

                #region 图片路径
                var play_imagesPath = relativePath + "dp/play_images/";

                foreach (var play in dp.Plays)
                {
                    var directory = play_imagesPath + play.Title + "/";
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

                dp.Eats.Add(new VillageEat() { Title = "红枫堂", Address = "峡川镇东坪村", Person = "李延标", Description = "", Tel = "0570-2610068", ReceptionNumber = 60, Level = 3, Price = 0, Location = DbGeography.FromText("POINT(119.032699213792 29.1853854672489)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "外婆家", Address = "峡川镇东坪村", Person = "郑银英", Description = "", Tel = "13506708395", ReceptionNumber = 40, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "柿民客栈", Address = "峡川镇东坪村", Person = "李诗毅", Description = "", Tel = "666486", ReceptionNumber = 200, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033435575377 29.1841740615007)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "荷塘轩", Address = "峡川镇东坪村", Person = "姚银花", Description = "", Tel = "13575661123", ReceptionNumber = 20, Level = 2, Price = 0, Location = DbGeography.FromText("POINT(119.0329237403 29.1854197393375)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "古道居", Address = "峡川镇东坪村", Person = "李招耀", Description = "", Tel = "13615702342", ReceptionNumber = 40, Level = 2, Price = 0, Location = DbGeography.FromText("POINT(119.032292242207 29.1854258964995)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "东坪院", Address = "峡川镇东坪村", Person = "姚水仙", Description = "", Tel = "13819019293", ReceptionNumber = 50, Level = 3, Price = 0, Location = DbGeography.FromText("POINT(119.033051947207 29.1852506910921)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "华兰阁", Address = "峡川镇东坪村", Person = "李国华", Description = "", Tel = "13676611662", ReceptionNumber = 50, Level = 3, Price = 0, Location = DbGeography.FromText("POINT(119.033121602618 29.1860496505277)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "红柿斋", Address = "峡川镇东坪村", Person = "李诗良", Description = "", Tel = "15957004737", ReceptionNumber = 40, Level = 2, Price = 0, Location = DbGeography.FromText("POINT(119.033222881785 29.1855682447133)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "米兰农庄", Address = "峡川镇东坪村", Person = "危米兰", Description = "", Tel = "13511405431", ReceptionNumber = 60, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.03248017981 29.1856409383757)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "冬雪苑", Address = "峡川镇东坪村", Person = "杨渭花", Description = "", Tel = "15924079115", ReceptionNumber = 50, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.032932747381 29.1847460420451)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "松清阁", Address = "峡川镇东坪村", Person = "黄花化", Description = "", Tel = "15215790797", ReceptionNumber = 40, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033036399867 29.1854129703936)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "李家庄", Address = "峡川镇东坪村", Person = "李荣华", Description = "", Tel = "15857082393", ReceptionNumber = 20, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033396799926 29.1842951926885)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "满意楼", Address = "峡川镇东坪村", Person = "饶志英", Description = "", Tel = "13857018246", ReceptionNumber = 160, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.033041670312 29.1856980166666)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "驴友之家", Address = "峡川镇东坪村", Person = "章银兰", Description = "", Tel = "15372723626", ReceptionNumber = 50, Level = 0, Price = 0, Location = DbGeography.FromText("POINT(119.032658398439 29.1858833715623)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "隐柿东坪", Address = "峡川镇东坪村", Person = "余夏仙", Description = "", Tel = "13867478563", ReceptionNumber = 200, Level = 0, Price = 20, Location = DbGeography.FromText("POINT(119.033311824464 29.1855339146446)"), Icon = "eat" });
                dp.Eats.Add(new VillageEat() { Title = "叙月楼", Address = "峡川镇东坪村", Person = "李雪堂", Description = "", Tel = "13819010468", ReceptionNumber = 15, Level = 0, Price = 20, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "eat" });

                #region 图片路径
                var eat_imagesPath = relativePath + "dp/eat_images/";

                foreach (var eat in dp.Eats)
                {
                    var directory = eat_imagesPath + eat.Title + "/";
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

                dp.Lives.Add(new VillageLive() { Title = "隐柿东坪", Address = "峡川镇东坪村", Person = "余夏仙", Description = "", Tel = "13867478563", BedsNumber = 3, Location = DbGeography.FromText("POINT(119.033311824464 29.1855339146446)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "红枫堂", Address = "峡川镇东坪村", Person = "李延标", Description = "", Tel = "0570-2610068", BedsNumber = 8, Location = DbGeography.FromText("POINT(119.032699213792 29.1853854672489)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "古道居", Address = "峡川镇东坪村", Person = "李招耀", Description = "", Tel = "13615702342", BedsNumber = 4, Location = DbGeography.FromText("POINT(119.032292242207 29.1854258964995)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "华兰阁", Address = "峡川镇东坪村", Person = "李国华", Description = "", Tel = "13676611662", BedsNumber = 11, Location = DbGeography.FromText("POINT(119.033121602618 29.1860496505277)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "米兰农庄", Address = "峡川镇东坪村", Person = "危米兰", Description = "", Tel = "13511405431", BedsNumber = 5, Location = DbGeography.FromText("POINT(119.03248017981 29.1856409383757)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "冬雪苑", Address = "峡川镇东坪村", Person = "杨渭花", Description = "", Tel = "15957004737", BedsNumber = 5, Location = DbGeography.FromText("POINT(119.032932747381 29.1847460420451)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "松清阁", Address = "峡川镇东坪村", Person = "黄花化", Description = "", Tel = "15924079115", BedsNumber = 3, Location = DbGeography.FromText("POINT(119.033036399867 29.1854129703936)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "李家庄", Address = "峡川镇东坪村", Person = "李荣华", Description = "", Tel = "15857082393", BedsNumber = 10, Location = DbGeography.FromText("POINT(119.033396799926 29.1842951926885)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "外婆家", Address = "峡川镇东坪村", Person = "郑银英", Description = "", Tel = "13506708395", BedsNumber = 4, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "满意楼", Address = "峡川镇东坪村", Person = "饶志英", Description = "", Tel = "13857018246", BedsNumber = 12, Location = DbGeography.FromText("POINT(119.033041670312 29.1856980166666)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "驴友之家", Address = "峡川镇东坪村", Person = "章银兰", Description = "", Tel = "15372723626", BedsNumber = 14, Location = DbGeography.FromText("POINT(119.032658398439 29.1858833715623)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "柿民客栈", Address = "峡川镇东坪村", Person = "李诗毅", Description = "", Tel = "666486", BedsNumber = 18, Location = DbGeography.FromText("POINT(119.033435575377 29.1841740615007)"), Icon = "live" });
                dp.Lives.Add(new VillageLive() { Title = "叙月楼", Address = "峡川镇东坪村", Person = "李雪堂", Description = "", Tel = "13819010468", BedsNumber = 10, Location = DbGeography.FromText("POINT(119.032800146875 29.185509287482)"), Icon = "live" });

                #region 图片路径
                var live_imagesPath = relativePath + "dp/live_images/";

                foreach (var live in dp.Lives)
                {
                    var directory = live_imagesPath + live.Title + "/";
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

                _villageService.InsertVillage(dp);
                #endregion

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok("finished");
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