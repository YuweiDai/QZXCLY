// pages/index/spot_live.js
var page = null;
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    live:null,
    urls: '',  
  },
  // 拨打电话
  makePhoneCall: function (event) {
    var phone = event.currentTarget.dataset.phone;
    wx.showModal({
      title: '提示',
      content: '是否拨打电话 ' + phone + ' ?',
      success: function (res) {
        if (res.confirm) {
          wx.makePhoneCall({
            phoneNumber: phone
          });
        }
      }
    })
  },

  // 切换至全景
  navToPanorama: function (event) {
    wx.navigateTo({
      url: 'webview?src=https://www.luckyday.top/' + page.data.live.panorama + 'vtour',      
    });
  },
  // 导航
  navTo: function (event) {
    var url = '../map/nav';
    var lon = page.data.live.longitude;
    var lat = page.data.live.latitude;
    wx.navigateTo({
      url: url + "?lon=" + lon + "&lat=" + lat,
    });
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
      page = this;

      var id = options.id;

      if (id == "" || id == null) wx.navigateBack({});

      //获取数据
      wx.showLoading({
          title: '数据加载中...',
      });
      //获取景区点
      wx.request({
          url: app.globalData.apiUrl + 'Villages/live/' + id,
          success: function (response) {
              var live = response.data;
              var size = 750 / app.globalData.rpx;
              if (live.logo == "" || live.logo == null)
                  live.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + live.name + "&&bg=836&fg=fff";

            
              console.log(live);

              var urls = [];
              for (var index in live.livePictures) {
                var url = live.livePictures[index].img.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
                live.livePictures[index].img = url;
                urls.push(url);                
              }
              live.logo = live.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);

              page.setData({
                  live: live
              });

              wx.setNavigationBarTitle({
                  title: live.name
              });
          },
          fail: function (response) {
              wx.showToast({
                  title: '获取数据失败...',
              })
          },
          complete: function () {
              wx.hideLoading();
          }
      });
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
  
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