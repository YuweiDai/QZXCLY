// pages/index/spot_eat.js
var page = null;
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    // id: 1,
    // title: "望乡楼",
    // address: "衢州市峡川镇东坪村",
    // suggestion: "羊骨粽子 酸梅汤 手擀面 ‍羊蝎子 ‍羊蝎子 ‍羊蝎子 火锅汤 驴肉饺子 驴肉火锅 滋补汤底",
    // descriptions:["周一至周日 10：00-20：00","内设WIFI"],
    // phone: "13812345678",
    // price: "51",
    // logo: "http://qzch.qz.gov.cn/qzxcly/resources/images/index/l2.png",    
    // photos: [
    //   { title: "门口", img: "http://img8.blog.eastmoney.com/zl/zltkg/201505/20150530075406190.jpg" },
    //   { title: "大厅", img: "http://s9.rr.itc.cn/r/wapChange/20169_12_15/a29q5o2502122756352.jpg" },
    //   { title: "包厢", img: "http://img2.niwota.com/album/images/2015-07-15/1436925113922-club.jpg" }
    // ],
    // urls: '',
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
        
        if (eat.logo == "" || eat.logo == null)
          eat.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + eat.name + "&&bg=836&fg=fff";

        eat.descriptions = eat.description.split(';');

        console.log(eat);

        // var urls = [];
        // for (var index in p.data.photos) {
        //   urls.push(p.data.photos[index].img);
        // }

        page.setData({
          eat: eat
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