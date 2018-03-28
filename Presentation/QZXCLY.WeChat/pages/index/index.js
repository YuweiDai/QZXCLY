//index.js
//获取应用实例
var app = getApp()
var page=null;
Page({
  data: {
    banners: [
      {
        id: 3,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b1.jpg',
        url: '',
        name: '康养衢江·隐柿东坪'
      },
      {
        id: 1,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b2.jpg',
        url: '',
        name: '告别午高峰'
      },
      {
        id: 2,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b3.jpg',
        url: '',
        name: '金牌好店'
      }
    ],
    icons: [
      {
        id: 1,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/spots.png',
        name: '景区',
        url: 'spots'
      },
      {
        id: 2,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/activity.jpg',
        name: '攻略',
        url: 'strategies'
      },
      {
        id: 3,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/map.png',
        name: '地图',
        url: '../map/index'
      }
    ],
    suggestions:[
      { name: "康养衢江·隐柿东坪", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/dp_1.jpg",id:1 },
      { name: "寻梦乡愁·桃源七里", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ql.png",id:3 },
      { name: "诗画乡村·七彩长虹", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ch.png",id:2 },
      { name: "九华乡", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/nbd.png",id:4 }        
    ],
    strategies: [
    ], 
    userInfo: {},
    hasUserInfo: false,
    canIUse: wx.canIUse('button.open-type.getUserInfo')
  },
 
  //事件处理函数
  toSpots: function (event){
    console.log(event);
    var url = event.currentTarget.dataset.url;
    if (url == undefined || url == null || url == "") console.log("url is undefined or null");
    else
    {
      wx.navigateTo({
        url: url
      });
    }
  },
  navToSpot(event) {
    wx.navigateTo({
      url: 'spot?id=' + event.currentTarget.dataset.id,
    });

  },
  bindViewTap: function () {
    wx.navigateTo({
      url: '../logs/logs'
    })
  },
  onLoad: function () {
    page=this;
    // wx.showModal({
    //   title: '提示',
    //   content: '检测到您正位于东坪景区，是否前往景区地图？',
    //   success: function (res) {
    //     if (res.confirm) {
    //       app.globalData.locationDetect=true;
    //       wx.navigateTo({
    //         url: 'spot',
    //       })
    //     }
    //   }
    // })

  //   { id: 0, title: "摘柿子，赏枫叶，走古道，东坪等你来！", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/dp.jpg", src: "https://www.luckyday.top/resources/strategies/dp001.html" },
  //     { id: 1, title: "桃源七里•图说2017", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ql.jpg", src: "https://m.sohu.com/a/213140963_695982/?pvid=000115_3w_a" },
  // { id: 2, title: "画里开化‖浙西乡村旅游的一个“引爆点”——七彩长虹！", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ch.jpg", src: "https://m.sohu.com/a/214971209_173747/?pvid=000115_3w_a" }
    wx.request({
      url:app.globalData.apiUrl+ 'Strategy/Random',
      success: function (response) {
        var strategies = response.data;

        strategies.forEach(function(item){
          item.src = app.globalData.resourceUrl + "/strategies/" + item.src;
        });


        page.setData({
          strategies: strategies
        });
      },
      fail: function (response) {
        console.log(response);
        wx.showToast({
          title: response//'获取数据失败...',
        })
      },
      complete: function () {
      }      
    });

    if (app.globalData.userInfo) {
      this.setData({
        userInfo: app.globalData.userInfo,
        hasUserInfo: true
      })
    } else if (this.data.canIUse) {
      // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
      // 所以此处加入 callback 以防止这种情况
      app.userInfoReadyCallback = res => {
        this.setData({
          userInfo: res.userInfo,
          hasUserInfo: true
        })
      }
    } else {
      // 在没有 open-type=getUserInfo 版本的兼容处理
      wx.getUserInfo({
        success: res => {
          app.globalData.userInfo = res.userInfo
          this.setData({
            userInfo: res.userInfo,
            hasUserInfo: true
          })
        }
      })
    }
  },
  onShow:function(event)
  {
    
  },
  getUserInfo: function (e) {
    console.log(e)
    app.globalData.userInfo = e.detail.userInfo
    this.setData({
      userInfo: e.detail.userInfo,
      hasUserInfo: true
    })
  }
})
