// pages/index/spot_service.js
var page = null;
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    service: null,
    urls: '',  
  },
    // 导航
  navTo: function (event) {
      var url = '../map/nav';
      var lon = page.data.service.longitude;
      var lat = page.data.service.latitude;
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
          url: app.globalData.apiUrl + 'Villages/service/' + id,
          success: function (response) {
              var service = response.data;
              var size = 750 / app.globalData.rpx;
              if (service.logo == "" || service.logo == null)
                  service.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + service.name + "&&bg=836&fg=fff";


              console.log(service);

              var urls = [];
              for (var index in service.livePictures) {
                  var url = service.livePictures[index].img.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
                  service.livePictures[index].img = url;
                  urls.push(url);
              }
              service.logo = service.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);

              page.setData({
                  service: service
              });

              wx.setNavigationBarTitle({
                  title: service.name
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