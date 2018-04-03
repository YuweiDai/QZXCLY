// pages/index/spot_eat.js
var page = null;
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    eat:null,
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
      url: 'webview?src=https://www.luckyday.top/' + page.data.eat.panorama + 'vtour', 
    });
  },
  // 导航
  navTo: function (event) {
    var url = '../map/nav';
    var lon = page.data.eat.longitude;
    var lat = page.data.eat.latitude;
    wx.navigateTo({
      url: url + "?lon=" + lon + "&lat=" + lat,
    });
  },
  // 点击图片预览
  previewImage: function (event) {
    var url = event.currentTarget.dataset.url;
    wx.previewImage({
      current: url, // 当前显示图片的http链接
      urls: page.data.urls // 需要预览的图片http链接列表
    })
  },  
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    page = this;

    var id = options.id;

    if(id==""|| id==null) wx.navigateBack({ });

    //获取数据
    wx.showLoading({
      title: '数据加载中...',
    });
    //获取景区点
    wx.request({
      url: app.globalData.apiUrl + 'Villages/Eat/' + id,
      success: function (response) {
        var eat = response.data;
        var size = 750 / app.globalData.rpx;
        if (eat.logo == "" || eat.logo == null)
          eat.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + eat.name + "&&bg=836&fg=fff";
          else
          eat.logo = eat.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);

        eat.descriptions = eat.description.split(';');

        console.log(eat);

        var urls = [];
        for (var index in eat.eatPictures) {
          var url = eat.eatPictures[index].img.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
          eat.eatPictures[index].img = url;
          urls.push(url);
        }
        eat.logo = eat.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);

        page.setData({
          urls:urls,
          eat: eat
        });

        wx.setNavigationBarTitle({
          title: eat.name
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