//app.js
App({

  post: function (url, data) {
    var promise = new Promise((resolve, reject) => {
      //init
      var that = this;
      var postData = data;
      /*
      //自动添加签名字段到postData，makeSign(obj)是一个自定义的生成签名字符串的函数
      postData.signature = that.makeSign(postData);
      */
      //网络请求
      wx.request({
        url: url,
        data: postData,
        method: 'POST',
        header: { 'content-type': 'application/x-www-form-urlencoded' },
        success: function (res) {//服务器返回数据
          if (res.data.status == 1) {
            resolve(res.data.data);
          } else {//返回错误提示信息
            reject(res.data.info);
          }
        },
        error: function (e) {
          reject('网络出错');
        }
      })
    });
    return promise;
  },

  onLaunch: function () {
    // 展示本地存储能力
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs)

    // 登录
    wx.login({
      success: res => {
        // 发送 res.code 到后台换取 openId, sessionKey, unionId
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
        // 可使用窗口宽度、高度
        console.log('height=' + res.windowHeight);
        console.log('width=' + res.windowWidth);
        // 计算主体部分高度,单位为px
        this.globalData.systemInfo=res;
        
        //this.globalData.deviceSize = { width: res.windowWidth, height: res.windowHeight, pixelRatio: res.pixelRatio };

        /* if (res.authSetting['scope.userInfo']) {
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
        } */
      }
    });   
  },
  globalData: {
    apiUrl: "http://qzghj.free.ngrok.cc/",  //1ded3a1f69ed8d58 https://www.luckyday.top/api/
    resourceUrl:"https://www.luckyday.top/resources/",
    picturesUrl:"http://qzch.qz.gov.cn/qzxcly/resources/",
    userInfo: null,
    systemInfo:null,
    locationDetect:false
  }
})