//index.js
//获取应用实例
const app = getApp()

Page({
  data: {
    banners: [
      {
        id: 3,
        img: '/resources/images/index/b1.png',
        url: '',
        name: '百亿巨惠任你抢'
      },
      {
        id: 1,
        img: '/resources/images/index/b2.png',
        url: '',
        name: '告别午高峰'
      },
      {
        id: 2,
        img: '/resources/images/index/b3.png',
        url: '',
        name: '金牌好店'
      }
    ],
    icons: [
      {
        id: 1,
        img: '/resources/images/index/icon_12.jpg',
        name: '景点',
        url: 'spots'
      },
      {
        id: 2,
        img: '/resources/images/index/icon_5.jpg',
        name: '攻略',
        url: 'strategies'
      },
      {
        id: 3,
        img: '/resources/images/index/icon_9.jpg',
        name: '活动',
        url: 'activities'
      }
    ],
    suggestions:[
      { name: "七里", img: "/resources/images/index/ql.jpg" },
      { name: "九华", img: "/resources/images/index/jh.gif" },
      { name: "石梁", img: "/resources/images/index/sl.gif" },
      { name: "杨林", img: "/resources/images/index/yl.jpg" }                        
    ],
    strategies: [
      { name: "玩转七里", img: "/resources/images/index/s1.png" },
      { name: "九华心得", img: "/resources/images/index/s2.png" },
      { name: "石梁体验", img: "/resources/images/index/s3.png" }
    ], 
    motto: 'Hello World',
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
      content: '检测到您正位于七里景区，是否前往景区地图？',
      success: function (res) {
        if (res.confirm) {
          app.globalData.locationDetect=true;
          wx.switchTab({
            url: '../map/index',
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
