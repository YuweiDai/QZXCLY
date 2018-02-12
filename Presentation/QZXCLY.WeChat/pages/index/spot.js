// pages/index/spot.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    spot:{
      name:"康养衢江· 隐柿东坪",      
      tags: ["AAA级景区", "柿子节", "康养"],
      price:"免费",
      address:'衢州市衢江区峡川镇驻地峡口村东北部13公里',
      phone:'13705706658',
      opentime:'9:00-16:00',
      desc:'东坪村地处衢州市衢江区峡川镇驻地峡口村东北部13公里，距衢州市区38公里，东临龙游县塔石镇金村村、西与乌石坂村相交、南与大理、李泽村为界，北与高岭村相连，地理位置险要，村落分散，村坊两旁山高林密，仅有一条乡村公路和千年古道与外界相连，村坊后背山山峰最高点海拔610米，村坊位置海拔420米，属典型的山区古村落。东坪村村史悠久，至今已有一千年左右，据《李氏宗谱》记载，高宗七子李烨为避武则天残害宗室子弟，远途跋涉，隐居福建古田长汀麻团岭，后移居浙江衢之西邑，往返于东坪与石屏源（今李泽）之间。元朝元统年间（1333—1335年）正式创业于东坪，男耕女织、读书知礼、克勤克俭，延续至今。东坪村气候四季分明，雨量充沛，属亚热带季风性气候，适宜针、阔叶林等多种作物生长。红柿、竹笋、油茶、板栗、粮食、蔬菜是东坪村的主要物产，其中红柿又是东坪村的主打产品历史悠久，源于唐末，盛产至今，其中号称“柿王”的一棵柿树，年产量均在3000斤左右，因为这里海拔高，温差大，光照足，无污染，特别适应柿树的种植、生长，柿果个大味甘，极富营养，曾作为贡品进献朝廷。相传，广为衢北百姓传诵的明朝中后期的“李泽娘娘”就是东坪人，这位娘娘入宫后的第一个中秋，望月思乡，想起家乡父老，想起家中红柿。然而，吃遍了全国各地送来的红柿，娘娘皆不如意，故决定以后的每年中秋，由衢州进贡一篮东坪红柿进皇宫。久而久之，东坪红柿便得名“贡柿”。东坪红柿品种众多，有客柿、西瓜柿、牛奶柿、鸟柿、汤瓶柿、六柿等。柿干质地柔软、肉质细嫩、霜厚均匀，含有蛋白质、糖、胡萝卜素等丰富的营养及磷、铁、钙、碘等多种微量元素，具有很高的营养及药用价值。',
      star:true,
      photos: [
        {
          id: 3,
          img: '/resources/images/index/dp.jpg',
        },
        {
          id: 1,
          img: '/resources/images/index/b2.png',
        },
        {
          id: 2,
          img: '/resources/images/index/b3.png',
        }
      ],
      current_photo:1      
    },
    filterId:2,
    play_list:[
      {
        title:"浙江省农家乐示范村——黄土岭",
        descriptions: "是典型的浙西山里人家民居村落，以村入口竹牌门楼始，1公里的谷地山间林中，点点民居，散落其中，廊桥横跨香溪，恰似一幅江南山水丹青画卷。由于山高谷深、空气凉爽，素有“小东北”之称。境内有山外山庄（黄土岭自然村）、涧石听泉、香溪观瀑、飞来奇石、际头峡谷、狮头瀑布、树王参天（浙江杉树王）、蝶鸟飞涧等自然景观。山清水秀，民风纯补，是一个休闲、避暑、度假的理想场所。",
        logo:"/resources/images/index/l1.jpg"
      },
      {
        title: "国家AA级旅游景区——杨坞村",
        descriptions: "杨坞村，旅游资源丰富，有民居建筑群、花岩林业观光园、三叠龙潭、杨花瀑布、红军墓、阳花尖等自然和人文景点。游览途中可一路观赏毛竹林、板栗林、柑橘林、红军墓、红豆杉、三叠龙潭和杨花瀑布等景点，感受自然的山野风光、绵绵不断地经济林景观和清澈透凉的溪流飞瀑。登上海拔1110米的阳花尖，峰峦奇秀、百里田畴和村落历历在目。返回后，到清风厅小憩，赏乡村风貌、品农家茶点、感受如诗山里人家。",
        logo: "/resources/images/index/l2.png"
      },
      {
        title: "深山状元村——均良",
        descriptions: "“笔架峰下诗书读；均良福地文运昌”便是均良村（文昌福地）的最佳诠释。自1970年来全村共培养出大中专生、本科生、研究生、和博士47名，其中博士后4名，硕士生6名,全村只有350人，占全村总人口数的13%左右。可谓是人才辈出，文运昌盛。中国传统的“耕读文化”在这个小山村里被演绎成一段“知识改变命运”的佳话。因此，当地人们称均良村为“状元村”。",
        logo: "/resources/images/index/l1.jpg"
      },
      {
        title: "高山蔬菜村——上村",
        descriptions: "南北延伸约数里的七里尖高山蔬菜基地，位于海拔800～1200m的上村，是省级无公害蔬菜基地，省级特色农业精品园区。高海拔、低气温、高含氧的特殊地理条件，使得盛产于此的茄子、四季豆、豌豆、西红柿、黄瓜，富含维生素、矿物质、蛋白质、碳水化合物。",
        logo: "/resources/images/index/l2.png"
      }                 
    ],
 
  eat_list:[
    {
      title: "桃源七里之山外山庄",
      descriptions: "衢州市七里乡黄土岭村7号",
      tel:"13362121222",
      person:"罗方剑",
      logo: "/resources/images/index/l1.jpg"
    },
    {
      title: "老张饭店",
      descriptions: "衢州市七里乡黄土岭村7号",
      logo: "/resources/images/index/l2.png"
    },
    {
      title: "老陈饭店",
      descriptions: "衢州市七里乡黄土岭村7号",
      logo: "/resources/images/index/l1.jpg"
    },
    {
      title: "老赵饭店",
      descriptions: "衢州市七里乡黄土岭村7号",
      logo: "/resources/images/index/l2.png"
    }                 
  ],
  live_list:[
    {
      title: "圃舍·源溪",
      descriptions: "柯城区九华乡新宅村大荫山下",
      tel: "13705706658",
      person: "罗方剑",
      logo: "/resources/images/index/l1.jpg"
    },
    {
      title: "庙源溪墅",
      descriptions: "九华乡茶铺村",
      tel: "18906708718",
      person: "罗方剑",      
      logo: "/resources/images/index/l2.png"
    },
    {
      title: "关西山房",
      descriptions: "九华乡茶铺村",
      tel: "18906708718",
      person: "罗方剑",   
      logo: "/resources/images/index/l1.jpg"
    },
    {
      title: "溢舍",
      descriptions: "九华乡茶铺村",
      tel: "18906708718",
      person: "罗方剑",   
      logo: "/resources/images/index/l2.png"
    }    
  ],
  },
  tapFilter: function (e) {    
    this.setData({
      filterId: e.target.dataset.id, 
    });
  },
  makePhoneCall:function(event){
    var phone = event.currentTarget.dataset.phone;
     wx.showModal({
      title: '提示',
      content: '是否拨打电话 ' + phone+' ?',
      success: function (res) {
        if (res.confirm) {
          wx.makePhoneCall({
            phoneNumber: phone
          }); 
        }
      }
    })
  },
  navTo: function (event) {
    wx.redirectTo({
      url: '../map/nav',
    })
  },
  starSpot:function(event)
  {
    var spot=this.data.spot;
    spot.star=!spot.star;
    this.setData(
      {
        spot: spot
      }
    );
    var title=spot.star?'收藏成功':'取消收藏';
    wx.showToast({
      title: title,
      icon: 'none',
      duration: 2000
    })
  },
  photoChange:function(event)
  {
    var spot = this.data.spot;
    spot.current_photo= event.detail.current+1;
    this.setData(
      {
        spot: spot
      }
    );
    console.log(event.detail);
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
  
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    var that=this;
    wx.setNavigationBarTitle({
      title: that.data.spot.name
    });
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
  
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {
  
  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {
  
  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {
  
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
  
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {
  
  }
})