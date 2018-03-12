using QZCHY.Core.Domain.Villages;
using QZCHY.Services.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QZCHY.API.Controllers
{
    [RoutePrefix("Demo")]
    public class DemoController : ApiController
    {
        private readonly IVillageService _villageService = null;

        public DemoController(IVillageService villageService)
        {
            _villageService = villageService;
        }

        [HttpGet]
        [Route("Import")]
        public IHttpActionResult ImportData()
        {

            var village = new Village
            {
                Name = "康养衢江· 隐柿东坪",
                Address = "衢州市衢江区峡川镇驻地峡口村东北部13公里",
                Desc = "东坪村地处衢州市衢江区峡川镇驻地峡口村东北部13公里，距衢州市区38公里，东临龙游县塔石镇金村村、西与乌石坂村相交、南与大理、李泽村为界，北与高岭村相连，地理位置险要，村落分散，村坊两旁山高林密，仅有一条乡村公路和千年古道与外界相连，村坊后背山山峰最高点海拔610米，村坊位置海拔420米，属典型的山区古村落。东坪村村史悠久，至今已有一千年左右，据《李氏宗谱》记载，高宗七子李烨为避武则天残害宗室子弟，远途跋涉，隐居福建古田长汀麻团岭，后移居浙江衢之西邑，往返于东坪与石屏源（今李泽）之间。元朝元统年间（1333—1335年）正式创业于东坪，男耕女织、读书知礼、克勤克俭，延续至今。东坪村气候四季分明，雨量充沛，属亚热带季风性气候，适宜针、阔叶林等多种作物生长。红柿、竹笋、油茶、板栗、粮食、蔬菜是东坪村的主要物产，其中红柿又是东坪村的主打产品历史悠久，源于唐末，盛产至今，其中号称“柿王”的一棵柿树，年产量均在3000斤左右，因为这里海拔高，温差大，光照足，无污染，特别适应柿树的种植、生长，柿果个大味甘，极富营养，曾作为贡品进献朝廷。相传，广为衢北百姓传诵的明朝中后期的“李泽娘娘”就是东坪人，这位娘娘入宫后的第一个中秋，望月思乡，想起家乡父老，想起家中红柿。然而，吃遍了全国各地送来的红柿，娘娘皆不如意，故决定以后的每年中秋，由衢州进贡一篮东坪红柿进皇宫。久而久之，东坪红柿便得名“贡柿”。东坪红柿品种众多，有客柿、西瓜柿、牛奶柿、鸟柿、汤瓶柿、六柿等。柿干质地柔软、肉质细嫩、霜厚均匀，含有蛋白质、糖、胡萝卜素等丰富的营养及磷、铁、钙、碘等多种微量元素，具有很高的营养及药用价值。",
                Location = System.Data.Entity.Spatial.DbGeography.FromText('POINT(119.0333890915 29.1851002329)'),
                OpenTime = "9:00-16:00",
                Phone = "18057081250",
                Tags= "AAA级景区;柿子节;康养",             
            };
            village.Plays.Add(new VillagePlay {
                 Title= "千年古道",
                 Description= "东坪千年古道位于衢州市峡川镇境内，距市区40公里。从东坪山脚下起，一条总长1500米，宽2米，共1144级的青石板古道蜿蜒盘曲伸向山顶，随着岁月的流逝，村民的脚步磨去了古道青石块的棱角而变得光滑。这就是相传具有1300多年历史的唐朝古道。",
                  

            });

            _villageService.InsertVillage(village);

            return Ok("finished");
        }

    }
}
