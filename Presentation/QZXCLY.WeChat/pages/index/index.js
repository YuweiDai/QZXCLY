//index.js
//获取应用实例
const app = getApp()

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
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b2.png',
        url: '',
        name: '告别午高峰'
      },
      {
        id: 2,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/b3.png',
        url: '',
        name: '金牌好店'
      }
    ],
    icons: [
      {
        id: 1,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/icon_12.jpg',
        name: '景区',
        url: 'spots'
      },
      {
        id: 2,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/icon_5.jpg',
        name: '攻略',
        url: 'strategies'
      },
      {
        id: 3,
        img: 'http://qzch.qz.gov.cn/qzxcly/resources/images/index/icon_9.jpg',
        name: '活动',
        url: 'activities'
      }
    ],
    suggestions:[
      { name: "康养衢江·隐柿东坪", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/dp_1.jpg" },
      { name: "寻梦乡愁·桃源七里", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ql.png" },
      { name: "七彩长虹", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ch.png" },
      { name: "廿八都古镇印象", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/nbd.png" }                        
    ],
    strategies: [
      { id: 0, title: "摘柿子，赏枫叶，走古道，东坪等你来！", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/dp.jpg", src: "https://m.sohu.com/a/199887818_99961751/?pvid=000115_3w_a" },
      { id: 1, title: "桃源七里•图说2017", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ql.jpg", src: "https://m.sohu.com/a/213140963_695982/?pvid=000115_3w_a" },
      { id: 2, title: "画里开化‖浙西乡村旅游的一个“引爆点”——七彩长虹！", img: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/ch.jpg", src: "https://m.sohu.com/a/214971209_173747/?pvid=000115_3w_a" }
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
    wx.showModal({
      title: '提示',
      content: '检测到您正位于东坪景区，是否前往景区地图？',
      success: function (res) {
        if (res.confirm) {
          app.globalData.locationDetect=true;
          wx.navigateTo({
            url: 'spot',
          })
        }
      }
    })

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
    
    wx.request({
      url: 'http://www.qz-map.com/api/feature/query', //仅为示例，并非真实的接口地址
      data: {
        auto_global: true,
        page: 1,
        size: 50,
        tcode: "160501, 160502,160503",
        zoom:15,
        bbox:"118.81864611083985,28.95371039932251,118.89074388916016,28.99314960067749"
      },
      header: {
        'content-type': 'application/json' // 默认值
      },
      success: function (res) {
        wx.showToast({
          title: '数据加载成功',
          icon: 'success',
          duration: 2000
        });
         
      },
      fail: function (res) {
        wx.showToast({
          title: '数据加载失败',
          icon: 'none',
          duration: 2000
        });        
      },
    })




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
