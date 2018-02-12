// pages/index/strategies.js
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    strategiesHeight:300,
    strategies: [
      { id: 0, title: "摘柿子，赏枫叶，走古道，东坪等你来！", img: "/resources/images/index/dp.jpg" },
      { id: 1, title: "桃源七里•图说2017", img: "/resources/images/index/ql.jpg" },
      { id: 2, title: "画里开化‖浙西乡村旅游的一个“引爆点”——七彩长虹！", img: "/resources/images/index/ch.jpg" }
    ], 
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
  
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
    // 计算高度
    this.setData({
      strategiesHeight: app.globalData.systemInfo.windowHeight - 97 / app.globalData.systemInfo.pixelRatio+50
    });    
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