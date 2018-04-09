//app.js
App({

  wechatLogin:function(){
    var that = this;

    // 登录
    wx.login({
      success: res => {
        //获取用户信息
        wx.getUserInfo({
          withCredentials: true,
          lang: "zh_CN",
          success: function (userInfo) {
            console.log(userInfo);
            // 发送 res.code 到后台换取 openId, sessionKey, unionId
            wx.request({
              url: that.globalData.apiUrl + 'token',
              data: "grant_type=password&password=" + userInfo.encryptedData + ";" + userInfo.iv + "&userName=" + res.code,
              headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
              method: "POST",
              success: function (res) {
                var authroziationData = { token: res.data.access_token, userName: res.data.userName, refreshToken: res.data.refresh_token };
                wx.setStorageSync('authroziationData', authroziationData);
              },
              fail: function (res) {
                console.log(res);
              }
            });
          }
        });
      }
    })
  },

  //刷新token
  refreshToken:function(){
     
    var authroziationData = wx.getStorageSync('authroziationData');

    if (authroziationData) {
      var data = "grant_type=refresh_token&refresh_token=" + authroziationData.refreshToken + "&client_id=";

      wx.request({
        url: that.globalData.apiUrl + 'token',
        data: data,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        method: "POST",
        success: function (res) {
          var authroziationData = { token: res.data.access_token, userName: res.data.userName, refreshToken: res.data.refresh_token };
          wx.setStorageSync('authroziationData', authroziationData);
        },
        fail: function (res) {
          console.log(res);
        }
      });
    }

    return deferred.promise;
  },

  request: function (url, method,data, doSuccess, doFail, doComplete){    
    var authroziationData = wx.getStorageSync('authroziationData');
    var header = { };
    if(authroziationData)
      header = { Authorization: 'Bearer ' + authroziationData.token };
    wx.request({
      url: url,
      data: data,
      header: header,
      method: method,
      success: function (res) {
        if (typeof doSuccess == "function") {
          doSuccess(res);
        }
      },
      fail: function () {
        if (typeof doFail == "function") {
          doFail();
        }
      },
      complete: function () {
        if (typeof doComplete == "function") {
          doComplete();
        }
      }
    });
  },

  onLaunch: function () {
    if (!wx.getStorageSync('authroziationData')) this.wechatLogin();

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