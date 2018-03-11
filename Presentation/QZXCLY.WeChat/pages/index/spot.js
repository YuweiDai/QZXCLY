// pages/index/spot.js
var innerAudioContext = null;
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
      strategies: [
        { id: 0, title: "摘柿子，赏枫叶，走古道，东坪等你来！", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/dp.jpg", src: "https://m.sohu.com/a/199887818_99961751/?pvid=000115_3w_a"  },
        { id: 1, title: "隐柿东坪•图说2017", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ql.jpg", src: "https://m.sohu.com/a/199887818_99961751/?pvid=000115_3w_a"  },
      ],       
      photos: [
        {
          id: 3,
          src: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/dp.jpg'
        },
        {
          id: 1,
          src: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b2.png',       
        },
        {
          id: 2,
          src: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b3.png',        
        }
      ],
      current_photo:1,
      play_list: [
        {
          id:0,
          title: "千年古道",
          descriptions: "东坪千年古道位于衢州市峡川镇境内，距市区40公里。从东坪山脚下起，一条总长1500米，宽2米，共1144级的青石板古道蜿蜒盘曲伸向山顶，随着岁月的流逝，村民的脚步磨去了古道青石块的棱角而变得光滑。这就是相传具有1300多年历史的唐朝古道。",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/dpgd_logo.jpg",
          audio: "http://sc1.111ttt.cn/2017/1/11/11/304112004168.mp3",
          panorama: "www.luckyday.top/resources/panoramas/index.html"
        },
        {
          id: 1,          
          title: "山涧溪水",
          descriptions: "景点文字介绍",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png",
          audio:"http://sc1.111ttt.cn/2017/1/11/11/304112002493.mp3"
        },
        {
          id: 2,          
          title: "古树景观",
          descriptions: "景点文字介绍",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l1.jpg",
          audio: "http://sc1.111ttt.cn/2017/1/11/11/304112003137.mp3"
        },
        {
          id: 3,          
          title: "古树群",
          descriptions: "景点文字介绍",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png"
        },
        {
          id: 4,          
          title: "火烧红枫",
          descriptions: "景点文字介绍",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png"
        },
        {
          id: 5,          
          title: "唐官文化岩壁石刻",
          descriptions: "景点文字介绍",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l1.jpg"
        },
        {
          id: 6,          
          title: "名宿文化广场",
          descriptions: "景点文字介绍",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png"
        }

      ],
      eat_list: [
        {
          id: 13,
          title: "隐柿东坪",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13867478563",
          price: "44",
          hot:true,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/ysdp.bmp"
        },        
        {
          id: 0,          
          title: "红枫堂",
          descriptions: "衢州市峡川镇东坪村",
          tel: "0570—2610068",
          level:3,
          price: "45",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hft.bmp"
        },
        {
          id: 1,          
          title: "荷塘轩",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13575661123",
          level: 2,
          price: "51",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/htx.bmp"
        },
        {
          id: 2,          
          title: "古道居",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13615702342",
          level: 2,
          price: "31",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/gdj.bmp"
        },
        {
          id: 3,          
          title: "东坪院",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13819019293",
          level: 3,
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/dpy.bmp"
        },
        {
          id: 4,
          title: "华兰阁",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13676611662",
          level: 3,
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hlg.bmp"
        },
        {
          id: 5,
          title: "红柿斋",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15957004737",
          level: 2,
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hsz.bmp"
        },
        {
          id: 6,
          title: "米兰农庄",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13511405431",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/mlnz.bmp"
        },
        {
          id: 7,
          title: "冬雪苑",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15924079115",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/dxy.bmp"
        },
        {
          id: 8,
          title: "松清阁",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15215790797",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/sqg.bmp"
        },
        {
          id: 9,
          title: "李家庄",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15857082393",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/ljz.bmp"
        },
        {
          id: 10,
          title: "外婆家",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13857018246",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/wpj.bmp"
        },
        {
          id: 11,
          title: "满意楼",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13857018246",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/myl.bmp"
        },
        {
          id: 12,
          title: "驴友之家",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15372723626",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/lyzj.bmp"
        },
        {
          id: 12,
          title: "柿民客栈",
          descriptions: "衢州市峡川镇东坪村",
          tel: "666486",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/smkz.bmp"
        },
        {
          id: 14,
          title: "叙月楼",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13819010468",
          price: "44",
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/xyl.bmp"
        }
      ],
      live_list: [
        {
          id: 13,
          title: "隐柿东坪",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13867478563",
          bedCount: 3,
          hot: true,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/ysdp.bmp"
        },
        {
          id: 0,
          title: "红枫堂",
          descriptions: "衢州市峡川镇东坪村",
          tel: "0570—2610068",
          level: 3,
          bedCount: 8,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hft.bmp"
        },
        {
          id: 2,
          title: "古道居",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13615702342",
          level: 2,
          bedCount: 4,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/gdj.bmp"
        },
        {
          id: 4,
          title: "华兰阁",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13676611662",
          level: 3,
          bedCount: 11,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/hlg.bmp"
        },
        {
          id: 6,
          title: "米兰农庄",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13511405431",
          bedCount: 5,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/mlnz.bmp"
        },
        {
          id: 7,
          title: "冬雪苑",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15924079115",
          bedCount:5,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/dxy.bmp"
        },
        {
          id: 8,
          title: "松清阁",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15215790797",
          bedCount: 3,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/sqg.bmp"
        },
        {
          id: 9,
          title: "李家庄",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15857082393",
          bedCount: 10,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/ljz.bmp"
        },
        {
          id: 10,
          title: "外婆家",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13857018246",
          bedCount: 4,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/wpj.bmp"
        },
        {
          id: 11,
          title: "满意楼",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13857018246",
          bedCount: 14,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/myl.bmp"
        },
        {
          id: 12,
          title: "驴友之家",
          descriptions: "衢州市峡川镇东坪村",
          tel: "15372723626",
          bedCount: 14,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/lyzj.bmp"
        },
        {
          id: 12,
          title: "柿民客栈",
          descriptions: "衢州市峡川镇东坪村",
          tel: "666486",
          bedCount: 18,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/smkz.bmp"
        },
        {
          id: 14,
          title: "叙月楼",
          descriptions: "衢州市峡川镇东坪村",
          tel: "13819010468",
          bedCount: 10,
          logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots/xyl.bmp"
        }
      ]   
    },
    filterId:"0", 
    audioPlayer:{
      playing:false,
      current:""
    }
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
    wx.navigateTo({
      url: '../map/nav',
    })
  },
  navToSpotSubItem:function(event){
    var id=event.currentTarget.dataset.id;
    console.log(id);
    var url="";
    switch (this.data.filterId)
    {
      case "0":
        url = "spot_play";
        break;
      case "1":
        url = "spot_eat";
        break;
      case "2":
        url = "spot_live";
      break;
    }
    wx.navigateTo({
      url: url,
    });
  },
  navToPanorama: function (event) {
    wx.navigateTo({
      url: 'webview?title=全景图&src=www.luckyday.top/resources/panoramas/index.html',
    });
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
  },
  playAudio:function(event)
  {   
    console.log(event);
    var page=this;

    var audioSrc = event.currentTarget.dataset.audio;
    if(audioSrc!="")
    {
      var same = this.data.audioPlayer.current == audioSrc;

      if (this.data.audioPlayer.playing) {
        if(same)
          innerAudioContext.pause();
        else 
        {
          if (!same) {
            innerAudioContext.src = audioSrc;
          }
          innerAudioContext.play();

        }
      }
      else {
        if (!same)
        {
          innerAudioContext.src = audioSrc;
        }
        innerAudioContext.play();
       
      }
     
    }    
  },

  updateAudioState: function (playing, audioSrc){ 
    this.setData({
      audioPlayer: {
        playing: playing,
        current: audioSrc
      }
    });
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var page=this;
    if(innerAudioContext==null)
      innerAudioContext = wx.createInnerAudioContext();

    innerAudioContext.onPlay(() => {
      console.log('开始播放');
      page.updateAudioState(true, innerAudioContext.src);
    });
    innerAudioContext.onStop(() => {
      console.log('onStop');
      page.updateAudioState(false,"");
    })
    innerAudioContext.onPause(() => {
      console.log('onPause');
      page.updateAudioState(false, "");
    });
    innerAudioContext.onEnded(() => {
      console.log('onEnded');
      page.updateAudioState(false, "");
    });      
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
    if(innerAudioContext!=null)
      innerAudioContext.stop();  
  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {
    if (innerAudioContext != null)
      innerAudioContext.stop(); 
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