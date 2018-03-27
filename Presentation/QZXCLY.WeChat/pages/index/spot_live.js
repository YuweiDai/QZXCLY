// pages/index/spot_live.js
var page = null;
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    //id: 1,
    //title: "庙源溪墅",
    //address: "柯城区九华乡茶铺村",
    //description: "乡贤王耀水建于2015年的蘑菇房“庙源溪墅”在衢城远近闻名。该民宿由中外合资木屋公司设计，采取全木质结构，木材取自加拿大，被称为“世界上最会呼吸的房子”。二楼大床房的楼梯口，悬挂有北大著名美术教授手绘的绘画作品。三楼放置有长达7米的榻榻米通铺。民宿内一应事物，全按五星级酒店配备。",
    //phone: "18906708718",
    //facilities:"热水器 空调 全屋WIFI",
    //panorama:"",
    //logo: "http://5b0988e595225.cdn.sohucs.com/images/20171002/f748eec5944141428bbb589b3bf4a3e2.jpeg",
    //photos: [
    //  { title: "门口", img: "http://img8.blog.eastmoney.com/zl/zltkg/201505/20150530075406190.jpg" },
    //  { title: "大厅", img: "http://s9.rr.itc.cn/r/wapChange/20169_12_15/a29q5o2502122756352.jpg" },
    //  { title: "包厢", img: "http://img2.niwota.com/album/images/2015-07-15/1436925113922-club.jpg" }
    //],
    //urls: '',  
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
      url: 'webview?title=全景图&src=http://www.ipanocloud.com/tour/share/151029AEQ1O',
    });
  },  
  // 导航
  navTo: function (event) {
    wx.navigateTo({
      url: '../map/nav',
    })
  },
  // 点击图片预览
  previewImage: function (event) {
    var url = event.currentTarget.dataset.url;
    wx.previewImage({
      current: url, // 当前显示图片的http链接
      urls: p.data.urls // 需要预览的图片http链接列表
    })
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

              if (live.logo == "" || live.logo == null)
                  live.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + live.name + "&&bg=836&fg=fff";

            
              console.log(live);

              // var urls = [];
              // for (var index in p.data.photos) {
              //   urls.push(p.data.photos[index].img);
              // }

              page.setData({
                  live: live
              });

              wx.setNavigationBarTitle({
                  title: spot.name
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