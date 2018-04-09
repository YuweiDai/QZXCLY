//app.js
App({

  onLaunch: function () {
    var that=this;
    // 展示本地存储能力
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs);

    if(wx.getStorageSync('loginSessionKey')) return;

    // 登录
    wx.login({
      success: res => {
        // 发送 res.code 到后台换取 openId, sessionKey, unionId
        wx.request({
          url: that.globalData.apiUrl+'token',
          data: "grant_type=password&password=000&userName="+res.code,
          headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          method:"POST",         
          success:function(res){
            console.log(res);
          } 
        });
      }
    })
    // 获取用户信息
    wx.getSetting({
      success: res => {
        if (res.authSetting['scope.userInfo']) {
          // 已经授权，可以直接调用 getUserInfo 获取头像昵称，不会弹框
          wx.getUserInfo({
            success: res => {
              // 可以将 res 发送给后台解码出 unionId
              this.globalData.userInfo = res.userInfo

              // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
              // 所以此处加入 callback 以防止这种情况
              if (this.userInfoReadyCallback) {
                this.userInfoReadyCallback(res)
              }
            }
          })
        }
      }
    });

    //获取手机信息
    wx.getSystemInfo({
      success: res => {
        console.log(res);
        // 计算主体部分高度,单位为px
        this.globalData.systemInfo=res;
        this.globalData.rpx = 750 / res.windowWidth;
      }
    });   
  },
  onHide: function () {
    // Do something when hide.
  },

  globalData: {
    apiUrl: "http://qzghj.free.ngrok.cc/",  //1ded3a1f69ed8d58  http://qzghj.free.ngrok.cc/ https://www.luckyday.top/api/
    apiUrl1: "http://qzghj.free.ngrok.cc/",    
    resourceUrl:"https://www.luckyday.top/resources/",
    picturesUrl:"http://qzch.qz.gov.cn/qzxcly/resources/",
    rpx:2,
    userInfo: null,
    systemInfo:null,
    locationDetect:false
  }
})