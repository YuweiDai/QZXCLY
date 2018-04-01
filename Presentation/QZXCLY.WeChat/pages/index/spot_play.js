// pages/index/spot_subitem.js
var page=null;
var innerAudioContext=null;
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    play:null,
    urls:'',
    playing:false
  },

  // 语音讲解
  playAudio: function (event) {
    var audioSrc = event.currentTarget.dataset.audio;
    if (audioSrc != "") {
      if (this.data.playing) {
        innerAudioContext.stop();
      }
      else {
        innerAudioContext.play();
      }
    }
  },

  // 切换至全景
  navToPanorama: function (event) {
    wx.navigateTo({
      url: 'webview?src=https://www.luckyday.top/threejs^village%' + page.data.play.panorama + '$pId%' + page.data.play.panoramaId,
    });
  },
  // 导航
  navTo: function (event) {
    var url = '../map/nav';
    var lon = page.data.play.longitude;
    var lat = page.data.play.latitude;
    wx.navigateTo({
      url: url + "?lon=" + lon + "&lat=" + lat,
    });
  },

  // 点击图片预览
  previewImage:function(event)
  {
    var url=event.currentTarget.dataset.url;
    wx.previewImage({
      current: url, // 当前显示图片的http链接
      urls:page.data.urls // 需要预览的图片http链接列表
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
      page=this;
    
      var id = options.id;

      if (id == "" || id == null) wx.navigateBack({});

      //获取数据
      wx.showLoading({
          title: '数据加载中...',
      });
      //获取景区点
      wx.request({
          url: app.globalData.apiUrl + 'Villages/Play/' + id,
          success: function (response) {
              var play = response.data;
              var size =  750 / app.globalData.rpx ;
              if (play.logo == "" || play.logo == null)
                play.logo = "http://www.atool.org/placeholder.png?size=" + size + "x" + size + "&text=" + play.name + "&&bg=836&fg=fff";

              console.log(play);

              var urls = [];
              for (var index in play.playPictures) {
                var url = play.playPictures[index].img.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);
                play.playPictures[index].img=url;
                urls.push(url);
              }

              console.log(play.playPictures);

              play.logo = play.logo.replace(app.globalData.apiUrl1, app.globalData.picturesUrl);

              page.setData({
                  play: play,
                  urls:urls
              });

              wx.setNavigationBarTitle({
                  title: play.name
              });


              innerAudioContext.src = page.data.play.audioUrl;
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

    if (innerAudioContext == null)
    {
      innerAudioContext = wx.createInnerAudioContext();
    }

    innerAudioContext.onPlay(() => {
      console.log('开始播放');
      page.setData({playing: true});
    });
    innerAudioContext.onStop(() => {
      console.log('onStop');
      page.setData({ playing: false });
    })
    innerAudioContext.onEnded(() => {
      console.log('onEnded');
      page.setData({ playing: false });
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
    if (innerAudioContext != null)
      innerAudioContext.stop();
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